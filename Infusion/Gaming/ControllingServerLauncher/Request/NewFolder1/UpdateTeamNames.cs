namespace Infusion.Networking.ControllingServer.Request.NewFolder1
{
    /// <summary>
    /// Request for changing team names
    /// </summary>
    public class UpdateTeamNames : RequestBase<UpdateTeamNames>
    {
        /// <summary>
        /// Gets or sets game id
        /// </summary>
        public int GameId { get; set; }

        /// <summary>
        /// Gets or sets old slot number 
        /// </summary>
        public string[] TeamNames { get; set; }
    }
}
