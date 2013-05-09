namespace Infusion.Gaming.LightCycles.Model.Data
{
    using System;
    using System.Globalization;
    using Infusion.Gaming.LightCyclesCommon.Definitions;
    using Infusion.Gaming.LightCyclesCommon.Extensions;

    /// <summary>
    /// The player.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="id">
        /// The player id.
        /// </param>
        public Player(char id)
            : this(id, new Team(id))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="id">
        /// The player id.
        /// </param>
        /// <param name="team">
        /// The player team assignment.
        /// </param>
        public Player(char id, Team team)
        {
            id = id.ToUpper();
            if (id < Constraints.MinTeamId || id > Constraints.MaxTeamId)
            {
                throw new ArgumentOutOfRangeException("id");
            }

            if (team == null)
            {
                throw new ArgumentNullException("team");
            }

            this.Id = id;
            this.Team = team;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public char Id { get; protected set; }

        /// <summary>
        /// Gets or sets the team.
        /// </summary>
        public Team Team { get; protected set; }
        
        /// <summary>
        /// Check if equals.
        /// </summary>
        /// <param name="obj">
        /// The object to compare to.
        /// </param>
        /// <returns>
        /// The result of comparison.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return obj.GetHashCode() == this.GetHashCode();
        }

        /// <summary>
        /// Gets the hash code of the object.
        /// </summary>
        /// <returns>
        /// The hash code.
        /// </returns>
        public override int GetHashCode()
        {
            return this.Id;
        }

        /// <summary>
        /// To string.
        /// </summary>
        /// <returns>
        /// String representation of an object.
        /// </returns>
        public override string ToString()
        {
            return this.Id.ToString(CultureInfo.InvariantCulture);
        }
    }
}