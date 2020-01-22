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
        bool _isDangerous;
        bool _isHeal;
        bool _isCheckpoint;
        bool _isEnd;
        bool _isMonster;
        bool _isPlayer;
        bool _isBoss;
        Map _context;
        int _incrementationAttack;
        int _incrementationWalk;
        int _playerAnimation;
        int _monsterWalk;
        int _monsterDead;
        int _monsterAttack;
        int _bossWalk;
        int _bossWalk2;
        int _bossDead;
        int _bossDead2;
        int _bossAttack;
        Texture _texture; //img
        Dictionary<string, Texture> _playerTexture;
        Dictionary<string, Texture> _monsterTexture;
        Dictionary<string, Texture> _bossTexture;
        Dictionary<string, Texture> _bossTexture2;
        SFML.Graphics.Sprite _sprite;
        Stopwatch _animTimer;

        int _bossAttack2;
        string _id2;
        string _name2;
        string _imgPath2;
        bool _isSolid2;
        bool _isDangerous2;
        bool _isCheckpoint2;
        bool _isEnd2;
        bool _isMonster2;
        bool _isPlayer2;
        Map _context2;
        int _incrementationAttack2;
        int _incrementationWalk2;
        int _monsterWalk2;
        int _monsterDead2;
        int _monsterAttack2;
        int _playerAnimation2;
        Texture _texture2; //img
        Dictionary<string, Texture> _playerTexture2;
        Dictionary<string, Texture> _monsterTexture2;
        SFML.Graphics.Sprite _sprite2;
        Stopwatch _animTimer2;

internal Sprite(string id, string name, string imgPath, bool isSolid, Map context, bool isMonster = false, bool isPlayer = false, bool isBoss = false)
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
            _isBoss = isBoss;
            _animTimer = new Stopwatch();
            _animTimer.Start();
            _playerAnimation = _monsterAttack = _monsterWalk = _incrementationWalk = _incrementationAttack = _monsterDead = _bossAttack = _bossDead = _bossDead2 = _bossWalk = _bossWalk2 = 1;
            _id2 = id;
            _name2 = name;
            _imgPath2 = imgPath;
            _isSolid2 = isSolid;
            _context2 = context;
            _isCheckpoint2 = false;
            _isEnd2 = false;
            _isMonster2 = isMonster;
            _isPlayer2 = isPlayer;
            _animTimer2 = new Stopwatch();
            _animTimer2.Start();


            _playerAnimation2 = _monsterAttack2 = _monsterWalk2 = _incrementationWalk2 = _incrementationAttack2 = _monsterDead2 = 1;

            if (_name == "CHECKPOINT")
            {
                if (_isSolid == true) throw new Exception("Checkpoint is solid!");
                _isCheckpoint = true;
                _isCheckpoint2 = true;
            }
            else if (_name == "END")
            {
                if (_isSolid == true) throw new Exception("Checkpoint is solid!");
                _isEnd = true;
                _isEnd2 = true;
            }

            //For GUI(texture, srpite)
            if (_isPlayer)
            {
                DirectoryInfo dir;
                FileInfo[] img;
                _playerTexture = new Dictionary<string, Texture>();
                _playerTexture2 = new Dictionary<string, Texture>();

                dir = new DirectoryInfo(imgPath);
                img = dir.GetFiles();

                foreach (FileInfo f in img)
                {
                    if (f.Extension == ".png")
                    {
                        string action = f.Name.ToLower().Substring(f.Name.IndexOf("_") + 1, f.Name.Length - f.Name.IndexOf("_") - 5); // -5 = ".png"
                        _playerTexture.Add(action, new Texture(f.FullName));
                        if (_playerTexture[action] == null) throw new Exception("Texture null!");
                        _playerTexture2.Add(action, new Texture(f.FullName));
                        if (_playerTexture2[action] == null) throw new Exception("Texture null!");
                    }
                }

                _texture = _playerTexture["stand"];
                _texture2 = _playerTexture["stand"];
            }

            else if (_isBoss)
            {
                DirectoryInfo dirBoss;
                FileInfo[] imgBoss;
                _bossTexture = new Dictionary<string, Texture>();
                _bossTexture2 = new Dictionary<string, Texture>();

                dirBoss = new DirectoryInfo(imgPath);
                imgBoss = dirBoss.GetFiles();

                foreach (FileInfo f in imgBoss)
                {
                    if (f.Extension == ".png")
                    {
                        string action = f.Name.ToLower().Substring(f.Name.IndexOf("_") + 1, f.Name.Length - f.Name.IndexOf("_") - 5); // -5 = ".png"
                        _bossTexture.Add(action, new Texture(f.FullName));
                        _bossTexture2.Add(action, new Texture(f.FullName));
                        if (_bossTexture[action] == null) throw new Exception("Texture null!");
                        if (_bossTexture2[action] == null) throw new Exception("Texture null!");
                    }
                }

                _texture = _bossTexture["stand"];
                _texture2 = _bossTexture["stand"];
            }


            else if (_isMonster)
            {
                DirectoryInfo dirMonster;
                FileInfo[] imgMonster;
                _monsterTexture = new Dictionary<string, Texture>();
                _monsterTexture2 = new Dictionary<string, Texture>();

                dirMonster = new DirectoryInfo(imgPath);
                imgMonster = dirMonster.GetFiles();

                foreach (FileInfo f in imgMonster)
                {
                    if (f.Extension == ".png")
                    {
                        string action = f.Name.ToLower().Substring(f.Name.IndexOf("_") + 1, f.Name.Length - f.Name.IndexOf("_") - 5); // -5 = ".png"
                        _monsterTexture.Add(action, new Texture(f.FullName));
                        if (_monsterTexture[action] == null) throw new Exception("Texture null!");
                        _monsterTexture2.Add(action, new Texture(f.FullName));
                        if (_monsterTexture2[action] == null) throw new Exception("Texture null!");
                    }
                }

                _texture = _monsterTexture["stand"];
                _texture2 = _monsterTexture["stand"];
            }



            else
            {
                _texture = new Texture(imgPath);
                if (_texture == null) throw new Exception("Texture null!");
            }

            _sprite = new SFML.Graphics.Sprite(_texture, new IntRect(new SFML.System.Vector2i(0, 0), (SFML.System.Vector2i)_texture.Size));
            if (_sprite == null) throw new Exception("Sprite null!");

            _sprite2 = new SFML.Graphics.Sprite(_texture, new IntRect(new SFML.System.Vector2i(0, 0), (SFML.System.Vector2i)_texture.Size));
            if (_sprite2 == null) throw new Exception("Sprite2 null!");
        }

        internal void DefaultAnimation()
        {
            if(!_context.GetGame.GetPlayer.IsJumping)
            {
                if (_context.GetGame.GetPlayer.Orientation == "right")
                {
                    _sprite.Origin = new SFML.System.Vector2f(0, 0);
                    _sprite.Scale = new SFML.System.Vector2f(1.0f, 1.0f);
                    //_sprite.TextureRect = new IntRect(new SFML.System.Vector2i(0, 0), (SFML.System.Vector2i)_playerTexture["stand"].Size);
                }
                else
                {
                    _sprite.Origin = new SFML.System.Vector2f(_playerTexture["stand"].Size.X / 2, 0);
                    _sprite.Scale = new SFML.System.Vector2f(-1.0f, 1.0f);
                    //_sprite.TextureRect = new IntRect((int)_playerTexture["stand"].Size.X, 0, (int)-_playerTexture["stand"].Size.X, (int)_playerTexture["stand"].Size.Y);
                }
                _sprite.Texture = _playerTexture["stand"];
            }
        }
        internal void DefaultAnimation2()
        {
            if (!_context2.GetGame2.GetPlayer2.IsJumping2)
            {
                if (_context2.GetGame2.GetPlayer2.Orientation2 == "right")
                {
                    _sprite2.Origin = new SFML.System.Vector2f(0, 0);
                    _sprite2.Scale = new SFML.System.Vector2f(1.0f, 1.0f);
                    //_sprite.TextureRect = new IntRect(new SFML.System.Vector2i(0, 0), (SFML.System.Vector2i)_playerTexture["stand"].Size);
                }
                else
                {
                    _sprite2.Origin = new SFML.System.Vector2f(_playerTexture2["stand"].Size.X / 2, 0);
                    _sprite2.Scale = new SFML.System.Vector2f(-1.0f, 1.0f);
                    //_sprite.TextureRect = new IntRect((int)_playerTexture["stand"].Size.X, 0, (int)-_playerTexture["stand"].Size.X, (int)_playerTexture["stand"].Size.Y);
                }
                _sprite2.Texture = _playerTexture2["stand"];
            }
        }
        internal void WalkAnimation()
        {
            if (!_context.GetGame.GetPlayer.IsJumping)
            {

                int nbrAction = 7;
                string action = "walk";
                if (_context.GetGame.GetPlayer.Orientation == "right" && _animTimer.ElapsedMilliseconds >= 100)//Right
                {

                    string numberTexture = action + _incrementationWalk;
                    _sprite.Texture = _playerTexture[numberTexture];
                    //_sprite.TextureRect = new IntRect(new SFML.System.Vector2i(0, 0), (SFML.System.Vector2i)_playerTexture[numberTexture].Size);
                    _sprite.Origin = new SFML.System.Vector2f(0, 0);
                    _sprite.Scale = new SFML.System.Vector2f(1.0f, 1.0f);
                    if (_incrementationWalk + 1 == nbrAction)
                    {
                        _incrementationWalk = 1;
                    }
                    else
                    {
                        _incrementationWalk++;
                    }
                    _animTimer.Restart();

                }
                else if (_animTimer.ElapsedMilliseconds >= 100) //left
                {
                    string numberTexture = action + _incrementationWalk;
                    _sprite.Texture = _playerTexture[numberTexture];
                    //_sprite.TextureRect = new IntRect((int)_playerTexture[numberTexture].Size.X, 0, (int)-_playerTexture[numberTexture].Size.X, (int)_playerTexture[numberTexture].Size.Y);
                    _sprite.Origin = new SFML.System.Vector2f(_playerTexture[numberTexture].Size.X/2, 0);
                    _sprite.Scale = new SFML.System.Vector2f(-1.0f, 1.0f);
                    if (_incrementationWalk + 1 == nbrAction)
                    {
                        _incrementationWalk = 1;
                    }
                    else
                    {
                        _incrementationWalk++;
                    }
                    _animTimer.Restart();
                }
                
            }
            else JumpAnimation();
        }
        internal void WalkAnimation2()
        {
            if (!_context2.GetGame2.GetPlayer2.IsJumping2)
            {

                int nbrAction = 7;
                string action = "walk";
                if (_context2.GetGame2.GetPlayer2.Orientation2 == "right" && _animTimer2.ElapsedMilliseconds >= 100)//Right
                {

                    string numberTexture = action + _incrementationWalk2;
                    _sprite2.Texture = _playerTexture2[numberTexture];
                    //_sprite.TextureRect = new IntRect(new SFML.System.Vector2i(0, 0), (SFML.System.Vector2i)_playerTexture[numberTexture].Size);
                    _sprite2.Origin = new SFML.System.Vector2f(0, 0);
                    _sprite2.Scale = new SFML.System.Vector2f(1.0f, 1.0f);
                    if (_incrementationWalk2 + 1 == nbrAction)
                    {
                        _incrementationWalk2 = 1;
                    }
                    else
                    {
                        _incrementationWalk2++;
                    }
                    _animTimer2.Restart();

                }
                else if (_animTimer2.ElapsedMilliseconds >= 100) //left
                {
                    string numberTexture = action + _incrementationWalk2;
                    _sprite2.Texture = _playerTexture[numberTexture];
                    //_sprite.TextureRect = new IntRect((int)_playerTexture[numberTexture].Size.X, 0, (int)-_playerTexture[numberTexture].Size.X, (int)_playerTexture[numberTexture].Size.Y);
                    _sprite2.Origin = new SFML.System.Vector2f(_playerTexture[numberTexture].Size.X / 2, 0);
                    _sprite2.Scale = new SFML.System.Vector2f(-1.0f, 1.0f);
                    if (_incrementationWalk2 + 1 == nbrAction)
                    {
                        _incrementationWalk2 = 1;
                    }
                    else
                    {
                        _incrementationWalk2++;
                    }
                    _animTimer2.Restart();
                }

            }
            else JumpAnimation2();
        }
        internal void JumpAnimation()
        {
            if (_context.GetGame.GetPlayer.Orientation == "right") //Right
            {
                if (_animTimer.ElapsedMilliseconds >= 100)
                {
                    if (_sprite.Texture == _playerTexture["jump2"])
                    {
                        _sprite.Texture = _playerTexture["jump3"];
                        _sprite.Origin = new SFML.System.Vector2f(0, 0);
                        _sprite.Scale = new SFML.System.Vector2f(1.0f, 1.0f);
                    }
                    else if (_sprite.Texture == _playerTexture["jump3"])
                    {
                        _sprite.Texture = _playerTexture["jump4"];
                        _sprite.Origin = new SFML.System.Vector2f(0, 0);
                        _sprite.Scale = new SFML.System.Vector2f(1.0f, 1.0f);
                    }
                    else if (_sprite.Texture == _playerTexture["jump4"])
                    {
                        _sprite.Texture = _playerTexture["jump5"];
                        _sprite.Origin = new SFML.System.Vector2f(0, 0);
                        _sprite.Scale = new SFML.System.Vector2f(1.0f, 1.0f);
                    }
                    else
                    {
                        _sprite.Texture = _playerTexture["jump2"];
                        _sprite.Origin = new SFML.System.Vector2f(0, 0);
                        _sprite.Scale = new SFML.System.Vector2f(1.0f, 1.0f);
                    }

                    _animTimer.Restart();
                }
            }
            else //Left
            {
                if (_animTimer.ElapsedMilliseconds >= 100)
                {
                    if (_sprite.Texture == _playerTexture["jump2"])
                    {
                        _sprite.Texture = _playerTexture["jump3"];
                        _sprite.Origin = new SFML.System.Vector2f(_playerTexture["jump3"].Size.X / 2, 0);
                        _sprite.Scale = new SFML.System.Vector2f(-1.0f, 1.0f);
                    }
                    else if (_sprite.Texture == _playerTexture["jump3"])
                    {
                        _sprite.Texture = _playerTexture["jump4"];
                        _sprite.Origin = new SFML.System.Vector2f(_playerTexture["jump4"].Size.X / 2, 0);
                        _sprite.Scale = new SFML.System.Vector2f(-1.0f, 1.0f);
                    }
                    else if (_sprite.Texture == _playerTexture["jump4"])
                    {
                        _sprite.Texture = _playerTexture["jump5"];
                        _sprite.Origin = new SFML.System.Vector2f(_playerTexture["jump5"].Size.X / 2, 0);
                        _sprite.Scale = new SFML.System.Vector2f(-1.0f, 1.0f);
                    }
                    else
                    {
                        _sprite.Texture = _playerTexture["jump2"];
                        _sprite.Origin = new SFML.System.Vector2f(_playerTexture["jump2"].Size.X / 2, 0);
                        _sprite.Scale = new SFML.System.Vector2f(-1.0f, 1.0f);
                    }

                    _animTimer.Restart();
                }
            }
            
        }
        internal void JumpAnimation2()
        {
            if (_context2.GetGame2.GetPlayer2.Orientation2 == "right") //Right
            {
                if (_animTimer2.ElapsedMilliseconds >= 100)
                {
                    if (_sprite2.Texture == _playerTexture2["jump2"])
                    {
                        _sprite2.Texture = _playerTexture2["jump3"];
                        _sprite2.Origin = new SFML.System.Vector2f(0, 0);
                        _sprite2.Scale = new SFML.System.Vector2f(1.0f, 1.0f);
                    }
                    else if (_sprite2.Texture == _playerTexture2["jump3"])
                    {
                        _sprite2.Texture = _playerTexture2["jump4"];
                        _sprite2.Origin = new SFML.System.Vector2f(0, 0);
                        _sprite2.Scale = new SFML.System.Vector2f(1.0f, 1.0f);
                    }
                    else if (_sprite2.Texture == _playerTexture2["jump4"])
                    {
                        _sprite2.Texture = _playerTexture2["jump5"];
                        _sprite2.Origin = new SFML.System.Vector2f(0, 0);
                        _sprite2.Scale = new SFML.System.Vector2f(1.0f, 1.0f);
                    }
                    else
                    {
                        _sprite2.Texture = _playerTexture2["jump2"];
                        _sprite2.Origin = new SFML.System.Vector2f(0, 0);
                        _sprite2.Scale = new SFML.System.Vector2f(1.0f, 1.0f);
                    }

                    _animTimer2.Restart();
                }
            }
            else //Left
            {
                if (_animTimer2.ElapsedMilliseconds >= 100)
                {
                    if (_sprite2.Texture == _playerTexture2["jump2"])
                    {
                        _sprite2.Texture = _playerTexture2["jump3"];
                        _sprite2.Origin = new SFML.System.Vector2f(_playerTexture2["jump3"].Size.X / 2, 0);
                        _sprite2.Scale = new SFML.System.Vector2f(-1.0f, 1.0f);
                    }
                    else if (_sprite2.Texture == _playerTexture2["jump3"])
                    {
                        _sprite2.Texture = _playerTexture2["jump4"];
                        _sprite2.Origin = new SFML.System.Vector2f(_playerTexture2["jump4"].Size.X / 2, 0);
                        _sprite2.Scale = new SFML.System.Vector2f(-1.0f, 1.0f);
                    }
                    else if (_sprite2.Texture == _playerTexture2["jump4"])
                    {
                        _sprite2.Texture = _playerTexture2["jump5"];
                        _sprite2.Origin = new SFML.System.Vector2f(_playerTexture2["jump5"].Size.X / 2, 0);
                        _sprite2.Scale = new SFML.System.Vector2f(-1.0f, 1.0f);
                    }
                    else
                    {
                        _sprite2.Texture = _playerTexture["jump2"];
                        _sprite2.Origin = new SFML.System.Vector2f(_playerTexture2["jump2"].Size.X / 2, 0);
                        _sprite2.Scale = new SFML.System.Vector2f(-1.0f, 1.0f);
                    }

                    _animTimer2.Restart();
                }
            }

        }

        internal void PlayerAnimation(int nbrAction, string action, int time)
        {
            if (_animTimer.ElapsedMilliseconds >= time)
            {

                if (_playerAnimation == nbrAction)
                {
                    _playerAnimation = 1;

                    if (_context.GetGame.GetPlayer.HurtPlayer) { _context.GetGame.GetPlayer.HurtPlayer = false; }
                }
                else
                {
                    _playerAnimation++;
                }

                string numberTexture = action + _playerAnimation;
                _sprite.Texture = _playerTexture[numberTexture];
                if (_context.GetGame.GetPlayer.Orientation == "right") //Right
                {
                    _sprite.Origin = new SFML.System.Vector2f(0, 0);
                    _sprite.Scale = new SFML.System.Vector2f(1.0f, 1.0f);
                }
                else if (_context.GetGame.GetPlayer.Orientation == "left")//Left
                {
                    _sprite.Origin = new SFML.System.Vector2f(_playerTexture[numberTexture].Size.X / 2, 0);
                    _sprite.Scale = new SFML.System.Vector2f(-1.0f, 1.0f);
                }
                _animTimer.Restart();

            }
        }

        internal void PlayerAnimation2(int nbrAction, string action, int time)
        {
            if (_animTimer2.ElapsedMilliseconds >= time)
            {

                if (_playerAnimation2 == nbrAction)
                {
                    _playerAnimation2 = 1;

                    if (_context2.GetGame2.GetPlayer2.HurtPlayer2) { _context2.GetGame2.GetPlayer2.HurtPlayer2 = false; }
                }
                else
                {
                    _playerAnimation2++;
                }

                string numberTexture = action + _playerAnimation2;
                _sprite.Texture = _playerTexture2[numberTexture];
                if (_context2.GetGame2.GetPlayer2.Orientation2 == "right") //Right
                {
                    _sprite2.Origin = new SFML.System.Vector2f(0, 0);
                    _sprite2.Scale = new SFML.System.Vector2f(1.0f, 1.0f);
                }
                else if (_context2.GetGame2.GetPlayer2.Orientation2 == "left")//Left
                {
                    _sprite2.Origin = new SFML.System.Vector2f(_playerTexture[numberTexture].Size.X / 2, 0);
                    _sprite2.Scale = new SFML.System.Vector2f(-1.0f, 1.0f);
                }
                _animTimer2.Restart();

            }
        }
        internal void AttackAnimation(int nbrAction, string action, int time)
        {
            if (_animTimer.ElapsedMilliseconds >= time)
            {

                if (_incrementationAttack == nbrAction)
                {
                    _incrementationAttack = 1;

                    if (_context.GetGame.GetPlayer.IsAttack) { _context.GetGame.GetPlayer.IsAttack = false; }
                }
                else
                {
                    _incrementationAttack++;
                }

                string numberTexture = action + _incrementationAttack;
                _sprite.Texture = _playerTexture[numberTexture];
                if (_context.GetGame.GetPlayer.Orientation == "right") //Right
                {
                    _sprite.Origin = new SFML.System.Vector2f(0, 0);
                    _sprite.Scale = new SFML.System.Vector2f(1.0f, 1.0f);
                }
                else if (_context.GetGame.GetPlayer.Orientation == "left")//Left
                {
                    _sprite.Origin = new SFML.System.Vector2f(_playerTexture[numberTexture].Size.X / 2, 0);
                    _sprite.Scale = new SFML.System.Vector2f(-1.0f, 1.0f);
                }
                _animTimer.Restart();

            }
        }

        internal void AttackAnimation2(int nbrAction, string action, int time)
        {
            if (_animTimer2.ElapsedMilliseconds >= time)
            {

                if (_incrementationAttack2 == nbrAction)
                {
                    _incrementationAttack2 = 1;

                    if (_context2.GetGame2.GetPlayer2.IsAttack2) { _context2.GetGame2.GetPlayer2.IsAttack2 = false; }
                }
                else
                {
                    _incrementationAttack2++;
                }

                string numberTexture2 = action + _incrementationAttack2;
                _sprite2.Texture = _playerTexture2[numberTexture2];
                if (_context2.GetGame2.GetPlayer2.Orientation2 == "right") //Right
                {
                    _sprite2.Origin = new SFML.System.Vector2f(0, 0);
                    _sprite2.Scale = new SFML.System.Vector2f(1.0f, 1.0f);
                }
                else if (_context2.GetGame2.GetPlayer2.Orientation2 == "left")//Left
                {
                    _sprite2.Origin = new SFML.System.Vector2f(_playerTexture2[numberTexture2].Size.X / 2, 0);
                    _sprite2.Scale = new SFML.System.Vector2f(-1.0f, 1.0f);
                }
                _animTimer2.Restart();

            }
        }


internal void BossOrientation(Boss boss)
        {
            if(boss.Orientation == "right")
            {
                _sprite.TextureRect = new IntRect(new SFML.System.Vector2i(0, 0), (SFML.System.Vector2i)_bossTexture["stand"].Size);
            }
            else
            {
                _sprite.TextureRect = new IntRect((int)_bossTexture["stand"].Size.X, 0, -(int)_bossTexture["stand"].Size.X, (int)_bossTexture["stand"].Size.Y);
            }
        }
        internal void BossOrientation2(Boss boss)
        {
            if (boss.Orientation2 == "right")
            {
                _sprite2.TextureRect = new IntRect(new SFML.System.Vector2i(0, 0), (SFML.System.Vector2i)_bossTexture["stand"].Size);
            }
            else
            {
                _sprite2.TextureRect = new IntRect((int)_bossTexture["stand"].Size.X, 0, -(int)_bossTexture["stand"].Size.X, (int)_bossTexture["stand"].Size.Y);
            }
        }

        internal void BossMoveAnimation(Boss boss)
        {
            if (boss.IsMoving)
            {
                int nbrAction = 24;
                string action = "walk (";

                if (_animTimer.ElapsedMilliseconds >= 40)
                {
                    string numberTexture = action + _bossWalk + ")";
                    _sprite.Texture = _bossTexture[numberTexture];
                    if (_bossWalk >= nbrAction)
                    {
                        _bossWalk = 1;
                    }
                    else
                    {
                        _bossWalk++;
                    }
                    _animTimer.Restart();

                }
            }
        }
        internal void BossMoveAnimation2(Boss boss)
        {
            if (boss.IsMoving2)
            {
                int nbrAction = 24;
                string action = "walk (";

                if (_animTimer2.ElapsedMilliseconds >= 40)
                {
                    string numberTexture = action + _bossWalk2 + ")";
                    _sprite2.Texture = _bossTexture[numberTexture];
                    if (_bossWalk2 >= nbrAction)
                    {
                        _bossWalk2 = 1;
                    }
                    else
                    {
                        _bossWalk2++;
                    }
                    _animTimer2.Restart();

                }
            }
        }
        internal void BossAttackAnimation(int nbrAction, string action, int speedAction)
        {
            if (_animTimer.ElapsedMilliseconds >= speedAction)
            {

                if (_bossAttack >= nbrAction)
                {
                    _bossAttack = 1;
                }
                else
                {
                    _bossAttack++;
                }

                string numberTexture = action + _bossAttack + ")";
                _sprite.Texture = _bossTexture[numberTexture];

                _animTimer.Restart();
            }
        }

        internal void BossAttackAnimation2(int nbrAction, string action, int speedAction)
        {
            if (_animTimer2.ElapsedMilliseconds >= speedAction)
            {

                if (_bossAttack2 >= nbrAction)
                {
                    _bossAttack2 = 1;
                }
                else
                {
                    _bossAttack2++;
                }

                string numberTexture = action + _bossAttack2 + ")";
                _sprite2.Texture = _bossTexture2[numberTexture];

                _animTimer2.Restart();
            }
        }

        internal void BossDeadAnimation(Boss boss)
        {
            int nbrAction = 15;
            string action = "dead (";

            if (_animTimer.ElapsedMilliseconds >= 40)
            {

                string numberTexture = action + _bossDead + ")";
                _sprite.TextureRect = new IntRect((int)_bossTexture[numberTexture].Size.X, 0, -(int)_bossTexture[numberTexture].Size.X, (int)_bossTexture[numberTexture].Size.Y);
                _sprite.Texture = _bossTexture[numberTexture];
                if (_bossDead < nbrAction)
                {
                    _bossDead++;
                }
                _animTimer.Restart();

            }
        }
        internal void BossDeadAnimation2(Boss boss)
        {
            int nbrAction = 15;
            string action = "dead (";

            if (_animTimer2.ElapsedMilliseconds >= 40)
            {

                string numberTexture = action + _bossDead2 + ")";
                _sprite2.TextureRect = new IntRect((int)_bossTexture[numberTexture].Size.X, 0, -(int)_bossTexture[numberTexture].Size.X, (int)_bossTexture[numberTexture].Size.Y);
                _sprite2.Texture = _bossTexture[numberTexture];
                if (_bossDead2 < nbrAction)
                {
                    _bossDead2++;
                }
                _animTimer2.Restart();

            }
        }


        internal void MonsterMoveAnimation(Monster monster)
        {
            if (monster.IsMoving)
            {
                int nbrAction = 24;
                string action = "walk";
                if (_animTimer.ElapsedMilliseconds >= 40)
                {
                    string numberTexture = action + _monsterWalk;
                    _sprite.Texture = _monsterTexture[numberTexture];
                    if (_monsterWalk >= nbrAction)
                    {
                        _monsterWalk = 1;
                    }
                    else
                    {
                        _monsterWalk++;
                    }

                    _animTimer.Restart();

                }

            }
        }
        internal void MonsterMoveAnimation2(Monster monster)
        {
            if (monster.IsMoving)
            {
                int nbrAction = 25;
                string action = "walk";
                if (monster.Orientation == "right" && _animTimer.ElapsedMilliseconds >= 40)//Right
                {

                    string numberTexture = action + _monsterWalk;
                    //_sprite.TextureRect = new IntRect(new SFML.System.Vector2i(0, 0), (SFML.System.Vector2i)_monsterTexture[numberTexture].Size);
                    _sprite.Texture = _monsterTexture[numberTexture];
                    if (_monsterWalk + 1 == nbrAction)
                    {
                        _monsterWalk = 1;
                    }
                    else
                    {
                        _monsterWalk++;
                    }

                    _animTimer.Restart();

                }
                else if (_animTimer.ElapsedMilliseconds >= 40) //left
                {
                    string numberTexture = action + _monsterWalk;
                    //_sprite.TextureRect = new IntRect((int)_monsterTexture[numberTexture].Size.X, 0, -(int)_monsterTexture[numberTexture].Size.X, (int)_monsterTexture[numberTexture].Size.Y);
                    _sprite.Texture = _monsterTexture[numberTexture];
                    if (_monsterWalk + 1 == nbrAction)
                    {
                        _monsterWalk = 1;
                    }
                    else
                    {
                        _monsterWalk++;
                    }

                    _animTimer.Restart();

                }

            }
        }

        internal void MonsterAttackAnimation(int nbrAction, string action, int speedAction)
        {
            if (_animTimer.ElapsedMilliseconds >= speedAction)
            {

                if (_monsterAttack >= nbrAction)
                {
                    _monsterAttack = 1;
                }
                else
                {
                    _monsterAttack++;
                }

                string numberTexture = action + _monsterAttack;
                _sprite.Texture = _monsterTexture[numberTexture];
                
                _animTimer.Restart();

            }
        }

        internal void MonsterAttackAnimation2(int nbrAction, string action, Monster monster)
        {
            if (_animTimer2.ElapsedMilliseconds >= 50)
            {

                if (_monsterAttack2 >= nbrAction)
                {
                    _monsterAttack2 = 1;
                }
                else
                {
                    _monsterAttack2++;
                }

                string numberTexture2 = action + _monsterAttack2;
                _sprite2.Texture = _monsterTexture[numberTexture2];

                _animTimer2.Restart();

            }
        }

        internal void MonsterDeadAnimation(Monster m)
        {
                int nbrAction = 15;
                string action = "dead";

            if (_animTimer.ElapsedMilliseconds >= 40)
            {

                string numberTexture = action + _monsterDead;
                //_sprite.TextureRect = new IntRect((int)_monsterTexture[numberTexture].Size.X, 0, -(int)_monsterTexture[numberTexture].Size.X, (int)_monsterTexture[numberTexture].Size.Y);
                _sprite.Texture = _monsterTexture[numberTexture];
                if (_monsterDead < nbrAction)
                {
                    _monsterDead++;
                }
                _animTimer.Restart();

            }
        }
        internal void MonsterDeadAnimation2(Monster m)
        {
            int nbrAction2 = 15;
            string action2 = "dead";

            if (_animTimer2.ElapsedMilliseconds >= 40)
            {

                string numberTexture2 = action2 + _monsterDead2;
                //_sprite.TextureRect = new IntRect((int)_monsterTexture[numberTexture].Size.X, 0, -(int)_monsterTexture[numberTexture].Size.X, (int)_monsterTexture[numberTexture].Size.Y);
                _sprite2.Texture = _monsterTexture2[numberTexture2];
                if (_monsterDead2 != nbrAction2)
                {
                    _monsterDead2++;
                }
                _animTimer2.Restart();

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

        internal SFML.Graphics.Sprite GetSprite2
        {
            get => _sprite2;
        }

        internal bool IsSolid
        {
            get => _isSolid;
            set => _isSolid = value;
        }
        internal bool IsSolid2
        {
            get => _isSolid2;
            set => _isSolid2 = value;
        }

        internal bool IsEnd
        {
            get => _isEnd;
        }
        internal bool IsEnd2
        {
            get => _isEnd2;
        }
        internal bool IsCheckpoint
        {
            get => _isCheckpoint;
        }
        internal string Id
        {
            get => _id;
        }

        internal bool IsDangerous
        {
            get => _isDangerous;
            set => _isDangerous = value;
        }
        internal bool IsDangerous2
        {
            get => _isDangerous2;
            set => _isDangerous2 = value;
        }

    }
}
