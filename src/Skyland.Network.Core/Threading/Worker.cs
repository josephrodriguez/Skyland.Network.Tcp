#region using

using System;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Skyland.Network.Core.Threading
{
    public class Worker : IDisposable
    {
        private readonly Action _action;
        private readonly CancellationTokenSource _source;

        public Worker(Action action)
        {
            _action = action;
            _source = new CancellationTokenSource();
        }

        public void Start()
        {
            Task.Factory.StartNew(() => Execute(TimeSpan.Zero, TimeSpan.Zero, -1, 1));
        }

        public void Start(TimeSpan delay)
        {
            Task.Factory.StartNew(() => Execute(delay, TimeSpan.Zero, -1, 1));
        }

        public void Start(TimeSpan delay, TimeSpan interval, int count)
        {
            Task.Factory.StartNew(() => Execute(delay, interval, -1, count));
        }

        public void Start(TimeSpan delay, TimeSpan interval, int timeout, int count)
        {
            Task.Factory.StartNew(() => Execute(delay, interval, timeout, count));
        }

        public void StartForever(TimeSpan interval)
        {
            Task.Factory.StartNew(() => Execute(TimeSpan.Zero, interval, -1, -1));
        }

        public void StartForever(TimeSpan interval, int timeout)
        {
            Task.Factory.StartNew(() => Execute(TimeSpan.Zero, interval, timeout, -1));
        }

        public void Cancel()
        {
            if(_source.IsCancellationRequested)
                return;

            _source.Cancel();
        }

        public void Dispose()
        {
        }

        private void Execute(TimeSpan delay, TimeSpan interval, int timeout, int count)
        {
            Task.Delay(delay).Wait();

            while (!_source.IsCancellationRequested && count != 0)
            {
                var backgroundTask = Task.Run(_action, _source.Token);

                try
                {
                    if (timeout > 0)
                        backgroundTask.Wait(timeout, _source.Token);
                    else
                        backgroundTask.Wait(_source.Token);

                    if (count > 0)
                        count--;

                    Task.Delay(interval).Wait();
                }
                catch (OperationCanceledException)
                {
                }
            }
        }
    }
}
