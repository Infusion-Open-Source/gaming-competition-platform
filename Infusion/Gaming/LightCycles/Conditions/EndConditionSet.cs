namespace Infusion.Gaming.LightCycles.Conditions
{
    using System;
    using System.Collections.Generic;

    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Defines;

    /// <summary>
    ///     Set of the game end conditions.
    /// </summary>
    internal class EndConditionSet : List<IEndCondition>, IEndCondition
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EndConditionSet"/> class.
        /// </summary>
        public EndConditionSet()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EndConditionSet"/> class.
        /// </summary>
        /// <param name="conditions">
        /// The set of game ending conditions
        /// </param>
        public EndConditionSet(IEnumerable<IEndCondition> conditions)
            : base(conditions)
        {
        }

        #endregion

        #region Public Properties
        
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
            foreach (IEndCondition endCondition in this)
            {
                if (endCondition.Check(gameState))
                {
                    this.Result = endCondition.Result;
                    return true;
                }
            }

            this.Result = GameResultEnum.Undefined;
            return false;
        }

        #endregion
    }
}