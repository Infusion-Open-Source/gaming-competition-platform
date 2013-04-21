using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Infusion.Gaming.LightCycles.Model.Serialization
{
    public class GameStateBroadcaster : IGameStateSink
    {
        public void Flush(IGameState state)
        {
            if (state == null)
            {
                throw new ArgumentNullException("gameState");
            }

            var writer = new StringWriter();
            var serializer = new StringGameStateSerializer(writer);
            serializer.Write(state);

            var snapshot = writer.GetStringBuilder().ToString();

        }
    }
}
