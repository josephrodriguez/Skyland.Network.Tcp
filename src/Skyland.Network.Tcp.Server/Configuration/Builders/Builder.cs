#region using

#endregion

namespace Skyland.Network.Tcp.Server.Configuration.Builders
{
    public abstract class Builder<TComponent>
    {
        internal abstract TComponent Build();
    }
}
