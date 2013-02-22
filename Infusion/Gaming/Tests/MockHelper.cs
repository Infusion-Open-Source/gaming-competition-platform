// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MockHelper.cs" company="Infusion">
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
//   The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Infusion.Gaming.LightCycles.Tests
{
    using System.Collections.Generic;

    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;

    using Moq;

    /// <summary>
    ///     The mock helper.
    /// </summary>
    public class MockHelper
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MockHelper" /> class.
        /// </summary>
        protected MockHelper()
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Creates game mock with given number of players.
        /// </summary>
        /// <param name="numberOfPlayers">
        /// The number of players.
        /// </param>
        /// <returns>
        /// The mocked game.
        /// </returns>
        public static Mock<IGame> CreateGame(int numberOfPlayers)
        {
            var players = new List<Player>();
            for (int i = 0; i < numberOfPlayers; i++)
            {
                players.Add(new Player((char)('A' + i)));
            }

            var mockMap = new Mock<IMap>();
            mockMap.SetupGet(map => map.Players).Returns(players);
            var mockState = new Mock<IGameState>();
            mockState.SetupGet(state => state.Map).Returns(mockMap.Object);
            var mockGame = new Mock<IGame>();
            mockGame.SetupGet(game => game.CurrentState).Returns(mockState.Object);
            return mockGame;
        }

        #endregion
    }
}