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
        const ushort MAX_LINES = 5;
        const ushort MAX_LINES2 = 4;

        RenderWindow _window;
        ushort _selected;
        Text[] _lines;
        Text[] _lines2;
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
            _lines2 = new Text[MAX_LINES2];
        }

        public void StartMainMenu()
        {
            //Background
            // Set background
            _background = new SFML.Graphics.Sprite(new Texture(@"..\..\..\..\Assets\Backgrounds\time-travel-background.png"));
            if (_background == null) throw new Exception("Sprite null!");

            //_background.Scale = new SFML.System.Vector2f(_window.Size.X / 1920, _window.Size.Y / 1080);
            _window.Draw(_background);

            //Lines
            _lines[0] = new Text("1 Player", _font, _charSize);
            _lines[1] = new Text("2 Player", _font, _charSize);
            _lines[2] = new Text("Map Editor", _font, _charSize);
            _lines[3] = new Text("Settings", _font, _charSize);
            _lines[4] = new Text("Quit", _font, _charSize);

            for (int i = 0; i < MAX_LINES; i++)
            {
                _lines[i].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_lines[i].GetGlobalBounds().Width) / 2, (_window.Size.Y / 7) * (i+1));
            }
        }

        public short RunMainMenu()
        {
            //Events
            short result = -2;
            short tampon = 0;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                _window.Close();
                result = -5;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
            {
                if (_selected == 4) result = -5;
                else result = (short)_selected;

                while (Keyboard.IsKeyPressed(Keyboard.Key.Enter)) ; //tampon
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
                RectangleShape r = null;

                if (i == _selected)
                {
                    r = new RectangleShape(new SFML.System.Vector2f(_lines[i].GetGlobalBounds().Width * 1.5f + 20, _lines[i].GetGlobalBounds().Height * 2 + 20));
                    r.Position = new SFML.System.Vector2f(_window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (_window.Size.Y / 7) * (i + 1) - (_lines[i].GetGlobalBounds().Height / 3) - 10);
                    r.FillColor = Color.Black;
                    _lines[i].FillColor = Color.White;
                }
                else
                {
                    r = new RectangleShape(new SFML.System.Vector2f(_lines[i].GetGlobalBounds().Width * 1.5f, _lines[i].GetGlobalBounds().Height * 2));
                    r.Position = new SFML.System.Vector2f(_window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (_window.Size.Y / 7) * (i + 1) - (_lines[i].GetGlobalBounds().Height / 3));
                    r.FillColor = Color.White;
                    _lines[i].FillColor = Color.Black;
                }

                _window.Draw(r);
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
            _background = new SFML.Graphics.Sprite(new Texture(@"..\..\..\..\Assets\Backgrounds\time-travel-background.png"));
            if (_background == null) throw new Exception("Sprite null!");

            _window.Draw(_background);

            //Lines
            for (int i = 0; i < MAX_LINES2 - 1; i++)
            {
                _lines2[i] = new Text(StringBetweenString(_maps[i], @"..\..\..\..\Maps\", ".totmap"), _font, _charSize);
            }

            _lines2[MAX_LINES2 - 1] = new Text("MAP EDITED", _font, _charSize);

            for (int i = 0; i < MAX_LINES2; i++)
            {
                _lines2[i].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (_lines2[i].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * (i + 1));
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
                if (_selected == MAX_LINES2-1) //Edited map
                {
                    result = "quit"; //temporaire
                }
                else
                {
                    result = _maps[_selected];
                }
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && _selected < MAX_LINES2 - 1)
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

            for (int i = 0; i < MAX_LINES2; i++)
            {
                RectangleShape r = null;
                if (i == _selected)
                {
                    r = new RectangleShape(new SFML.System.Vector2f(_lines2[i].GetGlobalBounds().Width * 1.5f + 20, _lines2[i].GetGlobalBounds().Height * 2 + 20));
                    r.Position = new SFML.System.Vector2f(_window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * (i + 1) - (_lines2[i].GetGlobalBounds().Height / 3) - 10);
                    r.FillColor = Color.Black;
                    _lines2[i].Color = Color.White;
                }
                else
                {
                    r = new RectangleShape(new SFML.System.Vector2f(_lines2[i].GetGlobalBounds().Width * 1.5f, _lines2[i].GetGlobalBounds().Height * 2));
                    r.Position = new SFML.System.Vector2f(_window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * (i + 1) - (_lines2[i].GetGlobalBounds().Height / 3));
                    r.FillColor = Color.White;
                    _lines2[i].Color = Color.Black;
                }
                _window.Draw(r);
                _window.Draw(_lines2[i]);
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
