using SFML.Graphics;
using SFML.Window;
using System;
using System.Diagnostics;
using System.Reflection;
using TripOverTime.EngineNamespace;
using static System.Net.Mime.MediaTypeNames;

namespace TripOverTime.Main
{
    class Program
    {
        private static uint _XResolution = 800;
        private static uint _YResolution = 600;
        private static uint _NbFPS = 60;

        static void Main(string[] args)
        {
            Console.WriteLine("Graphics tests !");

            // To manage FramePS and TicksPS
            Stopwatch spGui = new Stopwatch();
            Stopwatch spGame = new Stopwatch();
            float fps = _NbFPS;
            float tps = 60;

            //Window & Engine start
            RenderWindow window = new RenderWindow(new VideoMode(_XResolution, _YResolution), "T.O.T");
            window.SetVerticalSyncEnabled(true);
            Engine engine = new Engine(window);

            //Menu
            while (!engine.Close) //GAMELOOP MASTER
            {
                short choose = -2;
                short choose1 = -2;
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
                    choose = 1;
                    engine.GetSettings.StartSettings();
                    do
                    {
                        choose1 = engine.GetSettings.RunSettings();
                    } while (choose1 == -2);
                    if (choose1 == 0) //resolution
                    {
                        _XResolution = 1500;
                        _YResolution = 1000;
                        window.Close();
                        engine.Close = true;
                        RunAgain();
                    }
                    else if (choose1 == 1) //Settings2
                    {
                    }
                    else if (choose1 == -1) //Settings2
                    {
                    }
                    else if (choose1 == -2)//back to main menu
                    {
                        choose = -2;
                    }
                }
                else if (choose == -1)
                {
                    window.Close();
                    engine.Close = true;
                }
            }

            Console.WriteLine("End Game");
        }

        private static void RunAgain()
        {

            Console.WriteLine("Graphics tests !");

            // To manage FramePS and TicksPS
            Stopwatch spGui = new Stopwatch();
            Stopwatch spGame = new Stopwatch();
            float fps = _NbFPS;
            float tps = 60;

            //Window & Engine start
            RenderWindow window = new RenderWindow(new VideoMode(_XResolution, _YResolution), "T.O.T");
            window.SetVerticalSyncEnabled(true);
            Engine engine = new Engine(window);

            //Menu
            while (!engine.Close) //GAMELOOP MASTER
            {
                short choose = -2;
                short choose1 = -3;
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
                    choose = 1;
                    engine.GetSettings.StartSettings();
                    do
                    {
                        choose1 = engine.GetSettings.RunSettings();
                    } while (choose1 == -3);
                    if (choose1 == 0) //resolution
                    {
                        _XResolution = 800;
                        _YResolution = 600;
                        window.Close();
                        engine.Close = true;
                        RunAgain();
                    }
                    else if (choose1 == 1) //FPS
                    {
                        engine.Close = true;
                    }
                    else if (choose1 == -1) //FPS
                    {
                        RunAgain();
                    }
                    else if (choose1 == -2)//back to main menu
                    {
                        choose = -2;
                    }
                }
                else if (choose == -1)
                {
                    window.Close();
                    engine.Close = true;
                }
            }

            Console.WriteLine("End Game");
        }
        private uint XResolution
        {
            get { return _XResolution; }
            set { _XResolution = value; }
        }
        private uint YResolution
        {
            get { return _YResolution; }
            set { _YResolution = value; }
        }
        private uint NbFPS
        {
            get { return _NbFPS; }
            set { _NbFPS = value; }
        }

    }
}
