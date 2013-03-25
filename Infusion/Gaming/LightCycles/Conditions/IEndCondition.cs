namespace Infusion.Gaming.LightCycles.Conditions
{
    using Infusion.Gaming.LightCycles.Model.Defines;

    /// <summary>
    /// The Condition interface.
    /// </summary>
    public interface IEndCondition : ICondition
    {
        /// <summary>
        /// Gets the game result when condition is met.
        /// </summary>
        GameResultEnum Result { get; }
    }
}