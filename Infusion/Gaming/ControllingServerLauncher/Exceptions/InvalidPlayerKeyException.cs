using System;

namespace Infusion.Networking.ControllingServer.Exceptions
{
    public class InvalidPlayerKeyException : Exception
    {
        public InvalidPlayerKeyException()
        {
        }

        public InvalidPlayerKeyException(string message)
            : base(message)
        {
        }
    }
}
