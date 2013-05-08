namespace Infusion.Gaming.LightCycles.Model.Data
{
    using System;
    using System.Globalization;
    using Infusion.Gaming.LightCycles.Extensions;
    using Infusion.Gaming.LightCycles.Model.Defines;

    /// <summary>
    /// The team.
    /// </summary>
    public class Team
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Team"/> class.
        /// </summary>
        /// <param name="id">
        /// The team id.
        /// </param>
        public Team(char id)
        {
            id = id.ToUpper();
            if (id < Constraints.MinTeamId || id > Constraints.MaxTeamId)
            {
                throw new ArgumentOutOfRangeException("id");
            }

            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public char Id { get; protected set; }
        
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