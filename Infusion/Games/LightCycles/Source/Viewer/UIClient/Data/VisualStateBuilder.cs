namespace Infusion.Gaming.LightCycles.UIClient.Data
{
    using System;
    using System.Drawing;
    using Infusion.Gaming.LightCycles.UIClient.Data.Visuals;
    using Infusion.Gaming.LightCycles.UIClient.Extensions;
    using SlimDX;

    /// <summary>
    /// Creates game visual state
    /// </summary>
    public class VisualStateBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VisualStateBuilder" /> class.
        /// </summary>
        /// <param name="gameDetails">game details</param>
        /// <param name="windowRect">view window rectangle to occupy</param>
        public VisualStateBuilder(GameDetails gameDetails, RectangleF windowRect)
        {
            this.GameDetails = gameDetails;
            this.WindowRect = windowRect;
        }

        /// <summary>
        /// Gets or sets end point tag
        /// </summary>
        public GameDetails GameDetails { get; protected set; }

        /// <summary>
        /// Gets or sets window rectangle
        /// </summary>
        public RectangleF WindowRect { get; protected set; }

        /// <summary>
        /// Create visual state from game state on game turn
        /// </summary>
        /// <param name="turn">turn index</param>
        /// <param name="waitingForButton">is waiting for continue button press</param>
        /// <returns>visual state of the game</returns>
        public VisualState BuildTurnVisualState(int turn, bool waitingForButton)
        {
            var result = this.InitilizeVisualState(this.GameDetails.MapWidth, this.GameDetails.MapHeight);
            var turnDetails = this.GameDetails.Turns[turn];
            result = this.AddMapData(turnDetails, result);
            result = this.AddPlayerData(turnDetails, result);
            if (waitingForButton)
            {
                result.UserInterfaceLayer.Add(new VisualText("Turn: " + turnDetails.TurnNumber + "\nPress [SPACE] to continue..."));
            }
            else
            {
                result.UserInterfaceLayer.Add(new VisualText("Turn: " + turnDetails.TurnNumber));
            }

            return result;
        }

        /// <summary>
        /// Create visual state for game end
        /// </summary>
        /// <param name="waitingForButton">is waiting for continue button press</param>
        /// <returns>visual state of the game</returns>
        public VisualState BuildFinishedVisualState(bool waitingForButton)
        {
            var result = this.InitilizeVisualState(this.GameDetails.MapWidth, this.GameDetails.MapHeight);
            var turnDetails = this.GameDetails.Turns[this.GameDetails.Turns.Count - 1];
            result = this.AddMapData(turnDetails, result);
            result = this.AddPlayerData(turnDetails, result);

            result.UserInterfaceLayer.Add(new Mask(new Color4(0, 0, 0), 0.5f, this.WindowRect));
            if (this.GameDetails.GameResult.StartsWith("WinningTeam: "))
            {
                char winningTeamChar = this.GameDetails.GameResult.Replace("WinningTeam: ", string.Empty)[0];
                var winningTeam = this.GameDetails.FindTeam(winningTeamChar);
                result.UserInterfaceLayer.Add(VisualText.CreateEnormousHeadingText(winningTeam.Name, winningTeam.Color, new PointF((this.WindowRect.Width / 2) - 250, (this.WindowRect.Height / 2) - 200)));
                result.UserInterfaceLayer.Add(VisualText.CreateSuperHeadingText("Has won the game!", new PointF((this.WindowRect.Width / 2) - 250, (this.WindowRect.Height / 2) - 100)));
            }
            else if (this.GameDetails.GameResult.StartsWith("Winner: "))
            {
                char winnwerChar = this.GameDetails.GameResult.Replace("Winner: ", string.Empty)[0];
                var winner = this.GameDetails.FindPlayer(winnwerChar);
                int score = this.GameDetails.FindPlayersScore(winnwerChar);
                result.UserInterfaceLayer.Add(VisualText.CreateEnormousHeadingText(winner.Name, winner.Color, new PointF((this.WindowRect.Width / 2) - 250, (this.WindowRect.Height / 2) - 200)));
                result.UserInterfaceLayer.Add(VisualText.CreateSuperHeadingText("Has won the game!", new PointF((this.WindowRect.Width / 2) - 250, (this.WindowRect.Height / 2) - 100)));
                result.UserInterfaceLayer.Add(VisualText.CreateSuperHeadingText("Score: " + score, new PointF((this.WindowRect.Width / 2) - 250, (this.WindowRect.Height / 2) - 0)));
            }
            else
            {
                result.UserInterfaceLayer.Add(VisualText.CreateEnormousHeadingText(this.GameDetails.GameResult, new PointF((this.WindowRect.Width / 2) - 100, (this.WindowRect.Height / 2) - 300)));
            }

            if (waitingForButton)
            {
                result.UserInterfaceLayer.Add(new VisualText("Press [SPACE] to continue..."));
            }

            return result;
        }

        /// <summary>
        /// Create visual state for game start
        /// </summary>
        /// <param name="waitingForButton">is waiting for continue button press</param>
        /// <returns>visual state of the game</returns>
        public VisualState BuildStartVisualState(bool waitingForButton)
        {
            var result = this.InitilizeVisualState(this.GameDetails.MapWidth, this.GameDetails.MapHeight);
            var turnDetails = this.GameDetails.Turns[0];
            result = this.AddMapData(turnDetails, result);
            result = this.AddPlayerData(turnDetails, result);

            result.UserInterfaceLayer.Add(new Mask(new Color4(0, 0, 0), 0.5f, this.WindowRect));
            if (this.GameDetails.GameMode == "FreeForAll")
            {
                result.UserInterfaceLayer.Add(VisualText.CreateSuperHeadingText("Free For All", new PointF((this.WindowRect.Width / 2) - 150, (this.WindowRect.Height / 2) - 300)));
                result.UserInterfaceLayer.Add(VisualText.CreateSuperHeadingText("Players:", new PointF((this.WindowRect.Width / 2) - 120, (this.WindowRect.Height / 2) - 200)));
                for (int i = 0; i < this.GameDetails.Players.Count; i++)
                {
                    result.UserInterfaceLayer.Add(VisualText.CreateHeadingText(this.GameDetails.Players[i].Name, this.GameDetails.Players[i].Color, new PointF((this.WindowRect.Width / 2) - 200, (this.WindowRect.Height / 2) - 100 + (35 * i))));
                }
            }
            else if (this.GameDetails.GameMode == "TeamDeathMatch")
            {
                result.UserInterfaceLayer.Add(VisualText.CreateSuperHeadingText("Team Deathmatch", new PointF((this.WindowRect.Width / 2) - 170, (this.WindowRect.Height / 2) - 300)));
                result.UserInterfaceLayer.Add(VisualText.CreateSuperHeadingText("Teams:", new PointF((this.WindowRect.Width / 2) - 120, (this.WindowRect.Height / 2) - 200)));
                for (int i = 0; i < this.GameDetails.Teams.Count; i++)
                {
                    result.UserInterfaceLayer.Add(VisualText.CreateHeadingText(this.GameDetails.Teams[i].Name, this.GameDetails.Teams[i].Color, new PointF((this.WindowRect.Width / 2) - 200, (this.WindowRect.Height / 2) - 100 + (35 * i))));
                }
            }

            if (waitingForButton)
            {
                result.UserInterfaceLayer.Add(new VisualText("Press [SPACE] to continue..."));
            }

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
        /// Fill in visual state with map data
        /// </summary>
        /// <param name="turnDetails">game turn data</param>
        /// <param name="visualState">visualState to fill in</param>
        /// <returns>filled in visual state</returns>
        protected VisualState AddMapData(TurnDetails turnDetails, VisualState visualState)
        {
            for (int x = 0; x < turnDetails.Data.GetLength(1); x++)
            {
                for (int y = 0; y < turnDetails.Data.GetLength(0); y++)
                {
                    var location = turnDetails.Data[y, x];
                    if (location == '#')
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
        /// <param name="turnDetails">game turn data</param>
        /// <param name="visualState">visualState to fill in</param>
        /// <returns>filled in visual state</returns>
        protected VisualState AddPlayerData(TurnDetails turnDetails, VisualState visualState)
        {
            foreach (GameIdentity playerId in this.GameDetails.Players)
            {
                visualState.PlayersLocations.Add(playerId, new PointF(0, 0));
            }

            for (int x = 0; x < turnDetails.Data.GetLength(1); x++)
            {
                for (int y = 0; y < turnDetails.Data.GetLength(0); y++)
                {
                    var location = turnDetails.Data[y, x];
                    if (location >= 'a' && location <= 'z')
                    {
                        GameIdentity playerId = this.GameDetails.FindPlayer(location.ToUpper());
                        GameIdentity teamId = this.GameDetails.FindPlayersTeam(location.ToUpper());
                        if (teamId != null)
                        {
                            playerId = new GameIdentity(playerId.Id, playerId.Name, teamId.Color);
                        }

                        visualState.TrailsLayer[x, y] = new Visuals.Trail(playerId, turnDetails.TrailAges[y, x]);
                    }
                    else if (location >= 'A' && location <= 'Z')
                    {
                        GameIdentity playerId = this.GameDetails.FindPlayer(location.ToUpper());
                        GameIdentity teamId = this.GameDetails.FindPlayersTeam(location.ToUpper());
                        if (teamId != null)
                        {
                            playerId = new GameIdentity(playerId.Id, playerId.Name, teamId.Color);
                        }

                        visualState.PlayersLayer[x, y] = new Visuals.Bike(playerId);
                        visualState.PlayersLocations[playerId] = new PointF(x, y);
                    }
                }
            }

            return visualState;
        }
    }
}
