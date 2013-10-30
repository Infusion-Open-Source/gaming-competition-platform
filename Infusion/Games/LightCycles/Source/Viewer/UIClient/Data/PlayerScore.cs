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
            this.Identity = scoreText[0];
            this.Score = int.Parse(scoreText.Remove(0, 2));
        }

        /// <summary>
        /// Gets or sets player identity
        /// </summary>
        public char Identity { get; protected set; }

        /// <summary>
        /// Gets or sets player score
        /// </summary>
        public int Score { get; protected set; }
    }
}
