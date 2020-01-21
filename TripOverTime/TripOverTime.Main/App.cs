using SFML.Graphics;
using SFML.Window;
using System;
using System.Diagnostics;
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
                MapEditor.Run(window);
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
                choose = -2;
                MapSelected = null;
                StartApp();
            }
            else if(MapSelected == "tutorial")
            {
                StartTutorial(engine, engine.GetMenu.GetTutorialMap());
            }
            else
            {
                StartGame(engine, MapSelected);
            }  
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="engine">The engine.</param>
        /// <exception cref="Exception">WTF?!</exception>
        public void StartGame(Engine engine, string chooseMap)
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
                if (!loadNextLevel)
                {
                    choose = -2;
                    MapSelected = null;
                    StartApp();
                }
                else
                {
                    if (!EnableLevelTwo)
                    {
                        EnableLevelTwo = true;
                        chooseMap = engine.GetMenu.GetNextLevel(2);
                        Console.WriteLine("EnableLevelTwo and chooseMap: " + chooseMap);
                        StartGame(engine, chooseMap);
                    }
                    else if (!EnableLevelThree)
                    {
                        EnableLevelThree = true;
                        chooseMap = engine.GetMenu.GetNextLevel(3);
                        Console.WriteLine("EnableLevelThree and chooseMap: " + chooseMap);
                        StartGame(engine, chooseMap);
                    }
                    else
                    {
                        choose = -2;
                        MapSelected = null;
                        StartApp();
                    }                       
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

                bool restartLevel = engine.DieMenu();
                if (restartLevel)
                {
                    Console.WriteLine("******** Restart level: ");
                    Console.WriteLine("chooseMap: " + chooseMap);
                    StartGame(engine, chooseMap);
                }
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
            choose = -2;
            MapSelected = null;
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
                choose = -2;
                MapSelected = null;
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