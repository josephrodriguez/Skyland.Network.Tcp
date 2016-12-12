#region using

using System;
using System.Net;
using System.Net.Sockets;
using Skyland.Network.Core.Threading;
using Skyland.Network.Tcp.Server.Handlers;

#endregion

namespace Skyland.Network.Tcp.Server.Dispatchers
{
    internal class MessageDispatcher
    {
        private readonly Worker _worker;
        private readonly Socket _socket;

        public event MessageReceivedEventHandler OnMessageReceived;

        public MessageDispatcher(Socket socket)
        {
            if(socket == null)
                throw new ArgumentNullException(nameof(socket));

            _socket = socket;
            _worker = new Worker(Execute);
        }

        public EndPoint Endpoint
        {
            get { return _socket.RemoteEndPoint; }
        }

        public void Start()
        {
            _worker.StartForever(TimeSpan.FromSeconds(1));
        }

        public void Cancel()
        {
            _worker.Cancel();
        }

        private void Execute()
        {
            try
            {
                var stream = new NetworkStream(_socket, true);

                if (!stream.CanRead || !stream.DataAvailable)
                    return;

                if(OnMessageReceived == null)
                    return;

                while (stream.DataAvailable)
                {
                    var buffer = new byte[_socket.Available];

                    var count = stream.Read(buffer, 0, buffer.Length);
                    if (count <= 0)
                        return;

                    var message = new Message(_socket.RemoteEndPoint, buffer);
                    OnMessageReceived.Invoke(message);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
