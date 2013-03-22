
using System.Collections.Generic;
using Infusion.Gaming.LightCycles.Model.Serialization;

namespace Infusion.Gaming.LightCycles
{
    using System;
    using Infusion.Gaming.LightCycles.Conditions;
    using Infusion.Gaming.LightCycles.Events.Filtering;
    using Infusion.Gaming.LightCycles.Events.Processing;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.Defines;

    /// <summary>
    /// Game of LightCycles
    /// </summary>
    public class LightCyclesGame : Game
    {
        /// <summary>
        /// Starts game with random map
        /// </summary>
        /// <param name="numberOfPlayers">number of players in the game</param>
        /// <param name="gameMode">game mode to be played</param>
        public void StartOnRandomMap(int numberOfPlayers, GameModeEnum gameMode)
        {
            // init
            var generator = new MapStreamGenerator();
            string mapStream = generator.Generate(50, 20, numberOfPlayers);

            var mapSerializer = new MapSerializer();
            IMap map = mapSerializer.Read(mapStream);

            EndConditionSet endConditions = new EndConditionSet();
            endConditions.Add(new EndCondition(new NumberOfPlayers(0), GameResultEnum.FinishedWithoutWinner));

            switch (gameMode)
            {
                case GameModeEnum.FreeForAll:
                    endConditions.Add(new EndCondition(new NumberOfPlayers(1), GameResultEnum.FinshedWithWinner));
                    break;
                case GameModeEnum.TeamDeathmatch:
                    endConditions.Add(new EndCondition(new NumberOfTeams(1), GameResultEnum.FinshedWithWinners));
                    break;
                default:
                    throw new ArgumentOutOfRangeException("gameMode");
            }

            EventFilterSet eventFilters = new EventFilterSet();
            eventFilters.Add(new PlayersInGameFilter());
            eventFilters.Add(new PlayerRecentEventFilter());
            eventFilters.Add(new IdlePlayerMoveEventAppender(RelativeDirectionEnum.Undefined));

            EventProcessorSet eventProcesors = new EventProcessorSet();
            eventProcesors.Add(new EventLoggingProcessor(true));
            eventProcesors.Add(new PlayerMovesProcessor());
            eventProcesors.Add(new PlayerCollisionProcessor());
            eventProcesors.Add(new TrailAgingProcessor(0.2f));
            eventProcesors.Add(new GarbageProcessor(true));

            List<Player> players = new List<Player>();
            players.Add(new Player('A'));
            players.Add(new Player('B'));
            //foreach (PlayersStartingLocation startingLocation in map.StartingLocations.Keys)
            //    players.Add(startingLocation.Player);

            // start
            this.Start(
                GameModeEnum.FreeForAll,
                players,
                map,
                endConditions,
                eventFilters,
                eventProcesors);
        }
    }
}
