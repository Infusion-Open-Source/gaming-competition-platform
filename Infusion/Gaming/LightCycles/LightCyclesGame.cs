namespace Infusion.Gaming.LightCycles
{
    using System;
    using System.Collections.Generic;
    using Infusion.Gaming.LightCycles.Conditions;
    using Infusion.Gaming.LightCycles.Events.Filtering;
    using Infusion.Gaming.LightCycles.Events.Processing;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.Defines;
    using Infusion.Gaming.LightCycles.Model.MapData;
    using Infusion.Gaming.LightCycles.Model.Serialization;

    /// <summary>
    /// Game of LightCycles
    /// </summary>
    public class LightCyclesGame : Game
    {
        /// <summary>
        /// Starts a game with a random map
        /// </summary>
        /// <param name="gameInfo">game start info</param>
        public void Start(GameInfo gameInfo)
        {
            IMap map;
            if (gameInfo.UseMapFile)
            {
                IMapSerializer mapSerializer = new ImageMapSerializer(gameInfo.MapFileName);
                mapSerializer.Load();
                map = mapSerializer.Read();
            }
            else
            {
                map = new MapGenerator().GenerateMap(gameInfo.MapWidth, gameInfo.MapHeight, gameInfo.NumberOfPlayers, gameInfo.NumberOfTeams);
            }
            
            if (map == null)
            {
                throw new GameException("Map is NULL, terminating");
            }

            EndConditionSet endConditions = new EndConditionSet();
            endConditions.Add(new EndCondition(new NumberOfPlayers(0), GameResultEnum.FinishedWithoutWinner));

            switch (gameInfo.GameMode)
            {
                case GameModeEnum.FreeForAll:
                    endConditions.Add(new EndCondition(new NumberOfPlayers(1), GameResultEnum.FinshedWithWinner));
                    break;
                case GameModeEnum.TeamDeathMatch:
                    endConditions.Add(new EndCondition(new NumberOfTeams(1), GameResultEnum.FinshedWithWinners));
                    break;
                default:
                    throw new ArgumentOutOfRangeException("gameInfo");
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
            foreach (PlayersStartingLocation startingLocation in map.StartingLocations.Keys)
            {
                players.Add(startingLocation.Player);
            }

            // start
            this.Start(
                gameInfo.GameMode,
                players,
                map,
                endConditions,
                eventFilters,
                eventProcesors);
        }
    }
}
