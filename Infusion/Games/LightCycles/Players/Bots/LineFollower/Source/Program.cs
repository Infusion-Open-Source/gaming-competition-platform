using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Linefollower
{
    class Program
    {
        static void Main(string[] args)
        {
            string youAre = string.Empty;
            string players = string.Empty;
            string teams = string.Empty;
            string gameMode = string.Empty;

            while (true)
            {
                string line = Console.ReadLine();
                if(line == null || string.IsNullOrEmpty(line))
                {
                    continue;
                }
                if (line.StartsWith("Ready?"))
                {
                    Console.WriteLine("Ready!");
                }
                else if (line.StartsWith("You Are: "))
                {
                    youAre = line.Replace("You Are: ", string.Empty);
                }
                else if (line.StartsWith("Players: "))
                {
                    players = line.Replace("Players: ", string.Empty);
                }
                else if (line.StartsWith("Teams: "))
                {
                    teams = line.Replace("Teams: ", string.Empty);
                }
                else if (line.StartsWith("Game mode: "))
                {
                    gameMode = line.Replace("Game mode: ", string.Empty);
                }
                else if (line.StartsWith("Turn: "))
                {
                    int turn = int.Parse(line.Replace("Turn: ", string.Empty));
                    int numberOfLines = int.Parse(Console.ReadLine());
                    if (numberOfLines > 1)
                    {
                        string[] data = new string[numberOfLines];
                        for (int i = 0; i < numberOfLines; i++)
                        {
                            data[i] = Console.ReadLine();
                        }

                        // string response = DoTheAI(youAre, players, teams, gameMode, data);
                        string response = DoTheAISmarterWay(youAre, players, teams, gameMode, data);
                        Console.WriteLine(response);
                    }
                    else
                    {
                        // game lost I'm dead
                        string gameResult = Console.ReadLine();
                        break;
                    }
                }
                else if (line.StartsWith("Game ends"))
                {
                    // game end, check whether won or lost
                    string gameResult = Console.ReadLine();
                    break;
                }
            }
        }

        static Random Random = new Random((int)DateTime.Now.Ticks);

        /// <summary>
        /// check if there are no obstacles and pick random from available choices
        /// </summary>
        static string DoTheAISmarterWay(string youAre, string players, string teams, string gameMode, string[] data)
        {
            // find were am I
            int myLine = 0;
            int myIndex = 0;
            for (int line = 0; line < data.Length; line++)
            {
                for (int index = 0; index < data[line].Length; index++)
                {
                    if (data[line][index] == youAre[0])
                    {
                        myLine = line;
                        myIndex = index;
                    }
                }
            }

            // do the checks
            bool canGoLeft = data[myLine][myIndex - 1] == ' ';
            bool canGoRight = data[myLine][myIndex + 1] == ' ';
            bool canGoStraight = data[myLine - 1][myIndex] == ' ';

            if (canGoStraight)
                return "S"; // prefer straight, otherwise pick random

            List<string> myOptions = new List<string>();
            if (canGoLeft) myOptions.Add("L");
            if (canGoRight) myOptions.Add("R");

            if (myOptions.Count > 0)
                return myOptions[Random.Next(myOptions.Count)]; // get random available option
            return "S"; // go straight if no choce at all
        }
    }
}
