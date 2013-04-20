namespace Infusion.Gaming.LightCycles.Events
{
    using Infusion.Gaming.LightCycles.Model.Data;
    using Infusion.Gaming.LightCycles.Model.Defines;

    /// <summary>
    /// Move result class, aggregates possible results of player move action
    /// </summary>
    public class PlayerMoveResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerMoveResult"/> class.
        /// </summary>
        /// <param name="result">result of player move</param>
        public PlayerMoveResult(MoveResultEnum result)
            : this(result, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerMoveResult"/> class.
        /// </summary>
        /// <param name="result">result of player move</param>
        /// <param name="owningPlayer">owner of collided object</param>
        public PlayerMoveResult(MoveResultEnum result, Player owningPlayer)
        {
            this.Result = result;
            this.OwningPlayer = owningPlayer;
        }

        /// <summary>
        /// Gets or sets move result
        /// </summary>
        public MoveResultEnum Result { get; protected set; }
        
        /// <summary>
        /// Gets or sets a player owning collided object
        /// </summary>
        public Player OwningPlayer { get; protected set; }
    }
}
