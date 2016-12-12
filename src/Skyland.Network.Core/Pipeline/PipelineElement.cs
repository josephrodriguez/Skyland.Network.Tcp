namespace Skyland.Network.Core.Pipeline
{
    public class PipelineElement<T>
    {
        public PipelineElement(T argument)
        {
            Argument = argument;
        } 

        public T Argument { get; set; }
        public bool Completed { get; set; }
        public bool Cancelled { get; set; }
    }
}
