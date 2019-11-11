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
        SFML.Graphics.Sprite _hpBar;
        SFML.Graphics.Sprite _hpBar2;
        SFML.Graphics.Sprite _hpBar3;
        private SFML.System.Vector2f _hpBarPosition;

        internal GUI(Engine context)
        {
            _context = context;
            _videoMode = new VideoMode(800, 600);
            _window = new RenderWindow(_videoMode, "T.O.T");
            _window.SetVerticalSyncEnabled(true);
            _spritesDisplayed = new Dictionary<SFML.System.Vector2f, Sprite>();
            _background = new SFML.Graphics.Sprite();
            _moveTheMapOf = new SFML.System.Vector2f(0, 0);
            _hpBar = new SFML.Graphics.Sprite();
            _hpBarPosition = new SFML.System.Vector2f(0, 0);
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

            // Lifebar

            _window.Draw(_hpBar);
            _window.Draw(_hpBar2);
            _window.Draw(_hpBar3);

            // Player
            if (_context.GetGame.GetPlayer.IsAlive)
            {
                _context.GetGame.GetPlayer.GetPlayerSprite.GetSprite.Position = new SFML.System.Vector2f(_context.GetGame.GetPlayer.Position.X * 128, _videoMode.Height + _context.GetGame.GetPlayer.Position.Y * -128 - 56);
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

            _background.Position = new SFML.System.Vector2f(0, -(float)_videoMode.Height / 2);
            _window.Draw(_background);

            // Set lifeBar
            Texture lifebarTexture = new Texture(_context.GetGame.GetMapObject.GetLifeBar);
            if (lifebarTexture == null) throw new Exception("Texture null!");

            _hpBar = new SFML.Graphics.Sprite(lifebarTexture);
            if (_hpBar == null) throw new Exception("Sprite null!");

            _hpBar.Position = new SFML.System.Vector2f(0, 0);
            _window.Draw(_hpBar);

            //lifebar 2
            _hpBar2 = new SFML.Graphics.Sprite(lifebarTexture);
            if (_hpBar2 == null) throw new Exception("Sprite null!");

            _hpBar2.Position = new SFML.System.Vector2f(100, 0);
            _window.Draw(_hpBar2);

            //lifebar 3
            _hpBar3 = new SFML.Graphics.Sprite(lifebarTexture);
            if (_hpBar3 == null) throw new Exception("Sprite null!");

            _hpBar3.Position = new SFML.System.Vector2f(200, 0);
            _window.Draw(_hpBar3);


            Dictionary<Position, Sprite> map = _context.GetGame.GetMapObject.GetMap;

            foreach (KeyValuePair<Position, Sprite> s in map)
            {
                s.Value.GetSprite.Position = new SFML.System.Vector2f(s.Key.X * 128, _videoMode.Height + s.Key.Y * -128); //128*128 = Size of a sprite
                _window.Draw(s.Value.GetSprite);
                _spritesDisplayed.Add(s.Value.GetSprite.Position, s.Value);
            }
        }

        internal void Events()
        {


            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape)) {
                _window.Close();
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right)) {
                _moveTheMapOf += _context.GetGame.GetPlayer.MoveRight(_videoMode);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left)) {
                _moveTheMapOf -= _context.GetGame.GetPlayer.MoveLeft(_videoMode);
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
