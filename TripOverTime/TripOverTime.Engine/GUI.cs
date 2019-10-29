using System;
using System.Collections.Generic;
using System.Text;
using SFML.Window;
using SFML.Graphics;

namespace TripOverTime.EngineNamespace
{
    public class GUI
    {
        Engine _context;
        RenderWindow _window;
        VideoMode _videoMode;
        Dictionary<SFML.System.Vector2f, Sprite> _spritesDisplayed;
        SFML.Graphics.Sprite _background;
        private SFML.System.Vector2f _moveTheMapOf;

        internal GUI(Engine context)
        {
            _context = context;
            _videoMode = new VideoMode(800, 600);
            _window = new RenderWindow(_videoMode, "T.O.T");
            _window.SetVerticalSyncEnabled(true);
            _spritesDisplayed = new Dictionary<SFML.System.Vector2f, Sprite>();
            _background = new SFML.Graphics.Sprite();
            _moveTheMapOf = new SFML.System.Vector2f(0, 0);

            // Define function callled for events
            _window.KeyPressed += Window_KeyPressed;
        }

        public void ShowMap()
        {
            LoadMap();
            while(_window.IsOpen)
            {
                _window.Clear();
                // Background
                _window.Draw(_background);

                _window.DispatchEvents();

                // Load sprites
                foreach(KeyValuePair<SFML.System.Vector2f, Sprite> s in _spritesDisplayed)
                {
                    s.Value.GetSprite.Position = s.Key;
                    s.Value.GetSprite.Position += _moveTheMapOf;
                    _window.Draw(s.Value.GetSprite);
                }

                _window.Display();
            }
        }

        private void LoadMap()
        {
            if (!_window.IsOpen) throw new Exception("Window is not open!");

            // Set background
            Texture backgroundTexture = new Texture(_context.GetGame.GetMapObject.GetBackground);
            if (backgroundTexture == null) throw new Exception("Texture null!");
            backgroundTexture.Repeated = true;

            _background = new SFML.Graphics.Sprite(backgroundTexture);
            if (_background == null) throw new Exception("Sprite null!");

            _background.Position = new SFML.System.Vector2f(0, -(float)_videoMode.Height/2);
            _window.Draw(_background);


            Dictionary<Position, Sprite> map = _context.GetGame.GetMapObject.GetMap;

            foreach(KeyValuePair<Position, Sprite> s in map)
            {
                s.Value.GetSprite.Position = new SFML.System.Vector2f(s.Key.X*128, _videoMode.Height + s.Key.Y*-128); //128*128 = Size of a sprite
                _window.Draw(s.Value.GetSprite);
                _spritesDisplayed.Add(s.Value.GetSprite.Position, s.Value);
            }
        }

        /// <summary>
        /// Function called when a key is pressed
        /// </summary>
        private void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            var window = (Window)sender;

            switch(e.Code)
            {
                case Keyboard.Key.Escape:
                    window.Close();
                    break;
                case Keyboard.Key.Right:
                    if (_moveTheMapOf == new SFML.System.Vector2f(-128 * _context.GetGame.GetMapObject.GetLimitMax.X, 0)) Console.WriteLine("Border of the map");
                    else
                    {
                        //Map
                        _moveTheMapOf -= new SFML.System.Vector2f(128, 0);

                        //Player
                    }
                    break;
                case Keyboard.Key.Left:
                    if (_moveTheMapOf == new SFML.System.Vector2f(0, 0)) Console.WriteLine("Border of the map");
                    else
                    {
                        //Map
                        _moveTheMapOf += new SFML.System.Vector2f(128, 0);

                        //Player
                    }
                    break;
            }
        }

    }
}
