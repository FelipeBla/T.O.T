using System;
using System.Diagnostics;

namespace TripOverTime.EngineNamespace
{
    public class Player
    {
        const string PLAYER_ID = "PLAYER420";
        const string PLAYER_ID2 = "PLAYER2501";
        float pw;
        float ph;
        float pw2;
        float ph2;

        const float JUMPING_SPEED = 0.06f;
        const float GRAVITY_SPEED = 0.06f;
        const float JUMPING_LIMIT = 1.3f;
        const float PPLAYER_MOVE = 0.10f;

        const float PPLAYER_MOVE2 = 0.10f;

        readonly Game _context;
        readonly Checkpoint _checkpoint;
        readonly String _name;
        readonly Game _context2;
        readonly Checkpoint _checkpoint2;
        readonly String _name2;
        Position _position; // Graphical position
        Position2 _position2;
        Position _realPosition;
        Position2 _realPosition2;
        int _incrementationHeal;
        int _incrementationHeal2;
        int _incrementationAttack;
        int _incrementationAttack2;
        Life _life;
        ushort _attack;
        Life _life2;
        ushort _attack2;
        Sprite _sprite;
        Sprite _sprite2;
        bool _isJumping;
        bool _isJumping2;
        Position _origin;
        Position2 _origin2;
        string _orientation;
        string _orientation2;
        bool _isAttack;
        bool _isAttack2;
        bool _isHurt;
        bool _isHurt2;
        String _monsterKillName;
        String _monsterKillName2;
        Stopwatch _attackTimer;
        Stopwatch _attackTimer2;
        int _attackSpeed;
        int _attackSpeed2;
        float _attackRange;
        float _attackRange2;
        string _imgPath;

        internal Player(Game context, string name, Position position, Life life, ushort attack, string imgPath)
        {
            _context = context;
            _name = name;
            _position = position;
            _realPosition = new Position(_position.X, _position.Y);
            _life = life;
            _attack = attack;
            _isJumping = false;
            _isAttack = false;
            if (context != null)
                _sprite = new Sprite(PLAYER_ID, _name, imgPath, true, _context.GetMapObject, false, true);
            else
                _sprite = new Sprite(PLAYER_ID, _name, imgPath, true, null, false, true);
            _orientation = "right";

            _incrementationHeal = 0;
            _incrementationHeal2 = 0;
            _incrementationAttack = 0;
            _incrementationAttack2 = 0;

            _attackSpeed = 1;
            _attackRange = 1.0f; // En block

            _attackTimer = new Stopwatch();
            _attackTimer.Start();
            pw = 128;
            ph = 128;

            _monsterKillName = "void";

            //Player 2
            pw2 = 128;
            ph2 = 128;
            _monsterKillName2 = "void";
            _context2 = context;
            _name2 = name;
            _position2 = new Position2(_position.X, _position.Y);
            _realPosition2 = new Position2(_position2.X2, _position2.Y2);
            _life2 = life;
            _attack2 = attack;
            _isJumping2 = false;
            _isAttack2 = false;
            if (_context2 == null)
                _sprite2 = new Sprite(PLAYER_ID2, _name2, imgPath, true, null, false, true);
            else
                _sprite2 = new Sprite(PLAYER_ID2, _name2, imgPath, true, _context2.GetMapObject, false, true);
            _orientation2 = "right";

            _attackSpeed2 = 1;
            _attackRange = 2.0f; // En block
            _attackRange2 = 2.0f; // En block



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
        internal void Jump2()
        {
            if (!_isJumping2)
            {
                _origin2 = new Position2(_realPosition2.X2, _realPosition2.Y2);
                _realPosition2.Y2 += JUMPING_SPEED;
                _position2.Y2 += JUMPING_SPEED;
                _isJumping2 = true;

                _sprite2.JumpAnimation2();
            }
        }

        internal void Gravity()
        {
            if (_isJumping && _origin != null && _realPosition.Y <= _origin.Y + JUMPING_LIMIT) // Jump
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
            if (_isJumping2 && _origin2 != null && _realPosition2.Y2 <= _origin2.Y2 + JUMPING_LIMIT) // Jump
            {
                _realPosition2.Y2 += JUMPING_SPEED;
                _position2.Y2 += JUMPING_SPEED;
                _sprite2.JumpAnimation();
            }
            else // Fall
            {
                _origin2 = null;
                _realPosition2.Y2 -= GRAVITY_SPEED;
                _position2.Y2 -= GRAVITY_SPEED;
            }
        }

        internal void Attack()
        {
            if (_isAttack)
            {
                //Evite le spam d'attaque
                if (_attackTimer.ElapsedMilliseconds >= 400 / _attackSpeed)
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
                        if (_context.GetBoss != null && _context.GetBoss.Position.X <= _realPosition.X + _attackRange && _context.GetBoss.Position.X >= _realPosition.X && _context.GetBoss.Position.Y == _realPosition.Y)
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

                    }
                    if (_context.GetBoss != null && _context.GetBoss.Position.X >= _realPosition.X - _attackRange && _context.GetBoss.Position.X <= _realPosition.X && _context.GetBoss.Position.Y == _realPosition.Y)
                    {
                        _context.GetBoss.life.DecreasedPoint(_attack);
                    }
                    // Si il y a un monstre
                    if (monsterToAttack != null && monsterToAttack.isAlive)
                    {
                        //Attack
                        monsterToAttack.life.DecreasedPoint(_attack);
                        _incrementationHeal++;
                        if (_incrementationHeal > 2)
                        {
                            _life.BonusPoint(1);
                            _incrementationHeal = 0;
                        }
                        _incrementationAttack++;
                        if (_incrementationAttack > 1)
                        {
                            _attack++;
                            _incrementationAttack = 0;
                        }
                    }
                }
                _sprite.AttackAnimation(4, "attack", 100);
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
                            if (m.Position2.X2 <= _realPosition2.X2 + _attackRange2 && m.Position2.X2 >= _realPosition2.X2 && m.Position2.Y2 == _realPosition2.Y2)
                                monsterToAttack = m;
                        }
                        if (_context2.GetBoss2.Position2.X2 <= _realPosition2.X2 + _attackRange2 && _context2.GetBoss2.Position2.X2 >= _realPosition2.X2 && _context2.GetBoss2.Position2.Y2 == _realPosition2.Y2)
                            _context2.GetBoss2.life2.DecreasedPoint2(_attack2);
                    }
                    else
                    {
                        //try get monster with position
                        foreach (Monster m in _context2.GetMonsters2)
                        {
                            if (m.Position2.X2 >= _realPosition2.X2 - _attackRange2 && m.Position2.X2 <= _realPosition2.X2 && m.Position2.Y2 == _realPosition2.Y2)
                                monsterToAttack = m;
                        }
                    }
                    if (_context2.GetBoss2.Position2.X2 >= _realPosition2.X2 - _attackRange2 && _context2.GetBoss2.Position2.X2 <= _realPosition2.X2 && _context2.GetBoss2.Position2.Y2 == _realPosition2.Y2)
                    {
                        _context2.GetBoss2.life2.DecreasedPoint2(_attack2);
                    }

                    // Si il y a un monstre
                    if (monsterToAttack != null)
                    {
                        //Attack
                        monsterToAttack.life2.DecreasedPoint2(_attack2);
                    }
                    if (_incrementationHeal2 > 2)
                    {
                        _life2.BonusPoint2(1);
                        _incrementationHeal2 = 0;
                    }
                    _incrementationAttack2++;
                    if (_incrementationAttack2 > 1)
                    {
                        _attack2++;
                        _incrementationAttack2 = 0;
                    }
                }
                _sprite2.AttackAnimation2(4, "attack", 100);
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
            _position2.Y2 = (int)Math.Round(_position2.Y2);
            _realPosition2.Y2 = (int)Math.Round(_realPosition2.Y2);
        }

        internal void RoundX2() // To recalibrate X (because precision of float)
        {
            _realPosition2.X2 = (float)Math.Round(_realPosition2.X2, 2);
            _position2.X2 = (float)Math.Round(_position2.X2, 2);
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
                    if (_context.GetBoss != null)
                    {

                        if (Math.Round(_context.GetBoss.Position.X) == Math.Round(_realPosition.X) + 1 && _context.GetBoss.IsAlive)
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

        internal SFML.System.Vector2f MoveRight2(float width)
        {

            // ORIENTATION
            if (_orientation2 != "right")
            {
                //Sprite to right
                _orientation2 = "right";
            }

            // ANIMATION
            _sprite2.WalkAnimation2();

            // MOVE
            SFML.System.Vector2f moveTheMapOf2 = new SFML.System.Vector2f(0, 0);

            if (_realPosition2.X2 >= _context2.GetMapObject2.GetLimitMax2.X2) Console.WriteLine("Border of the map");
            else
            {
                Sprite s = null;
                bool blocked = false;

                if (_context2.GetMapObject2.GetMap2.TryGetValue(new Position2((float)Math.Round(_realPosition2.X2 + PLAYER_MOVE, MidpointRounding.ToPositiveInfinity), (float)Math.Round(_realPosition2.Y2, MidpointRounding.ToNegativeInfinity)), out s)) // Block is solid?
                {
                    foreach (Monster m in _context2.GetMonsters2) //Monster block?
                    {
                        if (m.Position2.Y2 == _realPosition2.Y2) //Meme niveau Y
                        {
                            if (Math.Round(m.Position2.X2) == Math.Round(_realPosition2.X2) + 1)
                            {
                                if (m.GetMonsterSprite2.IsSolid2)
                                {
                                    //Blocked by monster
                                    Console.WriteLine("MONSTTERRRRRRRR");
                                    blocked = true;
                                }
                            }
                        }
                    }

                    //boss
                    if ((_context2.GetBoss2.Position2.Y2 == _realPosition2.Y2 || _context2.GetBoss2.Position2.Y2 + 1 == _realPosition2.Y2) && _context2.GetBoss2.IsAlive2) //Meme niveau Y
                    {
                        if (Math.Round(_context2.GetBoss2.Position2.X2) == Math.Round(_realPosition2.X2) + 1)
                        {
                            if (_context2.GetBoss2.GetBossSprite2.IsSolid2)
                            {
                                //Blocked by boss
                                blocked = true;
                            }
                        }
                    }
                    if (!blocked)
                    {
                        if (s.IsSolid2) ;
                        // If player centered on screen, move the map now and not the player
                        else if (_sprite2.GetSprite2.Position.X < width / 2)
                        {
                            // Player move
                            _position2.X2 += PLAYER_MOVE2;
                            _realPosition2.X2 += PLAYER_MOVE2;
                        }
                        else
                        {
                            //Map move
                            moveTheMapOf2 = new SFML.System.Vector2f(128 / (1 / PLAYER_MOVE2), 0);
                            _realPosition2.X2 += PLAYER_MOVE2;
                        }
                    }
                }
            }
            if (_context2.GetMapObject2.GetCheckpointPosition2.Contains(_context2.GetPlayer2.Position2) == true) _checkpoint2.LastActivatedCheckpoint2 = _context2.GetPlayer2.Position2;

            return moveTheMapOf2;
        }

        internal SFML.System.Vector2f MoveLeft(float width)
        {
            // ORIENTATION
            if (_orientation != "left")
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
                    foreach (Monster m in _context.GetMonsters) //Monster block?
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
        internal SFML.System.Vector2f MoveLeft2(float width)
        {
            // ORIENTATION
            if (_orientation2 != "left")
            {
                //Sprite to left
                _orientation2 = "left";
            }

            // ANIMATION
            _sprite2.WalkAnimation2();

            // MOVE
            SFML.System.Vector2f moveTheMapOf2 = new SFML.System.Vector2f(0, 0);

            if (_realPosition2.X2 <= _context2.GetMapObject2.GetLimitMin2.X2) Console.WriteLine("Border of the map");
            else
            {
                Sprite s = null;
                bool blocked = false;

                if (_context2.GetMapObject2.GetMap2.TryGetValue(new Position2((float)Math.Round(_realPosition2.X2 - PLAYER_MOVE2, MidpointRounding.ToNegativeInfinity), (float)Math.Round(_realPosition2.Y2, MidpointRounding.ToNegativeInfinity)), out s)) // Block is solid?
                {
                    foreach (Monster m in _context2.GetMonsters2) //Monster block?
                    {
                        if (m.Position2.Y2 == _realPosition2.Y2) //Meme niveau Y
                        {
                            if (Math.Round(m.Position2.X2) == Math.Round(_realPosition2.X2) - 1)
                            {
                                if (m.GetMonsterSprite2.IsSolid2)
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
                        if (s.IsSolid2) ;
                        // If player left on screen, move the map now and not the player
                        else if (_sprite2.GetSprite2.Position.X > width / 5)
                        {
                            // Player move
                            _position2.X2 -= PLAYER_MOVE2; //0.25f
                            _realPosition2.X2 -= PLAYER_MOVE2;
                        }
                        else
                        {
                            //Map move
                            moveTheMapOf2 = new SFML.System.Vector2f(128 / (1 / PLAYER_MOVE2), 0);
                            _realPosition2.X2 -= PLAYER_MOVE2;
                        }
                    }
                }
            }

            return moveTheMapOf2;
        }
        public string KilledBy
        {
            get => _monsterKillName;
            set => _monsterKillName = value;
        }
        public string KilledBy2
        {
            get => _monsterKillName2;
            set => _monsterKillName2 = value;
        }
        internal bool IsJumping
        {
            get => _isJumping;
            set => _isJumping = value;
        }
        internal bool IsJumping2
        {
            get => _isJumping2;
            set => _isJumping2 = value;
        }

        internal bool IsAttack
        {
            get => _isAttack;
            set => _isAttack = value;
        }
        internal bool IsAttack2
        {
            get => _isAttack2;
            set => _isAttack2 = value;
        }

        internal bool IsAlive
        {
            get => _life.CurrentPoint > 0;
        }
        internal bool IsAlive2
        {
            get => _life2.CurrentPoint2 > 0;
        }

        public Life GetLife
        {
            get => _life;
        }
        public Life GetLife2
        {
            get => _life2;
        }

        internal Position Position
        {
            get => _position;
            set
            {
                _position = value;
            }
        }
        internal Position2 Position2
        {
            get => _position2;
            set
            {
                _position2 = value;
            }
        }

        internal Position RealPosition
        {
            get => _realPosition;
            set { _realPosition = value; }
        }

        internal Position2 RealPosition2
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

        internal bool IsOnTheGround2
        {
            get
            {
                Sprite sToPositive2 = null, sToNegative2 = null;
                _context2.GetMapObject2.GetMap2.TryGetValue(new Position2((float)Math.Round(_realPosition2.X2, MidpointRounding.ToPositiveInfinity), (float)Math.Round(_realPosition2.Y2 - 1, MidpointRounding.ToPositiveInfinity)), out sToPositive2);
                _context2.GetMapObject2.GetMap2.TryGetValue(new Position2((float)Math.Round(_realPosition2.X2, MidpointRounding.ToNegativeInfinity), (float)Math.Round(_realPosition2.Y2 - 1, MidpointRounding.ToPositiveInfinity)), out sToNegative2);
                if (sToPositive2 != null && !sToPositive2.IsSolid2 && sToNegative2 != null && !sToNegative2.IsSolid2)
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
        internal string Orientation2
        {
            get => _orientation2;
        }

        internal ushort GetAttack
        {
            get => _attack;
            set => _attack = value;
        }
        internal ushort GetAttack2
        {
            get => _attack2;
            set => _attack2 = value;
        }
        internal Sprite GetPlayerSprite
        {
            get => _sprite;
        }
        internal Sprite GetPlayerSprite2
        {
            get => _sprite2;
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
        internal float PLAYER_MOVE2
        {
            get => PPLAYER_MOVE2;
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
