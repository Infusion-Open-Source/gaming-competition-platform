namespace Infusion.Gaming.LightCycles.UIClient.Data
{
    using System.Collections.Generic;

    /// <summary>
    /// Game turn details class
    /// </summary>
    public class TurnDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TurnDetails"/> class.
        /// </summary>
        /// <param name="turnNumber">turn number</param>
        /// <param name="data">turn data</param>
        public TurnDetails(string turnNumber, string[] data)
        {
            this.TurnNumber = int.Parse(turnNumber);
            this.Data = new char[data.Length, data[0].Length];
            for (int ix1 = 0; ix1 < this.Data.GetLength(0); ix1++)
            {
                for (int ix2 = 0; ix2 < this.Data.GetLength(1); ix2++)
                {
                    this.Data[ix1, ix2] = data[ix1][ix2];
                }
            }

            this.Commands = new List<PlayerCommand>();            
        }
        
        /// <summary>
        /// Gets or sets turn number
        /// </summary>
        public int TurnNumber { get; protected set; }

        /// <summary>
        /// Gets or sets turn data
        /// </summary>
        public char[,] Data { get; protected set; }

        /// <summary>
        /// Gets or sets turn ages of trails
        /// </summary>
        public int[,] TrailAges { get; protected set; }

        /// <summary>
        /// Gets or sets turn commands
        /// </summary>
        public List<PlayerCommand> Commands { get; protected set; }
        
        /// <summary>
        /// Calculate initial trail ages
        /// </summary>
        public void CalcInitialTrailAges()
        {
            this.TrailAges = new int[this.Data.GetLength(0), this.Data.GetLength(1)];
            for (int x = 0; x < this.Data.GetLength(1); x++)
            {
                for (int y = 0; y < this.Data.GetLength(0); y++)
                {
                    char c = this.Data[y, x];
                    if (c >= 'a' && c <= 'z')
                    {
                        this.TrailAges[y, x] = 2;
                    }
                    else if (c >= 'A' && c <= 'Z')
                    {
                        this.TrailAges[y, x] = 1;
                    }   
                    else
                    {
                        this.TrailAges[y, x] = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Calculate trail ages accordingly to previous turn data
        /// </summary>
        /// <param name="previousTurn">previous turn data</param>
        public void CalcTrailAges(TurnDetails previousTurn)
        {
            this.TrailAges = new int[this.Data.GetLength(0), this.Data.GetLength(1)];
            for (int x = 0; x < this.Data.GetLength(1); x++)
            {
                for (int y = 0; y < this.Data.GetLength(0); y++)
                {
                    // trail age on bikes to 1, trail age that persists++
                    char c = this.Data[y, x];
                    if (c >= 'a' && c <= 'z')
                    {
                        this.TrailAges[y, x] = 1 + previousTurn.TrailAges[y, x];
                    }
                    else if (c >= 'A' && c <= 'Z')
                    {
                        this.TrailAges[y, x] = 1;
                    }
                    else
                    {
                        this.TrailAges[y, x] = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Add commands to turn details
        /// </summary>
        /// <param name="commands">commands to add</param>
        public void AddCommands(IEnumerable<string> commands)
        {
            foreach (string command in commands)
            {
                this.Commands.Add(new PlayerCommand(command));
            }
        }
    }
}
