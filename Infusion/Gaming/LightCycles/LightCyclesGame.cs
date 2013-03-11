
namespace Infusion.Gaming.LightCycles
{
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
