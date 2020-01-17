using System;
using System.Diagnostics;

namespace TripOverTime.EngineNamespace
{
    public class Player
    {
        const string PLAYER_ID = "PLAYER420";
        float pw;
        float ph;
        float pw2;
        float ph2;
        const float JUMPING_SPEED = 0.06f;
        const float GRAVITY_SPEED = 0.06f;
        const float JUMPING_LIMIT = 1.3f;
        const float PPLAYER_MOVE = 0.10f;

        readonly Game _context;
        readonly Checkpoint _checkpoint;
        readonly String _name;
        readonly Game _context2;
        readonly Checkpoint _checkpoint2;
        readonly String _name2;
        Position _position; // Graphical position
        Position _position2;
        Position _realPosition;
        Position _realPosition2;
        Life _life;
        ushort _attack;
        Sprite _sprite;
        bool _isJumping;
        bool _isJumping2;
        Position _origin;
        Position _origin2;
        string _orientation;
        string _orientation2;
        bool _isAttack;
        bool _isAttack2;
        bool _isHurt;
        bool _isHurt2;
        String _monsterKillName;
        Stopwatch _attackTimer;
        Stopwatch _attackTimer2;
        int _attackSpeed;
        int _attackSpeed2;
        float _attackRange;
        string _imgPath;

        internal Player(Game context, String name, Position position, Life life, ushort attack, string imgPath)
        {
            _context = context;
            _name = name;
            _position = position;
            _realPosition = new Position(_position.X, _position.Y);
            _life = life;
            _attack = attack;
            _isJumping = false;
            _isAttack = false;
            _sprite = new Sprite(PLAYER_ID, _name, imgPath, true, _context.GetMapObject, false, true, false);
            _imgPath = imgPath;
            _orientation = "right";

            _attackSpeed = 1;
            _attackRange = 1.0f; // En block

            _attackTimer = new Stopwatch();
            _attackTimer.Start();


            //Player 2
            pw = 128;
            ph = 128;

            _monsterKillName = "void";

            _context = context;
            _name = name;
            _position = position;
            _realPosition2 = new Position(_position.X, _position.Y);
            _life = life;
            _attack = attack;
            _isJumping = false;
            _isAttack = false;
            _sprite = new Sprite(PLAYER_ID, _name, imgPath, true, _context.GetMapObject, false, true);
            _orientation = "right";

            _attackSpeed = 1;
            _attackRange = 2.0f; // En block

            pw = 128;
            ph = 128;

            _monsterKillName = "void";

            _attackTimer2 = new Stopwatch();
            _attackTimer2.Start();
        }

        internal void Jump()
        {
            if (!_isJumping)
            {
                _origin = new Position(_realPosition.X, _realPosition.Y);
                _realPosition.Y += JUMPING_SPEED;
                _position.Y += JUMPING_SPEED;
                _isJumping = true;

                _sprite.JumpAnimation();
            }
        }

        internal void Gravity()
        {
            if(_isJumping && _origin != null && _realPosition.Y <= _origin.Y + JUMPING_LIMIT) // Jump
            {
                _realPosition.Y += JUMPING_SPEED;
                _position.Y += JUMPING_SPEED;
                _sprite.JumpAnimation();
            }
            else // Fall
            {
                _origin = null;
                _realPosition.Y -= GRAVITY_SPEED;
                _position.Y -= GRAVITY_SPEED;
            }
        }

        internal void Gravity2()
        {
            if (_isJumping2 && _origin != null && _realPosition2.Y <= _origin2.Y + JUMPING_LIMIT) // Jump
            {
                _realPosition2.Y += JUMPING_SPEED;
                _position2.Y += JUMPING_SPEED;
                _sprite.JumpAnimation();
            }
            else // Fall
            {
                _origin2 = null;
                _realPosition2.Y -= GRAVITY_SPEED;
                _position2.Y -= GRAVITY_SPEED;
            }
        }

        internal void Attack()
        {
            if (_isAttack)
            {
                //Evite le spam d'attaque
                if (_attackTimer.ElapsedMilliseconds >= 400/_attackSpeed)
                {
                    _attackTimer.Restart();
                    //Attack
                    Monster monsterToAttack = null;
                    //check orientation
                    if (_orientation == "right")
                    {
                        //try get monster with position
                        foreach (Monster m in _context.GetMonsters)
                        {
                            if (m.Position.X <= _realPosition.X + _attackRange && m.Position.X >= _realPosition.X && m.Position.Y == _realPosition.Y)
                                monsterToAttack = m;
                        }
                        // boss
                        if (_context.GetBoss.Position.X <= _realPosition.X + _attackRange && _context.GetBoss.Position.X >= _realPosition.X && _context.GetBoss.Position.Y == _realPosition.Y)
                            _context.GetBoss.life.DecreasedPoint(_attack);
                    }
                    else
                    {
                        //try get monster with position
                        foreach (Monster m in _context.GetMonsters)
                        {
                            if (m.Position.X >= _realPosition.X - _attackRange && m.Position.X <= _realPosition.X && m.Position.Y == _realPosition.Y)
                                monsterToAttack = m;
                        }
                        //boss
                        if (_context.GetBoss.Position.X >= _realPosition.X - _attackRange && _context.GetBoss.Position.X <= _realPosition.X && _context.GetBoss.Position.Y == _realPosition.Y)
                            _context.GetBoss.life.DecreasedPoint(_attack);
                    }

                    // Si il y a un monstre
                    if (monsterToAttack != null)
                    {
                        //Attack
                        monsterToAttack.life.DecreasedPoint(_attack);
                    }
                }
                _sprite.AttackAnimation(4,"attack", 100);
            }
        }

        internal void Attack2()
        {
            if (_isAttack2)
            {
                //Evite le spam d'attaque
                if (_attackTimer2.ElapsedMilliseconds >= 1000 / _attackSpeed2)
                {
                    _attackTimer2.Restart();
                    //Attack
                    Monster monsterToAttack = null;
                    //check orientation
                    if (_orientation2 == "right")
                    {
                        //try get monster with position
                        foreach (Monster m in _context2.GetMonsters2)
                        {
                            if (m.Position2.X <= _realPosition2.X + _attackRange && m.Position2.X >= _realPosition2.X && m.Position2.Y == _realPosition2.Y)
                                monsterToAttack = m;
                        }
                    }
                    else
                    {
                        //try get monster with position
                        foreach (Monster m in _context2.GetMonsters2)
                        {
                            if (m.Position2.X >= _realPosition2.X - _attackRange && m.Position2.X <= _realPosition2.X && m.Position2.Y == _realPosition2.Y)
                                monsterToAttack = m;
                        }
                    }

                    // Si il y a un monstre
                    if (monsterToAttack != null)
                    {
                        //Attack
                        monsterToAttack.life.DecreasedPoint(_attack);
                    }
                }
                _sprite.AttackAnimation(4, "attack", 100);
            }
        }

        internal void RoundY()
        {
            _position.Y = (int)Math.Round(_position.Y);
            _realPosition.Y = (int)Math.Round(_realPosition.Y);
        }

        internal void RoundX() // To recalibrate X (because precision of float)
        {
            _realPosition.X = (float)Math.Round(_realPosition.X, 2);
            _position.X = (float)Math.Round(_position.X, 2);
        }

        internal void RoundY2()
        {
            _position2.Y = (int)Math.Round(_position2.Y);
            _realPosition2.Y = (int)Math.Round(_realPosition2.Y);
        }

        internal void RoundX2() // To recalibrate X (because precision of float)
        {
            _realPosition2.X = (float)Math.Round(_realPosition2.X, 2);
            _position2.X = (float)Math.Round(_position2.X, 2);
        }

        internal SFML.System.Vector2f MoveRight(float width)
        {
            
            // ORIENTATION
            if (_orientation != "right")
            {
                //Sprite to right
                _orientation = "right";
            }

            // ANIMATION
            _sprite.WalkAnimation();

            // MOVE
            SFML.System.Vector2f moveTheMapOf = new SFML.System.Vector2f(0, 0);

            if (_realPosition.X >= _context.GetMapObject.GetLimitMax.X) Console.WriteLine("Border of the map");
            else
            {
                Sprite s = null;
                bool blocked = false;

                if (_context.GetMapObject.GetMap.TryGetValue(new Position((float)Math.Round(_realPosition.X + PLAYER_MOVE, MidpointRounding.ToPositiveInfinity), (float)Math.Round(_realPosition.Y, MidpointRounding.ToNegativeInfinity)), out s)) // Block is solid?
                {
                    foreach (Monster m in _context.GetMonsters) //Monster block?
                    {
                        if (m.Position.Y <= _realPosition.Y && m.Position.Y >= _realPosition.Y - 0.8f) //Meme niveau Y
                        {
                            if (Math.Round(m.Position.X) == Math.Round(_realPosition.X) + 1)
                            {
                                if (m.GetMonsterSprite.IsSolid)
                                {
                                    //Blocked by monster
                                    blocked = true;
                                }
                            }
                        }
                    }

                    //boss
                    if ((_context.GetBoss.Position.Y == _realPosition.Y || _context.GetBoss.Position.Y + 1 == _realPosition.Y) && _context.GetBoss.IsAlive) //Meme niveau Y
                    {
                        if (Math.Round(_context.GetBoss.Position.X) == Math.Round(_realPosition.X) + 1)
                        {
                            if (_context.GetBoss.GetBossSprite.IsSolid)
                            {
                                //Blocked by boss
                                blocked = true;
                            }
                        }
                    }

                    if (!blocked)
                    {
                        if (s.IsSolid) ;
                        // If player centered on screen, move the map now and not the player
                        else if (_sprite.GetSprite.Position.X < width / 2)
                        {
                            // Player move
                            _position.X += PLAYER_MOVE;
                            _realPosition.X += PLAYER_MOVE;
                        }
                        else
                        {
                            //Map move
                            moveTheMapOf = new SFML.System.Vector2f(128 / (1 / PLAYER_MOVE), 0);
                            _realPosition.X += PLAYER_MOVE;
                        }
                    }
                }
            }
            if (_context.GetMapObject.GetCheckpointPosition.Contains(_context.GetPlayer.Position) == true) _checkpoint.LastActivatedCheckpoint = _context.GetPlayer.Position;

            return moveTheMapOf;
        }

        internal SFML.System.Vector2f MoveLeft(float width)
        {
            // ORIENTATION
            if(_orientation != "left")
            {
                //Sprite to left
                _orientation = "left";
            }

            // ANIMATION
            _sprite.WalkAnimation();

            // MOVE
            SFML.System.Vector2f moveTheMapOf = new SFML.System.Vector2f(0, 0);

            if (_realPosition.X <= _context.GetMapObject.GetLimitMin.X) Console.WriteLine("Border of the map");
            else
            {
                Sprite s = null;
                bool blocked = false;

                if (_context.GetMapObject.GetMap.TryGetValue(new Position((float)Math.Round(_realPosition.X - PLAYER_MOVE, MidpointRounding.ToNegativeInfinity), (float)Math.Round(_realPosition.Y, MidpointRounding.ToNegativeInfinity)), out s)) // Block is solid?
                {
                    foreach(Monster m in _context.GetMonsters) //Monster block?
                    {
                        if (m.Position.Y <= _realPosition.Y && m.Position.Y >= _realPosition.Y - 0.8f) //Meme niveau Y
                        {
                            if (Math.Round(m.Position.X) == Math.Round(_realPosition.X) - 1)
                            {
                                if (m.GetMonsterSprite.IsSolid)
                                {
                                    //Blocked by monster
                                    Console.WriteLine("MONSTTERRRRRRRR");
                                    blocked = true;
                                }
                            }
                        }
                    }

                    if (!blocked)
                    {
                        if (s.IsSolid) ;
                        // If player left on screen, move the map now and not the player
                        else if (_sprite.GetSprite.Position.X > width / 5)
                        {
                            // Player move
                            _position.X -= PLAYER_MOVE; //0.25f
                            _realPosition.X -= PLAYER_MOVE;
                        }
                        else
                        {
                            //Map move
                            moveTheMapOf = new SFML.System.Vector2f(128 / (1 / PLAYER_MOVE), 0);
                            _realPosition.X -= PLAYER_MOVE;
                        }
                    }
                }
            }

            return moveTheMapOf;
        }

        public string KilledBy
        {
            get => _monsterKillName;
            set => _monsterKillName = value;
        }
        internal bool IsJumping
        {
            get => _isJumping;
            set => _isJumping = value;
        }

        internal bool IsAttack
        {
            get => _isAttack;
            set => _isAttack = value;
        }

        internal bool IsAlive
        {
            get => _life.CurrentPoint > 0;
        }

        public Life GetLife
        {
            get => _life;
        }

        internal Position Position
        {
            get => _position;
            set
            {
                _position = value;
            }
        }

        internal Position RealPosition
        {
            get => _realPosition;
            set { _realPosition = value; }
        }

        internal Position RealPosition2
        {
            get => _realPosition2;
            set { _realPosition2 = value; }
        }

        internal bool IsOnTheGround
        {
            get
            {
                Sprite sToPositive = null, sToNegative = null;
                _context.GetMapObject.GetMap.TryGetValue(new Position((float)Math.Round(_realPosition.X, MidpointRounding.ToPositiveInfinity), (float)Math.Round(_realPosition.Y - 1, MidpointRounding.ToPositiveInfinity)), out sToPositive);
                _context.GetMapObject.GetMap.TryGetValue(new Position((float)Math.Round(_realPosition.X, MidpointRounding.ToNegativeInfinity), (float)Math.Round(_realPosition.Y - 1, MidpointRounding.ToPositiveInfinity)), out sToNegative);
                if (sToPositive != null && !sToPositive.IsSolid && sToNegative != null && !sToNegative.IsSolid)
                {
                    return false;
                }
                return true;
            }
        }

        internal string Orientation
        {
            get => _orientation;
        }

        internal ushort GetAttack
        {
            get => _attack;
        }
        internal Sprite GetPlayerSprite
        {
            get => _sprite;
        }

        internal float PLAYER_WIDTH
        {
            get => pw;
        }

        internal float PLAYER_HEIGHT
        {
            get => ph;
        }

        internal float PLAYER_MOVE
        {
            get => PPLAYER_MOVE;
        }

        internal bool HurtPlayer
        {
            get => _isHurt;
            set => _isHurt = value;
        }

        internal bool HurtPlayer2
        {
            get => _isHurt2;
            set => _isHurt2 = value;
        }
    }
}
