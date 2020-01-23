using System;
using System.Collections.Generic;
using System.Text;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace TripOverTime.EngineNamespace
{
    public class GUI
    {
        Engine _context;
        Engine _context2;
        RenderWindow _window;
        Dictionary<SFML.System.Vector2f, Sprite> _spritesDisplayed;
        Dictionary<SFML.System.Vector2f, Sprite> _spritesDisplayed2;
        SFML.Graphics.Sprite _background;
        private SFML.System.Vector2f _moveTheMapOf;
        private SFML.System.Vector2f _moveTheMapOf2;
        SFML.Graphics.Sprite _hpBar;
        SFML.Graphics.Sprite _hpBar2;
        SFML.Graphics.Sprite _rect1;
        Texture _lifebarTexture;
        static SFML.Window.Keyboard.Key _LeftAction = Keyboard.Key.Left;
        static SFML.Window.Keyboard.Key _RightAction = Keyboard.Key.Right;
        static SFML.Window.Keyboard.Key _JumpAction = Keyboard.Key.Up;
        static SFML.Window.Keyboard.Key _AttackAction = Keyboard.Key.Space;

        static SFML.Window.Keyboard.Key _LeftAction2 = Keyboard.Key.Q;
        static SFML.Window.Keyboard.Key _RightAction2 = Keyboard.Key.D;
        static SFML.Window.Keyboard.Key _JumpAction2 = Keyboard.Key.Z;
        static SFML.Window.Keyboard.Key _AttackAction2 = Keyboard.Key.E;


        internal GUI(Engine context, RenderWindow window)
        {
            _context = context;
            _context2 = context;
            _window = window;
            _spritesDisplayed = new Dictionary<SFML.System.Vector2f, Sprite>();
            _spritesDisplayed2 = new Dictionary<SFML.System.Vector2f, Sprite>();
            _background = new SFML.Graphics.Sprite();
            _moveTheMapOf = new SFML.System.Vector2f(0, 0);
            _moveTheMapOf2 = new SFML.System.Vector2f(0, 0);
            _hpBar = new SFML.Graphics.Sprite();
            _rect1 = new SFML.Graphics.Sprite();
        }


        public void InitGame()
        {
            LoadMap();
        }
        public void InitGame2()
        {
            LoadMap2();
        }
        public void ShowMapMultiplayer()
        {
            if (!_window.IsOpen) _context.Close = true;

            _window.Clear();
            //view player 1
            View view1 = new View(new Vector2f(Settings.XResolution/2, Settings.YResolution/2), new Vector2f(Settings.XResolution, Settings.YResolution));
            view1.Viewport = new FloatRect(0f, 0f, 1f, 0.5f);
            view1.Size = new Vector2f(Settings.XResolution, Settings.YResolution);
            _window.SetView(view1);
            Vector2f PositionScreen1 = new Vector2f(400, 300);

            // Background Player 1
            _window.Draw(_background);

            // Load map
            foreach (KeyValuePair<SFML.System.Vector2f, Sprite> s in _spritesDisplayed)
            {
                s.Value.GetSprite.Position = s.Key;
                s.Value.GetSprite.Position -= _moveTheMapOf;
                _window.Draw(s.Value.GetSprite);
            }

            // Lifebar
            _hpBar.TextureRect = new IntRect(0, 0, (int)(_lifebarTexture.Size.X * _context.GetGame.GetPlayer.GetLife.PerCent), (int)_lifebarTexture.Size.Y);
            // HP Text
            Text hp = new Text(Convert.ToString(_context.GetGame.GetPlayer.GetLife.CurrentPoint), _context.GetFont, 28);
            hp.Position = new SFML.System.Vector2f(_lifebarTexture.Size.X / 2 - hp.GetGlobalBounds().Width / 2, _lifebarTexture.Size.Y / 2 - hp.GetGlobalBounds().Height / 2 - 5);
            _window.Draw(_hpBar);
            _window.Draw(hp);

            // Player
            if (_context.GetGame.GetPlayer.IsAlive)
            {
                _context.GetGame.GetPlayer.GetPlayerSprite.GetSprite.Position = new SFML.System.Vector2f(_context.GetGame.GetPlayer.Position.X * 128, _window.Size.Y + _context.GetGame.GetPlayer.Position.Y * -128 -65);
                _window.Draw(_context.GetGame.GetPlayer.GetPlayerSprite.GetSprite);
            }

            // Monsters
            foreach (Monster m in _context.GetGame.GetMonsters)
            {
                //Orientation
                if (m.Orientation == "right" && m.GetMonsterSprite.GetSprite.Scale != new SFML.System.Vector2f(1.0f, 1.0f)) //Right
                {
                    m.GetMonsterSprite.GetSprite.Origin = new SFML.System.Vector2f(0, 0);
                    m.GetMonsterSprite.GetSprite.Scale = new SFML.System.Vector2f(1.0f, 1.0f);
                }
                else if (m.Orientation == "left" && m.GetMonsterSprite.GetSprite.Scale != new SFML.System.Vector2f(-1.0f, 1.0f))//Left
                {
                    m.GetMonsterSprite.GetSprite.Origin = new SFML.System.Vector2f(m.GetMonsterSprite.GetSprite.Texture.Size.X / 2, 0);
                    m.GetMonsterSprite.GetSprite.Scale = new SFML.System.Vector2f(-1.0f, 1.0f);
                }

                m.GetMonsterSprite.GetSprite.Position = new SFML.System.Vector2f(m.Position.X * 128, _window.Size.Y + m.Position.Y * -128 - 40);
                m.GetMonsterSprite.GetSprite.Position -= _moveTheMapOf;

                _window.Draw(m.GetMonsterSprite.GetSprite);
            }

            // Boss
            _context.GetGame.GetBoss.GetBossSprite.GetSprite.Position = new SFML.System.Vector2f(_context.GetGame.GetBoss.Position.X * 128, _window.Size.Y + _context.GetGame.GetBoss.Position.Y * -128 -205);
            _context.GetGame.GetBoss.GetBossSprite.GetSprite.Position -= _moveTheMapOf;
            _window.Draw(_context.GetGame.GetBoss.GetBossSprite.GetSprite);
            Console.WriteLine("BossPos: " + _context.GetGame.GetBoss.Position.X + ";" + _context.GetGame.GetBoss.Position.Y);

            //view player 2
            View view2 = new View(new Vector2f(Settings.XResolution/2, Settings.YResolution/2), new Vector2f(Settings.XResolution, Settings.YResolution));
            view2.Viewport = new FloatRect(0f, 0.5f, 1f, 0.5f);
            view2.Size = new Vector2f(Settings.XResolution, Settings.YResolution);
            Vector2f PositionScreen2 = new Vector2f(400, 300);
            _window.SetView(view2);


            // Background Player 2
            _window.Draw(_background);
            // Load map
            foreach (KeyValuePair<SFML.System.Vector2f, Sprite> s in _spritesDisplayed2)
            {
                s.Value.GetSprite2.Position = s.Key;
                s.Value.GetSprite2.Position -= _moveTheMapOf2;
                _window.Draw(s.Value.GetSprite2);

            }

            // Lifebar
            _hpBar.TextureRect = new IntRect(0, 0, (int)(_lifebarTexture.Size.X * _context2.GetGame2.GetPlayer2.GetLife2.PerCent2), (int)_lifebarTexture.Size.Y);
            // HP Text
            Text hp2 = new Text(Convert.ToString(_context2.GetGame2.GetPlayer2.GetLife2.CurrentPoint2), _context.GetFont, 28);
            hp2.Position = new SFML.System.Vector2f(_lifebarTexture.Size.X / 2 - hp.GetGlobalBounds().Width / 2, _lifebarTexture.Size.Y / 2 - hp.GetGlobalBounds().Height / 2 - 5);
            _window.Draw(_hpBar);
            _window.Draw(hp2);
            

            // Player
            if (_context2.GetGame2.GetPlayer2.IsAlive2)
            {
                _context2.GetGame2.GetPlayer2.GetPlayerSprite2.GetSprite2.Position = new SFML.System.Vector2f(_context2.GetGame2.GetPlayer2.Position2.X2 * 128, _window.Size.Y + _context2.GetGame2.GetPlayer2.Position2.Y2 * -128 - 65);
                _window.Draw(_context2.GetGame2.GetPlayer2.GetPlayerSprite2.GetSprite2);
            }

            // Monsters
            foreach (Monster m in _context2.GetGame2.GetMonsters2)
            {
                //Orientation
                if (m.Orientation2 == "right" && m.GetMonsterSprite2.GetSprite2.Scale != new SFML.System.Vector2f(1.0f, 1.0f)) //Right
                {
                    m.GetMonsterSprite2.GetSprite2.Origin = new SFML.System.Vector2f(0, 0);
                    m.GetMonsterSprite2.GetSprite2.Scale = new SFML.System.Vector2f(1.0f, 1.0f);
                }
                else if (m.Orientation2 == "left" && m.GetMonsterSprite2.GetSprite2.Scale != new SFML.System.Vector2f(-1.0f, 1.0f))//Left
                {
                    m.GetMonsterSprite2.GetSprite2.Origin = new SFML.System.Vector2f(m.GetMonsterSprite2.GetSprite2.Texture.Size.X / 2, 0);
                    m.GetMonsterSprite2.GetSprite2.Scale = new SFML.System.Vector2f(-1.0f, 1.0f);
                }

                m.GetMonsterSprite2.GetSprite2.Position = new SFML.System.Vector2f(m.Position2.X2 * 128, _window.Size.Y + m.Position2.Y2 * -128 - 40);
                m.GetMonsterSprite2.GetSprite2.Position -= _moveTheMapOf2;

                _window.Draw(m.GetMonsterSprite2.GetSprite2);
            }
            // Boss
            _context2.GetGame2.GetBoss2.GetBossSprite2.GetSprite2.Position = new SFML.System.Vector2f(_context2.GetGame2.GetBoss2.Position2.X2 * 128, _window.Size.Y + _context2.GetGame2.GetBoss2.Position2.Y2 * -128 - 205);
            _context2.GetGame2.GetBoss2.GetBossSprite2.GetSprite2.Position -= _moveTheMapOf2;
            _window.Draw(_context2.GetGame2.GetBoss2.GetBossSprite2.GetSprite2);
            Console.WriteLine("BossPos: " + _context2.GetGame2.GetBoss2.Position2.X2 + ";" + _context2.GetGame2.GetBoss2.Position2.Y2);
            // Display
            _window.Display();

        }
        public void ShowMap()
        {
            if (!_window.IsOpen) _context.Close = true;

            _window.Clear();
            // Background Player 1
            _window.Draw(_background);

            // Load map
            foreach (KeyValuePair<SFML.System.Vector2f, Sprite> s in _spritesDisplayed)
            {
                s.Value.GetSprite.Position = s.Key;
                s.Value.GetSprite.Position -= _moveTheMapOf;
                _window.Draw(s.Value.GetSprite);
            }
            foreach (KeyValuePair<SFML.System.Vector2f, Sprite> s in _spritesDisplayed2)
            {
                s.Value.GetSprite2.Position = s.Key;
                s.Value.GetSprite2.Position -= _moveTheMapOf2;
                _window.Draw(s.Value.GetSprite2);
            }

            // Lifebar
            _hpBar.TextureRect = new IntRect(0, 0, (int)(_lifebarTexture.Size.X * _context.GetGame.GetPlayer.GetLife.PerCent), (int)_lifebarTexture.Size.Y);
            // HP Text
            _window.Draw(_hpBar);
            Text hp = new Text(Convert.ToString(_context.GetGame.GetPlayer.GetLife.CurrentPoint), _context.GetFont, 28);
            hp.Position = new SFML.System.Vector2f(_lifebarTexture.Size.X / 2 - hp.GetGlobalBounds().Width / 2, _lifebarTexture.Size.Y / 2 - hp.GetGlobalBounds().Height / 2 - 5);
            _window.Draw(hp);
           

            // Player
            if (_context.GetGame.GetPlayer.IsAlive)
            {
                //_context.GetGame.GetPlayer.GetPlayerSprite.GetSprite.Position = new SFML.System.Vector2f((_context.GetGame.GetPlayer.Position.X + 0.5f) * 128, _window.Size.Y + _context.GetGame.GetPlayer.Position.Y * -128 - 65);
                _context.GetGame.GetPlayer.GetPlayerSprite.GetSprite.Position = new SFML.System.Vector2f((_context.GetGame.GetPlayer.RealPosition.X) * 128, _window.Size.Y + _context.GetGame.GetPlayer.Position.Y * -128 - 65);
                _context.GetGame.GetPlayer.GetPlayerSprite.GetSprite.Position -= _moveTheMapOf;
                _window.Draw(_context.GetGame.GetPlayer.GetPlayerSprite.GetSprite);
            }

            // Monsters
            foreach (Monster m in _context.GetGame.GetMonsters)
            {
                //Orientation
                if (m.Orientation == "right" && m.GetMonsterSprite.GetSprite.Scale != new SFML.System.Vector2f(1.0f, 1.0f)) //Right
                {
                    m.GetMonsterSprite.GetSprite.Origin = new SFML.System.Vector2f(0, 0);
                    m.GetMonsterSprite.GetSprite.Scale = new SFML.System.Vector2f(1.0f, 1.0f);
                }
                else if (m.Orientation == "left" && m.GetMonsterSprite.GetSprite.Scale != new SFML.System.Vector2f(-1.0f, 1.0f))//Left
                {
                    m.GetMonsterSprite.GetSprite.Origin = new SFML.System.Vector2f(m.GetMonsterSprite.GetSprite.Texture.Size.X / 2, 0);
                    m.GetMonsterSprite.GetSprite.Scale = new SFML.System.Vector2f(-1.0f, 1.0f);
                }

                m.GetMonsterSprite.GetSprite.Position = new SFML.System.Vector2f(m.Position.X * 128, _window.Size.Y + m.Position.Y * -128 - 40);
                m.GetMonsterSprite.GetSprite.Position -= _moveTheMapOf;

                _window.Draw(m.GetMonsterSprite.GetSprite);
            }

            // Boss
            if (_context.GetGame.GetBoss != null)
            {
                _context.GetGame.GetBoss.GetBossSprite.GetSprite.Position = new SFML.System.Vector2f(_context.GetGame.GetBoss.Position.X * 128 - 150, _window.Size.Y + _context.GetGame.GetBoss.Position.Y * -128 - 205);
                _context.GetGame.GetBoss.GetBossSprite.GetSprite.Position -= _moveTheMapOf;
                _window.Draw(_context.GetGame.GetBoss.GetBossSprite.GetSprite);
            }
           
            // Display
            _window.Display();

        }
        internal void LoadMap()
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
            _hpBar.TextureRect = new IntRect(new SFML.System.Vector2i(0, 0), new SFML.System.Vector2i((int)_lifebarTexture.Size.X - ((int)_lifebarTexture.Size.X / 100) * (_context.GetGame.GetPlayer.GetLife.GetMaxPoint() - _context.GetGame.GetPlayer.GetLife.GetCurrentPoint), (int)_lifebarTexture.Size.Y));
            _window.Draw(_hpBar);
            
            Dictionary<Position, Sprite> map = _context.GetGame.GetMapObject.GetMap;
            _spritesDisplayed.Clear();

            foreach (KeyValuePair<Position, Sprite> s in map)
            {
                s.Value.GetSprite.Position = new SFML.System.Vector2f(s.Key.X * 128, _window.Size.Y + s.Key.Y * -128); //128*128 = Size of a sprite
                _window.Draw(s.Value.GetSprite);
                _spritesDisplayed.Add(s.Value.GetSprite.Position, s.Value);
            }
        }

        internal void LoadMap2()
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
            _hpBar.TextureRect = new IntRect(new SFML.System.Vector2i(0, 0), new SFML.System.Vector2i((int)_lifebarTexture.Size.X - ((int)_lifebarTexture.Size.X / 100) * (_context.GetGame.GetPlayer.GetLife.GetMaxPoint() - _context.GetGame.GetPlayer.GetLife.GetCurrentPoint), (int)_lifebarTexture.Size.Y));
            _window.Draw(_hpBar);



            Dictionary<Position2, Sprite> map = _context2.GetGame2.GetMapObject2.GetMap2;
            _spritesDisplayed.Clear();
            foreach (KeyValuePair<Position2, Sprite> s in map)
            {
                s.Value.GetSprite2.Position = new SFML.System.Vector2f(s.Key.X2 * 128, _window.Size.Y + s.Key.Y2 * -128); //128*128 = Size of a sprite
                _window.Draw(s.Value.GetSprite2);
                _spritesDisplayed.Add(s.Value.GetSprite2.Position, s.Value);
            }
            if (!_window.IsOpen) throw new Exception("Window is not open!");

            // Set background
            Texture backgroundTexture2 = new Texture(_context2.GetGame2.GetMapObject2.GetBackground);
            if (backgroundTexture2 == null) throw new Exception("Texture null!");
            backgroundTexture2.Repeated = true;

            _background = new SFML.Graphics.Sprite(backgroundTexture2);
            if (_background == null) throw new Exception("Sprite null!");

            _background.Scale = new SFML.System.Vector2f(_window.Size.X / 550, _window.Size.Y / 550);
            _window.Draw(_background);

            // Set lifeBar
            _lifebarTexture = new Texture(_context2.GetGame2.GetMapObject2.GetLifeBar);
            if (_lifebarTexture == null) throw new Exception("Texture null!");

            _hpBar = new SFML.Graphics.Sprite(_lifebarTexture);
            if (_hpBar == null) throw new Exception("Sprite null!");
            _hpBar.TextureRect = new IntRect(new SFML.System.Vector2i(0, 0), new SFML.System.Vector2i((int)_lifebarTexture.Size.X - ((int)_lifebarTexture.Size.X / 100) * (_context2.GetGame2.GetPlayer2.GetLife2.GetMaxPoint() - _context2.GetGame2.GetPlayer2.GetLife2.GetCurrentPoint2()), (int)_lifebarTexture.Size.Y));
            _window.Draw(_hpBar);



            Dictionary<Position2, Sprite> map2 = _context2.GetGame2.GetMapObject2.GetMap2;
            _spritesDisplayed2.Clear();
            foreach (KeyValuePair<Position2, Sprite> s2 in map2)
            {
                s2.Value.GetSprite2.Position = new SFML.System.Vector2f(s2.Key.X2 * 128, _window.Size.Y + s2.Key.Y2 * -128); //128*128 = Size of a sprite
                _window.Draw(s2.Value.GetSprite2);
                _spritesDisplayed2.Add(s2.Value.GetSprite2.Position, s2.Value);
            }
        }

        internal void Events()
        {
            //Graphics
            if (_context.GetGame.GetPlayer.IsAttack)
            {
                _context.GetGame.GetPlayer.Attack();
            }

            if (_context.GetGame.GetPlayer.HurtPlayer)
            {
                foreach (Monster monster in _context.GetGame.GetMonsters)
                {
                    monster.GetAttack.HurtPlayerAnimation();
                }
            }

            // Manette
            Joystick.Update();
            if (Joystick.IsConnected(0)) //Controller connected
            {
                // Actions
                for (uint i = 0; i < Joystick.GetButtonCount(0); i++) //Test tous les btn
                {
                    if (Joystick.IsButtonPressed(0, i))
                    {
                        switch (i)
                        {
                            case 0: //A - Jump
                                if (_context.GetGame.GetPlayer.RealPosition.Y < _context.GetGame.GetMapObject.GetLimitMax.Y && _context.GetGame.GetPlayer.IsOnTheGround)
                                {
                                    _context.GetGame.GetPlayer.Jump();
                                }
                                break;
                            case 1: //B - Attack
                                _context.GetGame.GetPlayer.IsAttack = true;
                                _context.GetGame.GetPlayer.Attack();
                                break;
                        }
                    }
                }
                // Moves
                if (Joystick.GetAxisPosition(0, Joystick.Axis.X) >= 80) // Droite
                {
                    _moveTheMapOf += _context.GetGame.GetPlayer.MoveRight((float)_window.Size.X);
                }
                else if (Joystick.GetAxisPosition(0, Joystick.Axis.X) <= -80) // Gauche
                {
                    _moveTheMapOf -= _context.GetGame.GetPlayer.MoveLeft((float)_window.Size.X);
                }
                else if (_context.GetGame.GetPlayer.IsOnTheGround && !_context.GetGame.GetPlayer.IsAttack)
                {
                    _context.GetGame.GetPlayer.GetPlayerSprite.DefaultAnimation();
                }
            }
            else
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    //_context.GetGame.GetPlayer.GetLife.CurrentPoint = 0; // TEMPORARYYYYYYYYYYYYYYYYYY
                }
                else if (Keyboard.IsKeyPressed(_RightAction))
                {
                    _moveTheMapOf += _context.GetGame.GetPlayer.MoveRight((float)_window.Size.X);
                }
                else if (Keyboard.IsKeyPressed(_LeftAction))
                {
                    _moveTheMapOf -= _context.GetGame.GetPlayer.MoveLeft((float)_window.Size.X);
                }

                if (Keyboard.IsKeyPressed(_JumpAction))
                {
                    if (_context.GetGame.GetPlayer.RealPosition.Y < _context.GetGame.GetMapObject.GetLimitMax.Y && _context.GetGame.GetPlayer.IsOnTheGround)
                    {
                        _context.GetGame.GetPlayer.Jump();
                    }
                }

                if (Keyboard.IsKeyPressed(_AttackAction)) // ATTACK
                {
                    _context.GetGame.GetPlayer.IsAttack = true;
                    _context.GetGame.GetPlayer.Attack();
                }

                if (!Keyboard.IsKeyPressed(_LeftAction) && !Keyboard.IsKeyPressed(_RightAction) && !Keyboard.IsKeyPressed(_JumpAction) && !_context.GetGame.GetPlayer.IsAttack && !_context.GetGame.GetPlayer.HurtPlayer)
                {
                    _context.GetGame.GetPlayer.GetPlayerSprite.DefaultAnimation();
                }
            }
        }

        internal void Events2()
        {
            //Graphics
            if (_context.GetGame.GetPlayer.IsAttack)
            {
                _context.GetGame.GetPlayer.Attack();
            }

            if (_context.GetGame.GetPlayer.HurtPlayer)
            {
                foreach (Monster monster in _context.GetGame.GetMonsters)
                {
                    monster.GetAttack.HurtPlayerAnimation();
                }
            }

            // Manette
            Joystick.Update();
            if (Joystick.IsConnected(0)) //Controller connected
            {
                // Actions
                for (uint i = 0; i < Joystick.GetButtonCount(0); i++) //Test tous les btn
                {
                    if (Joystick.IsButtonPressed(0, i))
                    {
                        switch (i)
                        {
                            case 0: //A - Jump
                                if (_context.GetGame.GetPlayer.RealPosition.Y < _context.GetGame.GetMapObject.GetLimitMax.Y && _context.GetGame.GetPlayer.IsOnTheGround)
                                {
                                    _context.GetGame.GetPlayer.Jump();
                                }
                                break;
                            case 1: //B - Attack
                                _context.GetGame.GetPlayer.IsAttack = true;
                                _context.GetGame.GetPlayer.Attack();
                                break;
                        }
                    }
                }
                // Moves
                if (Joystick.GetAxisPosition(0, Joystick.Axis.X) >= 80) // Droite
                {
                    _moveTheMapOf += _context.GetGame.GetPlayer.MoveRight((float)_window.Size.X);
                }
                else if (Joystick.GetAxisPosition(0, Joystick.Axis.X) <= -80) // Gauche
                {
                    _moveTheMapOf -= _context.GetGame.GetPlayer.MoveLeft((float)_window.Size.X);
                }
                else if (_context.GetGame.GetPlayer.IsOnTheGround && !_context.GetGame.GetPlayer.IsAttack)
                {
                    _context.GetGame.GetPlayer.GetPlayerSprite.DefaultAnimation();
                }
            }
            else
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    //_context.GetGame.GetPlayer.GetLife.CurrentPoint = 0; // TEMPORARYYYYYYYYYYYYYYYYYY
                }
                else if (Keyboard.IsKeyPressed(_RightAction))
                {
                    _moveTheMapOf += _context.GetGame.GetPlayer.MoveRight((float)_window.Size.X);
                }
                else if (Keyboard.IsKeyPressed(_LeftAction))
                {
                    _moveTheMapOf -= _context.GetGame.GetPlayer.MoveLeft((float)_window.Size.X);
                }

                if (Keyboard.IsKeyPressed(_JumpAction))
                {
                    if (_context.GetGame.GetPlayer.RealPosition.Y < _context.GetGame.GetMapObject.GetLimitMax.Y && _context.GetGame.GetPlayer.IsOnTheGround)
                    {
                        _context.GetGame.GetPlayer.Jump();
                    }
                }

                if (Keyboard.IsKeyPressed(_AttackAction)) // ATTACK
                {
                    _context.GetGame.GetPlayer.IsAttack = true;
                    _context.GetGame.GetPlayer.Attack();
                }

                if (!Keyboard.IsKeyPressed(_LeftAction) && !Keyboard.IsKeyPressed(_RightAction) && !Keyboard.IsKeyPressed(_JumpAction) && !_context.GetGame.GetPlayer.IsAttack && !_context.GetGame.GetPlayer.HurtPlayer)
                {
                    _context.GetGame.GetPlayer.GetPlayerSprite.DefaultAnimation();
                }
            }
            // ------------------------------PLAYER 2
            //Graphics
            if (_context2.GetGame2.GetPlayer2.IsAttack2)
            {
                _context2.GetGame2.GetPlayer2.Attack2();
            }

            if (_context2.GetGame2.GetPlayer2.HurtPlayer2)
            {
                foreach (Monster monster in _context2.GetGame2.GetMonsters2)
                {
                    monster.GetAttack2.HurtPlayerAnimation2();
                }
            }
            else
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    //_context.GetGame.GetPlayer.GetLife.CurrentPoint = 0; // TEMPORARYYYYYYYYYYYYYYYYYY
                }
                else if (Keyboard.IsKeyPressed(_RightAction2))
                {
                    _moveTheMapOf2 += _context2.GetGame2.GetPlayer2.MoveRight2((float)_window.Size.X);
                }
                else if (Keyboard.IsKeyPressed(_LeftAction2))
                {
                    _moveTheMapOf2 -= _context2.GetGame2.GetPlayer2.MoveLeft2((float)_window.Size.X);
                }

                if (Keyboard.IsKeyPressed(_JumpAction2))
                {
                    if (_context2.GetGame2.GetPlayer2.RealPosition2.Y2 < _context2.GetGame2.GetMapObject2.GetLimitMax2.Y2 && _context2.GetGame2.GetPlayer2.IsOnTheGround2)
                    {
                        _context2.GetGame2.GetPlayer2.Jump2();
                    }
                }

                if (Keyboard.IsKeyPressed(_AttackAction2)) // ATTACK
                {
                    _context2.GetGame2.GetPlayer2.IsAttack2 = true;
                    _context2.GetGame2.GetPlayer2.Attack2();
                }

                if (!Keyboard.IsKeyPressed(_LeftAction2) && !Keyboard.IsKeyPressed(_RightAction2) && !Keyboard.IsKeyPressed(_JumpAction2) && !_context2.GetGame2.GetPlayer2.IsAttack2 && !_context2.GetGame2.GetPlayer2.HurtPlayer2)
                {
                    _context2.GetGame2.GetPlayer2.GetPlayerSprite2.DefaultAnimation2();
                }
            }
        }

        public void ShowLoading(int percent)
        {
            if (percent > 100)
            {
                percent = 100;
            }
            else if (percent < 0)
            {
                percent = 0;
            }

            _window.Clear();

            // Background
            _window.Draw(new SFML.Graphics.Sprite(new Texture(@"..\..\..\..\Assets\Backgrounds\time-travel-background.png")));

            // Loading bar
            RectangleShape r1 = new RectangleShape(new Vector2f(((_window.Size.X / 2)/100) * percent, 30));
            RectangleShape r2 = new RectangleShape(new Vector2f((_window.Size.X / 2) + 10, 40));

            r1.FillColor = Color.Green;
            r2.FillColor = Color.White;

            r2.Position = new Vector2f((_window.Size.X / 2) - (r2.Size.X / 2), (_window.Size.Y / 2) - (r2.Size.Y / 2));
            r1.Position = new Vector2f(r2.Position.X + 5, (_window.Size.Y / 2) - (r1.Size.Y / 2));

            _window.Draw(r2);
            _window.Draw(r1);

            // Display
            _window.Display();
        }

        public SFML.Window.Keyboard.Key RightAction
        {
            get { return _RightAction; }
            set { _RightAction = value; }
        }

        public SFML.Window.Keyboard.Key LeftAction
        {
            get { return _LeftAction; }
            set { _LeftAction = value; }
        }

        public SFML.Window.Keyboard.Key JumpAction
        {
            get { return _JumpAction; }
            set { _JumpAction = value; }
        }

        internal SFML.Window.Keyboard.Key AttackAction
        {
            get { return _AttackAction; }
            set { _AttackAction = value; }
        }

        public SFML.Window.Keyboard.Key RightAction2
        {
            get { return _RightAction2; }
            set { _RightAction2 = value; }
        }

        public SFML.Window.Keyboard.Key LeftAction2
        {
            get { return _LeftAction2; }
            set { _LeftAction2 = value; }
        }

        public SFML.Window.Keyboard.Key JumpAction2
        {
            get { return _JumpAction2; }
            set { _JumpAction2 = value; }
        }

        internal SFML.Window.Keyboard.Key AttackAction2
        { 
            get { return _AttackAction2; }
            set { _AttackAction2 = value; }
        }
    }
}
