using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    public class Menu
    {
        const ushort MAX_LINES = 4;

        RenderWindow _window;
        ushort _selected;
        Text[] _lines;
        SFML.Graphics.Sprite _background;
        uint _charSize = 32;
        string[] _maps;
        Font _font;


        internal Menu(RenderWindow window)
        {
            if (window == null) throw new ArgumentException("Window is null");

            _window = window;
            _selected = 0;
            _font = new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf");
            _lines = new Text[MAX_LINES];
        }

        public void StartMainMenu()
        {
            //Background
            // Set background
            _background = new SFML.Graphics.Sprite(new Texture(@"..\..\..\..\Assets\Backgrounds\blue_grass.png"));
            if (_background == null) throw new Exception("Sprite null!");

            _background.Scale = new SFML.System.Vector2f(_window.Size.X / 550, _window.Size.Y / 550);
            _window.Draw(_background);

            //Lines
            _lines[0] = new Text("Start", _font, _charSize);
            _lines[1] = new Text("Settings", _font, _charSize);
            _lines[2] = new Text("Quit", _font, _charSize);
            _lines[0] = new Text("1 Player", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _lines[1] = new Text("2 Player", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _lines[2] = new Text("Settings", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _lines[3] = new Text("Quit", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);

            _lines[0].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_lines[0].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * 1);
            _lines[1].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_lines[1].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * 2);
            _lines[2].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_lines[2].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * 3);
            _lines[3].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_lines[2].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * 4);

            _window.Display();
        }

        public short RunMainMenu()
        {
            //Events
            short result = -2;
            short tampon = 0;

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
        public void InitMapMenu()
        {
            _selected = 0;

            // Get all maps
            _maps = Directory.GetFiles(@"..\..\..\..\Maps\", "*.totmap");

            // Init menu
            //Background
            // Set background
            _background = new SFML.Graphics.Sprite(new Texture(@"..\..\..\..\Assets\Backgrounds\blue_grass.png"));
            if (_background == null) throw new Exception("Sprite null!");

            _background.Scale = new SFML.System.Vector2f(_window.Size.X / 550, _window.Size.Y / 550);
            _window.Draw(_background);

            //Lines
            for (int i = 0; i < _maps.Length; i++)
            {
                _lines[i] = new Text(StringBetweenString(_maps[i], @"..\..\..\..\Maps\", ".totmap"), _font, _charSize);
            }

            for (int i = 0; i < _lines.Length; i++)
            {
                _lines[i].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_lines[i].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * (i + 1));
            }

            _window.Display();

        }

        public string ChooseMapMenu()
        {
            //Run menu
            //Events
            string result = "null";
            short tampon = 0;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                result = "quit";
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
            {
                result = _maps[_selected];
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

        private string StringBetweenString(string original, string str1, string str2)
        {
            int firstStringPosition = original.IndexOf(str1);
            int secondStringPosition = original.IndexOf(str2);
            return original.Substring(firstStringPosition + str1.Length, secondStringPosition - str1.Length);
        }
    }
}
