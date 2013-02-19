namespace Infusion.Gaming.LightCycles.Conditions
{
    /// <summary>
    /// The Condition interface.
    /// </summary>
    public interface ICondition
    {
        #region Public Methods and Operators

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

        #endregion
    }
}