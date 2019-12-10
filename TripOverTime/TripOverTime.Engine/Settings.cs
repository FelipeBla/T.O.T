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
        const ushort MAX_LINES_KB = 4;
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


        internal Settings(RenderWindow window)
        {
            if (window == null) throw new ArgumentException("Window is null");

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

            _lines[0].Position = new SFML.System.Vector2f(_window.Size.X / 2, (_window.Size.Y / 6) * 1);
            _lines[1].Position = new SFML.System.Vector2f(_window.Size.X / 2, (_window.Size.Y / 6) * 2);
            _lines[2].Position = new SFML.System.Vector2f(_window.Size.X / 2, (_window.Size.Y / 6) * 3);
            _lines[3].Position = new SFML.System.Vector2f(_window.Size.X / 2, (_window.Size.Y / 6) * 4);

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

            _lines[0].Position = new SFML.System.Vector2f(_window.Size.X / 2, (_window.Size.Y / 6) * 1);
            _lines[1].Position = new SFML.System.Vector2f(_window.Size.X / 2, (_window.Size.Y / 6) * 2);
            _lines[2].Position = new SFML.System.Vector2f(_window.Size.X / 2, (_window.Size.Y / 6) * 3);
            _lines[3].Position = new SFML.System.Vector2f(_window.Size.X / 2, (_window.Size.Y / 6) * 4);

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

            _lines[0].Position = new SFML.System.Vector2f(_window.Size.X / 2, (_window.Size.Y / 6) * 1);
            _lines[1].Position = new SFML.System.Vector2f(_window.Size.X / 2, (_window.Size.Y / 6) * 2);
            _lines[2].Position = new SFML.System.Vector2f(_window.Size.X / 2, (_window.Size.Y / 6) * 3);
            _lines[3].Position = new SFML.System.Vector2f(_window.Size.X / 2, (_window.Size.Y / 6) * 4);

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
            _lines[0] = new Text("Jump", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _lines[1] = new Text("Left", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _lines[2] = new Text("Right", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _lines[3] = new Text("Return", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);

            _lines[0].Position = new SFML.System.Vector2f(_window.Size.X / 2, (_window.Size.Y / 6) * 1);
            _lines[1].Position = new SFML.System.Vector2f(_window.Size.X / 2, (_window.Size.Y / 6) * 2);
            _lines[2].Position = new SFML.System.Vector2f(_window.Size.X / 2, (_window.Size.Y / 6) * 3);
            _lines[3].Position = new SFML.System.Vector2f(_window.Size.X / 2, (_window.Size.Y / 6) * 4);

            _window.Display();
        }
        public short RunSettings()
        {
            //Events
            short result = -2;
            short tampon = 0;

            Stopwatch TimerS = new Stopwatch();
            TimerS.Start();
            float tps = 60;
            var timeToSleep = 80;
            if (TimerS.Elapsed.TotalMilliseconds < timeToSleep)
                Thread.Sleep(timeToSleep);

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                _window.Close();
                result = -1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
            {
                if (_selected == 2) result = -1;
                else result = (short)_selected;
                
            }


            if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && _selected < MAX_LINES - 1)
            {
                tampon = 1;
                _selected++;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && _selected > 0)
            {
                tampon = 2;
                _selected--;
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
                while (Keyboard.IsKeyPressed(Keyboard.Key.Down)) ; //Tampon
            }
            else if (tampon == 2)
            {
                while (Keyboard.IsKeyPressed(Keyboard.Key.Up)) ; //Tampon
            }

            return result;
        }

        public short RunSettingsResolution()
        {
            Stopwatch TimerS = new Stopwatch();
            TimerS.Start();
            float tps = 60;
            var timeToSleep = 80;
            if (TimerS.Elapsed.TotalMilliseconds < timeToSleep)
                Thread.Sleep(timeToSleep);
            //Events
            short result = -3;
            short tampon = 0;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                _window.Close();
                result = -1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
            {
                if (_selectedResolution == 2) result = -1;
                else result = (short)_selectedResolution;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && _selectedResolution < MAX_LINES - 1)
            {
                tampon = 1;
                _selectedResolution++;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && _selectedResolution > 0)
            {
                tampon = 2;
                _selectedResolution--;
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
                while (Keyboard.IsKeyPressed(Keyboard.Key.Down)) ; //Tampon
            }
            else if (tampon == 2)
            {
                while (Keyboard.IsKeyPressed(Keyboard.Key.Up)) ; //Tampon
            }

            return result;
        }

        public short RunSettingsFPS()
        {
            Stopwatch TimerS = new Stopwatch();
            TimerS.Start();
            float tps = 60;
            var timeToSleep = 80;
            if (TimerS.Elapsed.TotalMilliseconds < timeToSleep)
                Thread.Sleep(timeToSleep);
            //Events
            short result = -3;
            short tampon = 0;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                _window.Close();
                result = -1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
            {
                if (_selectedFPS == 2) result = -1;
                else result = (short)_selectedFPS;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && _selectedFPS < MAX_LINES - 1)
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
                while (Keyboard.IsKeyPressed(Keyboard.Key.Down)) ; //Tampon
            }
            else if (tampon == 2)
            {
                while (Keyboard.IsKeyPressed(Keyboard.Key.Up)) ; //Tampon
            }

            return result;
        }
        public short RunSettingsKB()
        {
            Stopwatch TimerS = new Stopwatch();
            TimerS.Start();
            float tps = 60;
            var timeToSleep = 80;
            if (TimerS.Elapsed.TotalMilliseconds < timeToSleep)
                Thread.Sleep(timeToSleep);
            //Events
            short result = -3;
            short tampon = 0;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                _window.Close();
                result = -1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
            {
                if (_selectedKB == 2) result = -1;
                else result = (short)_selectedKB;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && _selectedKB < MAX_LINES - 1)
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

            for (int i = 0; i < MAX_LINES; i++)
            {
                if (i == _selectedKB)
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
                while (Keyboard.IsKeyPressed(Keyboard.Key.Down)) ; //Tampon
            }
            else if (tampon == 2)
            {
                while (Keyboard.IsKeyPressed(Keyboard.Key.Up)) ; //Tampon
            }

            return result;
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

