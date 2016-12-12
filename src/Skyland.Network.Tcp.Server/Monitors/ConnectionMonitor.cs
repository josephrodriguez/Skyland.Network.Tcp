#region using

using System;
using System.Net;
using System.Net.Sockets;
using Skyland.Network.Core.Threading;
using Skyland.Network.Tcp.Server.Enum;
using Skyland.Network.Tcp.Server.Handlers;

#endregion

namespace Skyland.Network.Tcp.Server.Monitors
{
    internal class ConnectionMonitor
    {
        private readonly Worker _worker;
        private readonly Socket _socket;

        private ConnectionState _currentStatus;

        public event StatusChangedEventHandler OnStatusChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param><exception cref="T:System.ArgumentNullException"><paramref name="socket"/> is null. </exception>
        public ConnectionMonitor(Socket socket)
        {
            if(socket == null)
                throw new ArgumentNullException(nameof(socket));

            _socket = socket;
            _worker = new Worker(Execute);
            _currentStatus = ConnectionState.Unknow;
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
            if (_currentStatus == ConnectionState.Unknow)
                _currentStatus = _socket.Connected ? ConnectionState.Open : ConnectionState.Closed;

            var status =
                _socket.Available > 0 ||
                (_socket.Connected && !_socket.Poll(1000, SelectMode.SelectRead))
                    ? ConnectionState.Open
                    : ConnectionState.Closed;

            if(_currentStatus == status)
                return;

            _currentStatus = status;

            OnStatusChanged?.Invoke(_socket.RemoteEndPoint, _currentStatus);
        }
    }
}
