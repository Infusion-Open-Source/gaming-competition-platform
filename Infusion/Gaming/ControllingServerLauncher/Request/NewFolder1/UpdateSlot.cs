namespace Infusion.Networking.ControllingServer.Request.NewFolder1
{
    /// <summary>
    /// Request for moving players between slot
    /// </summary>
    public class UpdateSlot : RequestBase<UpdateSlot>
    {
        /// <summary>
        /// Gets or sets game id
        /// </summary>
        public int GameId { get; set; }

        /// <summary>
        /// Gets or sets old slot number 
        /// </summary>
        public int OldSlotNumber { get; set; }

        /// <summary>
        /// Gets or sets new slot number 
        /// </summary>
        public int NewSlotNumber { get; set; }
    }
}
