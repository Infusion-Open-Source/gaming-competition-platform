namespace UIClient.Data
{
    using System;
    using System.Drawing;
    using Infusion.Gaming.LightCycles.Model;
    using UIClient.Data.Visuals;
    using GameData = Infusion.Gaming.LightCycles.Model.Data;
    using MapData = Infusion.Gaming.LightCycles.Model.MapData;

    /// <summary>
    /// Creates game visual state
    /// </summary>
    public class VisualStateBuilder
    {
        /// <summary>
        /// Create visual state from game state
        /// </summary>
        /// <param name="game">game state to consider</param>
        /// <param name="windowRect">game window dimensions</param>
        /// <returns>visual state of the game</returns>
        public VisualState CreateVisualState(IGame game, RectangleF windowRect)
        {
            if (game == null)
            {
                throw new ArgumentNullException("game");
            }

            VisualState result = new VisualState();
            result = this.AddMapData(game.CurrentState.Map, result);
            result = this.AddPlayerData(game.CurrentState.PlayersData, result);

            // adjust size of the grid accordingly to size of the map
            float widthRatio = windowRect.Width / result.GridLayer.Width;
            float heightRatio = windowRect.Height / result.GridLayer.Height;
            result.GridSize = (int)(0.99f * Math.Min(widthRatio, heightRatio));
            if (result.GridSize > 30)
            {
                result.GridSize = 30;
            }
            
            /*
            TODO: add as visuals on user interface layer
            result.Turn = iGame.CurrentState.Turn;
            result.State = iGame.State;
            result.Mode = iGame.Mode;
            result.Result = iGame.Result;
            */ 
            return result;
        }

        /// <summary>
        /// Fill in visual state with map data
        /// </summary>
        /// <param name="map">map data to consider</param>
        /// <param name="visualState">visualState to fill in</param>
        /// <returns>filled in visual state</returns>
        public VisualState AddMapData(MapData.IMap map, VisualState visualState)
        {
            if (map == null)
            {
                throw new ArgumentNullException("map");
            }

            visualState.ObstaclesLayer = new VisualsSurface(map.Width, map.Height);
            visualState.GridLayer = new VisualsSurface(map.Width, map.Height);
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    var location = map[x, y];
                    if (location is MapData.Obstacle)
                    {
                        visualState.ObstaclesLayer[x, y] = new Visuals.Obstacle();
                    }
                    else if (location is MapData.PlayersStartingLocation)
                    {
                        visualState.GridLayer[x, y] = new Visuals.Grid();
                    }
                    else if (location is MapData.Space)
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
        /// <param name="playersData">players data to consider</param>
        /// <param name="visualState">visualState to fill in</param>
        /// <returns>filled in visual state</returns>
        public VisualState AddPlayerData(GameData.IPlayersData playersData, VisualState visualState)
        {
            if (playersData == null)
            {
                throw new ArgumentNullException("playersData");
            }

            visualState.TrailsLayer = new VisualsSurface(playersData.Width, playersData.Height);
            visualState.PlayersLayer = new VisualsSurface(playersData.Width, playersData.Height);
            for (int x = 0; x < playersData.Width; x++)
            {
                for (int y = 0; y < playersData.Height; y++)
                {
                    var data = playersData[x, y];
                    if (data is GameData.Trail)
                    {
                        visualState.TrailsLayer[x, y] = new Visuals.Trail(((GameData.Trail)data).Player.Id, ((GameData.Trail)data).Player.Team.Id, ((GameData.Trail)data).Age);
                    }
                    else if (data is GameData.LightCycleBike)
                    {
                        visualState.PlayersLayer[x, y] = new Visuals.Bike(((GameData.LightCycleBike)data).Player.Id, ((GameData.LightCycleBike)data).Player.Team.Id);
                    }
                }
            }

            /*
            TODO: add as visuals on user interface layer
            visualState.Teams = new List<int>();
            foreach (var team in iPlayersData.Teams)
            {
                visualState.Teams.Add(team.Id);
            }

            visualState.Players = new Dictionary<int, int>();
            foreach (var player in iPlayersData.Players)
            {
                visualState.Players.Add(player.Id, player.Team.Id);
            }*/

            return visualState;
        }
    }
}
