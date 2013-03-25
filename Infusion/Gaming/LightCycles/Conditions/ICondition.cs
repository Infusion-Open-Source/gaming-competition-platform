namespace Infusion.Gaming.LightCycles.Conditions
{
    using Infusion.Gaming.LightCycles.Model;

    /// <summary>
    ///     The Condition interface.
    /// </summary>
    public interface ICondition
    {
        #region Public Methods and Operators

        /// <summary>
        /// Performs condition check.
        /// </summary>
        /// <param name="gameState">
        /// The game state on which condition check should be performed.
        /// </param>
        /// <returns>
        /// The result of the condition check.
        /// </returns>
        bool Check(IGameState gameState);

        #endregion
    }
}