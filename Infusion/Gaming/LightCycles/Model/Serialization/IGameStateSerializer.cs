using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infusion.Gaming.LightCycles.Model.Serialization
{
    interface IGameStateSerializer
    {
        void Write(IGameState gameState);
    }
}
