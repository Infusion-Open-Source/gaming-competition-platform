using System;

namespace Infusion.Networking.ControllingServer.Exceptions
{
    public class InssuficientPriviledgesException : Exception
    {
        public InssuficientPriviledgesException()
        {
        }

        public InssuficientPriviledgesException(string message)
            : base(message)
        {
        }
    }
}
