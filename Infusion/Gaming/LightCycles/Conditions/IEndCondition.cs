using Infusion.Gaming.LightCycles.Definitions;

namespace Infusion.Gaming.LightCycles.Conditions
{
    /// <summary>
    /// The Condition interface.
    /// </summary>
    public interface IEndCondition : ICondition
    {
        /// <summary>
        /// Gets the game result when condition is met.
        /// </summary>
        GameResult Result { get; }
    }
}