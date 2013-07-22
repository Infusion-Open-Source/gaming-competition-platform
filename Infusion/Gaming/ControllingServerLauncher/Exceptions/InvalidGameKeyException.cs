using System;

namespace Infusion.Networking.ControllingServer.Exceptions
{
    public class InvalidGameKeyException : Exception
    {
        public InvalidGameKeyException()
        {
        }

        public InvalidGameKeyException(string message)
            : base(message)
        {
        }
    }
}
