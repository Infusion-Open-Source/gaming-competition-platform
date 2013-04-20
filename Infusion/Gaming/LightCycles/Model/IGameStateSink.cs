using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infusion.Gaming.LightCycles.Model
{
    public interface IGameStateSink
    {
        void Flush(IGameState state);
    }
}
