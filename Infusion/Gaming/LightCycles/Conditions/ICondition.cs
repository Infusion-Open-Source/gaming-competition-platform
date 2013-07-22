using Infusion.Gaming.LightCycles.Model.State;

namespace Infusion.Gaming.LightCycles.Conditions
{
    using Infusion.Gaming.LightCycles.Model;

    /// <summary>
    /// The Condition interface.
    /// </summary>
    public interface ICondition
    {
        /// <summary>
        /// Performs condition check.
        /// </summary>
        /// <param name="game">
        /// The game on which condition check should be performed.
        /// </param>
        /// <returns>
        /// The result of the condition check.
        /// </returns>
        bool Check(IGame game);
    }
}