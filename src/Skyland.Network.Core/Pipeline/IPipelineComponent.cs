namespace Skyland.Network.Core.Pipeline
{
    public interface IPipelineComponent<T>
    {
        void Execute(PipelineElement<T> arg);
    }
}
