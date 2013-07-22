using Infusion.Networking;

namespace Infusion.Gaming.LightCyclesNetworking.Response
{
    /// <summary>
    /// Message with lobby state data
    /// </summary>
    public class GameData : ResponseBase<GameData>
    {
        /// <summary>
        /// Gets or sets game data
        /// </summary>
        public string Data { get; set; }
    }
}
