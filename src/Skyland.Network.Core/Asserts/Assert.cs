#region using

using System;

#endregion

namespace Skyland.Network.Core.Asserts
{
    public static class Assert
    {
        public static void IsNull(object obj)
        {
            if(ReferenceEquals(obj, null))
                return;
            throw new Exception();
        }

        public static void IsNotNull(object obj)
        {
            if (!ReferenceEquals(obj, null))
                return;
            throw new Exception();
        }
    }
}
