using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using TripOverTime.EngineNamespace;


namespace TripOverTime.Main
{
    class Program
    {
        static void Main(string[] args) //fonction principale
        {
            RunAgain();
        }

        private static bool RunAgain() //fonction du jeu
        {
            // Settings
            if (File.Exists("settings"))
            {
                string settingsFile = File.ReadAllText("settings");
                string[] st = settingsFile.Split("\n");
                Settings.Fullscreen = Convert.ToBoolean(st[0]);
                Settings.NbFPS = Convert.ToUInt16(st[1]);
                Settings.XResolution = Convert.ToUInt16(st[2]);
                Settings.YResolution = Convert.ToUInt16(st[3]);

            }
            else
            {
                string settingsFile = Settings.Fullscreen + "\n" + Settings.NbFPS + "\n" + Settings.XResolution + "\n" + Settings.YResolution;
                File.WriteAllText("settings", settingsFile);
            }

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


                if (choose == 0) //Lauch GAME 1P
                {
                // Choose Map
                ChooseMapMenu:
                    string chooseMap = "null";
                    engine.GetMenu.InitMapMenu();
                    do
                    {
                        if (spGame.ElapsedMilliseconds >= 1000 / tps)
                        {
                            chooseMap = engine.GetMenu.ChooseMapMenu();
                            spGame.Restart();
                        }
                    } while (chooseMap == "null");

                    if (chooseMap == "quit")
                    {
                        window.Close();
                        RunAgain();
                    }
                    else if (chooseMap == "editedMap")
                    {
                        engine.GetMenu.InitMapEditedMenu();
                        do
                        {
                            if (spGame.ElapsedMilliseconds >= 1000 / tps)
                            {
                                chooseMap = engine.GetMenu.ChooseMapEditedMenu();
                                spGame.Restart();
                            }
                        } while (chooseMap == "editedMap");

                        if (chooseMap == "back")
                        {
                            //back to map menu
                            goto ChooseMapMenu;
                        }
                    }
                    // Start a game
                    engine.StartGame(chooseMap); //map, player sprite
                    engine.GetGUI.InitGame();
                    engine.GetGUI.ShowLoading(100);

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
                            while (engine.GetGame.GetPlayer.GetLife.GetCurrentPoint > 0)
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
                        if (engine.GetGame.GetPlayer.KilledBy != "Void")
                        {
                            engine.DieMenu();
                        }
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
                    do
                    {
                        if (spGame.ElapsedMilliseconds >= 1000 / tps)
                        {
                            chooseMap = engine.GetMenu.ChooseMapMenu();
                            spGame.Restart();
                        }
                    } while (chooseMap == "null" || chooseMap == "editedMap");

                    if (chooseMap == "quit")
                    {
                        RunAgain();
                        window.Close();
                    }
                    // Start a game
                    engine.StartGame2(chooseMap); //map, player sprite
                    engine.GetGUI.InitGame2();

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
                        engine.WinMenu();
                    }
                    if (result == 2)
                    {
                        //WIN
                        Console.WriteLine("YOU WIN!");
                        engine.WinMenu2();
                    }
                    else if (result == -1)
                    {
                        //DIE
                        Console.WriteLine("YOU DIE!");

                        if (engine.GetGame.GetPlayer.KilledBy == "Trap")
                        {
                            while (engine.GetGame.GetPlayer.GetLife.GetCurrentPoint > 0)
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
                        if (engine.GetGame2.GetPlayer2.KilledBy2 == "Trap")
                        {
                            while (engine.GetGame2.GetPlayer2.GetLife2.GetCurrentPoint2() > 0)
                            {
                                engine.GetGame2.GetPlayer2.GetLife2.DecreasedPoint2(1);
                                if (spGui.ElapsedMilliseconds >= 1000 / fps)
                                {
                                    //GUI
                                    engine.GetGUI.ShowMapMultiplayer();
                                    spGui.Restart();
                                }
                            }
                        }
                        if (engine.GetGame.GetPlayer.KilledBy != "void")
                        {
                            engine.DieMenu();
                        }
                      
                        if (engine.GetGame2.GetPlayer2.KilledBy2 != "void")
                        {
                            engine.DieMenu2();
                        }
                    }
                    else
                    {
                        throw new Exception("WTF?!");
                    }
                }
                else if (choose == 2) // Map editor
                {
                    MapEditor m = new MapEditor();
                    m.Run(window);
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
