
namespace Infusion.Gaming.LightCycles.Conditions
{
    using System;

    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Defines;

    /// <summary>
    ///     The game end condition.
    /// </summary>
    public class EndCondition : IEndCondition
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EndCondition"/> class.
        /// </summary>
        /// <param name="condition">
        /// The condition carried by end condition check
        /// </param>
        /// <param name="result">
        /// The result of the end condition when condition is met
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when condition argument is null
        /// </exception>
        public EndCondition(ICondition condition, GameResultEnum result)
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            this.Condition = condition;
            this.Result = result;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the condition.
        /// </summary>
        public ICondition Condition { get; protected set; }

        /// <summary>
        ///     Gets or sets the game result when condition is met.
        /// </summary>
        public GameResultEnum Result { get; protected set; }

        #endregion

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
        public bool Check(IGameState gameState)
        {
            return this.Condition.Check(gameState);
        }

        #endregion
    }
}