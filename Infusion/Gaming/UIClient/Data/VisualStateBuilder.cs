using Infusion.Gaming.LightCyclesCommon.Networking.Game.Response;

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
        /// Initializes a new instance of the <see cref="VisualStateBuilder" /> class.
        /// </summary>
        /// <param name="endPointTag">UI end point name to show</param>
        /// <param name="windowRect">view window rectangle to occupy</param>
        public VisualStateBuilder(string endPointTag, RectangleF windowRect)
        {
            this.EndPointTag = endPointTag;
            this.WindowRect = windowRect;
        }

        /// <summary>
        /// Gets or sets end point tag
        /// </summary>
        public string EndPointTag { get; protected set; }

        /// <summary>
        /// Gets or sets window rectangle
        /// </summary>
        public RectangleF WindowRect { get; protected set; }

        /// <summary>
        /// Create visual state from game state on game turn
        /// </summary>
        /// <param name="details">game details</param>
        /// <param name="status">game status</param>
        /// <param name="gameData">game data</param>
        /// <returns>visual state of the game</returns>
        public VisualState BuildTurnVisualState(GameDetails details, GameStatus status, GameData gameData)
        {
            var result = this.InitilizeVisualState(details.MapWidth, details.MapHeight);
            char[,] data = this.ConvertToCharMap(this.CleanUpGameState(gameData.Data), details.MapWidth, details.MapHeight);
            result = this.AddMapData(data, details.MapWidth, details.MapHeight, result);
            result = this.AddPlayerData(data, details.MapWidth, details.MapHeight, result);
            result.UserInterfaceLayer.Add(new VisualText("Listening on: " + this.EndPointTag + "      Turn: " + status.TurnNumber));
            return result;
        }

        /// <summary>
        /// Create visual state for game end
        /// </summary>
        /// <param name="details">game details</param>
        /// <param name="status">game status</param>
        /// <param name="endResult">game end result</param>
        /// <returns>visual state of the game</returns>
        public VisualState BuildFinishedVisualState(GameDetails details, GameStatus status, GameEndResult endResult)
        {
            var result = this.InitilizeVisualState(details.MapWidth, details.MapHeight);            
            string message = string.Empty;
            if (endResult.GameResult == GameResult.FinshedWithWinners)
            {
                message = "Team [" + endResult.WinningTeam + "] has won the game!";
            }
            else if (endResult.GameResult == GameResult.FinshedWithWinner)
            {
                message = "Player [" + endResult.Winner + "] has won the game!";
            }
            else if (endResult.GameResult == GameResult.FinishedWithoutWinner)
            {
                message = "No winner!";
            }
            else if (endResult.GameResult == GameResult.Terminated)
            {
                message = "Game terminated!";
            }

            result.UserInterfaceLayer.Add(new Mask(new Color4(0, 0, 0), 0.3f, this.WindowRect));
            result.UserInterfaceLayer.Add(new VisualText("Listening on: " + this.EndPointTag + "      Turn: " + status.TurnNumber));
            result.UserInterfaceLayer.Add(VisualText.CreateSuperHeadingText(message, new PointF((this.WindowRect.Width / 2) - 400, (this.WindowRect.Height / 2) - 50)));
            return result;
        }

        /// <summary>
        /// Create visual state for game start
        /// </summary>
        /// <param name="details">game details</param>
        /// <param name="status">game status</param>
        /// <returns>visual state of the game</returns>
        public VisualState BuildStartVisualState(GameDetails details, GameStatus status)
        {
            this.trailAges = new int[details.MapWidth, details.MapHeight];
            var result = this.InitilizeVisualState(details.MapWidth, details.MapHeight);
            string message = "Waiting for game to start";
            result.UserInterfaceLayer.Add(new VisualText("Listening on: " + this.EndPointTag));
            result.UserInterfaceLayer.Add(VisualText.CreateSuperHeadingText(message, new PointF((this.WindowRect.Width / 2) - 200, (this.WindowRect.Height / 2) - 50)));
            return result;
        }
        
        /// <summary>
        /// Create visual state from game state 
        /// </summary>
        /// <param name="mapWidth">game map width</param>
        /// <param name="mapHeight">game map height</param>
        /// <returns>visual state of the game</returns>
        protected VisualState InitilizeVisualState(int mapWidth, int mapHeight)
        {
            VisualState result = new VisualState();
            result.BackgroundLayer = new VisualsCollection();
            result.ObstaclesLayer = new VisualsSurface(mapWidth, mapHeight);
            result.GridLayer = new VisualsSurface(mapWidth, mapHeight);
            result.TrailsLayer = new VisualsSurface(mapWidth, mapHeight);
            result.PlayersLayer = new VisualsSurface(mapWidth, mapHeight);
            result.UserInterfaceLayer = new VisualsCollection();

            // adjust size of the grid accordingly to size of the map
            float widthRatio = this.WindowRect.Width / result.GridLayer.Width;
            float heightRatio = this.WindowRect.Height / result.GridLayer.Height;
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
        protected IEnumerable<string> CleanUpGameState(string gameState)
        {
            List<string> result = new List<string>();
            foreach (string s in gameState.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
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
        /// <param name="mapWidth">game map width</param>
        /// <param name="mapHeight">game map height</param>
        /// <returns>character map</returns>
        protected char[,] ConvertToCharMap(IEnumerable<string> gameState, int mapWidth, int mapHeight)
        {
            List<string> data = new List<string>(gameState);
            char[,] charMap = new char[mapWidth, mapHeight];
            for (int y = 0; y < mapWidth; y++)
            {
                for (int x = 0; x < mapHeight; x++)
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
        /// <param name="mapWidth">game map width</param>
        /// <param name="mapHeight">game map height</param>
        /// <param name="visualState">visualState to fill in</param>
        /// <returns>filled in visual state</returns>
        protected VisualState AddMapData(char[,] data, int mapWidth, int mapHeight, VisualState visualState)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
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
        /// <param name="mapWidth">game map width</param>
        /// <param name="mapHeight">game map height</param>
        /// <param name="visualState">visualState to fill in</param>
        /// <returns>filled in visual state</returns>
        protected VisualState AddPlayerData(char[,] data, int mapWidth, int mapHeight, VisualState visualState)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
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
