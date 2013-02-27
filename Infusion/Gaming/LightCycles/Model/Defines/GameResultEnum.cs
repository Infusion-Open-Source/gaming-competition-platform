// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameResultEnum.cs" company="Infusion">
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
//   The game result enumeration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infusion.Gaming.LightCycles.Model.Defines
{
    /// <summary>
    ///     The game result enumeration.
    /// </summary>
    public enum GameResultEnum
    {
        /// <summary>
        ///     Undefined result.
        /// </summary>
        Undefined = 0, 

        /// <summary>
        ///     Game is still running.
        /// </summary>
        StillRunning, 

        /// <summary>
        ///     Game finished with a winner.
        /// </summary>
        FinshedWithWinner, 

        /// <summary>
        ///     Game finished without a winner.
        /// </summary>
        FinishedWithoutWinner, 

        /// <summary>
        ///     Game has been terminated.
        /// </summary>
        Terminated
    }
}