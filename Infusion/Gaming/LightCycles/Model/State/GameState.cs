using System;
using Infusion.Gaming.LightCycles.Model.Data;

namespace Infusion.Gaming.LightCycles.Model.State
{
    /// <summary>
    /// The game state.
    /// </summary>
    public class GameState : IGameState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameState"/> class.
        /// </summary>
        /// <param name="turn"> The turn. </param>
        /// <param name="objects"> The game objects state. </param>
        public GameState(int turn, Objects objects)
        {
            if (turn < 0)
            {
                throw new ArgumentOutOfRangeException("turn");
            }

            if (objects == null)
            {
                throw new ArgumentNullException("objects");
            }

            this.Turn = turn;
            this.Objects = objects;
        }

        /// <summary>
        /// Gets or sets the turn.
        /// </summary>
        public int Turn { get; protected set; }

        /// <summary>
        /// Gets or sets game objects states
        /// </summary>
        public Objects Objects { get; protected set; }
    }
}