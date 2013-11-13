namespace Infusion.Gaming.LightCycles.UIClient
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Threading;
    using Infusion.Gaming.LightCycles.UIClient.Data;

    /// <summary>
    /// Program entry point
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The application entry point.
        /// </summary>
        /// <param name="args">program arguments</param>
        [STAThread]
        public static void Main(string[] args)
        {
            string dir = args[0];
            bool moveToArchive = args.Length > 1 && args[1] == "Archive";
            if (Directory.Exists(dir))
            {
                PlaybackSettings playbackSettigs = new PlaybackSettings();
                playbackSettigs.WaitOnStart = true;
                playbackSettigs.WaitOnEnd = true;
                playbackSettigs.WaitOnTurn = true;
                using (GameView view = new GameView(playbackSettigs))
                {
                    Thread netThread = new Thread(PlaybackThread);
                    netThread.Start(new object[] { dir, view, moveToArchive });
                    view.Run();
                    netThread.Abort();
                }
            }
        }

        /// <summary>
        /// Game playback thread routine
        /// </summary>
        /// <param name="arg">thread arguments</param>
        public static void PlaybackThread(object arg)
        {
            object[] args = (object[])arg;
            string logFilesPath = (string)args[0];
            GameView view = (GameView)args[1];
            bool moveToArchive = (bool)args[2];

            try
            {
                while (!view.IsInitialized)
                {
                    Thread.Sleep(10);
                }

                string archivePath = Path.Combine(logFilesPath, "Archive");
                Directory.CreateDirectory(archivePath);

                while (true)
                {
                    foreach (string file in Directory.GetFiles(logFilesPath, "*.log"))
                    {
                        GameDetails gameDetails;
                        try
                        {
                            using (TextReader reader = new StreamReader(File.OpenRead(file)))
                            {
                                gameDetails = GameDetails.ReadIn(reader);
                                reader.Close();
                            }
                        }
                        catch (Exception)
                        {
                            // if file is bad then skip it
                            gameDetails = null;
                            File.Move(file, file + ".corrupted");
                        }

                        if (gameDetails != null)
                        {
                            var visualStateBuilder = new VisualStateBuilder(gameDetails, new RectangleF(0, 0, view.WindowWidth, view.WindowHeight));
                            StartRoutine(view, visualStateBuilder);
                            TurnRoutine(view, visualStateBuilder, gameDetails);
                            EndRoutine(view, visualStateBuilder);
                            
                            if (moveToArchive)
                            {
                                File.Move(file, Path.Combine(archivePath, Path.GetFileName(file)));
                            }
                        }
                    }
                }
            }
            catch (ThreadAbortException)
            {
            }
        }

        /// <summary>
        /// Handle game start
        /// </summary>
        /// <param name="view">game view</param>
        /// <param name="visualStateBuilder">view state builder</param>
        public static void StartRoutine(GameView view, VisualStateBuilder visualStateBuilder)
        {
            if (view.PlaybackSettings.WaitOnStart)
            {
                view.UpdateVisualState(visualStateBuilder.BuildStartVisualState(true));
                int spaceHits = view.PlaybackSettings.SpaceHitsCount;
                while (spaceHits == view.PlaybackSettings.SpaceHitsCount)
                {
                    Thread.Sleep(10);
                }
            }
            else
            {
                view.UpdateVisualState(visualStateBuilder.BuildStartVisualState(false));
                DateTime endTime = DateTime.Now.AddMilliseconds(view.PlaybackSettings.DelayOnStart);
                int spaceHits = view.PlaybackSettings.SpaceHitsCount;
                while (DateTime.Now < endTime)
                {
                    if (spaceHits != view.PlaybackSettings.SpaceHitsCount)
                    {
                        break;
                    }

                    Thread.Sleep(10);
                }
            }
        }

        /// <summary>
        /// Handle game turn
        /// </summary>
        /// <param name="view">game view</param>
        /// <param name="visualStateBuilder">game view state builder</param>
        /// <param name="gameDetails">game details</param>
        public static void TurnRoutine(GameView view, VisualStateBuilder visualStateBuilder, GameDetails gameDetails)
        {
            for (int turn = 0; turn < gameDetails.Turns.Count;)
            {
                if (view.PlaybackSettings.WaitOnTurn)
                {
                    view.UpdateVisualState(visualStateBuilder.BuildTurnVisualState(turn, true));
                    int spaceHits = view.PlaybackSettings.SpaceHitsCount;
                    int backHits = view.PlaybackSettings.BackHitsCount;
                    while (true)
                    {
                        if (spaceHits != view.PlaybackSettings.SpaceHitsCount)
                        {
                            turn++; // on space go to next turn
                            break;
                        }

                        if (backHits != view.PlaybackSettings.BackHitsCount)
                        {
                            turn--; // on back go to previous turn
                            if (turn < 0)
                            {
                                turn = 0;
                            }

                            break;
                        }

                        Thread.Sleep(10);
                    }
                }
                else
                {
                    view.UpdateVisualState(visualStateBuilder.BuildTurnVisualState(turn, false));
                    DateTime endTime = DateTime.Now.AddMilliseconds(view.PlaybackSettings.TurnDelayTime);
                    while (DateTime.Now < endTime)
                    {
                        Thread.Sleep(10);
                    }

                    turn++; // in auto run mode go forward
                }
            }
        }

        /// <summary>
        /// Handle end of game
        /// </summary>
        /// <param name="view">game view</param>
        /// <param name="visualStateBuilder">game view state builder</param>
        public static void EndRoutine(GameView view, VisualStateBuilder visualStateBuilder)
        {
            if (view.PlaybackSettings.WaitOnEnd)
            {
                view.UpdateVisualState(visualStateBuilder.BuildFinishedVisualState(true));
                int spaceHits = view.PlaybackSettings.SpaceHitsCount;
                while (spaceHits == view.PlaybackSettings.SpaceHitsCount)
                {
                    Thread.Sleep(10);
                }
            }
            else
            {
                view.UpdateVisualState(visualStateBuilder.BuildFinishedVisualState(false));
                DateTime endTime = DateTime.Now.AddMilliseconds(view.PlaybackSettings.DelayOnEnd);
                int spaceHits = view.PlaybackSettings.SpaceHitsCount;
                while (DateTime.Now < endTime)
                {
                    if (spaceHits != view.PlaybackSettings.SpaceHitsCount)
                    {
                        break;
                    }

                    Thread.Sleep(10);
                }
            }
        }
    }
}
