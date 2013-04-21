namespace Infusion.Gaming.LightCycles
{
    using System;
    using System.Collections.Generic;
    using Infusion.Gaming.LightCycles.Bots;
    using Infusion.Gaming.LightCycles.Events;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Model.Defines;
    using Infusion.Gaming.LightCycles.Model.Serialization;

    /// <summary>
    /// Runs game in a loop
    /// </summary>
    public class GameRunner
    {
        /// <summary>
        /// List of AI players
        /// </summary>
        private readonly List<IBot> bots = new List<IBot>();

        private IList<IGameStateSink> sinks = new List<IGameStateSink>();

        /// <summary>
        /// Gets or sets game main object
        /// </summary>
        public IGame Game { get; protected set; }

        public void RegisterStateSink(IGameStateSink sink)
        {
            this.sinks.Add(sink);
        }

        /// <summary>
        /// Start a game
        /// </summary>
        /// <param name="gameInfo">game start info</param>
        public void StartGame(GameInfo gameInfo)
        {
            var botsFactory = new BotFactory();
            this.Game = new LightCyclesGame();
            ((LightCyclesGame)this.Game).Start(gameInfo);
            foreach (var player in this.Game.CurrentState.PlayersData.Players)
            {
                this.bots.Add(botsFactory.CreateBot(player));
            }

            FlushState();
        }
        
        private void FlushState()
        {
            foreach (var sink in this.sinks)
            {
                sink.Flush(this.Game.CurrentState);
            }
        }

        /// <summary>
        /// Run a game step
        /// </summary>
        /// <returns>Is game still running</returns>
        public bool RunGame()
        {
            if (this.Game.State == GameStateEnum.Running)
            {
                var events = new List<Event>();
                foreach (var bot in this.bots)
                {
                    events.AddRange(bot.GetPlayerEvents(this.Game.CurrentState));
                }

                this.Game.Step(events);
                FlushState();
            }

            return this.Game.State == GameStateEnum.Running;
        }

        /// <summary>
        /// End the game
        /// </summary>
        public void EndGame()
        {
            this.ShowGameResultMessage();
        }
        

        /// <summary>
        /// Shows game result message on a console
        /// </summary>
        public void ShowGameResultMessage()
        {
            switch (this.Game.Result)
            {
                case GameResultEnum.FinshedWithWinners:
                    Console.WriteLine("End, winning team is: " + this.Game.CurrentState.PlayersData.Players[0].Team.Id);
                    break;
                case GameResultEnum.FinshedWithWinner:
                    Console.WriteLine("End, winner is: " + this.Game.CurrentState.PlayersData.Players[0]);
                    break;
                case GameResultEnum.FinishedWithoutWinner:
                    Console.WriteLine("End, no winner");
                    break;
                case GameResultEnum.Terminated:
                    Console.WriteLine("Terminated");
                    break;
            }
        }
    }
}
