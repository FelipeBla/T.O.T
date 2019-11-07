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
        Dictionary<SFML.System.Vector2f, Sprite> _spritesDisplayed;
        SFML.Graphics.Sprite _background;
        private SFML.System.Vector2f _moveTheMapOf;

        internal GUI(Engine context, RenderWindow window)
        {
            _context = context;
            _window = window;
            _spritesDisplayed = new Dictionary<SFML.System.Vector2f, Sprite>();
            _background = new SFML.Graphics.Sprite();
            _moveTheMapOf = new SFML.System.Vector2f(0, 0);
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
                _context.GetGame.GetPlayer.GetPlayerSprite.GetSprite.Position = new SFML.System.Vector2f(_context.GetGame.GetPlayer.Position.X * 128, _window.Size.Y + _context.GetGame.GetPlayer.Position.Y * -128 - 56);
                Console.WriteLine("Real X: " + _context.GetGame.GetPlayer.RealPosition.X + " X:" + _context.GetGame.GetPlayer.Position.X + " | Real Y: " + _context.GetGame.GetPlayer.RealPosition.Y + " Y: " + _context.GetGame.GetPlayer.Position.Y);
                _window.Draw(_context.GetGame.GetPlayer.GetPlayerSprite.GetSprite);
            }

            // Monsters

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

            _background.Position = new SFML.System.Vector2f(0, -(float)_window.Size.Y / 2);
            _window.Draw(_background);


            Dictionary<Position, Sprite> map = _context.GetGame.GetMapObject.GetMap;

            foreach (KeyValuePair<Position, Sprite> s in map)
            {
                s.Value.GetSprite.Position = new SFML.System.Vector2f(s.Key.X * 128, _window.Size.Y + s.Key.Y * -128); //128*128 = Size of a sprite
                _window.Draw(s.Value.GetSprite);
                _spritesDisplayed.Add(s.Value.GetSprite.Position, s.Value);
            }
        }

        internal void Events()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape)) {
                _context.GetGame.GetPlayer.GetLife.CurrentPoint = 0; // TEMPORARYYYYYYYYYYYYYYYYYY
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right)) {
                _moveTheMapOf += _context.GetGame.GetPlayer.MoveRight((float)_window.Size.X);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left)) {
                _moveTheMapOf -= _context.GetGame.GetPlayer.MoveLeft((float)_window.Size.X);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up)) {
                if (_context.GetGame.GetPlayer.RealPosition.Y < _context.GetGame.GetMapObject.GetLimitMax.Y)
                {
                    _context.GetGame.GetPlayer.Jump();
                }
            }            
        }
    }
}
