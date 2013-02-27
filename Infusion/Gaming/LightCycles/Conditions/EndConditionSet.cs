// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EndConditionSet.cs" company="Infusion">
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

using System.Collections.Generic;
using Infusion.Gaming.LightCycles.Model.Defines;

namespace Infusion.Gaming.LightCycles.Conditions
{
    using System;

    using Infusion.Gaming.LightCycles.Model;

    /// <summary>
    ///     Set of the game end conditions.
    /// </summary>
    public class EndConditionSet : List<IEndCondition>, IEndCondition
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EndCondition"/> class.
        /// </summary>
        public EndConditionSet()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EndCondition"/> class.
        /// </summary>
        /// <param name="conditions">
        /// The set of game ending conditions
        /// </param>
        public EndConditionSet(IEnumerable<IEndCondition> conditions)
            : base(conditions)
        {
        }

        #endregion

        #region Public Properties
        
        /// <summary>
        ///     Gets the game result when condition is met.
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
            foreach (IEndCondition endCondition in this)
            {
                if(endCondition.Check(game))
                {
                    this.Result = endCondition.Result;
                    return true;
                }
            }

            this.Result = GameResultEnum.Undefined;
            return false;
        }

        #endregion
    }
}