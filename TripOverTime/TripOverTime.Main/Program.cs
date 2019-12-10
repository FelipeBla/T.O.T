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
        private static readonly EventHandler<KeyEventArgs> OnKeyPressedV;
        SFML.Window.Keyboard.Key _jumpButton;
        SFML.Window.Keyboard.Key _rightButton;
        SFML.Window.Keyboard.Key _leftButton;
        SFML.Window.Keyboard.Key _attackButton;
 

        static void Main(string[] args)
        {
            RunAgain();
        }

        private static void RunAgain()
        {
            Console.WriteLine("Graphics tests !");

            // To manage FramePS and TicksPS
            Stopwatch spGui = new Stopwatch();
            Stopwatch spGame = new Stopwatch();
            float fps = Settings.NbFPS;
            float tps = 60;
            int _jumpButton = 5;

            //Window & Engine start
            RenderWindow window = new RenderWindow(new VideoMode(Settings.XResolution, Settings.YResolution), "T.O.T");
            window.SetVerticalSyncEnabled(true);
            Engine engine = new Engine(window);

            //Menu
            while (!engine.Close) //GAMELOOP MASTER
            {
                short choose = -2;
                short chooseSettings = -2;
                short chooseResolution = -2;
                short chooseFPS = -2;
                short chooseKB = -2;
                engine.GetMenu.StartMainMenu();
                do
                {
                    choose = engine.GetMenu.RunMainMenu();
                } while (choose == -2);


                if (choose == 0) //Lauch GAME
                {
                    // Start a game
                    engine.StartGame(@"..\..\..\..\Maps\test2.totmap", @"..\..\..\..\Assets\Players\Variable sizes\Pink"); //map, player sprite
                    engine.GetGUI.InitGame();

                    short result = 1;
                    // GameLoop
                    spGui.Start();
                    spGame.Start();
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
                    }
                    else if (result == -1)
                    {
                        //DIE
                        Console.WriteLine("YOU DIE!");
                    }
                    else
                    {
                        throw new Exception("WTF?!");
                    }
                }
                else if (choose == 1) //Settings
                {
                    choose = 1;
                    engine.GetSettings.StartSettings();
                    do
                    {
                        chooseSettings = engine.GetSettings.RunSettings();
                    } while (chooseSettings == -2);
                    if (chooseSettings == 0) //resolution
                    {
                        choose = 1;
                        chooseSettings = 0;
                        engine.GetSettings.StartSettingsResolution();
                            do
                            {
                                chooseResolution = engine.GetSettings.RunSettingsResolution();
                            }
                            while (chooseResolution == -3);
                            if (chooseResolution == 0) //800x600
                            {
                                Settings.XResolution = 800;
                                Settings.YResolution = 600;
                                window.Close();
                                engine.Close = true;
                                RunAgain();
                            }
                            else if (chooseResolution == 1) //1280 x 720
                            {
                                Settings.XResolution = 1280;
                                Settings.YResolution = 720;
                                window.Close();
                                engine.Close = true;
                                RunAgain();
                        }
                            else if (chooseResolution == -1) //1920 x 1080 
                            {
                                Settings.XResolution = 1920;
                                Settings.YResolution = 1080;
                                window.Close();
                                engine.Close = true;
                                RunAgain();
                        }
                            else if (chooseResolution == -2)//back to main menu
                            {
                                chooseSettings = -2;
                            }
                    }
                    else if (chooseSettings == 1) //FPS
                    {
                        choose = 1;
                        chooseSettings = 0;
                        engine.GetSettings.StartSettingsFPS();
                        do
                        {
                            chooseFPS = engine.GetSettings.RunSettingsFPS();
                        }
                        while (chooseFPS == -3);
                        if (chooseFPS == 0) //30
                        {
                            Settings.NbFPS = 30;
                            window.Close();
                            engine.Close = true;
                            RunAgain();
                        }
                        else if (chooseFPS == 1) //60
                        {
                            Settings.NbFPS = 60;
                            window.Close();
                            engine.Close = true;
                            RunAgain();
                        }
                        else if (chooseFPS == -1) //120
                        {
                            Settings.NbFPS = 120;
                            window.Close();
                            engine.Close = true;
                            RunAgain();
                        }
                        else if (chooseFPS == -2)//back to main menu
                        {
                            chooseSettings = -2;
                        }
                    }
                    else if (chooseSettings == -1) //KeyBinding
                    {
                        choose = 1;
                        chooseSettings = 0;
                        int i = 1;
                        bool IsPressed = false;
                        int NbOfKeys = 35;

                        engine.GetSettings.StartSettingsKB();
                        do
                        {
                            chooseKB = engine.GetSettings.RunSettingsKB();
                        }
                        while (chooseKB == -3);
                        if (chooseKB == 0) //Jump
                        {
                            while(IsPressed = false)
                            {
                                while (i < NbOfKeys)
                                {
                                    if (Keyboard.IsKeyPressed(list[i]))
                                    {
                                        _jumpAction = list[i];
                                    }
                                    i++;
                              }
                            }
                            window.Close();
                            engine.Close = true;
                            RunAgain();
                        }
                        else if (chooseKB == 1) //Left
                        {
                            window.Close();
                            engine.Close = true;
                            RunAgain();
                        }
                        else if (chooseKB == -1) //Right
                        {
                            window.Close();
                            engine.Close = true;
                            RunAgain();
                        }
                        else if (chooseKB == -2)//back to main menu
                        {
                            chooseSettings = -2;
                        }
                    }
                    else if (chooseSettings == -2)//back to main menu
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

        public void OnKeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
            _jumpButton = e.Code;
            Console.WriteLine("wesh c bon");
        }

    }
}
