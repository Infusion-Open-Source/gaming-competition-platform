// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndCondition.cs" company="Infusion">
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
//   The game end condition.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infusion.Gaming.LightCycles.Conditions
{
    using System;

    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Defines;

    /// <summary>
    ///     The game end condition.
    /// </summary>
    public class EndCondition : IEndCondition
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EndCondition"/> class.
        /// </summary>
        /// <param name="condition">
        /// The condition carried by end condition check
        /// </param>
        /// <param name="result">
        /// The result of the end condition when condition is met
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when condition argument is null
        /// </exception>
        public EndCondition(ICondition condition, GameResultEnum result)
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            this.Condition = condition;
            this.Result = result;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the condition.
        /// </summary>
        public ICondition Condition { get; protected set; }

        /// <summary>
        ///     Gets or sets the game result when condition is met.
        /// </summary>
        public GameResultEnum Result { get; protected set; }

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
            return this.Condition.Check(game);
        }

        #endregion
    }
}