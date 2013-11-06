using System.Collections.Generic;

namespace Infusion.Gaming.LightCycles
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using Infusion.Gaming.LightCycles.Config;
    using Infusion.Gaming.LightCycles.Definitions;
    using Infusion.Gaming.LightCycles.Model;
    using Infusion.Gaming.LightCycles.Util;
    
    /// <summary>
    /// Program entry point
    /// </summary>
    public class Program
    {
        public static Dictionary<Mapping, int> numberOfWins = new Dictionary<Mapping, int>();
        public static Dictionary<Mapping, int> numberOfLoses = new Dictionary<Mapping, int>();
        public static Dictionary<Mapping, int> numberOfDraws = new Dictionary<Mapping, int>();
        public static Dictionary<Mapping, int> totalScore = new Dictionary<Mapping, int>();
        public static List<string> tournamentLog = new List<string>();

        /// <summary>
        /// Entry point method
        /// </summary>
        /// <param name="args">program arguments</param>
        public static void Main(string[] args)
        {
            if (args.Length > 3 && args[3] == "Tournament")
            {
                PlayTournament(args);
            }
            else
            {
                PlayRegular(args);
            }
        }

        /// <summary>
        /// Play in tournament mode
        /// </summary>
        /// <param name="args">program arguments</param>
        public static void PlayTournament(string[] args)
        {
            RunSettings runSettings = new ConfigProvider<RunSettings>().Load(args[0]);
            GameSettings gameSettings = new ConfigProvider<GameSettings>().Load(args[1]);
            TeamsAndPlayers teamsAndPlayers = new ConfigProvider<TeamsAndPlayers>().Load(args[2]);           
            foreach (Mapping mapping in runSettings.PlayerMappings)
            {
                numberOfWins.Add(mapping, 0);
                numberOfLoses.Add(mapping, 0);
                numberOfDraws.Add(mapping, 0);
                totalScore.Add(mapping, 0);
            }

            const int numberOfGamesToPlay = 1;
            int totalNumberOfGames = numberOfGamesToPlay * runSettings.PlayerMappings.Count * (runSettings.PlayerMappings.Count - 1);
            int gameNumber = 1;
            foreach (Mapping mappingA in runSettings.PlayerMappings)
            {
                foreach (Mapping mappingB in runSettings.PlayerMappings)
                {
                    if (mappingA.Id != mappingB.Id)
                    {
                        for (int game = 0; game < numberOfGamesToPlay; game++)
                        {
                            string outputFileName = Path.Combine(".\\Logs", GetOneOnOneLogFileName(mappingA.Name, mappingB.Name, game));
                            using (TextWriter output = new StreamWriter(outputFileName))
                            {
                                RunSettings gameRunSettings = new RunSettings();
                                gameRunSettings.FogOfWar = runSettings.FogOfWar;
                                gameRunSettings.TimeLimit = runSettings.TimeLimit;
                                gameRunSettings.ViewArea = runSettings.ViewArea;
                                gameRunSettings.PlayerMappings = new List<Mapping>();
                                gameRunSettings.PlayerMappings.Add(new Mapping() { Id = "A", Name = mappingA.Name });
                                gameRunSettings.PlayerMappings.Add(new Mapping() { Id = "B", Name = mappingB.Name });

                                Stopwatch stopwatch = new Stopwatch();
                                stopwatch.Start();
                                GameRunner runner = new GameRunner(output, gameRunSettings, gameSettings, teamsAndPlayers);
                                runner.Run();
                                output.Close();
                                stopwatch.Stop();

                                int scoreA = runner.Game.PlayerSetup.Scoreboard[new Identity('A')];
                                int scoreB = runner.Game.PlayerSetup.Scoreboard[new Identity('B')];
                                string result = "DRAW";
                                if (runner.Game.Result == GameResult.FinshedWithWinner)
                                {
                                    if (mappingA.Id[0] == runner.Game.Winner.Identifier)
                                    {
                                        result = mappingA.Name;
                                        numberOfWins[mappingA]++;
                                        numberOfLoses[mappingB]++;
                                    }
                                    else
                                    {
                                        result = mappingB.Name;
                                        numberOfLoses[mappingA]++;
                                        numberOfWins[mappingB]++;
                                    }
                                }
                                else
                                {
                                    numberOfDraws[mappingA]++;
                                    numberOfDraws[mappingB]++;
                                }

                                totalScore[mappingA] += scoreA;
                                totalScore[mappingB] += scoreB;

                                StringBuilder outputBuilder = new StringBuilder();
                                outputBuilder.Append("Game ");
                                outputBuilder.Append(gameNumber++);
                                outputBuilder.Append(" of ");
                                outputBuilder.Append(totalNumberOfGames);
                                outputBuilder.Append(" [");
                                outputBuilder.Append(mappingA.Name);
                                outputBuilder.Append("] vs [");
                                outputBuilder.Append(mappingB.Name);
                                outputBuilder.Append("] result: ");
                                outputBuilder.Append(result);
                                outputBuilder.Append(" (");
                                outputBuilder.Append(scoreA);
                                outputBuilder.Append(",");
                                outputBuilder.Append(scoreB);
                                outputBuilder.Append("), time: ");
                                outputBuilder.Append(stopwatch.ElapsedMilliseconds / 1000);
                                outputBuilder.Append(" sec");
                                Console.Out.WriteLine(outputBuilder.ToString());

                                tournamentLog.Add(outputBuilder.ToString());
                                OutputScoreboard(runSettings.PlayerMappings);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Output scoreboard to file
        /// </summary>
        /// <param name="playerMappings">player mappings</param>
        public static void OutputScoreboard(List<Mapping> playerMappings)
        {
            StringBuilder outputBuilder = new StringBuilder();
            outputBuilder.AppendLine("Scoreboard:");

            int longestName = 0;
            foreach (Mapping mapping in playerMappings)
            {
                if(mapping.Name.Length > longestName)
                {
                    longestName = mapping.Name.Length;
                }
            }

            foreach (Mapping mapping in playerMappings)
            {
                outputBuilder.Append(mapping.Name);
                for (int i = mapping.Name.Length; i < longestName + 3; i++)
                {
                    outputBuilder.Append(" ");
                }

                outputBuilder.Append(" wins:\t");
                outputBuilder.Append(numberOfWins[mapping]);
                outputBuilder.Append("\tloses:\t");
                outputBuilder.Append(numberOfLoses[mapping]);
                outputBuilder.Append("\tdraws:\t");
                outputBuilder.Append(numberOfDraws[mapping]);
                outputBuilder.Append("\ttotal score:\t");
                outputBuilder.Append(totalScore[mapping]);
                outputBuilder.AppendLine();
            }

            outputBuilder.AppendLine();
            foreach(string gameResult in tournamentLog)
            {
                outputBuilder.AppendLine(gameResult);
            }

            File.WriteAllText(".\\Scoreboard.txt", outputBuilder.ToString());
        }

        /// <summary>
        /// Play in regular mode
        /// </summary>
        /// <param name="args">program arguments</param>
        public static void PlayRegular(string[] args)
        {
            RunSettings runSettings = new ConfigProvider<RunSettings>().Load(args[0]);
            GameSettings gameSettings = new ConfigProvider<GameSettings>().Load(args[1]);
            TeamsAndPlayers teamsAndPlayers = new ConfigProvider<TeamsAndPlayers>().Load(args[2]);
            string outputFileName = Path.Combine(".\\Logs", GetLogFileName(args[1]));
            using (TextWriter output = new StreamWriter(outputFileName))
            {
                GameRunner runner = new GameRunner(output, runSettings, gameSettings, teamsAndPlayers);
                runner.Run();
                output.Close();
            }
        }

        /// <summary>
        /// Gets log file name
        /// </summary>
        /// <param name="gameSettingsFile">game settings file name</param>
        /// <returns>name of game log file</returns>
        public static string GetLogFileName(string gameSettingsFile)
        {
            string settingsFileName = Path.GetFileNameWithoutExtension(gameSettingsFile);
            StringBuilder outputNameBuilder = new StringBuilder();
            outputNameBuilder.Append(settingsFileName);
            outputNameBuilder.Append("_");
            outputNameBuilder.Append(DateTime.Now.Ticks);
            outputNameBuilder.Append(".log");
            return outputNameBuilder.ToString();
        }

        /// <summary>
        /// Gets log file name for one on one game
        /// </summary>
        /// <param name="playerA">player A name</param>
        /// <param name="playerB">player B name</param>
        /// <param name="gameNumber">game number</param>
        /// <returns>name of game log file</returns>
        public static string GetOneOnOneLogFileName(string playerA, string playerB, int gameNumber)
        {
            StringBuilder outputNameBuilder = new StringBuilder();
            outputNameBuilder.Append(playerA);
            outputNameBuilder.Append("_vs_");
            outputNameBuilder.Append(playerB);
            outputNameBuilder.Append("_");
            outputNameBuilder.Append(gameNumber);
            outputNameBuilder.Append(".log");
            return outputNameBuilder.ToString();
        }
    }
}
