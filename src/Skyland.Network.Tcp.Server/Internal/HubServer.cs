#region using

using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using Skyland.Network.Core.Logging;
using Skyland.Network.Core.Pipeline;
using Skyland.Network.Core.Pipeline.Handlers;
using Skyland.Network.Core.Threading;
using Skyland.Network.Tcp.Server.Configuration.Interfaces;
using Skyland.Network.Tcp.Server.Dispatchers;
using Skyland.Network.Tcp.Server.Enum;
using Skyland.Network.Tcp.Server.Internal.Pipeline;
using Skyland.Network.Tcp.Server.Monitors;

#endregion

namespace Skyland.Network.Tcp.Server.Internal
{
    internal class HubServer : IHubServer
    {
        private static readonly object SyncObject = new object();

        private readonly IPEndPoint _endpoint;

        private int _connectionsCount;
        private Worker _worker;

        private readonly ConcurrentDictionary<EndPoint, ConnectionMonitor> _monitors;
        private readonly ConcurrentDictionary<EndPoint, MessageDispatcher> _dispatchers;


        private readonly ISslConfiguration _sslConfiguration;
        private readonly ICompressConfiguration _compressConfiguration;
        private readonly INetworkConfiguration _configuration;
        private readonly ILog _log;

        private TcpListener _listener;

        private IPipeline<TcpClient> _clientPipeline; 

        /// <summary>
        /// Return the connected clients count
        /// </summary>
        public int Count
        {
            get { return _connectionsCount; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param> <param name="ssl"></param> <param name="compression"></param> <param name="events"></param> <param name="options"></param>
        protected internal HubServer(IPEndPoint endpoint, ISslConfiguration ssl, ICompressConfiguration compression, INetworkConfiguration options)
        {
            if(ssl == null)
                throw new ArgumentNullException(nameof(ssl));

            _endpoint = endpoint;
            _connectionsCount = 0;

            _monitors = new ConcurrentDictionary<EndPoint, ConnectionMonitor>();
            _dispatchers = new ConcurrentDictionary<EndPoint, MessageDispatcher>();

            _sslConfiguration = ssl;
            _compressConfiguration = compression;
            _configuration = options;
            _log = LoggerFactory.Current.GetLogger<HubServer>();
        }

        public void Start()
        {
            _log.Info("Starting Hub server: {0}", _endpoint);

            CreatePipeline();

            _listener = new TcpListener(_endpoint);
            _listener.Start();

            _worker = new Worker(ProcessClients);
            _worker.StartForever(TimeSpan.Zero);

            _log.Info("Started Hub server: {0}", _endpoint);
        }

        public void Stop()
        {
            _log.Info("Stopping Hub server");

            _listener.Stop();
            _worker.Cancel();

            _log.Info("Hub server stopped");
        }

        private void CreatePipeline()
        {
            _clientPipeline = new Pipeline<TcpClient>();
            _clientPipeline.OnCompleted += ClientPipelineOnOnCompleted;
            _clientPipeline.OnError += ClientPipelineOnOnError;

            _clientPipeline
                .Register(new FilterComponent())
                .Register(new NetworkPipelineComponent(_configuration));

            if (_sslConfiguration.Enabled)
                _clientPipeline.Register(new SslPipelineComponent(_sslConfiguration));
        }

        private void ClientPipelineOnOnError(object sender, ErrorArgs<TcpClient> args)
        {
        }

        private void ClientPipelineOnOnCompleted(TcpClient outputElement)
        {
        }

        private void ProcessClients()
        {
            try
            {
                var client = _listener.AcceptTcpClient();

                var endpoint = client.Client.RemoteEndPoint;

                _clientPipeline.Execute(client);

                //Log information of socket
                _log.Debug("Connected socket: {0}.", endpoint);

                //Raise OnClientConnected event
                 //_events.RaiseClientConnected(endpoint);

                if (_connectionsCount >= _configuration.AllowedConnections)
                {
                    client.Close();

                    _log.Debug("Rejected socket ({0})", endpoint);

                    //_events.RaiseClientRejected(endpoint);
                }
                else
                {
                    AcceptConnectedClient(client);
                    //_events.RaiseClientAccepted(endpoint);
                }
            }
            catch (Exception exception)
            {
                _log.Error(exception);
            }
        }

        private void AcceptConnectedClient(TcpClient client)
        {
            IncrementConnections();

            var socket = client.Client;

            if (ConfigureSocket(socket))
                return;

            DecrementConnections();
            Cleanup(socket.RemoteEndPoint);
        }

        private bool ConfigureSocket(Socket socket)
        {
            return 
                CreateMonitorForSocket(socket) && 
                CreateDispatcherForSocket(socket);
        }

        private void Cleanup(EndPoint endPoint)
        {
            ConnectionMonitor monitor;
            if (_monitors.TryRemove(endPoint, out monitor))
                monitor.Cancel();

            MessageDispatcher dispatcher;
            if (_dispatchers.TryRemove(endPoint, out dispatcher))
                dispatcher.Cancel();
        }

        private void IncrementConnections()
        {
            lock (SyncObject) {
                _connectionsCount++;
            }
        }

        private void DecrementConnections()
        {
            lock (SyncObject) {
                _connectionsCount--;
            }
        }

        private bool CreateMonitorForSocket(Socket socket)
        {
            var monitor = new ConnectionMonitor(socket);
            monitor.OnStatusChanged += MonitorOnOnStatusChangedEvent;

            var result = _monitors.TryAdd(socket.RemoteEndPoint, monitor);
            if(!result)
                return false;

            monitor.Start();

            return true;
        }

        private bool CreateDispatcherForSocket(Socket socket)
        {
            var dispatcher = new MessageDispatcher(socket);
            dispatcher.OnMessageReceived += DispatcherOnOnMessageReceived;

            var result = _dispatchers.TryAdd(socket.RemoteEndPoint, dispatcher);
            if (!result)
                return false;

            dispatcher.Start();

            return true;
        }

        private void MonitorOnOnStatusChangedEvent(EndPoint endpoint, ConnectionState status)
        {
            if (status != ConnectionState.Closed) return;

            //Fire event handlers for disconnected client
            //_events.RaiseClientDisconnected(endpoint);

            DecrementConnections();
            Cleanup(endpoint);
        }

        private void DispatcherOnOnMessageReceived(Message message)
        {
            if (_compressConfiguration.Enabled)
                message.Data = _compressConfiguration.Compressor.Decompress(message.Data);

           // _events.RaiseMessageReceived(message);
        }
    }
}
