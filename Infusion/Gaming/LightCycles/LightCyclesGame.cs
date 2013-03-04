// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LightCyclesGame.cs" company="Infusion">
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
//   The direction helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infusion.Gaming.LightCycles
{
    using Infusion.Gaming.LightCycles.Conditions;
    using Infusion.Gaming.LightCycles.Events.Filtering;
    using Infusion.Gaming.LightCycles.Events.Processing;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.Defines;

    public class LightCyclesGame : Game
    {
        /// <summary>
        /// Starts game with random map
        /// </summary>
        /// <param name="numberOfPlayers">number of players in the game</param>
        public void StartOnRandomMap(int numberOfPlayers)
        {
            // init
            var generator = new MapStreamGenerator();
            string mapStream = generator.Generate(50, 20, numberOfPlayers);

            var mapSerializer = new MapSerializer();
            IMap map = mapSerializer.Read(mapStream);

            // start
            this.Start(
                GameModeEnum.FreeForAll,
                map.Players,
                map,
                new EndConditionSet
                        {
                            new EndCondition(new NumberOfPlayers(0), GameResultEnum.FinishedWithoutWinner),
                            new EndCondition(new NumberOfPlayers(1), GameResultEnum.FinshedWithWinner),
                            new EndCondition(new NumberOfTeams(1), GameResultEnum.FinshedWithWinners),
                        },
                new EventFilterSet
                        {
                            new PlayersInGameFilter(),
                            new PlayerRecentEventFilter(),
                            new IdlePlayerMoveEventAppender(RelativeDirectionEnum.Undefined)
                        },
                new EventProcessorSet
                        {
                            new EventLoggingProcessor(true),
                            new PlayerMovesProcessor(),
                            new PlayerCollisionProcessor(),
                            new TrailAgingProcessor(0.2f),
                            new GarbageProcessor(true)
                        });
        }
    }
}
