namespace Infusion.Gaming.LightCycles
{
    using System;
    using System.Collections.Generic;
    using Infusion.Gaming.LightCycles.Bots;
    using Infusion.Gaming.LightCycles.Events;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="GameRunner" /> class.
        /// </summary>
        public GameRunner()
        {
            this.ConsoleOutputEnabled = true;
        }

        /// <summary>
        /// Gets or sets game main object
        /// </summary>
        public LightCyclesGame Game { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether console output is enabled
        /// </summary>
        public bool ConsoleOutputEnabled { get; set; }
        
        /// <summary>
        /// Start a game
        /// </summary>
        /// <param name="numberOfPlayers">number of players in the game</param>
        /// <param name="gameMode">game mode to be played</param>
        public void StartGame(int numberOfPlayers, GameModeEnum gameMode)
        {
            var botsFactory = new BotFactory();
            this.Game = new LightCyclesGame();
            this.Game.StartOnRandomMap(numberOfPlayers, gameMode);
            foreach (var player in this.Game.CurrentState.PlayersData.Players)
            {
                this.bots.Add(botsFactory.CreateBot(player));
            }

            this.ShowGameState();
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
                this.ShowGameState();
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
        /// Show game current state on a console
        /// </summary>
        public void ShowGameState()
        {
            new GameStateRenderer().Render(this.Game.CurrentState);
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
