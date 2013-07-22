using System.Collections.Generic;
using Infusion.Gaming.LightCycles.Model.State;

namespace Infusion.Gaming.LightCycles.Conditions
{
    using System;
    using Infusion.Gaming.LightCycles.Model;

    /// <summary>
    /// The number of teams condition
    /// </summary>
    public class NumberOfTeams : ICondition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumberOfTeams"/> class.
        /// </summary>
        /// <param name="exactNumber">
        /// The exact number of players in game.
        /// </param>
        public NumberOfTeams(int exactNumber)
            : this(exactNumber, exactNumber)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberOfTeams"/> class.
        /// </summary>
        /// <param name="minNumber">
        /// The min number of teams in game.
        /// </param>
        /// <param name="maxNumber">
        /// The max number of teams in game.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when number of teams less than zero, or max less than min
        /// </exception>
        public NumberOfTeams(int minNumber, int maxNumber)
        {
            if (minNumber < 0)
            {
                throw new ArgumentOutOfRangeException("minNumber");
            }

            if (maxNumber < minNumber)
            {
                throw new ArgumentOutOfRangeException("maxNumber");
            }

            this.Min = minNumber;
            this.Max = maxNumber;
        }

        /// <summary>
        /// Gets or sets the max number of teams.
        /// </summary>
        public int Max { get; protected set; }

        /// <summary>
        /// Gets or sets the min number of teams.
        /// </summary>
        public int Min { get; protected set; }

        /// <summary>
        /// Performs condition check.
        /// </summary>
        /// <param name="game">
        /// The game on which condition check should be performed.
        /// </param>
        /// <returns>
        /// The result of the condition check.
        /// </returns>
        public bool Check(IGame game)
        {
            int count = game.CurrentState.Objects.Teams.Count;
            return this.Min <= count && count <= this.Max;
        }
    }
}