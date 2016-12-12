#region using

using System;
using System.Collections.Concurrent;
using System.Threading;
using Skyland.Network.Core.Pipeline.Handlers;
using Skyland.Network.Core.Threading;

#endregion

namespace Skyland.Network.Core.Pipeline.Internal
{
    class Stage<T>
    {
        private readonly IPipelineComponent<T> _pipelineComponent;
        private readonly CancellationToken _token;
        private readonly BlockingCollection<PipelineElement<T>> _buffer;
        private readonly Worker _worker;

        public int StageId { get; private set; }

        public Stage(IPipelineComponent<T> pipelineComponent, CancellationToken token, int stageId, int boundCapacity)
        {
            if(pipelineComponent == null)
                throw new ArgumentNullException();

            _pipelineComponent = pipelineComponent;
            _token = token;
            _buffer = new BlockingCollection<PipelineElement<T>>();

            _worker = new Worker(ExecuteOnBackground);
            _worker.Start();

            StageId = stageId;
        }

        public event StageCompletedEventHandler<T> OnCompleted;
        public event StageErrorEventHandler<T> OnError;


        public void Execute(PipelineElement<T> element)
        {
            _buffer.Add(element, _token);
        }

        private void ExecuteOnBackground()
        {
            foreach (var argument in _buffer.GetConsumingEnumerable())
            {
                if (_token.IsCancellationRequested) break;

                try
                {
                    _pipelineComponent.Execute(argument);
                    RaiseOnCompleted(argument);
                }
                catch (Exception exception)
                {
                    var args = new ErrorArgs<T>(argument.Argument, exception);
                    RaiseOnError(_pipelineComponent, args);
                }
            }
        }

        private void RaiseOnCompleted(PipelineElement<T> arg)
        {
            if(OnCompleted == null) return;
            OnCompleted(this, arg);
        }

        private void RaiseOnError(object sender, ErrorArgs<T> args)
        {
            if (OnError == null) return;
            OnError(sender, args);
        }
    }
}
