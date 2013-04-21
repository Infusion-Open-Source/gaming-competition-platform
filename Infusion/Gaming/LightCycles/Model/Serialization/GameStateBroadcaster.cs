using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infusion.Gaming.LightCycles.Model.Serialization
{
    public class GameStateBroadcaster : IGameStateSink
    {
        public void Flush(IGameState state)
        {
            throw new NotImplementedException();
        }
    }
}
