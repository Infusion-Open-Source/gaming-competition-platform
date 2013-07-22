using System;

namespace Infusion.Networking.ControllingServer.Exceptions
{
    public class PlayerKickedException : Exception
    {
        public PlayerKickedException()
        {
        }

        public PlayerKickedException(string message)
            : base(message)
        {
        }
    }
}
