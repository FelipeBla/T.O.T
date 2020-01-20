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
        int _bossDead;
        int _bossAttack;
        Texture _texture; //img
        Dictionary<string, Texture> _playerTexture;
        Dictionary<string, Texture> _monsterTexture;
        Dictionary<string, Texture> _bossTexture;
        SFML.Graphics.Sprite _sprite;
        SFML.Graphics.Sprite _sprite2;
        Stopwatch _animTimer;

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
            _playerAnimation = _monsterAttack = _monsterWalk = _incrementationWalk = _incrementationAttack = _monsterDead = _bossAttack = _bossDead = _bossWalk = 1;

            if (_name == "CHECKPOINT")
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

            else if (_isBoss)
            {
                DirectoryInfo dirBoss;
                FileInfo[] imgBoss;
                _bossTexture = new Dictionary<string, Texture>();

                dirBoss = new DirectoryInfo(imgPath);
                imgBoss = dirBoss.GetFiles();

                foreach (FileInfo f in imgBoss)
                {
                    if (f.Extension == ".png")
                    {
                        string action = f.Name.ToLower().Substring(f.Name.IndexOf("_") + 1, f.Name.Length - f.Name.IndexOf("_") - 5); // -5 = ".png"
                        _bossTexture.Add(action, new Texture(f.FullName));
                        if (_bossTexture[action] == null) throw new Exception("Texture null!");
                    }
                }

                _texture = _bossTexture["stand"];
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

        internal bool IsEnd
        {
            get => _isEnd;
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
        
    }
}
