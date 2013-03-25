namespace Infusion.Gaming.LightCycles.Conditions
{
    using Infusion.Gaming.LightCycles.Model.Defines;

    /// <summary>
    ///     The Condition interface.
    /// </summary>
    public interface IEndCondition : ICondition
    {
        #region Public Methods and Operators
        
        /// <summary>
        ///     Gets the game result when condition is met.
        /// </summary>
        GameResultEnum Result { get; }

        #endregion
    }
}