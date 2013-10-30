namespace Infusion.Gaming.LightCycles.UIClient.Data
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Game identity
    /// </summary>
    public class GameIdentity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameIdentity"/> class.
        /// </summary>
        /// <param name="identity">identity identifier</param>
        /// <param name="name">identity name</param>
        /// <param name="color">identity color</param>
        public GameIdentity(string identity, string name, string color)
        {
            this.Id = identity[0];
            this.Name = name;
            string[] parts = color.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            this.Color = Color.FromArgb(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameIdentity"/> class.
        /// </summary>
        /// <param name="identity">identity identifier</param>
        /// <param name="name">identity name</param>
        /// <param name="color">identity color</param>
        public GameIdentity(char identity, string name, Color color)
        {
            this.Id = identity;
            this.Name = name;
            this.Color = color;
        }

        /// <summary>
        /// Gets or sets identifier
        /// </summary>
        public char Id { get; protected set; }

        /// <summary>
        /// Gets or sets name assigned to identity
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets or sets color assigned to identity
        /// </summary>
        public Color Color { get; protected set; }
    }
}
