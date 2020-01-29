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
        const ushort MAX_LINES = 5;
        const ushort MAX_LINES_KB = 5;
        const ushort MAX_LINES_MULTIPLAYER = 3;
        private static uint _XResolution = 1920;
        private static uint _YResolution = 1080;
        private static uint _NbFPS = 60;
        private static int _MultiplayerOrNot = 0;
        private static bool _fullscreen = false;

        RenderWindow _window;
        ushort _selected;
        ushort _selectedResolution;
        ushort _selectedFPS;
        ushort _selectedKB;
        Text[] _lines;
        Text[] _linesKB;
        Text[] _linesMultiplayer;
        SFML.Graphics.Sprite _background;
        uint _charSize = 32;
        Engine _context;
        bool _applyNeed;

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
            _linesMultiplayer = new Text[MAX_LINES_MULTIPLAYER];
            // Set background
            _background = new SFML.Graphics.Sprite(new Texture(@"..\..\..\..\Assets\Backgrounds\time-travel-background.png"));
            if (_background == null) throw new Exception("Sprite null!");
        }

        public void StartSettings()
        {
            //Background
            _window.Draw(_background);

            //Lines
            _lines = new Text[5];
            _lines[0] = new Text("Resolution", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _lines[1] = new Text("FPS", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _lines[2] = new Text("Key Binding Player 1", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _lines[3] = new Text("Key Binding Player 2", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _lines[4] = new Text("Return", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);

            for (int i = 0; i < 5; i++)
            {
                _lines[i].FillColor = Color.White;
                _lines[i].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_lines[i].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * (i + 1));
            }

            _window.Display();
        }
        public void StartSettingsMultiplayer()
        {
            //Background
            _window.Draw(_background);

            //Lines
            _lines = new Text[3];

            _lines[0] = new Text("1 Player", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _lines[1] = new Text("2 Player", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _lines[2] = new Text("Return", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);

            _lines[0].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_lines[0].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * 1);
            _lines[1].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_lines[1].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * 2);
            _lines[2].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_lines[2].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * 3);

            _window.Display();
        }

        public void StartSettingsResolution()
        {
            //Background
            _window.Draw(_background);

            //Lines
            _lines = new Text[5];

            _lines[0] = new Text("800x600", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _lines[1] = new Text("1280 x 720", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _lines[2] = new Text("1920 x 1080 ", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _lines[3] = new Text("Fullscreen", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _lines[4] = new Text("Return", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);

            _lines[0].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_lines[0].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * 1);
            _lines[1].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_lines[1].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * 2);
            _lines[2].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_lines[2].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * 3);
            _lines[3].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_lines[3].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * 4);
            _lines[4].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_lines[3].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * 5);

            if (_fullscreen)
            {
                _lines[3].Color = Color.Green;
            }
            else
            {
                _lines[3].Color = Color.Red;
            }

            _window.Display();
        }

        public void StartSettingsFPS()
        {
            //Background
            _window.Draw(_background);

            //Lines
            _lines = new Text[4];

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
            _window.Draw(_background);

            //Lines
            _linesKB[0] = new Text("Jump : " + _context.GetGUI.JumpAction, new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _linesKB[1] = new Text("Left : " + _context.GetGUI.LeftAction, new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _linesKB[2] = new Text("Right : " + _context.GetGUI.RightAction, new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _linesKB[3] = new Text("Attack : " + _context.GetGUI.AttackAction, new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _linesKB[4] = new Text("Return", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);

            for (int i = 0; i < MAX_LINES_KB; i++)
            {
                _linesKB[i].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_linesKB[i].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * (i+1));
            }

            _window.Display();
        }

        public void StartSettingsKB2()
        {
            //Background
            _window.Draw(_background);

            //Lines
            _linesKB[0] = new Text("Jump : " + _context.GetGUI.JumpAction2, new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _linesKB[1] = new Text("Left : " + _context.GetGUI.LeftAction2, new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _linesKB[2] = new Text("Right : " + _context.GetGUI.RightAction2, new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _linesKB[3] = new Text("Attack : " + _context.GetGUI.AttackAction2, new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _linesKB[4] = new Text("Return", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);

            for (int i = 0; i < MAX_LINES_KB; i++)
            {
                _linesKB[i].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_linesKB[i].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * (i+1));
            }

            _window.Display();
        }
        public bool RunSettings()
        {
            bool returnMain = false;
            _applyNeed = false;
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

                    for (int i = 0; i < _lines.Length; i++)
                    {
                        RectangleShape r = new RectangleShape(new SFML.System.Vector2f(_lines[i].GetGlobalBounds().Width + 20, _lines[i].GetGlobalBounds().Height + 20));
                        r.Position = new SFML.System.Vector2f(_lines[i].Position.X - 10, _lines[i].Position.Y - 5);

                        if (i == _selected)
                        {
                            r.FillColor = Color.Black;
                            _lines[i].FillColor = Color.White;
                        }
                        else
                        {
                            r.FillColor = Color.White;
                            _lines[i].FillColor = Color.Black;
                        }
                        _window.Draw(r);
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
                        //resolution
                        StartSettingsResolution();
                        RunSettingsResolution();
                        break;
                    case 1:
                        //fps
                        StartSettingsFPS();
                        RunSettingsFPS();
                        break;
                    case 2:
                        //keybinding player 2
                        StartSettingsKB();
                        RunSettingsKB();
                        break;
                    case 3:
                        //keybinding player 2
                        StartSettingsKB2();
                        RunSettingsKB2();
                        break;
                    case 4:
                        //quit
                        returnMain = true;
                        break;
                }
            } while (!returnMain);

            string settingsFile = _fullscreen + "\n" + _NbFPS + "\n" + _XResolution + "\n" + _YResolution;
            System.IO.File.WriteAllText("settings", settingsFile);

            return _applyNeed;
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
                    choose = 4;
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

                for (int i = 0; i < _lines.Length; i++)
                {
                    RectangleShape r = new RectangleShape(new SFML.System.Vector2f(_lines[i].GetGlobalBounds().Width + 20, _lines[i].GetGlobalBounds().Height + 20));
                    r.Position = new SFML.System.Vector2f(_lines[i].Position.X - 10, _lines[i].Position.Y - 5);
                    
                    if (i == _selectedResolution)
                    {
                        r.FillColor = Color.Black;
                        _lines[i].FillColor = Color.White;
                    }
                    else
                    {
                        r.FillColor = Color.White;
                        _lines[i].FillColor = Color.Black;
                    }

                    if (i == 3)
                    {
                        if (_fullscreen)
                        {
                            _lines[3].FillColor = Color.Green;
                        }
                        else
                        {
                            _lines[3].FillColor = Color.Red;
                        }
                    }

                    _window.Draw(r);
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
                    //fullscreen
                    _fullscreen = !_fullscreen;
                    restart();
                    break;
                    //4 quit (return to settings main)
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

                for (int i = 0; i < _lines.Length; i++)
                {
                    RectangleShape r = new RectangleShape(new SFML.System.Vector2f(_lines[i].GetGlobalBounds().Width + 20, _lines[i].GetGlobalBounds().Height + 20));
                    r.Position = new SFML.System.Vector2f(_lines[i].Position.X - 10, _lines[i].Position.Y - 5);

                    if (i == _selectedFPS)
                    {
                        r.FillColor = Color.Black;
                        _lines[i].FillColor = Color.White;
                    }
                    else
                    {
                        r.FillColor = Color.White;
                        _lines[i].FillColor = Color.Black;
                    }
                    _window.Draw(r);
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

                    for (int i = 0; i < _linesKB.Length; i++)
                    {
                        RectangleShape r = new RectangleShape(new SFML.System.Vector2f(_linesKB[i].GetGlobalBounds().Width + 20, _linesKB[i].GetGlobalBounds().Height + 20));
                        r.Position = new SFML.System.Vector2f(_linesKB[i].Position.X - 10, _linesKB[i].Position.Y - 5);

                        if (i == _selectedKB)
                        {
                            r.FillColor = Color.Black;
                            _linesKB[i].FillColor = Color.White;
                        }
                        else
                        {
                            r.FillColor = Color.White;
                            _linesKB[i].FillColor = Color.Black;
                        }
                        _window.Draw(r);
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
                    _window.Clear();
                    _window.Draw(_background);

                    for (int i = 0; i < _linesKB.Length; i++)
                    {
                        RectangleShape r = new RectangleShape(new SFML.System.Vector2f(_linesKB[i].GetGlobalBounds().Width + 20, _linesKB[i].GetGlobalBounds().Height + 20));
                        r.Position = new SFML.System.Vector2f(_linesKB[i].Position.X - 10, _linesKB[i].Position.Y - 5);

                        if (i == _selectedKB)
                        {
                            r.FillColor = Color.Black;
                            _linesKB[i].FillColor = Color.White;
                        }
                        else
                        {
                            r.FillColor = Color.White;
                            _linesKB[i].FillColor = Color.Black;
                        }

                        _linesKB[choose].FillColor = Color.Blue;

                        _window.Draw(r);
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

        public void RunSettingsKB2()
        {
            short choose = -3;
            do
            {
                StartSettingsKB2();
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

                    for (int i = 0; i < _linesKB.Length; i++)
                    {
                        RectangleShape r = new RectangleShape(new SFML.System.Vector2f(_linesKB[i].GetGlobalBounds().Width + 20, _linesKB[i].GetGlobalBounds().Height + 20));
                        r.Position = new SFML.System.Vector2f(_linesKB[i].Position.X - 10, _linesKB[i].Position.Y - 5);

                        if (i == _selectedKB)
                        {
                            r.FillColor = Color.Black;
                            _linesKB[i].FillColor = Color.White;
                        }
                        else
                        {
                            r.FillColor = Color.White;
                            _linesKB[i].FillColor = Color.Black;
                        }
                        _window.Draw(r);
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
                    _window.Clear();
                    _window.Draw(_background);

                    for (int i = 0; i < _linesKB.Length; i++)
                    {
                        RectangleShape r = new RectangleShape(new SFML.System.Vector2f(_linesKB[i].GetGlobalBounds().Width + 20, _linesKB[i].GetGlobalBounds().Height + 20));
                        r.Position = new SFML.System.Vector2f(_linesKB[i].Position.X - 10, _linesKB[i].Position.Y - 5);

                        if (i == _selectedKB)
                        {
                            r.FillColor = Color.Black;
                            _linesKB[i].FillColor = Color.White;
                        }
                        else
                        {
                            r.FillColor = Color.White;
                            _linesKB[i].FillColor = Color.Black;
                        }

                        _linesKB[choose].FillColor = Color.Blue;

                        _window.Draw(r);
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
                                    _context.GetGUI.JumpAction2 = a.Code;
                                    break;
                                case 1:
                                    //Left
                                    _context.GetGUI.LeftAction2 = a.Code;
                                    break;
                                case 2:
                                    //Right
                                    _context.GetGUI.RightAction2 = a.Code;
                                    break;
                                case 3:
                                    //Attack
                                    _context.GetGUI.AttackAction2 = a.Code;
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
            _applyNeed = true;
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

        public static bool Fullscreen
        {
            get => _fullscreen;
            set => _fullscreen = value;
        }
        public static int MultiplayerOrNot
        {
            get { return _MultiplayerOrNot; }
            set { _MultiplayerOrNot = value; }
        }
    }

}

