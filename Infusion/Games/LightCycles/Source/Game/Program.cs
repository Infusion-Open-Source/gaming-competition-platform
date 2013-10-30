namespace Infusion.Gaming.LightCycles
{
    using Infusion.Gaming.LightCycles.Config;
    using Infusion.Gaming.LightCycles.Util;
    
    /// <summary>
    /// Program entry point
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point method
        /// </summary>
        /// <param name="args">program arguments</param>
        public static void Main(string[] args)
        {
            RunSettings runSettings = new ConfigProvider<RunSettings>().Load(args[0]);
            GameSettings gameSettings = new ConfigProvider<GameSettings>().Load(args[1]);
            TeamsAndPlayers teamsAndPlayers = new ConfigProvider<TeamsAndPlayers>().Load(args[2]);
            GameRunner runner = new GameRunner(runSettings, gameSettings, teamsAndPlayers);
            runner.Run();
        }
    }
}
