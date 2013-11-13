namespace Infusion.Gaming.LightCycles.Model
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
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
        /// <param name="randomize">should randomize start locations</param>
        public GameSlotsPool(PlayerSetup playersInfo, IEnumerable<PlayersStartingLocation> startLocations, bool randomize)
        {
            List<PlayersStartingLocation> playerStartLocations = new List<PlayersStartingLocation>(startLocations);
            GameSlotsCollection newSlots = new GameSlotsCollection();
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
                newSlots.Add(gameSlot);
            }

            if (randomize)
            {
                newSlots = this.RanodmizeStartLocations(newSlots);
            }

            this.slots = newSlots;
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

        /// <summary>
        /// Make start locations random
        /// </summary>
        /// <param name="slotsToRandomize">list of slots</param>
        /// <returns>slots with randomized start locations</returns>
        private GameSlotsCollection RanodmizeStartLocations(GameSlotsCollection slotsToRandomize)
        {
            List<Point> locations = new List<Point>();
            foreach (GameSlot slot in slotsToRandomize)
            {
                locations.Add(slot.StartLocation);
            }

            Random random = new Random((int)DateTime.Now.Ticks);
            GameSlotsCollection newSlots = new GameSlotsCollection();
            foreach (GameSlot slot in slotsToRandomize)
            {
                int locationIndex = random.Next(locations.Count);
                newSlots.Add(new GameSlot(slot.SlotNumber, slot.Player, slot.Team, locations[locationIndex]));
                locations.RemoveAt(locationIndex);
            }

            return newSlots;
        }        
    }
}
