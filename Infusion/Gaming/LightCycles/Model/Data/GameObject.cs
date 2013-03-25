namespace Infusion.Gaming.LightCycles.Model.Data
{
    /// <summary>
    /// The game object on player data map.
    /// </summary>
    public abstract class GameObject
    {
        /// <summary>
        /// Clones game object
        /// </summary>
        /// <returns>Cloned game object</returns>
        public abstract GameObject Clone();
    }
}