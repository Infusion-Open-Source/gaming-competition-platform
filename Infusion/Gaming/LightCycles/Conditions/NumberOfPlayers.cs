// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NumberOfPlayers.cs" company="Infusion">
//    Copyright (C) 2013 Paweł Drozdowski
//
//    This file is part of LightCycles Game Engine.
//
//    LightCycles Game Engine is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    LightCycles Game Engine is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with LightCycles Game Engine.  If not, see http://www.gnu.org/licenses/.
// </copyright>
// <summary>
//   The number of players condition
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infusion.Gaming.LightCycles.Conditions
{
    using System;

    /// <summary>
    ///     The number of players condition
    /// </summary>
    public class NumberOfPlayers : ICondition
    {
        #region Constructors and Destructors

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

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the max number of players.
        /// </summary>
        public int Max { get; protected set; }

        /// <summary>
        ///     Gets or sets the min number of players.
        /// </summary>
        public int Min { get; protected set; }

        #endregion

        #region Public Methods and Operators

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
            int count = game.CurrentState.Map.Players.Count;
            return this.Min <= count && count <= this.Max;
        }

        #endregion
    }
}