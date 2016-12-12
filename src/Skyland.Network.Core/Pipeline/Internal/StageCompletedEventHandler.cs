namespace Skyland.Network.Core.Pipeline.Internal
{
    public delegate void StageCompletedEventHandler<TElement>(object sender, PipelineElement<TElement> argument);
}
