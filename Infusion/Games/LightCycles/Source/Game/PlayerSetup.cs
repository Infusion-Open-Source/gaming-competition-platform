namespace Infusion.Gaming.LightCycles
{
    using System;
    using System.Collections.Generic;
    using Infusion.Gaming.LightCycles.Model;

    /// <summary>
    /// Game slot info
    /// </summary>
    public class PlayerSetup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerSetup" /> class.
        /// </summary>
        /// <param name="players">players identifier per slot, example: ABCDEFGH show which player is on which slot</param>
        /// <param name="teams">team identifier per slot, example: AAAABBBB show which team is on which player slot</param>
        public PlayerSetup(string players, string teams)
        {
            if (string.IsNullOrEmpty(players))
            {
                throw new ArgumentNullException("players");
            }

            if (players.Length != teams.Length)
            {
                throw new ArgumentOutOfRangeException("teams");
            }

            this.PlayersIdentities = new IdentityCollection();
            this.TeamsIdentities = new IdentityCollection();
            this.Scoreboard = new Dictionary<Identity, int>();
            for (int i = 0; i < players.Length; i++)
            {
                this.PlayersIdentities.Add(new Identity(players[i]));
                this.TeamsIdentities.Add(new Identity(teams[i]));
                this.Scoreboard.Add(new Identity(players[i]), 0);
            }
        }

        /// <summary>
        /// Gets number of players in game
        /// </summary>
        public int NumberOfPlayers
        {
            get
            {
                return this.PlayersIdentities.Unique.Count;
            }
        }

        /// <summary>
        /// Gets number of teams in game
        /// </summary>
        public int NumberOfTeams
        {
            get
            {
                return this.TeamsIdentities.Unique.Count;
            }
        }

        /// <summary>
        /// Gets or sets number of players in game
        /// </summary>
        public IdentityCollection PlayersIdentities { get; protected set; }

        /// <summary>
        /// Gets or sets number of players in game
        /// </summary>
        public IdentityCollection TeamsIdentities { get; protected set; }

        /// <summary>
        /// Gets or sets players scores in game
        /// </summary>
        public Dictionary<Identity, int> Scoreboard { get; protected set; }
    }
}
