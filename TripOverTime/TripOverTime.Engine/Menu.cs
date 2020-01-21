using SFML.Graphics;
using SFML.Window;
using System;
using System.IO;

namespace TripOverTime.EngineNamespace
{
    public class Menu
    {
        const ushort MAX_LINES = 5;
        const ushort MAX_LINES2 = 5;

        RenderWindow _window;
        int _selected;
        private int MapSelected { get; set; }
        Text[] MenuList;
        Text[] MapList;
        SFML.Graphics.Sprite _background;
        uint _charSize = 32;
        string[] _maps;
        Font _font;

        internal Menu(RenderWindow window)
        {
            if (window == null) throw new ArgumentException("Window is null");

            _window = window;
            _selected = 0;
            MapSelected = 0;
            _font = new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf");
            MenuList = new Text[MAX_LINES];
            MapList = new Text[MAX_LINES2];
        }

        /// <summary>
        /// Starts the main menu.
        /// </summary>
        /// <exception cref="Exception">Sprite null!</exception>
        public void StartMainMenu()
        {
            // Set background
            _background = new SFML.Graphics.Sprite(new Texture(@"..\..\..\..\Assets\Backgrounds\time-travel-background.png"));
            if (_background == null) throw new Exception("Sprite null!");

            //_background.Scale = new SFML.System.Vector2f(_window.Size.X / 1920, _window.Size.Y / 1080);
            _window.Draw(_background);

            //Lines
            MenuList[0] = new Text("1 Player", _font, _charSize);
            MenuList[1] = new Text("2 Player", _font, _charSize);
            MenuList[2] = new Text("Map Editor", _font, _charSize);
            MenuList[3] = new Text("Settings", _font, _charSize);
            MenuList[4] = new Text("Quit", _font, _charSize);

            for (int i = 0; i < MAX_LINES; i++)
            {
                MenuList[i].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (MenuList[i].GetGlobalBounds().Width) / 2, (_window.Size.Y / 7) * (i+1));
            }
        }

        /// <summary>
        /// Gets the main menu.
        /// </summary>
        /// <returns></returns>
        public short GetMainMenu()
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
            GetMainMenuUI();

            if (tampon == 1)
            {
                while (Keyboard.IsKeyPressed(Keyboard.Key.Down)); //Tampon
            }
            else if (tampon == 2)
            {
                while (Keyboard.IsKeyPressed(Keyboard.Key.Up)) ; //Tampon
            }

            return result;
        }

        /// <summary>
        /// Gets the main menu UI.
        /// </summary>
        private void GetMainMenuUI()
        {
            //Graphics
            _window.Clear();
            _window.Draw(_background);

            for (int i = 0; i < MAX_LINES; i++)
            {
                RectangleShape r = null;

                if (i == _selected)
                {
                    r = new RectangleShape(new SFML.System.Vector2f(MenuList[i].GetGlobalBounds().Width * 1.5f + 20, MenuList[i].GetGlobalBounds().Height * 2 + 20));
                    r.Position = new SFML.System.Vector2f(_window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (_window.Size.Y / 7) * (i + 1) - (MenuList[i].GetGlobalBounds().Height / 3) - 10);
                    r.FillColor = Color.Black;
                    MenuList[i].FillColor = Color.White;
                }
                else
                {
                    r = new RectangleShape(new SFML.System.Vector2f(MenuList[i].GetGlobalBounds().Width * 1.5f, MenuList[i].GetGlobalBounds().Height * 2));
                    r.Position = new SFML.System.Vector2f(_window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (_window.Size.Y / 7) * (i + 1) - (MenuList[i].GetGlobalBounds().Height / 3));
                    r.FillColor = Color.White;
                    MenuList[i].FillColor = Color.Black;
                }

                _window.Draw(r);
                _window.Draw(MenuList[i]);
            }

            _window.Display();
        }

        /// <summary>
        /// Initializes the map menu.
        /// </summary>
        /// <exception cref="Exception">Sprite null!</exception>
        public void InitMapMenu()
        {
            MapSelected = 0;

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
                MapList[i] = new Text(StringBetweenString(_maps[i], @"..\..\..\..\Maps\", ".totmap"), _font, _charSize);
            }

            MapList[MAX_LINES2 - 1] = new Text("MAP EDITED", _font, _charSize);

            for (int i = 0; i < MAX_LINES2; i++)
            {
                MapList[i].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (MapList[i].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * (i + 1));
            }

            _window.Display();
        }

        public string GetMapMenu(bool EnableLevelTwo = false, bool EnableLevelThree = false)
        {
            //Run menu
            //Events
            string result = null;
            short tampon = 0;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                result = "mainMenu";
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
            {
                if (MapSelected == MAX_LINES2-1) //Edited map
                {
                    result = "mainMenu"; //temporaire
                }
                else if (MapSelected == 0)
                {
                    result = "tutorial";
                }
                else
                {
                    result = _maps[MapSelected];
                }
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && MapSelected < MAX_LINES2 - 1)
            {
                tampon = 1;

                if(MapSelected < 1)
                {
                    MapSelected++;
                }
                else if(MapSelected == 1 && EnableLevelTwo)
                {
                    MapSelected = 2;
                }else if (MapSelected == 2 && EnableLevelThree)
                {
                    MapSelected = 3;
                }else if (MapSelected >= 3)
                {
                    MapSelected++;
                }
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && MapSelected > 0)
            {
                tampon = 2;
                MapSelected--;
            }

            //Get Map Menu UI
            GetMapMenuUI(EnableLevelTwo, EnableLevelThree);

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

        /// <summary>
        /// Gets the map menu UI.
        /// </summary>
        private void GetMapMenuUI(bool EnableLevelTwo, bool EnableLevelThree)
        {
            //Graphics
            _window.Clear();
            _window.Draw(_background);

            for (int i = 0; i < MAX_LINES2; i++)
            {
                RectangleShape r = null;
                if (i == MapSelected)
                {
                    r = new RectangleShape(new SFML.System.Vector2f(MapList[i].GetGlobalBounds().Width * 1.5f + 20, MapList[i].GetGlobalBounds().Height * 2 + 20));
                    r.Position = new SFML.System.Vector2f(_window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * (i + 1) - (MapList[i].GetGlobalBounds().Height / 3) - 10);
                    r.FillColor = Color.Black;
                    MapList[i].Color = Color.White;
                }
                else if((i == 2 && !EnableLevelTwo) || (i == 3 && !EnableLevelThree))
                {
                    r = new RectangleShape(new SFML.System.Vector2f(MapList[i].GetGlobalBounds().Width * 1.5f, MapList[i].GetGlobalBounds().Height * 2));
                    r.Position = new SFML.System.Vector2f(_window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * (i + 1) - (MapList[i].GetGlobalBounds().Height / 3));
                    r.FillColor = Color.Transparent;
                    MapList[i].Color = Color.Black;
                }
                else
                {
                    r = new RectangleShape(new SFML.System.Vector2f(MapList[i].GetGlobalBounds().Width * 1.5f, MapList[i].GetGlobalBounds().Height * 2));
                    r.Position = new SFML.System.Vector2f(_window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * (i + 1) - (MapList[i].GetGlobalBounds().Height / 3));
                    r.FillColor = Color.White;
                    MapList[i].Color = Color.Black;
                }

                

                _window.Draw(r);
                _window.Draw(MapList[i]);
            }

            _window.Display();
        }

        private string StringBetweenString(string original, string str1, string str2)
        {
            int firstStringPosition = original.IndexOf(str1);
            int secondStringPosition = original.IndexOf(str2);
            return original.Substring(firstStringPosition + str1.Length, secondStringPosition - str1.Length);
        }

        /// <summary>
        /// Gets the next level.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns></returns>
        public string GetNextLevel(int level)
        {
            // Get all maps
            _maps = Directory.GetFiles(@"..\..\..\..\Maps\", "*.totmap");

            return _maps[level];
        }

        public string GetTutorialMap()
        {
            // Get all maps
            _maps = Directory.GetFiles(@"..\..\..\..\Maps\", "*.totmap");

            return _maps[0];
        }
    }
}
