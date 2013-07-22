﻿using Infusion.Gaming.LightCycles.Model.State;
using System;

namespace Infusion.Gaming.LightCycles.Serialization
{
    /// <summary>
    /// Game state reader from text
    /// </summary>
    public class TextGameStateReader : IGameStateReader<string>
    {
        /// <summary>
        /// Reads game state
        /// </summary>
        /// <param name="text">input data</param>
        /// <returns>Read game state</returns>
        public IGameState Read(string text)
        {
            throw new NotImplementedException();
        }
    }
}
