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

        internal GUI(Engine context)
        {
            _context = context;
            _videoMode = new VideoMode(800, 600);
            _window = new RenderWindow(_videoMode, "T.O.T");
            _window.SetVerticalSyncEnabled(true);
            _spritesDisplayed = new Dictionary<SFML.System.Vector2f, Sprite>();
            _background = new SFML.Graphics.Sprite();

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
                    _window.Draw(s.Value.GetSprite);
                }

                _window.Display();
            }
        }

        private void LoadMap()
        {
            if (!_window.IsOpen) throw new Exception("Window is not open!");

            // Set background
            Texture backgroundTexture = new Texture(_context.GetGame.GetMapObject.GetBackground, new IntRect(0, 0, 800, 600));
            if (backgroundTexture == null) throw new Exception("Texture null!");
            backgroundTexture.Repeated = true;

            _background = new SFML.Graphics.Sprite(backgroundTexture, new IntRect(0, 0, (int)_videoMode.Width, (int)_videoMode.Height) );
            if (_background == null) throw new Exception("Sprite null!");

            float xScale = (float)_videoMode.Width / (float)backgroundTexture.Size.X;
            float yScale = (float)_videoMode.Height / (float)backgroundTexture.Size.Y;

            _background.Scale = new SFML.System.Vector2f(xScale, yScale);
            //_background.Position = new SFML.System.Vector2f(100, 0);
            _window.Draw(_background);


            Dictionary<Position, Sprite> map = _context.GetGame.GetMapObject.GetMap;

            foreach(KeyValuePair<Position, Sprite> s in map)
            {
                s.Value.GetSprite.Position = new SFML.System.Vector2f(s.Key.X*128, _videoMode.Height/2 + s.Key.Y*-128); //128*128 = Size of a sprite
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
            if (e.Code == Keyboard.Key.Escape)
            {
                window.Close();
            }
        }

    }
}
