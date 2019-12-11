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
        SFML.Graphics.Sprite _hpBar;
        Texture _lifebarTexture;
        static SFML.Window.Keyboard.Key _LeftAction = Keyboard.Key.Left;
        static SFML.Window.Keyboard.Key _RightAction = Keyboard.Key.Right;
        static SFML.Window.Keyboard.Key _JumpAction = Keyboard.Key.Up;
        static SFML.Window.Keyboard.Key _AttackAction = Keyboard.Key.Space;


        internal GUI(Engine context, RenderWindow window)
        {
            _context = context;
            _window = window;
            _spritesDisplayed = new Dictionary<SFML.System.Vector2f, Sprite>();
            _background = new SFML.Graphics.Sprite();
            _moveTheMapOf = new SFML.System.Vector2f(0, 0);
            _hpBar = new SFML.Graphics.Sprite();
        }


        public void InitGame()
        {
            LoadMap();
        }

        public void ShowMap()
        {
            if (!_window.IsOpen) _context.Close = true;

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
            _hpBar.TextureRect = new IntRect(new SFML.System.Vector2i(0, 0), new SFML.System.Vector2i((int)_lifebarTexture.Size.X - ((int)_lifebarTexture.Size.X / 100) * (_context.GetGame.GetPlayer.GetLife.GetMaxPoint() - _context.GetGame.GetPlayer.GetLife.GetCurrentPoint()), (int)_lifebarTexture.Size.Y));
            _window.Draw(_hpBar);

            // Player
            if (_context.GetGame.GetPlayer.IsAlive)
            {
                _context.GetGame.GetPlayer.GetPlayerSprite.GetSprite.Position = new SFML.System.Vector2f(_context.GetGame.GetPlayer.Position.X * 128, _window.Size.Y + _context.GetGame.GetPlayer.Position.Y * -128 -65);
                _window.Draw(_context.GetGame.GetPlayer.GetPlayerSprite.GetSprite);
            }

            // Monsters
            foreach (Monster m in _context.GetGame.GetMonsters) 
            {
                m.GetMonsterSprite.GetSprite.Position = new SFML.System.Vector2f(m.Position.X * 128, _window.Size.Y + m.Position.Y * -128 -40);
                m.GetMonsterSprite.GetSprite.Position -= _moveTheMapOf;                
                _window.Draw(m.GetMonsterSprite.GetSprite);
            }

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

            _background.Scale = new SFML.System.Vector2f(_window.Size.X / 550, _window.Size.Y / 550);
            _window.Draw(_background);

            // Set lifeBar
            _lifebarTexture = new Texture(_context.GetGame.GetMapObject.GetLifeBar);
            if (_lifebarTexture == null) throw new Exception("Texture null!");

            _hpBar = new SFML.Graphics.Sprite(_lifebarTexture);
            if (_hpBar == null) throw new Exception("Sprite null!");
            _hpBar.TextureRect = new IntRect(new SFML.System.Vector2i(0, 0), new SFML.System.Vector2i((int)_lifebarTexture.Size.X - ((int)_lifebarTexture.Size.X/100) * (_context.GetGame.GetPlayer.GetLife.GetMaxPoint() - _context.GetGame.GetPlayer.GetLife.GetCurrentPoint()), (int)_lifebarTexture.Size.Y));
            _window.Draw(_hpBar);

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
                //_context.GetGame.GetPlayer.GetLife.CurrentPoint = 0; // TEMPORARYYYYYYYYYYYYYYYYYY
            }
            if (Keyboard.IsKeyPressed(_RightAction)) {
                _moveTheMapOf += _context.GetGame.GetPlayer.MoveRight((float)_window.Size.X);
            }
            if (Keyboard.IsKeyPressed(_LeftAction)) {
                _moveTheMapOf -= _context.GetGame.GetPlayer.MoveLeft((float)_window.Size.X);
            }
            if (Keyboard.IsKeyPressed(_JumpAction)) {
                if (_context.GetGame.GetPlayer.RealPosition.Y < _context.GetGame.GetMapObject.GetLimitMax.Y && _context.GetGame.GetPlayer.IsOnTheGround)
                {
                    _context.GetGame.GetPlayer.Jump();
                }
            }

            if (Keyboard.IsKeyPressed(_AttackAction)) // ATTACK
            {
                _context.GetGame.GetPlayer.IsAttack = true;
                _context.GetGame.GetPlayer.Attack();
                foreach (Monster m in _context.GetGame.GetMonsters)
                {
                    if (m.Position.X + 2 > _context.GetGame.GetPlayer.RealPosition.X && m.Position.X - 2 < _context.GetGame.GetPlayer.RealPosition.X)
                    {
                        m.life.DecreasedPoint(_context.GetGame.GetPlayer.GetAttack);
                    }
                }
            }

            if (!Keyboard.IsKeyPressed(_LeftAction) && !Keyboard.IsKeyPressed(_RightAction) && !Keyboard.IsKeyPressed(_JumpAction) && !Keyboard.IsKeyPressed(_AttackAction))
            {
                _context.GetGame.GetPlayer.GetPlayerSprite.DefaultAnimation();
            }

            // RIEN A FAIRE ICI
            foreach (Monster m in _context.GetGame.GetMonsters)
            {
                if (!m.isAlive)
                {
                    m.MonsterDead();
                }
                else if (m.Position.X - 6 < _context.GetGame.GetPlayer.RealPosition.X && m.Position.X - 1 > _context.GetGame.GetPlayer.RealPosition.X) //left
                {
                    m.Orientation = "left";
                    m.MonsterMove();
                }
                else if (m.Position.X + 6 > _context.GetGame.GetPlayer.RealPosition.X && m.Position.X + 1 < _context.GetGame.GetPlayer.RealPosition.X) //right
                {
                    m.Orientation = "right";
                    m.MonsterMove();
                }
            }
        }
        public static SFML.Window.Keyboard.Key RightAction
        {
            get { return _RightAction; }
            set { _RightAction = value; }
        }

        public static SFML.Window.Keyboard.Key LeftAction
        {
            get { return _LeftAction; }
            set { _LeftAction = value; }
        }

        public static SFML.Window.Keyboard.Key JumpAction
        {
            get { return _JumpAction; }
            set { _JumpAction = value; }
        }

        internal static SFML.Window.Keyboard.Key AttackAction
        {
            get { return _AttackAction; }
            set { _AttackAction = value; }
        }
    }
}
