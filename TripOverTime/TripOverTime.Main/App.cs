using SFML.Graphics;
using SFML.Window;
using System;
using System.Diagnostics;
using System.IO;
using TripOverTime.EngineNamespace;

namespace TripOverTime.Main
{
    public class App
    {
        short choose = -2;
        string MapSelected = null;
        public bool EnableLevelTwo { get; set; }
        public bool EnableLevelThree { get; set; }

        // To manage FramePS and TicksPS
        Stopwatch spGui = new Stopwatch();
        Stopwatch spGame = new Stopwatch();
        RenderWindow window = null;
        float fps = Settings.NbFPS;
        float tps = 60;

        /// <summary>
        /// Fonction gérant l'intégralité du jeu : Du Menu, à la gestiion de la mort du joueur
        /// </summary>
        /// <returns>Return uniquement quand le jeu est terminé</returns>
        public void StartApp() //fonction du jeu
        {

            #region initialized
            choose = -2;
            MapSelected = null;

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

            //Window & Engine start
            if (Settings.Fullscreen)
            {
                window = new RenderWindow(new VideoMode(Settings.XResolution, Settings.YResolution), "T.O.T", Styles.Fullscreen);
            }
            else
            {
                window = new RenderWindow(new VideoMode(Settings.XResolution, Settings.YResolution), "T.O.T");
            }

            window.SetVerticalSyncEnabled(true);

            #endregion

            Engine engine = new Engine(window);
            engine.GetMenu.StartMainMenu();
            spGame.Start();

            #region Main Menu
            while (choose == -2)
            {
                if (spGame.ElapsedMilliseconds >= 1000 / tps)
                {
                    choose = engine.GetMenu.GetMainMenu();
                    spGame.Restart();
                }
            }

            if (choose == 0)
            {
                //Lauch GAME 1P
                StartOnePlayer(engine);
            }
            else if (choose == 1)
            {
                // Launch Game 2P
                StartTwoPlayer(engine);
            }
            else if (choose == 2) 
            {
                // Map editor
                MapEditor m = new MapEditor();
                m.Run(window);
            }
            else if (choose == 3) 
            {
                //Settings
                if (engine.GetSettings.RunSettings()) // if true need to apply
                {
                    window.Close();
                    StartApp();
                }
            }
            else if (choose == -5)
            {
                Quit(true);
            }
            #endregion

        }

        /// <summary>
        /// Starts the one player.
        /// </summary>
        /// <param name="engine">The engine.</param>
        /// <exception cref="Exception">WTF?!</exception>
        public void StartOnePlayer(Engine engine)
        {
            // Choose Map                    
            engine.GetMenu.InitMapMenu();

            while (MapSelected == null)
            {
                if (spGame.ElapsedMilliseconds >= 1000 / tps)
                {
                    MapSelected = engine.GetMenu.GetMapMenu(EnableLevelTwo, EnableLevelThree);
                    spGame.Restart();
                }
            }

            if (MapSelected == "mainMenu")
            {
                StartApp();
            }
            else if(MapSelected == "tutorial")
            {
                StartTutorial(engine, engine.GetMenu.GetTutorialMap());
            }
            else
            {
                StartOnePlayerGame(engine, MapSelected);
            }  
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="engine">The engine.</param>
        /// <exception cref="Exception">WTF?!</exception>
        public void StartOnePlayerGame(Engine engine, string chooseMap)
        {
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
                Console.WriteLine("YOU WIN!");
                bool loadNextLevel = engine.WinMenu();
                GetNextActionOnePlayer(loadNextLevel, false, chooseMap, engine);
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

                bool restartLevel = engine.DieMenu();
                GetNextActionOnePlayer(false, restartLevel, chooseMap, engine);
            }
        }

        /// <summary>
        /// Starts the two player game.
        /// </summary>
        /// <param name="engine">The engine.</param>
        /// <param name="chooseMap">The choose map.</param>
        public void StartTwoPlayerGame(Engine engine, string chooseMap)
        {
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
                bool loadNextLevel = engine.WinMenu();
                GetNextActionTwoPlayer(loadNextLevel, false, chooseMap, engine);
            }
            else if (result == 2)
            {
                //WIN
                Console.WriteLine("YOU WIN!");
                bool loadNextLevel = engine.WinMenu2();
                GetNextActionTwoPlayer(loadNextLevel, false, chooseMap, engine);
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
                    bool restartLevel = engine.DieMenu();
                    GetNextActionTwoPlayer(false, restartLevel, chooseMap, engine);
                }

                if (engine.GetGame2.GetPlayer2.KilledBy2 != "void")
                {
                    bool restartLevel = engine.DieMenu2();
                    GetNextActionTwoPlayer(false, restartLevel, chooseMap, engine);
                }
            }
            
        }

        /// <summary>
        /// Gets the next action.
        /// </summary>
        /// <param name="loadNextLevel">if set to <c>true</c> [load next level].</param>
        /// <param name="chooseMap">The choose map.</param>
        /// <param name="engine">The engine.</param>
        public void GetNextActionTwoPlayer(bool loadNextLevel, bool restartLevel, string chooseMap, Engine engine)
        {
            if (loadNextLevel)
            {
                if (!EnableLevelTwo)
                {
                    EnableLevelTwo = true;
                    chooseMap = engine.GetMenu.GetNextLevel(2);
                    StartTwoPlayerGame(engine, chooseMap);
                }
                else if (!EnableLevelThree)
                {
                    EnableLevelThree = true;
                    chooseMap = engine.GetMenu.GetNextLevel(3);
                    StartTwoPlayerGame(engine, chooseMap);
                }
                else
                {
                    StartApp();
                }
            }
            else if(restartLevel)
            {
                StartTwoPlayerGame(engine, chooseMap);
            }
            else
            {
                StartApp();
            }
        }

        /// <summary>
        /// Gets the next action one player.
        /// </summary>
        /// <param name="loadNextLevel">if set to <c>true</c> [load next level].</param>
        /// <param name="restartLevel">if set to <c>true</c> [restart level].</param>
        /// <param name="chooseMap">The choose map.</param>
        /// <param name="engine">The engine.</param>
        public void GetNextActionOnePlayer(bool loadNextLevel, bool restartLevel, string chooseMap, Engine engine)
        {
            if (loadNextLevel)
            {
                if (!EnableLevelTwo)
                {
                    EnableLevelTwo = true;
                    chooseMap = engine.GetMenu.GetNextLevel(2);
                    StartOnePlayerGame(engine, chooseMap);
                }
                else if (!EnableLevelThree)
                {
                    EnableLevelThree = true;
                    chooseMap = engine.GetMenu.GetNextLevel(3);
                    StartOnePlayerGame(engine, chooseMap);
                }
                else
                {
                    StartApp();
                }
            }
            else if (restartLevel)
            {
                StartOnePlayerGame(engine, chooseMap);
            }
            else
            {
                StartApp();
            }
        }

        /// <summary>
        /// Starts the tutorial.
        /// </summary>
        /// <param name="engine">The engine.</param>
        /// <param name="chooseMap">The choose map.</param>
        public void StartTutorial(Engine engine, string chooseMap)
        {
            // Start a game
            engine.StartGame(chooseMap); //map, player sprite
            engine.GetGUI.InitGame(true);

            short result = 1;
            spGui.Start();
            while (result == 1)
            {
                if (spGame.ElapsedMilliseconds >= 1000 / tps)
                {
                    // GameTick
                    result = engine.TutorialGameTick();
                    spGame.Restart();
                }

                if (spGui.ElapsedMilliseconds >= 1000 / fps)
                {
                    //GUI
                    engine.GetGUI.ShowTutorialMap();
                    spGui.Restart();
                }
            }

            //Restart game
            StartApp();

        }

        public void StartTwoPlayer(Engine engine)
        {
            // Choose Map                    
            engine.GetMenu.InitMapMenu();

            while (MapSelected == null)
            {
                if (spGame.ElapsedMilliseconds >= 1000 / tps)
                {
                    MapSelected = engine.GetMenu.GetMapMenu(EnableLevelTwo, EnableLevelThree);
                    spGame.Restart();
                }
            }

            if (MapSelected == "mainMenu")
            {
                StartApp();
            }
            else if (MapSelected == "tutorial")
            {
                StartTutorial(engine, engine.GetMenu.GetTutorialMap());
            }
            else
            {
                StartTwoPlayerGame(engine, MapSelected);
            }
        }

        /// <summary>
        /// Quits the specified close.
        /// </summary>
        /// <param name="close">if set to <c>true</c> [close].</param>
        /// <returns></returns>
        public bool Quit(bool close = false)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                close = true;
            }

            if (close && window != null)
            {
                window.Close();
            }

            return close;
        }
    }
}