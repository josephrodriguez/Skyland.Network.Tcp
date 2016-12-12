#region using

using Skyland.Network.Core.Pipeline.Handlers;

#endregion

namespace Skyland.Network.Core.Pipeline
{
    public interface IPipeline<T>
    {
        event PipelineErrorEventHandler<T> OnError;

        event PipelineCompletedEventHandler<T> OnCompleted;

        int Count { get; }

        void Execute(T element);
        void Wait();
        void Cancel();

        IPipeline<T> Register(IPipelineComponent<T> component);
    }
}
