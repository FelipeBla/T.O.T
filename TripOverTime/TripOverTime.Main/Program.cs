using SFML.Graphics;
using SFML.Window;
using System;
using System.Diagnostics;
using TripOverTime.EngineNamespace;

namespace TripOverTime.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Graphics tests !");

            // To manage FramePS and TicksPS
            Stopwatch spGui = new Stopwatch();
            Stopwatch spGame = new Stopwatch();
            float fps = 60;
            float tps = 60;

            //Window & Engine start
            RenderWindow window = new RenderWindow(new VideoMode(800, 600), "T.O.T");
            window.SetVerticalSyncEnabled(true);
            Engine engine = new Engine(window);

            //Menu
            while (!engine.Close) //GAMELOOP MASTER
            {
                short choose = -2;
                engine.GetMenu.StartMainMenu();
                do
                {
                    choose = engine.GetMenu.RunMainMenu();
                } while (choose == -2);


                if (choose == 0) //Lauch GAME
                {
                    // Start a game
                    engine.StartGame(@"..\..\..\..\Maps\test.totmap", @"..\..\..\..\Assets\Players\Variable sizes\Pink"); //map, player sprite
                    engine.GetGUI.InitGame();

                    bool playerAlive = true;
                    // GameLoop
                    spGui.Start();
                    spGame.Start();
                    while (playerAlive)
                    {
                        if (spGame.ElapsedMilliseconds >= 1000 / tps)
                        {
                            // GameTick
                            playerAlive = engine.GameTick();
                            spGame.Restart();
                        }

                        if (spGui.ElapsedMilliseconds >= 1000 / fps)
                        {
                            //GUI
                            engine.GetGUI.ShowMap();
                            spGui.Restart();
                        }

                    }
                }
                else if (choose == 1) //Settings
                {
                    engine.GetSettings.StartSettings();

                }
                else if (choose == -1)
                {
                    window.Close();
                    engine.Close = true;
                }
            }

            Console.WriteLine("End Game");
        }
    }
}
