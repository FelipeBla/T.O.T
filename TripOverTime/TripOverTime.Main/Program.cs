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
        /// <summary>
        /// Variable stockant le choix du menu principal par l'utilisateur : 1P, 2P, Map editor ou Quit
        /// </summary>
        short choose = -2;
        /// <summary> 
        /// Nom en string de la map sélectionnée par l'utilisateur
        /// </summary>
        string chooseMap = "null";
        static void Main(string[] args) //fonction principale
        {
            Program program = new Program();
            program.RunAgain();
            

        }

        /// <summary>
        /// Fonction gérant l'intégralité du jeu : Du Menu, à la gestiion de la mort du joueur
        /// </summary>
        /// <returns>Return uniquement quand le jeu est terminé</returns>
        private bool RunAgain() //fonction du jeu
        {            
            // To manage FramePS and TicksPS
            Stopwatch spGui = new Stopwatch();
            Stopwatch spGame = new Stopwatch();
            float fps = Settings.NbFPS;
            float tps = 60;

            //Window & Engine start
            RenderWindow window = null;

            if (Settings.Fullscreen)
            {
                window = new RenderWindow(new VideoMode(Settings.XResolution, Settings.YResolution), "T.O.T", Styles.Fullscreen);
            }
            else
            {
                window = new RenderWindow(new VideoMode(Settings.XResolution, Settings.YResolution), "T.O.T");
            }

            window.SetVerticalSyncEnabled(true);
            Engine engine = new Engine(window);
            

            //Menu
            while (!engine.Close && window.IsOpen) //GAMELOOP MASTER
            {
                System.Threading.Thread.Sleep(200); // Evite certains bug                
                engine.GetMenu.StartMainMenu();
                spGame.Start();

                while (choose == -2) 
                {
                    if (spGame.ElapsedMilliseconds >= 1000 / tps)
                    {
                        choose = engine.GetMenu.RunMainMenu();
                        spGame.Restart();
                    }
                } 


                if (choose == 0) //Lauch GAME 1P
                {
                    if(chooseMap == "null")
                    {
                        // Choose Map                    
                        engine.GetMenu.InitMapMenu();

                        while (chooseMap == "null")
                        {
                            if (spGame.ElapsedMilliseconds >= 1000 / tps)
                            {
                                chooseMap = engine.GetMenu.ChooseMapMenu();
                                spGame.Restart();
                            }
                        }
                    }

                    Console.WriteLine(chooseMap);
                    if (chooseMap == "quit")
                    {
                        window.Close();
                        RunAgain();
                    }
                    // Start a game
                    engine.StartGame(chooseMap); //map, player sprite
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
                        bool loadNextLevel = engine.WinMenu();
                        if (!loadNextLevel)
                        {
                            choose = -2;
                            chooseMap = "null";
                        }
                        else
                        {
                            chooseMap = engine.GetMenu.GetNextLevel();
                        }
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
                else if (choose == 1) // Launch Game 2P
                {
                    string chooseMap = "null";
                    engine.GetMenu.InitMapMenu();
                    while (chooseMap == "null")
                    {
                        if (spGame.ElapsedMilliseconds >= 1000 / tps)
                        {
                            chooseMap = engine.GetMenu.ChooseMapMenu();
                            spGame.Restart();
                        }
                    }

                    Console.WriteLine(chooseMap);
                    if (chooseMap == "quit")
                    {
                        RunAgain();
                        window.Close();
                    }
                    // Start a game
                    engine.StartGame(chooseMap); //map, player sprite
                    engine.GetGUI.InitGame();

                    short result = 1;
                    // GameLoop
                    spGui.Start();
                    while (result == 1)
                    {
                        if (spGame.ElapsedMilliseconds >= 1000 / tps)
                        {
                            // GameTick
                            result = engine.GameTick2();
                            spGame.Restart();
                        }

                        if (spGui.ElapsedMilliseconds >= 1000 / fps)
                        {
                            //GUI
                            engine.GetGUI.ShowMapMultiplayer();
                            spGui.Restart();
                        }
                    }

                    if (result == 0)
                    {
                        //WIN
                        Console.WriteLine("YOU WIN!");
                        bool loadNextLevel = engine.WinMenu();
                        if (!loadNextLevel)
                        {
                            choose = -2;
                            chooseMap = "null";
                        }
                        else
                        {
                            chooseMap = engine.GetMenu.GetNextLevel();
                        }
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
                                    engine.GetGUI.ShowMapMultiplayer();
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
                else if (choose == 2) // Map editor
                {
                    MapEditor.Run(window);
                }
                else if (choose == 3) //Settings
                {
                    if(engine.GetSettings.RunSettings()) // if true need to apply
                    {
                        window.Close();
                        RunAgain();
                    }
                }
                else if (choose == -5) // Quit
                {
                    window.Close();
                    engine.Close = true;
                    return false;
                }
               
            }

            return true;
            //Console.WriteLine("End Game");

        }        
    }
}
