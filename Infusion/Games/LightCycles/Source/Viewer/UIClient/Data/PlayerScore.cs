namespace Infusion.Gaming.LightCycles.UIClient.Data
{
    /// <summary>
    /// Players score class
    /// </summary>
    public class PlayerScore
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerScore"/> class.
        /// </summary>
        /// <param name="scoreText">score text to parse</param>
        public PlayerScore(string scoreText)
        {
            string[] parts = scoreText.Split(new char[] { ':' }, System.StringSplitOptions.RemoveEmptyEntries);

            this.Identity = parts[0][0];
            this.Name = parts[1];
            this.Score = int.Parse(parts[2]);
        }

        /// <summary>
        /// Gets or sets player identity
        /// </summary>
        public char Identity { get; protected set; }

        /// <summary>
        /// Gets or sets player name
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets or sets player score
        /// </summary>
        public int Score { get; protected set; }
    }
}
