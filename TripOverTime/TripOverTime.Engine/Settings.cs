using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace TripOverTime.EngineNamespace
{
    public class Settings
    {
        const ushort MAX_LINES = 4;
        const ushort MAX_LINES_KB = 5;
        private static uint _XResolution = 800;
        private static uint _YResolution = 600;
        private static uint _NbFPS = 60;

        RenderWindow _window;
        ushort _selected;
        ushort _selectedResolution;
        ushort _selectedFPS;
        ushort _selectedKB;
        Text[] _lines;
        Text[] _linesKB;
        SFML.Graphics.Sprite _background;
        uint _charSize = 32;
        Engine _context;

        internal Settings(Engine engine, RenderWindow window)
        {
            if (window == null) throw new ArgumentException("Window is null");

            _context = engine;
            _window = window;
            _selected = 0;
            _selectedResolution = 0;
            _selectedFPS = 0;
            _selectedKB = 0;
            _lines = new Text[MAX_LINES];
            _linesKB = new Text[MAX_LINES_KB];

        }

        public void StartSettings()
        {
            //Background
            // Set background
            _background = new SFML.Graphics.Sprite(new Texture(@"..\..\..\..\Assets\Backgrounds\colored_desert.png"));
            if (_background == null) throw new Exception("Sprite null!");

            _background.Scale = new SFML.System.Vector2f(_window.Size.X / 550, _window.Size.Y / 550);
            _window.Draw(_background);

            //Lines
            _lines[0] = new Text("Resolution", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _lines[1] = new Text("FPS", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _lines[2] = new Text("Key Binding", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _lines[3] = new Text("Return", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);

            _lines[0].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_lines[0].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * 1);
            _lines[1].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_lines[1].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * 2);
            _lines[2].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_lines[2].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * 3);
            _lines[3].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_lines[3].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * 4);

            _window.Display();
        }

        public void StartSettingsResolution()
        {
            //Background
            // Set background
            _background = new SFML.Graphics.Sprite(new Texture(@"..\..\..\..\Assets\Backgrounds\colored_desert.png"));
            if (_background == null) throw new Exception("Sprite null!");

            _background.Scale = new SFML.System.Vector2f(_window.Size.X / 550, _window.Size.Y / 550);
            _window.Draw(_background);

            //Lines
            _lines[0] = new Text("800x600", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _lines[1] = new Text("1280 x 720", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _lines[2] = new Text("1920 x 1080 ", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _lines[3] = new Text("Return", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);

            _lines[0].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_lines[0].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * 1);
            _lines[1].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_lines[1].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * 2);
            _lines[2].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_lines[2].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * 3);
            _lines[3].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_lines[3].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * 4);

            _window.Display();
        }

        public void StartSettingsFPS()
        {
            //Background
            // Set background
            _background = new SFML.Graphics.Sprite(new Texture(@"..\..\..\..\Assets\Backgrounds\colored_desert.png"));
            if (_background == null) throw new Exception("Sprite null!");

            _background.Scale = new SFML.System.Vector2f(_window.Size.X / 550, _window.Size.Y / 550);
            _window.Draw(_background);

            //Lines
            _lines[0] = new Text("30", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _lines[1] = new Text("60", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _lines[2] = new Text("120", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _lines[3] = new Text("Return", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);

            _lines[0].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_lines[0].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * 1);
            _lines[1].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_lines[1].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * 2);
            _lines[2].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_lines[2].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * 3);
            _lines[3].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_lines[3].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * 4);

            _window.Display();
        }

        public void StartSettingsKB()
        {
            //Background
            // Set background
            _background = new SFML.Graphics.Sprite(new Texture(@"..\..\..\..\Assets\Backgrounds\colored_desert.png"));
            if (_background == null) throw new Exception("Sprite null!");

            _background.Scale = new SFML.System.Vector2f(_window.Size.X / 550, _window.Size.Y / 550);
            _window.Draw(_background);

            //Lines
            _linesKB[0] = new Text("Jump : " + _context.GetGUI.JumpAction, new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _linesKB[1] = new Text("Left : " + _context.GetGUI.LeftAction, new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _linesKB[2] = new Text("Right : " + _context.GetGUI.RightAction, new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _linesKB[3] = new Text("Attack : " + _context.GetGUI.AttackAction, new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _linesKB[4] = new Text("Return", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);

            for (int i = 0; i < MAX_LINES_KB; i++)
            {
                _linesKB[i].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_linesKB[i].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * i);
            }

            _window.Display();
        }
        public void RunSettings()
        {
            bool returnMain = false;
            do
            {
                StartSettings();
                //Events
                ushort tampon = 0;
                short choose = -3;
                Thread.Sleep(100);
                do
                {
                    if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                    {
                        choose = 3;
                    }
                    else if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
                    {
                        choose = (short)_selected;
                    }
                    else if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && _selected < MAX_LINES - 1)
                    {
                        _selected++;
                        tampon = 1;
                    }
                    else if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && _selected > 0)
                    {
                        _selected--;
                        tampon = 2;
                    }

                    //Graphics
                    _window.Clear();
                    _window.Draw(_background);

                    for (int i = 0; i < MAX_LINES; i++)
                    {
                        if (i == _selected)
                        {
                            _lines[i].Color = Color.Red;
                        }
                        else
                        {
                            _lines[i].Color = Color.Black; //meilleure visibilité
                        }
                        _window.Draw(_lines[i]);
                    }

                    _window.Display();

                    if (tampon == 1)
                    {
                        tampon = 0;
                        while (Keyboard.IsKeyPressed(Keyboard.Key.Down)) ; //Tampon
                    }
                    else if (tampon == 2)
                    {
                        tampon = 0;
                        while (Keyboard.IsKeyPressed(Keyboard.Key.Up)) ; //Tampon
                    }

                } while (choose == -3);

                switch (choose)
                {
                    case 2:
                        //keybinding
                        StartSettingsKB();
                        RunSettingsKB();
                        break;
                    case 0:
                        //resolution
                        StartSettingsResolution();
                        RunSettingsResolution();
                        break;
                    case 1:
                        //fps
                        StartSettingsFPS();
                        RunSettingsFPS();
                        break;
                    case 3:
                        //quit
                        returnMain = true;
                        break;
                }
            } while (!returnMain);
        }

        public void RunSettingsResolution()
        {
            short choose = -3;
            ushort tampon = 0;
            Thread.Sleep(100);
            do
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    choose = 3;
                    while (Keyboard.IsKeyPressed(Keyboard.Key.Escape)) Thread.Sleep(1);
                }

                else if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
                {
                    choose = (short)_selectedResolution;
                    while (Keyboard.IsKeyPressed(Keyboard.Key.Enter)) Thread.Sleep(1);
                }

                else if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && _selectedResolution < MAX_LINES - 1)
                {
                    _selectedResolution++;
                    tampon = 1;
                }
                else if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && _selectedResolution > 0)
                {
                    _selectedResolution--;
                    tampon = 2;
                }

                //Graphics
                _window.Clear();
                _window.Draw(_background);

                for (int i = 0; i < MAX_LINES; i++)
                {
                    if (i == _selectedResolution)
                    {
                        _lines[i].Color = Color.Red;
                    }
                    else
                    {
                        _lines[i].Color = Color.Black;
                    }
                    _window.Draw(_lines[i]);
                }
                _window.Display();

                if (tampon == 1)
                {
                    tampon = 0;
                    while (Keyboard.IsKeyPressed(Keyboard.Key.Down)) ; //Tampon
                }
                else if (tampon == 2)
                {
                    tampon = 0;
                    while (Keyboard.IsKeyPressed(Keyboard.Key.Up)) ; //Tampon
                }

            } while (choose == -3);

            switch (choose)
            {
                case 2:
                    //1920*1080
                    _XResolution = 1920;
                    _YResolution = 1080;
                    restart();
                    break;
                case 0:
                    //800*600
                    _XResolution = 800;
                    _YResolution = 600;
                    restart();
                    break;
                case 1:
                    //1280*720
                    _XResolution = 1280;
                    _YResolution = 720;
                    restart();
                    break;
                case 3:
                    //quit (return to settings main)

                    break;
            }

        }

        public void RunSettingsFPS()
        {
            Thread.Sleep(100);
            short choose = -3;
            short tampon = 0;
            do
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    choose = 3;
                }

                else if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
                {
                    choose = (short)_selectedFPS;
                }

                else if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && _selectedFPS < MAX_LINES - 1)
                {
                    tampon = 1;
                    _selectedFPS++;
                }
                else if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && _selectedFPS > 0)
                {
                    tampon = 2;
                    _selectedFPS--;
                }


                //Graphics
                _window.Clear();
                _window.Draw(_background);

                for (int i = 0; i < MAX_LINES; i++)
                {
                    if (i == _selectedFPS)
                    {
                        _lines[i].Color = Color.Red;
                    }
                    else
                    {
                        _lines[i].Color = Color.Black;
                    }
                    _window.Draw(_lines[i]);
                }

                _window.Display();

                if (tampon == 1)
                {
                    tampon = 0;
                    while (Keyboard.IsKeyPressed(Keyboard.Key.Down)) ; //Tampon
                }
                else if (tampon == 2)
                {
                    tampon = 0;
                    while (Keyboard.IsKeyPressed(Keyboard.Key.Up)) ; //Tampon
                }
            } while (choose == -3);

            switch (choose)
            {
                case 0:
                    //30fps
                    _NbFPS = 30;
                    break;
                case 1:
                    //60fps
                    _NbFPS = 60;
                    break;
                case 2:
                    //120fps
                    _NbFPS = 120;
                    break;
                case 3:
                    //quit (return to settings main)

                    break;
            }


        }
        public void RunSettingsKB()
        {
            short choose = -3;
            do
            {
                StartSettingsKB();
                //Events
                choose = -3;
                short tampon = 0;
                Thread.Sleep(100);
                do
                {

                    if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                    {
                        choose = 4;
                    }
                    else if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
                    {
                        choose = (short)_selectedKB;
                    }
                    else if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && _selectedKB < MAX_LINES_KB - 1)
                    {
                        tampon = 1;
                        _selectedKB++;
                    }
                    else if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && _selectedKB > 0)
                    {
                        tampon = 2;
                        _selectedKB--;
                    }

                    //Graphics
                    _window.Clear();
                    _window.Draw(_background);

                    for (int i = 0; i < MAX_LINES_KB; i++)
                    {
                        if (i == _selectedKB)
                        {
                            _linesKB[i].Color = Color.Red;
                        }
                        else
                        {
                            _linesKB[i].Color = Color.Black;
                        }
                        _window.Draw(_linesKB[i]);
                    }

                    _window.Display();

                    if (tampon == 1)
                    {
                        tampon = 0;
                        while (Keyboard.IsKeyPressed(Keyboard.Key.Down)) ; //Tampon
                    }
                    else if (tampon == 2)
                    {
                        tampon = 0;
                        while (Keyboard.IsKeyPressed(Keyboard.Key.Up)) ; //Tampon
                    }
                } while (choose == -3);

                if (choose != 4)
                {
                    _linesKB[choose].Color = Color.Blue;
                    _window.Clear();
                    _window.Draw(_background);
                    for (int i = 0; i < MAX_LINES_KB; i++)
                    {
                        _window.Draw(_linesKB[i]);
                    }
                    _window.Display();

                    //Clear event buffer
                    Thread.Sleep(100);
                    _window.KeyReleased += (s, a) =>
                    {
                        Console.WriteLine("Clear " + a.Code);
                    };

                    _window.DispatchEvents();
                    bool again = true;
                    // Get Key
                    _window.KeyPressed += (s, a) =>
                    {
                        Console.WriteLine("Key : " + a.Code);
                        if (a.Code != Keyboard.Key.Enter)
                        {
                            switch (choose)
                            {
                                case 0:
                                    //Jump
                                    _context.GetGUI.JumpAction = a.Code;
                                    break;
                                case 1:
                                    //Left
                                    _context.GetGUI.LeftAction = a.Code;
                                    break;
                                case 2:
                                    //Right
                                    _context.GetGUI.RightAction = a.Code;
                                    break;
                                case 3:
                                    //Attack
                                    _context.GetGUI.AttackAction = a.Code;
                                    break;
                            }
                            again = false;
                        }
                    };
                    while (again) _window.WaitAndDispatchEvents();
                }
                //Re-while
            } while (choose != 4);

        }

        private void restart() //apply changes
        {

        }

        public static uint XResolution
        {
            get { return _XResolution; }
            set { _XResolution = value; }
        }
        public static uint YResolution
        {
            get { return _YResolution; }
            set { _YResolution = value; }
        }
        public static uint NbFPS
        {
            get { return _NbFPS; }
            set { _NbFPS = value; }
        }

    }

}

