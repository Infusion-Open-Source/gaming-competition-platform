namespace Infusion.Gaming.LightCycles.Conditions
{
    using System;
    using Infusion.Gaming.LightCycles.Definitions;
    
    /// <summary>
    /// The game end condition.
    /// </summary>
    public class EndCondition : IEndCondition
    {
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
        public EndCondition(ICondition condition, GameResult result)
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            this.Condition = condition;
            this.Result = result;
        }

        /// <summary>
        /// Gets or sets the condition.
        /// </summary>
        public ICondition Condition { get; protected set; }

        /// <summary>
        /// Gets or sets the game result when condition is met.
        /// </summary>
        public GameResult Result { get; protected set; }

        /// <summary>
        /// Performs condition check.
        /// </summary>
        /// <param name="game">
        /// The game on which condition check should be performed.
        /// </param>
        /// <returns>
        /// The result of the condition check.
        /// </returns>
        public bool Check(IGame game)
        {
            return this.Condition.Check(game);
        }
    }
}