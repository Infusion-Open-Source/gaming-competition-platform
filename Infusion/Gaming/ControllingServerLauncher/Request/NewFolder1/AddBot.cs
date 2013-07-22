namespace Infusion.Networking.ControllingServer.Request.NewFolder1
{
    /// <summary>
    /// Request adding bot to the game slot
    /// </summary>
    public class AddBot : RequestBase<AddBot>
    {
        /// <summary>
        /// Gets or sets game id
        /// </summary>
        public int GameId { get; set; }

        /// <summary>
        /// Gets or sets slot number where to add bot
        /// </summary>
        public int SlotNumber { get; set; }

        /// <summary>
        /// Gets or sets bot type
        /// </summary>
        public string BotType { get; set; }

        /// <summary>
        /// Gets or sets bot player name
        /// </summary>
        public string BotName { get; set; }
    }
}
