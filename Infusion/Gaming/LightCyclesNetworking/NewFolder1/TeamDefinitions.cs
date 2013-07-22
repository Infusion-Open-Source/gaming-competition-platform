using System.Collections.Generic;
using System.Drawing;
using Infusion.Gaming.LightCycles.Extensions;

namespace Infusion.Gaming.LightCyclesNetworking.NewFolder1
{
    /// <summary>
    /// Team definitions class
    /// </summary>
    public class TeamDefinitions : List<TeamDefinition>
    {/*
        /// <summary>
        /// Gets the team definitions
        /// </summary>
        public static TeamDefinitions TeamDefinitions
        {
            get
            {
                return new TeamDefinitions
                    {
                        new TeamDefinition('A', Color.FromArgb(255, 0, 0)),
                        new TeamDefinition('B', Color.FromArgb(0, 255, 0)),
                        new TeamDefinition('C', Color.FromArgb(0, 0, 255)),
                        new TeamDefinition('D', Color.FromArgb(255, 255, 0)),
                        new TeamDefinition('E', Color.FromArgb(255, 0, 255)),
                        new TeamDefinition('F', Color.FromArgb(0, 255, 255)),
                        new TeamDefinition('G', Color.FromArgb(255, 127, 0)),
                        new TeamDefinition('H', Color.FromArgb(255, 0, 127)),
                    };
            }
        }*/

        /// <summary>
        /// Is color defined for team or trail
        /// </summary>
        /// <param name="color">color to check</param>
        /// <returns>does color correspond to team or team trail</returns>
        public bool IsColorDefined(Color color)
        {
            return this.IsTrailColor(color) || this.IsTeamColor(color);
        }

        /// <summary>
        /// Is trails color defined
        /// </summary>
        /// <param name="color">color to check</param>
        /// <returns>does color correspond to the team trail</returns>
        public bool IsTrailColor(Color color)
        {
            foreach (var definition in this)
            {
                if (definition.TrailColor.Equals(color))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Is team color defined
        /// </summary>
        /// <param name="color">color to check</param>
        /// <returns>does color correspond to the team</returns>
        public bool IsTeamColor(Color color)
        {
            foreach (var definition in this)
            {
                if (definition.Color.AreSame(color))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Is team defined
        /// </summary>
        /// <param name="id">character to check</param>
        /// <returns>is team for given character defined</returns>
        public bool IsTeamDefined(char id)
        {
            foreach (var definition in this)
            {
                if (definition.Id.Equals(id))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Get team by color
        /// </summary>
        /// <param name="color">color for which to find team</param>
        /// <returns>team char for given color</returns>
        public TeamDefinition GetTeamByColor(Color color)
        {
            foreach (var definition in this)
            {
                if (definition.Color.AreSame(color) || definition.TrailColor.AreSame(color))
                {
                    return definition;
                }
            }

            return null;
        }

        /// <summary>
        /// Get team by id
        /// </summary>
        /// <param name="id">team to look for</param>
        /// <returns>team color</returns>
        public TeamDefinition GetTeamById(char id)
        {
            foreach (var definition in this)
            {
                if (definition.Id.Equals(id))
                {
                    return definition;
                }
            }

            return null;
        }
    }
}
