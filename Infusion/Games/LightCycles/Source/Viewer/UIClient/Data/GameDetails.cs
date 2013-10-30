namespace Infusion.Gaming.LightCycles.UIClient.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Game details container
    /// </summary>
    public class GameDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameDetails"/> class.
        /// </summary>
        public GameDetails()
        {
            this.Players = new List<GameIdentity>();
            this.Teams = new List<GameIdentity>();
            this.Turns = new List<TurnDetails>();
            this.Scores = new List<PlayerScore>();
        }

        /// <summary>
        /// Gets or sets Game mode
        /// </summary>
        public string GameMode { get; protected set; }

        /// <summary>
        /// Gets or sets Player to slot assignments
        /// </summary>
        public string PlayersAssignments { get; protected set; }

        /// <summary>
        /// Gets or sets Teams to slot assignments
        /// </summary>
        public string TeamsAssignments { get; protected set; }

        /// <summary>
        /// Gets or sets Source of map
        /// </summary>
        public string MapSource { get; protected set; }

        /// <summary>
        /// Gets or sets Name of map
        /// </summary>
        public string MapName { get; protected set; }

        /// <summary>
        /// Gets or sets Name of map file
        /// </summary>
        public string MapFileName { get; protected set; }

        /// <summary>
        /// Gets or sets Map width
        /// </summary>
        public int MapWidth { get; protected set; }

        /// <summary>
        /// Gets or sets Map height
        /// </summary>
        public int MapHeight { get; protected set; }

        /// <summary>
        /// Gets or sets Trails aging 
        /// </summary>
        public float TrailAging { get; protected set; }

        /// <summary>
        /// Gets or sets Obstacle ratio
        /// </summary>
        public float ObstacleRatio { get; protected set; }

        /// <summary>
        /// Gets or sets Players identity
        /// </summary>
        public List<GameIdentity> Players { get; protected set; }

        /// <summary>
        /// Gets or sets Teams identity
        /// </summary>
        public List<GameIdentity> Teams { get; protected set; }

        /// <summary>
        /// Gets or sets Turns data
        /// </summary>
        public List<TurnDetails> Turns { get; protected set; }

        /// <summary>
        /// Gets or sets Result of the game
        /// </summary>
        public string GameResult { get; protected set; }

        /// <summary>
        /// Gets or sets Players scores
        /// </summary>
        public List<PlayerScore> Scores { get; protected set; }

        /// <summary>
        /// Reads GameDetails from given text reader
        /// </summary>
        /// <param name="reader">text reader to use</param>
        /// <returns>Read in game details</returns>
        public static GameDetails ReadIn(TextReader reader)
        {
            bool hasEnded = false;
            GameDetails gameDetails = new GameDetails();
            while (true)
            {
                string line = reader.ReadLine();
                if (line == null || string.IsNullOrEmpty(line))
                {
                    continue;
                }

                if (line.StartsWith("Game mode: "))
                {
                    gameDetails.GameMode = line.Replace("Game mode: ", string.Empty);
                    continue;
                }

                if (line.StartsWith("Players: "))
                {
                    gameDetails.PlayersAssignments = line.Replace("Players: ", string.Empty);
                    continue;
                }

                if (line.StartsWith("Teams: "))
                {
                    gameDetails.TeamsAssignments = line.Replace("Teams: ", string.Empty);
                    continue;
                }

                if (line.StartsWith("Map source: "))
                {
                    gameDetails.MapSource = line.Replace("Map source: ", string.Empty);
                    continue;
                }

                if (line.StartsWith("Map name: "))
                {
                    gameDetails.MapName = line.Replace("Map name: ", string.Empty);
                    continue;
                }

                if (line.StartsWith("Map file name: "))
                {
                    gameDetails.MapFileName = line.Replace("Map file name: ", string.Empty);
                    continue;
                }

                if (line.StartsWith("Map width: "))
                {
                    gameDetails.MapWidth = int.Parse(line.Replace("Map width: ", string.Empty));
                    continue;
                }

                if (line.StartsWith("Map height: "))
                {
                    gameDetails.MapHeight = int.Parse(line.Replace("Map height: ", string.Empty));
                    continue;
                }

                if (line.StartsWith("Trail aging: "))
                {
                    gameDetails.TrailAging = float.Parse(line.Replace("Trail aging: ", string.Empty));
                    continue;
                }

                if (line.StartsWith("Obstacle ratio: "))
                {
                    gameDetails.ObstacleRatio = float.Parse(line.Replace("Obstacle ratio: ", string.Empty));
                    continue;
                }

                if (line.StartsWith("Players data: "))
                {
                    string numPlayers = line.Replace("Players data: ", string.Empty);
                    for (int i = 0; i < int.Parse(numPlayers); i++)
                    {
                        string playerSetup = reader.ReadLine();
                        playerSetup = playerSetup.Remove(0, 7);
                        string[] param = playerSetup.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        gameDetails.Players.Add(new GameIdentity(param[0], param[1], param[2]));
                    }
                }

                if (line.StartsWith("Teams data: "))
                {
                    string numTeams = line.Replace("Teams data: ", string.Empty);
                    for (int i = 0; i < int.Parse(numTeams); i++)
                    {
                        string teamSetup = reader.ReadLine();
                        teamSetup = teamSetup.Remove(0, 5);
                        string[] param = teamSetup.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        gameDetails.Teams.Add(new GameIdentity(param[0], param[1], param[2]));
                    }
                }

                if (line.StartsWith("Turn: "))
                {
                    string turnNumber = line.Replace("Turn: ", string.Empty);
                    string[] data = new string[0];
                    int numberOfLinesInData = int.Parse(reader.ReadLine());
                    data = new string[numberOfLinesInData];
                    for (int i = 0; i < numberOfLinesInData; i++)
                    {
                        data[i] = reader.ReadLine();
                    }

                    TurnDetails turnDetails = new TurnDetails(turnNumber, data);
                    gameDetails.Turns.Add(turnDetails);

                    if (gameDetails.MapWidth == 0 || gameDetails.MapHeight == 0)
                    {
                        gameDetails.MapWidth = turnDetails.Data.GetLength(1);
                        gameDetails.MapHeight = turnDetails.Data.GetLength(0);
                    }
                }

                if (line.StartsWith("Commands: "))
                {
                    string commandsCount = line.Replace("Commands: ", string.Empty);
                    string[] commands = new string[0];
                    int numberOfLinesInCommands = int.Parse(commandsCount);
                    commands = new string[numberOfLinesInCommands];
                    for (int i = 0; i < numberOfLinesInCommands; i++)
                    {
                        commands[i] = reader.ReadLine();
                    }

                    gameDetails.Turns[gameDetails.Turns.Count - 1].AddCommands(commands);
                }

                if (line.StartsWith("Game ends"))
                {
                    gameDetails.GameResult = reader.ReadLine();
                    line = reader.ReadLine();
                    string scoresCount = line.Replace("Scores data: ", string.Empty);
                    List<PlayerScore> scores = new List<PlayerScore>();
                    int numberOfScores = int.Parse(scoresCount);
                    for (int i = 0; i < numberOfScores; i++)
                    {
                        scores.Add(new PlayerScore(reader.ReadLine().Replace("Score ", string.Empty)));
                    }

                    gameDetails.Scores = scores;
                    hasEnded = true;
                    break;
                }
            }

            reader.Close();
            reader.Dispose();

            // recalc trail ages
            gameDetails.Turns[0].CalcInitialTrailAges();
            for (int i = 1; i < gameDetails.Turns.Count; i++)
            {
                gameDetails.Turns[i].CalcTrailAges(gameDetails.Turns[i - 1]);
            }

            if (!hasEnded)
            {
                throw new ArgumentException("reader");
            }

            return gameDetails;
        }

        /// <summary>
        /// Finds teams identity
        /// </summary>
        /// <param name="teamId">team id</param>
        /// <returns>teams identity</returns>
        public GameIdentity FindTeam(char teamId)
        {
            foreach (GameIdentity identity in this.Teams)
            {
                if (identity.Id == teamId)
                {
                    return identity;
                }
            }

            return null;
        }

        /// <summary>
        /// Finds players identity
        /// </summary>
        /// <param name="playerId">player id</param>
        /// <returns>players identity</returns>
        public GameIdentity FindPlayer(char playerId)
        {
            foreach (GameIdentity identity in this.Players)
            {
                if (identity.Id == playerId)
                {
                    return identity;
                }
            }

            return null;
        }

        /// <summary>
        /// Finds players team
        /// </summary>
        /// <param name="playerId">player id</param>
        /// <returns>players team</returns>
        public GameIdentity FindPlayersTeam(char playerId)
        {
            int slot = this.PlayersAssignments.IndexOf(playerId);
            if (this.TeamsAssignments.Length > 0)
            {
                char teamId = this.TeamsAssignments[slot];
                return this.FindTeam(teamId);
            }

            return null;
        }

        /// <summary>
        /// Finds players score
        /// </summary>
        /// <param name="playerId">player id</param>
        /// <returns>players score</returns>
        public int FindPlayersScore(char playerId)
        {
            foreach (PlayerScore score in this.Scores)
            {
                if (score.Identity == playerId)
                {
                    return score.Score;
                }
            }

            throw new ArgumentOutOfRangeException("playerId");
        }
    }
}
