namespace Infusion.Gaming.LightCycles.Model
{
    using System.Collections.Generic;

    using Infusion.Gaming.LightCycles.Conditions;

    /// <summary>
    /// The settings.
    /// </summary>
    public class Settings
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        /// <param name="mode">
        /// The game mode.
        /// </param>
        /// <param name="endConditions">
        /// Set of the end conditions.
        /// </param>
        public Settings(GameModeEnum mode, IEnumerable<EndCondition> endConditions)
        {
            this.Mode = mode;
            this.EndConditions = new List<EndCondition>(endConditions);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the end conditions.
        /// </summary>
        public List<EndCondition> EndConditions { get; protected set; }

        /// <summary>
        /// Gets or sets the game mode.
        /// </summary>
        public GameModeEnum Mode { get; protected set; }

        #endregion
    }
}