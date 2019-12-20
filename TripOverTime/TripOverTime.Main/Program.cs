using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using TripOverTime.EngineNamespace;


namespace TripOverTime.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAgain();
        }

        private static void RunAgain()
        {
            // To manage FramePS and TicksPS
            Stopwatch spGui = new Stopwatch();
            Stopwatch spGame = new Stopwatch();
            float fps = Settings.NbFPS;
            float tps = 60;

            //Window & Engine start
            RenderWindow window = new RenderWindow(new VideoMode(Settings.XResolution, Settings.YResolution), "T.O.T");
            window.SetVerticalSyncEnabled(true);
            Engine engine = new Engine(window);
            

            //Menu
            while (!engine.Close) //GAMELOOP MASTER
            {
                System.Threading.Thread.Sleep(200); // Evite certains bug
                short choose = -2;
                engine.GetMenu.StartMainMenu();
                spGame.Start();

                do
                {
                    if (spGame.ElapsedMilliseconds >= 1000 / tps)
                    {
                        choose = engine.GetMenu.RunMainMenu();
                        spGame.Restart();
                    }
                } while (choose == -2);


                if (choose == 0) //Lauch GAME
                {
                    // Start a game
                    engine.StartGame(@"..\..\..\..\Maps\test2.totmap", @"..\..\..\..\Assets\Players\Variable sizes\Knight\AllViking"); //map, player sprite
                    engine.GetGUI.InitGame();

                    short result = 1;
                    // GameLoop
                    spGui.Start();
                    while (result == 1)
                    {
                        if (spGame.ElapsedMilliseconds >= 1000 / tps)
                        {
                            // GameTick
                            result = engine.GameTick();
                            spGame.Restart();
                        }

                        if (spGui.ElapsedMilliseconds >= 1000 / fps)
                        {
                            //GUI
                            engine.GetGUI.ShowMap();
                            spGui.Restart();
                        }
                    }
                    if (result == 0)
                    {
                        //WIN
                        Console.WriteLine("YOU WIN!");
                        engine.WinMenu();
                    }
                    else if (result == -1)
                    {
                        //DIE
                        Console.WriteLine("YOU DIE!");

                        if (engine.GetGame.GetPlayer.KilledBy == "Trap")
                        {
                            while (engine.GetGame.GetPlayer.GetLife.GetCurrentPoint() > 0)
                            {
                                engine.GetGame.GetPlayer.GetLife.DecreasedPoint(1);
                                if (spGui.ElapsedMilliseconds >= 1000 / fps)
                                {
                                    //GUI
                                    engine.GetGUI.ShowMap();
                                    spGui.Restart();
                                }
                            }
                        }

                        engine.DieMenu();
                    }
                    else
                    {
                        throw new Exception("WTF?!");
                    }
                }
                else if (choose == 1) //Settings
                {
                    if(engine.GetSettings.RunSettings()) // if true need to apply
                    {
                        RunAgain();
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
    }
}
