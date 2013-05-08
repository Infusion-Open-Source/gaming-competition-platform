namespace Infusion.Gaming.LightCycles.Model.Defines
{
    using System.Drawing;
    using Infusion.Gaming.LightCycles.Extensions;

    /// <summary>
    /// Team definitions class
    /// </summary>
    public class TeamDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TeamDefinition"/> class.
        /// </summary>
        /// <param name="id">Team Id</param>
        /// <param name="color">Team color</param>
        public TeamDefinition(char id, Color color)
        {
            this.Id = id;
            this.TrailId = id.ToLower();
            this.Color = color;
            this.TrailColor = this.ConvertToTrailColor(color);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamDefinition"/> class.
        /// </summary>
        /// <param name="id">Team Id</param>
        /// <param name="color">Team color</param>
        /// <param name="trailColor">Team trial color</param>
        public TeamDefinition(char id, Color color, Color trailColor)
        {
            this.Id = id.ToUpper();
            this.TrailId = id.ToLower();
            this.Color = color;
            this.TrailColor = trailColor;
        }

        /// <summary>
        /// Gets or sets team id
        /// </summary>
        public char Id { get; protected set; }

        /// <summary>
        /// Gets or sets team id
        /// </summary>
        public char TrailId { get; protected set; }

        /// <summary>
        /// Gets or sets team color
        /// </summary>
        public Color Color { get; protected set; }

        /// <summary>
        /// Gets or sets team trail color
        /// </summary>
        public Color TrailColor { get; protected set; }
        
        /// <summary>
        /// Convert from team color to trail color
        /// </summary>
        /// <param name="color">team color to convert</param>
        /// <returns>team trail color</returns>
        protected Color ConvertToTrailColor(Color color)
        {
            return Color.FromArgb(color.R / 2, color.G / 2, color.B / 2);
        }
    }
}
