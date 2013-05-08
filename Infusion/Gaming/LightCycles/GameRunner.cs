namespace Infusion.Gaming.LightCycles
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading;
    using Infusion.Gaming.LightCycles.Bots;
    using Infusion.Gaming.LightCycles.Events;
    using Infusion.Gaming.LightCycles.Messaging;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="GameRunner" /> class.
        /// </summary>
        /// <param name="messageSink">message sink</param>
        public GameRunner(IMessageSink messageSink)
        {
            if (messageSink == null)
            {
                throw new ArgumentNullException("messageSink");
            }

            this.MessageSink = messageSink;
            this.GatheringTimeout = 3;
            this.TurnTime = 100;
            this.DelayOnStart = 3;
            this.DelayOnEnd = 3;
        }
        
        /// <summary>
        /// Gets or sets message sink
        /// </summary>
        public IMessageSink MessageSink { get; protected set; }

        /// <summary>
        /// Gets or sets game main object
        /// </summary>
        public IGame Game { get; protected set; }

        /// <summary>
        /// Gets or sets player gathering timeout (in seconds)
        /// </summary>
        public int GatheringTimeout { get; set; }

        /// <summary>
        /// Gets or sets game turn delay time (in milliseconds)
        /// </summary>
        public int TurnTime { get; set; }

        /// <summary>
        /// Gets or sets game delay on start (in seconds)
        /// </summary>
        public int DelayOnStart { get; set; }

        /// <summary>
        /// Gets or sets game delay on end (in seconds)
        /// </summary>
        public int DelayOnEnd { get; set; }

        /// <summary>
        /// Start a game
        /// </summary>
        /// <param name="gameInfo">game start info</param>
        public void InitilizeGame(GameInfo gameInfo)
        {
            var game = new LightCyclesGame();
            game.Start(gameInfo);
            this.Game = game;
            
            this.MessageSink.Flush("[Initialize][Mode]" + this.Game.Mode.ToString());
            this.MessageSink.Flush("[Initialize][MapWidth]" + this.Game.CurrentState.Map.Width);
            this.MessageSink.Flush("[Initialize][MapHeight]" + this.Game.CurrentState.Map.Height);
            this.MessageSink.Flush("[Initialize][NumberOfPlayers]" + this.Game.CurrentState.PlayersData.Players.Count);
            this.MessageSink.Flush("[Initialize][NumberOfTeams]" + this.Game.CurrentState.PlayersData.Teams.Count);
        }

        /// <summary>
        /// Gather players for the game
        /// </summary>
        public void GatherPlayers()
        {
            DateTime gatheringStart = DateTime.Now;
            while (DateTime.Now < gatheringStart.AddSeconds(this.GatheringTimeout))
            {
                // TODO: resolve server players list, for now only bots are playing, introduce some interface providing players
                int ocupiedSlots = 0;
                int totalSlots = this.Game.CurrentState.PlayersData.Players.Count;
                this.MessageSink.Flush("[Waiting for players][Slots]" + ocupiedSlots + "/" + totalSlots);
                Thread.Sleep(500);
            }

            var botsFactory = new BotFactory();
            foreach (var player in this.Game.CurrentState.PlayersData.Players)
            {
                this.MessageSink.Flush("[Waiting for players][AddPlayer][Bot]" + player.Id + "," + player.Team.Id);
                this.bots.Add(botsFactory.CreateBot(player));
            }
        }

        /// <summary>
        /// Start the game
        /// </summary>
        public void StartGame()
        {
            for (int i = this.DelayOnStart; i > 0; i--)
            {
                this.MessageSink.Flush("[GameStart][In]" + i);
                Thread.Sleep(1000);
            }

            this.MessageSink.Flush("[GameStart][Now]");
            this.FlushState();
            Thread.Sleep(this.TurnTime);
        }
        
        /// <summary>
        /// Run a game
        /// </summary>
        /// <returns>returns a value indicating whether game is still running</returns>
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
                this.FlushState();
                Thread.Sleep(this.TurnTime);
            }

            return this.Game.State == GameStateEnum.Running;
        }

        /// <summary>
        /// End the game
        /// </summary>
        public void EndGame()
        {
            switch (this.Game.Result)
            {
                case GameResultEnum.FinshedWithWinners:
                    this.MessageSink.Flush("[GameEnd][Team wins]" + this.Game.CurrentState.PlayersData.Players[0].Team.Id);
                    break;
                case GameResultEnum.FinshedWithWinner:
                    this.MessageSink.Flush("[GameEnd][Player wins]" + this.Game.CurrentState.PlayersData.Players[0]);
                    break;
                case GameResultEnum.FinishedWithoutWinner:
                    this.MessageSink.Flush("[GameEnd][No winner]");
                    break;
                case GameResultEnum.Terminated:
                    this.MessageSink.Flush("[GameEnd][Terminated]");
                    break;
            }

            for (int i = this.DelayOnEnd; i > 0; i--)
            {
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Flushes game state
        /// </summary>
        private void FlushState()
        {
            StringBuilder builder = new StringBuilder();
            using (TextWriter writer = new StringWriter(builder))
            {
                new TextGameStateWriter(writer).Write(this.Game.CurrentState);
                writer.Close();
            }

            this.MessageSink.Flush("[TurnStart]" + this.Game.CurrentState.Turn);
            this.MessageSink.Flush(builder.ToString());
            this.MessageSink.Flush("[TurnEnd]" + this.Game.CurrentState.Turn);
        }
    }
}
