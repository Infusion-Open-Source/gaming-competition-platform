using System;
using System.Collections.Generic;
using Infusion.Gaming.LightCycles.Definitions;

namespace Infusion.Gaming.LightCyclesNetworking.Bots
{
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
        /// internal list of bots
        /// </summary>
        private readonly List<IBot> bots = new List<IBot>();

        /// <summary>
        /// internal list of weights (sum weights to 1.0 because of lame pick up logic!)
        /// </summary>
        private readonly List<double> weights = new List<double>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BotFactory" /> class.
        /// </summary>
        public BotFactory()
        {
            this.RegisterBot(new LineFollowerBot(), 0.5);
            this.RegisterBot(new RandomPigBot(), 0.3);
            this.RegisterBot(new AlwaysLeftBot(), 0.2);
        }

        /// <summary>
        /// Gets list of registered bots
        /// </summary>
        public List<IBot> Bots
        {
            get
            {
                return new List<IBot>(bots);
            }
        }

        /// <summary>
        /// Gets list of registered bots weights
        /// </summary>
        public List<double> Weights
        {
            get
            {
                return new List<double>(weights);
            }
        }

        /// <summary>
        /// Registers new bot in factory
        /// </summary>
        /// <param name="bot">bot instance</param>
        /// <param name="weight">bot pick up probability weight</param>
        public void RegisterBot(IBot bot, double weight)
        {
            if (bot == null)
            {
                throw new ArgumentNullException("bot");
            }

            if (weight < 0 || weight > 1)
            {
                throw new ArgumentOutOfRangeException("weight");
            }

            this.bots.Add(bot);
            this.weights.Add(weight);
        }
        
        /// <summary>
        /// Get bot with given name
        /// </summary>
        /// <param name="botName">name of a bot</param>
        /// <returns>bot assigned to given name</returns>
        public IBot GetBot(string botName)
        {
            foreach (IBot bot in this.bots)
            {
                if (string.Equals(bot.Name, botName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return bot;
                }
            }

            // if name not found, pick one randomly
            for (int i = 0; i < this.weights.Count - 1; i++)
            {
                if (this.random.NextDouble() <= this.weights[i])
                {
                    return this.bots[i];
                }
            }

            return this.bots[this.bots.Count - 1];
        }
    }
}
