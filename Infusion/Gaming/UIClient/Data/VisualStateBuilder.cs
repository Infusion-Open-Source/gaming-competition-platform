namespace Infusion.Gaming.LightCycles.UIClient.Data
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;
    using Infusion.Gaming.LightCycles.UIClient.Data.Visuals;
    using Infusion.Gaming.LightCyclesCommon.Definitions;
    using Infusion.Gaming.LightCyclesCommon.Extensions;
    using SlimDX;

    /// <summary>
    /// Creates game visual state
    /// </summary>
    public class VisualStateBuilder
    {
        /// <summary>
        /// Keeps age of light trails
        /// </summary>
        private int[,] trailAges;

        /// <summary>
        /// Gets or sets Game mode
        /// </summary>
        public string GameMode { get; set; }

        /// <summary>
        /// Gets or sets Map width
        /// </summary>
        public int MapWidth { get; set; }

        /// <summary>
        /// Gets or sets Map height
        /// </summary>
        public int MapHeight { get; set; }

        /// <summary>
        /// Gets or sets Number of players
        /// </summary>
        public int NumberOfPlayers { get; set; }

        /// <summary>
        /// Gets or sets Number of teams
        /// </summary>
        public int NumberOfTeams { get; set; }

        /// <summary>
        /// Gets or sets Game turn
        /// </summary>
        public int Turn { get; set; }

        /// <summary>
        /// Gets or sets text describing endpoint
        /// </summary>
        public string EndPoint { get; set; }

        /// <summary>
        /// Create visual state from game state on game turn
        /// </summary>
        /// <param name="gameState">game state</param>
        /// <param name="windowRect">game window dimensions</param>
        /// <returns>visual state of the game</returns>
        public VisualState GameTurn(List<string> gameState, RectangleF windowRect)
        {
            var result = this.InitilizeVisualState(windowRect);
            char[,] data = this.ConvertToCharMap(this.CleanUpGameState(gameState));
            result = this.AddMapData(data, result);
            result = this.AddPlayerData(data, result);
            result.UserInterfaceLayer.Add(new VisualText("Listening on: " + this.EndPoint + "      Turn: " + this.Turn));
            return result;
        }

        /// <summary>
        /// Create visual state for game end
        /// </summary>
        /// <param name="gameState">game state</param>
        /// <param name="gameResult">game result</param>
        /// <param name="winnerName">name of game winner</param>
        /// <param name="windowRect">game window dimensions</param>
        /// <returns>visual state of the game</returns>
        public VisualState GameEnds(List<string> gameState, string gameResult, string winnerName, RectangleF windowRect)
        {
            var result = this.InitilizeVisualState(windowRect);
            char[,] data = this.ConvertToCharMap(this.CleanUpGameState(gameState));
            result = this.AddMapData(data, result);
            result = this.AddPlayerData(data, result);

            string message = string.Empty;
            if (gameResult == "team")
            {
                message = "Team [" + winnerName + "] has won the game!";
            }
            else if (gameResult == "player")
            {
                message = "Player [" + winnerName + "] has won the game!";
            }
            else if (gameResult == "no winner")
            {
                message = "No winner!";
            }
            else if (gameResult == "terminated")
            {
                message = "Game terminated!";
            }

            result.UserInterfaceLayer.Add(new Mask(new Color4(0, 0, 0), 0.3f, windowRect));
            result.UserInterfaceLayer.Add(new VisualText("Listening on: " + this.EndPoint + "      Turn: " + this.Turn));
            result.UserInterfaceLayer.Add(VisualText.CreateSuperHeadingText(message, new PointF((windowRect.Width / 2) - 400, (windowRect.Height / 2) - 50)));
            return result;
        }

        /// <summary>
        /// Create visual state for game start
        /// </summary>
        /// <param name="startsInCounter">number of seconds to game start</param>
        /// <param name="windowRect">game window dimensions</param>
        /// <returns>visual state of the game</returns>
        public VisualState GameStarts(int startsInCounter, RectangleF windowRect)
        {
            this.trailAges = new int[this.MapWidth, this.MapHeight];
            var result = this.InitilizeVisualState(windowRect);

            string message = string.Empty;
            if (startsInCounter > 0)
            {
                message = "Game starts in " + startsInCounter + "...";
            }
            else
            {
                message = "GO!";
            }

            result.UserInterfaceLayer.Add(new VisualText("Listening on: " + this.EndPoint + "      Turn: " + this.Turn));
            result.UserInterfaceLayer.Add(VisualText.CreateSuperHeadingText(message, new PointF((windowRect.Width / 2) - 200, (windowRect.Height / 2) - 50)));
            return result;
        }

        /// <summary>
        /// Create visual state for player gathering phase
        /// </summary>
        /// <param name="ocupiedSlots">slots already occupied</param>
        /// <param name="totalSlots">total number of game slots</param>
        /// <param name="windowRect">game window dimensions</param>
        /// <returns>visual state of the game</returns>
        public VisualState GatheringPlayers(int ocupiedSlots, int totalSlots, RectangleF windowRect)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Game mode: " + this.GameMode);
            builder.AppendLine("Number of players: " + this.NumberOfPlayers);
            builder.AppendLine("Number of teams: " + this.NumberOfTeams);
            builder.AppendLine("Map width: " + this.MapWidth);
            builder.AppendLine("Map height: " + this.MapHeight);
            builder.AppendLine("Players connected: " + ocupiedSlots);

            var result = this.InitilizeVisualState(windowRect);
            result.UserInterfaceLayer.Add(new VisualText("Listening on: " + this.EndPoint + "      Turn: " + this.Turn));
            result.UserInterfaceLayer.Add(VisualText.CreateHeadingText("Waiting for players", new PointF((windowRect.Width / 2) - 150, 200)));
            result.UserInterfaceLayer.Add(VisualText.CreateRegularText(builder.ToString(), new PointF((windowRect.Width / 2) - 120, 300)));
            
            return result;
        }

        /// <summary>
        /// Create visual state from game state 
        /// </summary>
        /// <param name="windowRect">game window dimensions</param>
        /// <returns>visual state of the game</returns>
        protected VisualState InitilizeVisualState(RectangleF windowRect)
        {
            VisualState result = new VisualState();
            result.BackgroundLayer = new VisualsCollection();
            result.ObstaclesLayer = new VisualsSurface(this.MapWidth, this.MapHeight);
            result.GridLayer = new VisualsSurface(this.MapWidth, this.MapHeight);
            result.TrailsLayer = new VisualsSurface(this.MapWidth, this.MapHeight);
            result.PlayersLayer = new VisualsSurface(this.MapWidth, this.MapHeight);
            result.UserInterfaceLayer = new VisualsCollection();

            // adjust size of the grid accordingly to size of the map
            float widthRatio = windowRect.Width / result.GridLayer.Width;
            float heightRatio = windowRect.Height / result.GridLayer.Height;
            result.GridSize = (int)(0.99f * Math.Min(widthRatio, heightRatio));
            if (result.GridSize > 30)
            {
                result.GridSize = 30;
            }

            return result;
        }

        /// <summary>
        /// Cleans up game state
        /// </summary>
        /// <param name="gameState">game state before clean up</param>
        /// <returns>game state after clean up</returns>
        protected IEnumerable<string> CleanUpGameState(IEnumerable<string> gameState)
        {
            List<string> result = new List<string>();
            foreach (string s in gameState)
            {
                if (!string.IsNullOrWhiteSpace(s))
                {
                    result.Add(s.Trim());
                }
            }

            return result;
        }

        /// <summary>
        /// Convert to char map
        /// </summary>
        /// <param name="gameState">game state</param>
        /// <returns>character map</returns>
        protected char[,] ConvertToCharMap(IEnumerable<string> gameState)
        {
            List<string> data = new List<string>(gameState);
            char[,] charMap = new char[this.MapWidth, this.MapHeight];
            for (int y = 0; y < this.MapHeight; y++)
            {
                for (int x = 0; x < this.MapWidth; x++)
                {
                    if (data[y].Length > x)
                    {
                        charMap[x, y] = data[y][x];
                    }
                    else
                    {
                        charMap[x, y] = Constraints.MapObstacleCharacter;
                    }
                }
            }

            return charMap;
        }

        /// <summary>
        /// Fill in visual state with map data
        /// </summary>
        /// <param name="data">map data to parse</param>
        /// <param name="visualState">visualState to fill in</param>
        /// <returns>filled in visual state</returns>
        protected VisualState AddMapData(char[,] data, VisualState visualState)
        {
            for (int x = 0; x < this.MapWidth; x++)
            {
                for (int y = 0; y < this.MapHeight; y++)
                {
                    var location = data[x, y];
                    if (location == Constraints.MapObstacleCharacter)
                    {
                        visualState.ObstaclesLayer[x, y] = new Visuals.Obstacle();
                    }
                    else 
                    {
                        visualState.GridLayer[x, y] = new Visuals.Grid();
                    }
                }
            }

            return visualState;
        }

        /// <summary>
        /// Fill in visual state with map data
        /// </summary>
        /// <param name="data">players data to parse</param>
        /// <param name="visualState">visualState to fill in</param>
        /// <returns>filled in visual state</returns>
        protected VisualState AddPlayerData(char[,] data, VisualState visualState)
        {
            for (int x = 0; x < this.MapWidth; x++)
            {
                for (int y = 0; y < this.MapHeight; y++)
                {
                    var location = data[x, y];
                    if (location >= Constraints.MinPlayerTrailId && location <= Constraints.MaxPlayerTrailId)
                    {
                        this.trailAges[x, y]++;
                        visualState.TrailsLayer[x, y] = new Visuals.Trail(location.ToUpper(), location.ToUpper(), this.trailAges[x, y]);
                    }
                    else if (location >= Constraints.MinPlayerId && location <= Constraints.MaxPlayerId)
                    {
                        this.trailAges[x, y] = 1;
                        visualState.PlayersLayer[x, y] = new Visuals.Bike(location, location);
                    }
                    else
                    {
                        this.trailAges[x, y] = 0;
                    }
                }
            }
            
            return visualState;
        }
    }
}
