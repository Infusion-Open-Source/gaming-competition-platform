﻿namespace Infusion.Gaming.LightCycles.Conditions
{
    using System;

    using Infusion.Gaming.LightCycles.Model;

    /// <summary>
    /// The number of players condition
    /// </summary>
    public class NumberOfPlayers : ICondition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumberOfPlayers"/> class.
        /// </summary>
        /// <param name="exactNumber">
        /// The exact number of players in game.
        /// </param>
        public NumberOfPlayers(int exactNumber)
            : this(exactNumber, exactNumber)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberOfPlayers"/> class.
        /// </summary>
        /// <param name="minNumber">
        /// The min number of players in game.
        /// </param>
        /// <param name="maxNumber">
        /// The max number of players in game.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when number of players less than zero, or max less than min
        /// </exception>
        public NumberOfPlayers(int minNumber, int maxNumber)
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
        /// Gets or sets the max number of players.
        /// </summary>
        public int Max { get; protected set; }

        /// <summary>
        /// Gets or sets the min number of players.
        /// </summary>
        public int Min { get; protected set; }

        /// <summary>
        /// Performs condition check.
        /// </summary>
        /// <param name="gameState">
        /// The game state on which condition check should be performed.
        /// </param>
        /// <returns>
        /// The result of the condition check.
        /// </returns>
        public bool Check(IGameState gameState)
        {
            int count = gameState.PlayersData.Players.Count;
            return this.Min <= count && count <= this.Max;
        }
    }
}