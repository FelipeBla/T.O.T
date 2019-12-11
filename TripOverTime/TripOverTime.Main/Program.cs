using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using TripOverTime.EngineNamespace;
using static System.Net.Mime.MediaTypeNames;

namespace TripOverTime.Main
{
    class Program
    {
        private static readonly EventHandler<KeyEventArgs> OnKeyPressedV;
        static Dictionary<int, SFML.Window.Keyboard.Key> _keyList;
        static SFML.Window.Keyboard.Key _jumpButton;
        static SFML.Window.Keyboard.Key _rightButton;
        static SFML.Window.Keyboard.Key _leftButton;
        static SFML.Window.Keyboard.Key _attackButton;



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

            //Window & Engine start
            RenderWindow window = new RenderWindow(new VideoMode(Settings.XResolution, Settings.YResolution), "T.O.T");
            window.SetVerticalSyncEnabled(true);
            Engine engine = new Engine(window);

            //Menu
            while (!engine.Close) //GAMELOOP MASTER
            {
                System.Threading.Thread.Sleep(200); // Evite certains bug
                //engine = new Engine(window); // Redemarre le jeu
                short choose = -2;
                short chooseSettings = -2;
                short chooseResolution = -2;
                short chooseFPS = -2;
                short chooseKB = -2;
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
                        int NbOfKeys = 34;

                        Dictionary<int, SFML.Window.Keyboard.Key> _keyList = new Dictionary<int, SFML.Window.Keyboard.Key>();
                        // add key list to the dictionnary
                        _keyList.Add(1, Keyboard.Key.Numpad0);
                        _keyList.Add(2, Keyboard.Key.A);
                        _keyList.Add(3, Keyboard.Key.B);
                        _keyList.Add(4, Keyboard.Key.C);
                        _keyList.Add(5, Keyboard.Key.D);
                        _keyList.Add(6, Keyboard.Key.E);
                        _keyList.Add(7, Keyboard.Key.F);
                        _keyList.Add(8, Keyboard.Key.G);
                        _keyList.Add(9, Keyboard.Key.H);
                        _keyList.Add(10, Keyboard.Key.I);
                        _keyList.Add(11, Keyboard.Key.J);
                        _keyList.Add(12, Keyboard.Key.K);
                        _keyList.Add(13, Keyboard.Key.L);
                        _keyList.Add(14, Keyboard.Key.M);
                        _keyList.Add(15, Keyboard.Key.N);
                        _keyList.Add(16, Keyboard.Key.O);
                        _keyList.Add(17, Keyboard.Key.P);
                        _keyList.Add(18, Keyboard.Key.Q);
                        _keyList.Add(19, Keyboard.Key.R);
                        _keyList.Add(20, Keyboard.Key.S);
                        _keyList.Add(21, Keyboard.Key.T);
                        _keyList.Add(22, Keyboard.Key.U);
                        _keyList.Add(23, Keyboard.Key.V);
                        _keyList.Add(24, Keyboard.Key.W);
                        _keyList.Add(25, Keyboard.Key.X);
                        _keyList.Add(26, Keyboard.Key.Y);
                        _keyList.Add(27, Keyboard.Key.Z);
                        _keyList.Add(28, Keyboard.Key.Left);
                        _keyList.Add(29, Keyboard.Key.Right);
                        _keyList.Add(30, Keyboard.Key.Up);
                        _keyList.Add(31, Keyboard.Key.Down);
                        _keyList.Add(32, Keyboard.Key.Num0);
                        _keyList.Add(33, Keyboard.Key.Num1);
                        _keyList.Add(34, Keyboard.Key.Num2);



                        engine.GetSettings.StartSettingsKB();
                        do
                        {
                            chooseKB = engine.GetSettings.RunSettingsKB();
                        }
                        while (chooseKB == -3);
                        if (chooseKB == 0) //Jump
                        {
                            IsPressed = false;
                            while (IsPressed == false)
                            {
                                i = 1;
                                while (i < NbOfKeys)
                                {
                                    if (Keyboard.IsKeyPressed(_keyList[i]))
                                    {
                                        GUI.JumpAction = _keyList[i];
                                        if(GUI.JumpAction == _keyList[i])
                                        {
                                            IsPressed = true;
                                        }
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
                            while (IsPressed == false)
                            {
                                i = 1;
                                while (i < NbOfKeys)
                                {
                                    if (Keyboard.IsKeyPressed(_keyList[i]))
                                    {
                                        GUI.LeftAction = _keyList[i];
                                        if (GUI.LeftAction == _keyList[i])
                                        {
                                            IsPressed = true;
                                        }
                                    }
                                    i++;
                                }
                            }
                            window.Close();
                            engine.Close = true;
                            RunAgain();
                        }
                        else if (chooseKB == -1) //Right
                        {
                            while (IsPressed == false)
                            {
                                i = 1;
                                while (i < NbOfKeys)
                                {
                                    if (Keyboard.IsKeyPressed(_keyList[i]))
                                    {
                                        GUI.RightAction = _keyList[i];
                                        if (GUI.RightAction == _keyList[i])
                                        {
                                            IsPressed = true;
                                        }
                                    }
                                    i++;
                                }
                            }
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
