using System;

namespace Infusion.Gaming.LightCyclesNetworking.Exceptions
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
