using System;
using System.Drawing;
using Infusion.Gaming.LightCycles.Conditions;
using Infusion.Gaming.LightCycles.Definitions;
using Infusion.Gaming.LightCycles.Model;
using Infusion.Gaming.LightCycles.Model.Data.MapObjects;

namespace Infusion.Gaming.LightCycles
{
    /// <summary>
    /// Game slot info
    /// </summary>
    public class PlayersInfo
    {
        /// <summary>
        /// Creates new instance of PlayersInfo
        /// </summary>
        /// <param name="numberOfPlayers">number of players</param>
        /// <param name="numberOfTeams">number of teams</param>
        public PlayersInfo(int numberOfPlayers, int numberOfTeams)
        {
            if (numberOfPlayers < Constraints.MinNumberOfPlayers || numberOfPlayers > Constraints.MaxNumberOfPlayers)
            {
                throw new ArgumentOutOfRangeException("numberOfPlayers");
            }

            if (numberOfTeams < Constraints.MinNumberOfTeams || numberOfTeams > Constraints.MaxNumberOfTeams)
            {
                throw new ArgumentOutOfRangeException("numberOfTeams");
            }

            this.NumberOfPlayers = numberOfPlayers;
            this.NumberOfTeams = numberOfTeams;
            this.PlayersIdentities = new IdentityCollection();
            this.TeamsIdentities = new IdentityCollection();
        }

        /// <summary>
        /// Gets or sets number of players in game
        /// </summary>
        public int NumberOfPlayers { get; protected set; }

        /// <summary>
        /// Gets or sets number of teams in game
        /// </summary>
        public int NumberOfTeams { get; protected set; }

        /// <summary>
        /// Gets or sets number of players in game
        /// </summary>
        public IdentityCollection PlayersIdentities { get; protected set; }
        
        /// <summary>
        /// Gets or sets number of players in game
        /// </summary>
        public IdentityCollection TeamsIdentities { get; protected set; }
    }
}
