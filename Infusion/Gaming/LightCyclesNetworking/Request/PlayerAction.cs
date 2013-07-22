using Infusion.Gaming.LightCycles.Definitions;
using Infusion.Networking;

namespace Infusion.Gaming.LightCyclesNetworking.Request
{
    /// <summary>
    /// Player action request
    /// </summary>
    public class PlayerAction : RequestBase<PlayerAction>
    {
        /// <summary>
        /// Gets or sets player key
        /// </summary>
        public string PlayerKey { get; set; }

        /// <summary>
        /// Gets or sets game key
        /// </summary>
        public string GameKey { get; set; }

        /// <summary>
        /// Gets or sets player action for given turn
        /// </summary>
        public RelativeDirection MoveDirection { get; set; }
    }
}
