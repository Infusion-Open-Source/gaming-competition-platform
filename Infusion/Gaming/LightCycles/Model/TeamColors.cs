namespace Infusion.Gaming.LightCycles.Model
{
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// Team colors assignments singleton
    /// </summary>
    public class TeamColors : Dictionary<int, Color>
    {
        /// <summary>
        /// Gets team colors data
        /// </summary>
        public static readonly TeamColors Data = new TeamColors();

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamColors" /> class.
        /// </summary>
        protected TeamColors()
        {
            this.Add('A', Color.FromArgb(255, 0, 0));
            this.Add('B', Color.FromArgb(0, 255, 0));
            this.Add('C', Color.FromArgb(0, 0, 255));
            this.Add('D', Color.FromArgb(255, 255, 0));
            this.Add('E', Color.FromArgb(255, 0, 255));
            this.Add('F', Color.FromArgb(0, 255, 255));
            this.Add('G', Color.FromArgb(255, 127, 0));
            this.Add('H', Color.FromArgb(255, 0, 127));
            this.Add('I', Color.FromArgb(127, 0, 255));
            this.Add('J', Color.FromArgb(0, 255, 127));
            this.Add('K', Color.FromArgb(0, 127, 255));
            this.Add('L', Color.FromArgb(127, 255, 0));
        }
    }
}
