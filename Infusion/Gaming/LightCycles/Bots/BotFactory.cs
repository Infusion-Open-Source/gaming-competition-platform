﻿namespace Infusion.Gaming.LightCycles.Bots
{
    using System;
    using Infusion.Gaming.LightCycles.Model.Data;

    /// <summary>
    /// Bot Factory - produces different types of bots
    /// </summary>
    public class BotFactory
    {
        /// <summary>
        /// internal random number generator
        /// </summary>
        private readonly Random random = new Random();

        /// <summary>
        /// Creates random bot
        /// </summary>
        /// <param name="player">player to be played by bot</param>
        /// <returns>brand new bot</returns>
        public IBot CreateBot(Player player)
        {
            if (this.random.NextDouble() > 0.5)
            {
                return new RandomPigBot(player);
            }
            else
            {
                return new LineFollowerBot(player);
            }
        }

        /// <summary>
        /// Creates bot by name
        /// </summary>
        /// <param name="name">bot name</param>
        /// <param name="player">player to be played by bot</param>
        /// <returns>brand new bot</returns>
        public IBot CreateBot(string name, Player player)
        {
            if (string.Equals(name, "RandomPig", StringComparison.InvariantCultureIgnoreCase))
            {
                return new RandomPigBot(player);
            }

            if (string.Equals(name, "LineFollower", StringComparison.InvariantCultureIgnoreCase))
            {
                return new LineFollowerBot(player);
            }

            return new LineFollowerBot(player);
        }
    }
}