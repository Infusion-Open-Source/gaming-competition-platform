using Infusion.Gaming.LightCycles;
using Infusion.Gaming.LightCycles.Conditions;
using Infusion.Gaming.LightCycles.Definitions;
using Infusion.Gaming.LightCycles.Events.Filtering;
using Infusion.Gaming.LightCycles.Events.Processing;
using Infusion.Gaming.LightCycles.Model;
using Infusion.Gaming.LightCycles.Serialization;

namespace Infusion.Gaming.LightCyclesNetworking
{
    /// <summary>
    /// Extends core game implementation with extra features required by server
    /// </summary>
    public class ServerGame : Game
    {
        public ServerGame(GameMode mode, MapInfo mapInfo, PlayersInfo playersInfo, IMapProvider mapProvider, IGameSlotsPool slotPool, IEndCondition endCondition, IEventFilter eventFilter, IEventProcessor eventProcessor) 
            : base(mode, mapInfo, playersInfo, mapProvider, slotPool, endCondition, eventFilter, eventProcessor)
        {
        }

        public ServerGame(GameMode mode, MapInfo mapInfo, PlayersInfo playersInfo) 
            : base(mode, mapInfo, playersInfo)
        {
        }

        /// <summary>
        /// Gets or sets game players info
        /// </summary>
        public ServerPlayersInfo ServerPlayersInfo { get { return (ServerPlayersInfo)this.PlayersInfo; } }
    }
}