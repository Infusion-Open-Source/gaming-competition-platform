﻿
namespace Infusion.Gaming.LightCycles.Model
{
    using System;
    using System.Globalization;

    using Infusion.Gaming.LightCycles.Extensions;

    /// <summary>
    ///     The player.
    /// </summary>
    public class Player
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="id">
        /// The player id.
        /// </param>
        public Player(char id)
            : this(id, id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="id">
        /// The player id.
        /// </param>
        /// <param name="teamId">
        /// The player team Id.
        /// </param>
        public Player(char id, char teamId)
        {
            id = id.ToUpper();
            teamId = teamId.ToUpper();
            if (id < 'A' || id > 'Z')
            {
                throw new ArgumentOutOfRangeException("id");
            }

            if (teamId < 'A' || teamId > 'Z')
            {
                throw new ArgumentOutOfRangeException("teamId");
            }

            this.Id = id;
            this.TeamId = teamId;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public char Id { get; protected set; }

        /// <summary>
        ///     Gets or sets the team Id.
        /// </summary>
        public char TeamId { get; protected set; }

        #endregion

        #region Public Methods and Operators

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
        ///     Gets the hash code of the object.
        /// </summary>
        /// <returns>
        ///     The hash code.
        /// </returns>
        public override int GetHashCode()
        {
            return this.Id;
        }

        /// <summary>
        ///     To string.
        /// </summary>
        /// <returns>
        ///     String representation of an object.
        /// </returns>
        public override string ToString()
        {
            return this.Id.ToString(CultureInfo.InvariantCulture);
        }

        #endregion
    }
}