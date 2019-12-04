using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using SFML.Graphics;

namespace TripOverTime.EngineNamespace
{
    class Sprite
    {
        string _id;
        string _name;
        string _imgPath;
        bool _isSolid;
        bool _isCheckpoint;
        bool _isEnd;
        bool _isMonster;
        bool _isPlayer;
        Map _context;
        Texture _texture; //img
        Dictionary<string, Texture> _playerTexture;
        Dictionary<string, Texture> _monsterTexture;
        SFML.Graphics.Sprite _sprite;
        Stopwatch _animTimer;

        internal Sprite(string id, string name, string imgPath, bool isSolid, Map context, bool isMonster = false, bool isPlayer = false)
        {
            if (String.IsNullOrEmpty(imgPath)) throw new ArgumentException("imgPath is null or empty!");
            if (context == null) throw new ArgumentNullException("context is null!");
            if (String.IsNullOrEmpty(name)) throw new ArgumentException("name is null or empty!");

            _id = id;
            _name = name;
            _imgPath = imgPath;
            _isSolid = isSolid;
            _context = context;
            _isCheckpoint = false;
            _isEnd = false;
            _isMonster = isMonster;
            _isPlayer = isPlayer;
            _animTimer = new Stopwatch();
            _animTimer.Start();

            if(_name == "CHECKPOINT")
            {
                if (_isSolid == true) throw new Exception("Checkpoint is solid!");
                _isCheckpoint = true;
            }
            else if (_name == "END")
            {
                if (_isSolid == true) throw new Exception("Checkpoint is solid!");
                _isEnd = true;
            }

            //For GUI(texture, srpite)
            if (_isPlayer)
            {
                DirectoryInfo dir;
                FileInfo[] img;
                _playerTexture = new Dictionary<string, Texture>();

                dir = new DirectoryInfo(imgPath);
                img = dir.GetFiles();

                foreach (FileInfo f in img)
                {
                    if (f.Extension == ".png")
                    {
                        string action = f.Name.ToLower().Substring(f.Name.IndexOf("_") + 1, f.Name.Length - f.Name.IndexOf("_") - 5); // -5 = ".png"
                        _playerTexture.Add(action, new Texture(f.FullName));
                        if (_playerTexture[action] == null) throw new Exception("Texture null!");
                    }
                }

                _texture = _playerTexture["stand"];


            }
            else if (_isMonster)
            {
                DirectoryInfo dirMonster;
                FileInfo[] imgMonster;
                _monsterTexture = new Dictionary<string, Texture>();

                dirMonster = new DirectoryInfo(imgPath);
                imgMonster = dirMonster.GetFiles();

                foreach (FileInfo f in imgMonster)
                {
                    if (f.Extension == ".png")
                    {
                        string action = f.Name.ToLower().Substring(f.Name.IndexOf("_") + 1, f.Name.Length - f.Name.IndexOf("_") - 5); // -5 = ".png"
                        _monsterTexture.Add(action, new Texture(f.FullName));
                        if (_monsterTexture[action] == null) throw new Exception("Texture null!");
                    }
                }

                _texture = _monsterTexture["stand"];
            }
            else
            {
                _texture = new Texture(imgPath);
                if (_texture == null) throw new Exception("Texture null!");
            }

            

            _sprite = new SFML.Graphics.Sprite(_texture, new IntRect(new SFML.System.Vector2i(0, 0), (SFML.System.Vector2i)_texture.Size));
            if (_sprite == null) throw new Exception("Sprite null!");
        }

        internal void WalkAnimation()
        {
            if (!_context.GetGame.GetPlayer.IsJumping)
            {
                if (_context.GetGame.GetPlayer.Orientation == "right") //Right
                {
                    if (_animTimer.ElapsedMilliseconds >= 250)
                    {
                        if (_sprite.Texture == _playerTexture["walk1"])
                        {
                            _sprite.TextureRect = new IntRect(new SFML.System.Vector2i(0, 0), (SFML.System.Vector2i)_playerTexture["walk2"].Size);
                            _sprite.Texture = _playerTexture["walk2"];
                        }
                        else
                        {
                            _sprite.TextureRect = new IntRect(new SFML.System.Vector2i(0, 0), (SFML.System.Vector2i)_playerTexture["walk1"].Size);
                            _sprite.Texture = _playerTexture["walk1"];
                        }

                        _animTimer.Restart();
                    }
                }
                else //Left
                {
                    if (_animTimer.ElapsedMilliseconds >= 250)
                    {
                        if (_sprite.Texture == _playerTexture["walk1"])
                        {
                            _sprite.TextureRect = new IntRect((int)_playerTexture["walk2"].Size.X, 0, -(int)_playerTexture["walk2"].Size.X, (int)_playerTexture["walk2"].Size.Y);
                            _sprite.Texture = _playerTexture["walk2"];
                        }
                        else
                        {
                            _sprite.TextureRect = new IntRect((int)_playerTexture["walk1"].Size.X, 0, -(int)_playerTexture["walk1"].Size.X, (int)_playerTexture["walk1"].Size.Y);
                            _sprite.Texture = _playerTexture["walk1"];
                        }

                        _animTimer.Restart();
                    }
                }
            }
            else JumpAnimation();
        }

        internal void JumpAnimation()
        {
            if (_context.GetGame.GetPlayer.IsJumping)
            {
                if (_context.GetGame.GetPlayer.Orientation == "right") //Right
                {
                    _sprite.TextureRect = new IntRect(new SFML.System.Vector2i(0, 0), (SFML.System.Vector2i)_playerTexture["jump"].Size);
                    _sprite.Texture = _playerTexture["jump"];
                }
                else //Left
                {
                    _sprite.TextureRect = new IntRect((int)_playerTexture["jump"].Size.X, 0, -(int)_playerTexture["jump"].Size.X, (int)_playerTexture["jump"].Size.Y);
                    _sprite.Texture = _playerTexture["jump"];
                }
            }
        }

        internal void MonsterMoveAnimation()
        {
            foreach (Monster m in _context.GetGame.GetMonsters)
            {
                if (m.IsMoving)
                {
                    if (m.Orientation == "left") //left
                    {
                        if (_animTimer.ElapsedMilliseconds >= 250)
                        {
                            if (_sprite.Texture == _playerTexture["stand"])
                            {
                                _sprite.TextureRect = new IntRect(new SFML.System.Vector2i(0, 0), (SFML.System.Vector2i)_playerTexture["move"].Size);
                                _sprite.Texture = _playerTexture["move"];
                            }
                            else
                            {
                                _sprite.TextureRect = new IntRect(new SFML.System.Vector2i(0, 0), (SFML.System.Vector2i)_playerTexture["stand"].Size);
                                _sprite.Texture = _playerTexture["stand"];
                            }

                            _animTimer.Restart();
                        }
                    }
                    else //right
                    {
                        if (_animTimer.ElapsedMilliseconds >= 250)
                        {
                            if (_sprite.Texture == _playerTexture["stand"])
                            {
                                _sprite.TextureRect = new IntRect((int)_playerTexture["move"].Size.X, 0, -(int)_playerTexture["move"].Size.X, (int)_playerTexture["move"].Size.Y);
                                _sprite.Texture = _playerTexture["move"];
                            }
                            else
                            {
                                _sprite.TextureRect = new IntRect((int)_playerTexture["stand"].Size.X, 0, -(int)_playerTexture["stand"].Size.X, (int)_playerTexture["stand"].Size.Y);
                                _sprite.Texture = _playerTexture["stand"];
                            }

                            _animTimer.Restart();
                        }
                    }
                }
            }
        }

        internal void MonsterDeadAnimation()
        {
            foreach (Monster m in _context.GetGame.GetMonsters)
            {
                _sprite.Texture = _playerTexture["dead"];
            }
        }

        internal Texture GetTexture
        {
            get => _texture;
        }

        internal SFML.Graphics.Sprite GetSprite
        {
            get => _sprite;
        }

        internal bool IsSolid
        {
            get => _isSolid;
        }
        internal string Id
        {
            get => _id;
        }
        
    }
}
