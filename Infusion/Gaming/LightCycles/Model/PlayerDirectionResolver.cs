namespace Infusion.Gaming.LightCycles.Model
{
    using System;
    using System.Collections.Generic;

    using Infusion.Gaming.LightCycles.Model.Data;

    /// <summary>
    /// The player direction resolver.
    /// </summary>
    public class PlayerDirectionResolver
    {
        #region Public Methods and Operators

        /// <summary>
        /// Resolves player direction analyzing map in current and previous state.
        /// </summary>
        /// <param name="map">
        /// The current map.
        /// </param>
        /// <param name="prevMap">
        /// The previous map.
        /// </param>
        /// <returns>
        /// The dictionary with players directions.
        /// </returns>
        public Dictionary<Player, DirectionEnum> Resolve(IMap map, IMap prevMap)
        {
            if (map == null)
            {
                throw new ArgumentNullException("map");
            }

            if (prevMap == null)
            {
                throw new ArgumentNullException("prevMap");
            }

            var directions = new Dictionary<Player, DirectionEnum>();
            foreach (Player player in map.Players)
            {
                if (!prevMap.PlayerLocations.ContainsKey(player))
                {
                    throw new ArgumentException("prevMap is inavlid, unable to find current player in T-1 map");
                }

                directions.Add(
                    player, DirectionHelper.CheckDirection(map.PlayerLocations[player], prevMap.PlayerLocations[player]));
            }

            return directions;
        }

        #endregion
    }
}