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


        public void InitGame()
        {
            LoadMap();
        }

        public void ShowMap()
        {
            if (!_window.IsOpen) _context.CLOSE = true;

            _window.Clear();

            // Background
            _window.Draw(_background);

            // Load map
            foreach (KeyValuePair<SFML.System.Vector2f, Sprite> s in _spritesDisplayed)
            {
                s.Value.GetSprite.Position = s.Key;
                s.Value.GetSprite.Position -= _moveTheMapOf;
                _window.Draw(s.Value.GetSprite);
            }

            // Player
            if (_context.GetGame.GetPlayer.IsAlive)
            {
                _context.GetGame.GetPlayer.GetPlayerSprite.GetSprite.Position = new SFML.System.Vector2f(_context.GetGame.GetPlayer.Position.X * 128, _context.GetGame.GetPlayer.Position.Y * 128);
                _window.Draw(_context.GetGame.GetPlayer.GetPlayerSprite.GetSprite);
            }

            // Monsters

            // Events
            _window.DispatchEvents();

            // Display
            _window.Display();

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

            _background.Position = new SFML.System.Vector2f(0, -(float)_videoMode.Height / 2);
            _window.Draw(_background);


            Dictionary<Position, Sprite> map = _context.GetGame.GetMapObject.GetMap;

            foreach (KeyValuePair<Position, Sprite> s in map)
            {
                s.Value.GetSprite.Position = new SFML.System.Vector2f(s.Key.X * 128, _videoMode.Height + s.Key.Y * -128); //128*128 = Size of a sprite
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

            switch (e.Code)
            {
                case Keyboard.Key.Escape:
                    window.Close();
                    break;
                case Keyboard.Key.Right:
                    if (_moveTheMapOf == new SFML.System.Vector2f(-128 * _context.GetGame.GetMapObject.GetLimitMax.X, 0) && _context.GetGame.GetPlayer.Position.X >= _context.GetGame.GetMapObject.GetLimitMax.X) Console.WriteLine("Border of the map");
                    else
                    {
                        // If player centered on screen, move the map now and not the player
                        if( _context.GetGame.GetPlayer.GetPlayerSprite.GetSprite.Position.X < _videoMode.Width/2)
                        {
                            // Player move
                            _context.GetGame.GetPlayer.Position.X += 0.2f;
                        }
                        else
                        {
                            //Map move
                            _moveTheMapOf += new SFML.System.Vector2f(128, 0);
                        }
                    }
                    break;
                case Keyboard.Key.Left:
                    if (_moveTheMapOf == new SFML.System.Vector2f(0, 0) && _context.GetGame.GetPlayer.Position.X <= _context.GetGame.GetMapObject.GetLimitMin.X) Console.WriteLine("Border of the map");
                    else
                    {
                        // If player left on screen, move the map now and not the player
                        if (_context.GetGame.GetPlayer.GetPlayerSprite.GetSprite.Position.X > _videoMode.Width / 5)
                        {
                            // Player move
                            _context.GetGame.GetPlayer.Position.X -= 0.2f;
                        }
                        else
                        {
                            //Map move
                            _moveTheMapOf -= new SFML.System.Vector2f(128, 0);
                        }

                    }
                    break;
                case Keyboard.Key.Up:
                    if(_context.GetGame.GetPlayer.Position.Y < _context.GetGame.GetMapObject.GetLimitMax.Y)
                    {
                        _context.GetGame.GetPlayer.Position.Y -= 1;
                    }
                    
                    break;
            }
        }

    }
}
