namespace Infusion.Gaming.LightCycles.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Strongly typed collection of game slots
    /// </summary>
    public class GameSlotsCollection : List<GameSlot>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameSlotsCollection" /> class.
        /// </summary>
        public GameSlotsCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameSlotsCollection" /> class.
        /// </summary>
        /// <param name="collection">initializing collection</param>
        public GameSlotsCollection(IEnumerable<GameSlot> collection)
            : base(collection)
        {
        }

        /// <summary>
        /// Gets all players
        /// </summary>
        public IdentityCollection PlayersIdentites
        {
            get
            {
                IdentityCollection results = new IdentityCollection();
                foreach (GameSlot slot in this)
                {
                    results.Add(slot.Player);
                }

                return results;
            }
        }

        /// <summary>
        /// Gets all teams
        /// </summary>
        public IdentityCollection TeamsIdentites
        {
            get
            {
                IdentityCollection results = new IdentityCollection();
                foreach (GameSlot slot in this)
                {
                    results.Add(slot.Team);
                }

                return results;
            }
        }

        /// <summary>
        /// Gets slot for given player identity
        /// </summary>
        /// <param name="identity">player identity</param>
        /// <returns>players slot</returns>
        public GameSlot GetPlayersSlot(Identity identity)
        {
            foreach (GameSlot slot in this)
            {
                if (slot.Player.Equals(identity))
                {
                    return slot;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets slots for given team identity
        /// </summary>
        /// <param name="identity">team identity</param>
        /// <returns>team slots</returns>
        public GameSlotsCollection GetTeamSlots(Identity identity)
        {
            GameSlotsCollection results = new GameSlotsCollection();
            foreach (GameSlot slot in this)
            {
                if (slot.Team.Equals(identity))
                {
                    results.Add(slot);
                }
            }

            return results;
        }
    }
}
