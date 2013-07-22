using System.Drawing;
using System.Threading;
using Infusion.Gaming.LightCycles;
using Infusion.Gaming.LightCycles.Definitions;
using Infusion.Gaming.LightCycles.Model;
using Infusion.Networking;

namespace Infusion.Gaming.LightCyclesNetworking
{
    /// <summary>
    /// Game slot info
    /// </summary>
    public class ServerPlayersInfo : PlayersInfo
    {
        /// <summary>
        /// Creates new instance of ServerPlayersInfo
        /// </summary>
        /// <param name="numberOfSlots">number of players slots</param>
        /// <param name="numberOfPlayers">number of players</param>
        /// <param name="numberOfTeams">number of teams</param>
        public ServerPlayersInfo(int numberOfSlots, int numberOfPlayers, int numberOfTeams)
            : base(numberOfPlayers, numberOfTeams)
        {
            this.NumberOfSlots = numberOfSlots;
            this.PlayersNames = new string[numberOfSlots];
            this.PlayersKeys = new string[numberOfSlots];
            this.PlayersConnections = new Client[numberOfSlots];
            this.PlayersWaitHandles = new WaitHandle[numberOfSlots];
            this.PlayersTypes = new PlayerType[numberOfSlots];
            this.PlayersColors = new Color[numberOfSlots];
            this.TeamsNames = new string[numberOfSlots];
            this.TeamsColors = new Color[numberOfSlots];
            this.PlayersNames = new string[numberOfSlots];
        }

        /// <summary>
        /// Gets or sets number of players in game
        /// </summary>
        public int NumberOfSlots { get; protected set; }

        /// <summary>
        /// Gets or sets number of players in game
        /// </summary>
        public string[] PlayersNames { get; protected set; }

        /// <summary>
        /// Gets or sets number of players in game
        /// </summary>
        public string[] PlayersKeys { get; protected set; }

        /// <summary>
        /// Gets or sets number of players in game
        /// </summary>
        public Client[] PlayersConnections { get; protected set; }

        /// <summary>
        /// Gets or sets number of players in game
        /// </summary>
        public WaitHandle[] PlayersWaitHandles { get; protected set; }

        /// <summary>
        /// Gets or sets number of players in game
        /// </summary>
        public PlayerType[] PlayersTypes { get; protected set; }

        /// <summary>
        /// Gets or sets number of players in game
        /// </summary>
        public Color[] PlayersColors { get; protected set; }

        /// <summary>
        /// Gets or sets number of players in game
        /// </summary>
        public string[] TeamsNames { get; protected set; }

        /// <summary>
        /// Gets or sets number of players in game
        /// </summary>
        public Color[] TeamsColors { get; protected set; }

        /// <summary>
        /// Get player index by key
        /// </summary>
        /// <param name="playerKey">player key</param>
        /// <returns>player index</returns>
        public int PlayerIndexByKey(string playerKey)
        {
            for (int i = 0; i < this.NumberOfPlayers; i++)
            {
                if (this.PlayersKeys[i].Equals(playerKey))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Get player index by key
        /// </summary>
        /// <param name="playerIdentity">player identity</param>
        /// <returns>player index</returns>
        public int PlayerIndexByIdentity(char playerIdentity)
        {
            for (int i = 0; i < this.NumberOfPlayers; i++)
            {
                if (this.PlayersIdentities[i].Identifier.Equals(playerIdentity))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Get player index by team
        /// </summary>
        /// <param name="teamIdentity">team identity</param>
        /// <returns>player index</returns>
        public int TeamIndexByIdentity(char teamIdentity)
        {
            for (int i = 0; i < this.NumberOfTeams; i++)
            {
                if (this.TeamsIdentities[i].Identifier.Equals(teamIdentity))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
