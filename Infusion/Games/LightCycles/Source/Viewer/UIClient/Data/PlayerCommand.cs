namespace Infusion.Gaming.LightCycles.UIClient.Data
{
    /// <summary>
    /// Players command
    /// </summary>
    public class PlayerCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerCommand"/> class.
        /// </summary>
        /// <param name="commandText">command text to parse</param>
        public PlayerCommand(string commandText)
        {
            this.Identity = commandText[0];
            this.Command = commandText.Remove(0, 2);
        }

        /// <summary>
        /// Gets or sets player identity
        /// </summary>
        public char Identity { get; protected set; }

        /// <summary>
        /// Gets or sets player command
        /// </summary>
        public string Command { get; protected set; }
    }
}
