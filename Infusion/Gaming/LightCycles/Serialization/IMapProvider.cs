using Infusion.Gaming.LightCycles.Definitions;
using Infusion.Gaming.LightCycles.Model.Data;

namespace Infusion.Gaming.LightCycles.Serialization
{
    /// <summary>
    /// interface for game map provider
    /// </summary>
    public interface IMapProvider
    {
        /// <summary>
        /// Provides a map 
        /// </summary>
        /// <returns>map to be used</returns>
        Map Provide();
    }
}
