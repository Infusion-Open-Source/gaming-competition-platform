// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerEvent.cs" company="Infusion">
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
//   The base class for player event.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Infusion.Gaming.LightCycles.Model;

namespace Infusion.Gaming.LightCycles.Events
{
    using Infusion.Gaming.LightCycles.Model.Data;

    /// <summary>
    ///     The base class for player event.
    /// </summary>
    public abstract class PlayerEvent : Event
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerEvent"/> class.
        /// </summary>
        /// <param name="player">
        /// The player related to the event.
        /// </param>
        protected PlayerEvent(Player player)
        {
            this.Player = player;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the player.
        /// </summary>
        public Player Player { get; protected set; }

        #endregion
    }
}