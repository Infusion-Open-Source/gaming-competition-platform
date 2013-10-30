namespace Infusion.Gaming.LightCycles.Model
{
    using System;
    using System.Collections.Generic;
    using Infusion.Gaming.LightCycles.Model.Data.MapObjects;

    /// <summary>
    /// Manages game slots
    /// </summary>
    public class GameSlotsPool : IGameSlotsPool
    {
        /// <summary>
        /// internal set of slots
        /// </summary>
        private GameSlotsCollection slots = new GameSlotsCollection();

        /// <summary>
        /// Initializes a new instance of the <see cref="GameSlotsPool" /> class.
        /// </summary>
        /// <param name="playersInfo">players info</param>
        /// <param name="startLocations">start locations</param>
        public GameSlotsPool(PlayerSetup playersInfo, IEnumerable<PlayersStartingLocation> startLocations)
        {
            List<PlayersStartingLocation> playerStartLocations = new List<PlayersStartingLocation>(startLocations);
            for (int slot = 0; slot < playersInfo.NumberOfPlayers; slot++)
            {
                Identity playerIdentity = playersInfo.PlayersIdentities[slot];
                Identity teamIdentity = playersInfo.TeamsIdentities[slot];
                PlayersStartingLocation playerStartLocation = null;
                foreach (PlayersStartingLocation startLocation in playerStartLocations)
                {
                    if (startLocation.Player.Equals(playerIdentity))
                    {
                        playerStartLocation = startLocation;
                    }
                }

                if (playerStartLocation == null)
                {
                    throw new ArgumentException("setup");
                }

                GameSlot gameSlot = new GameSlot(
                    slot,
                    playerIdentity,
                    teamIdentity,
                    playerStartLocation.Location);
                this.slots.Add(gameSlot);
            }
        }

        /// <summary>
        /// Gets game slots
        /// </summary>
        public GameSlotsCollection Slots
        {
            get
            {
                return new GameSlotsCollection(this.slots);
            }
        }
    }
}
