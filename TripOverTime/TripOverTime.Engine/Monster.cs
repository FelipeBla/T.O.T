using System;
using System.Collections.Generic;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    internal class Monster
    {
        const float JUMPING_SPEED = 0.1f;
        const float GRAVITY_SPEED = 0.1f;
        const float JUMPING_LIMIT = 1.5f;
        float pw;
        float ph;
        float pw2;
        float ph2;

        const string MONSTER_ID = "MONSTER420";
        readonly Game _context;
        readonly Game _context2;
        readonly String _name;
        readonly String _name2;
        Position _position;
        Position2 _position2;
        Life _life;
        Life _life2;
        bool _isAlive;
        bool _isAlive2;
        Attack _attack;
        Attack _attack2;
        Sprite _sprite;
        Sprite _sprite2;
        bool _ismoving;
        bool _ismoving2;
        Position _origin;
        Position2 _origin2;
        string _orientation;
        string _orientation2;
        float _monsterMove;
        float _monsterMove2;
        float _range;
        float _range2;
        string _attackCombo;

        internal Monster(Game context, string name, Position position, Life life, ushort attack, float monsterMove, float range, string attackCombo, Sprite sprite = null, bool multiplayer = false)
        {
            _context = context;
            _name = name;
            _position = position;
            _life = life;
            _isAlive = true;
            _attack = new Attack(context, this, attack, attackCombo);
            if (sprite == null)
            {
                if (_context == null) _sprite = new Sprite(MONSTER_ID, _name, $@"..\..\..\..\Assets\Monster\{name}", true, true, null, true, false, false);
                else _sprite = new Sprite(MONSTER_ID, _name, $@"..\..\..\..\Assets\Monster\{name}", true, true, _context.GetMapObject, true, false, false);
            }
            else
            {
                _sprite = sprite;
            }

            pw = _sprite.GetSprite.TextureRect.Width;
            ph = _sprite.GetSprite.TextureRect.Height;
            _monsterMove = monsterMove / 100;
            _range = range + 0.2f;
            _attackCombo = attackCombo;

            if (multiplayer)
            {
                _context2 = context;
                _name2 = name;
                _range2 = range + 0.2f;
                _position2 = new Position2(_position.X, _position.Y);
                _life2 = life;
                _isAlive2 = true;
                _attack2 = new Attack(context, this, attack, attackCombo);
                if (_context2 == null) _sprite = new Sprite(MONSTER_ID, _name + "2", $@"..\..\..\..\Assets\Monster\{name}", true, true, null, true, false, false);
                else _sprite2 = new Sprite(MONSTER_ID, _name + "2", $@"..\..\..\..\Assets\Monster\{name}", true, true, _context2.GetMapObject, true, false, false);
                //ph2 = _sprite2.GetSprite2.TextureRect.Height;
                _monsterMove2 = monsterMove / 100;
            }
        }

        internal void MonsterMove()
        {
            //Console.WriteLine("Monster position: [ " + _position.X + " ; " + _position.Y + " ] Orientation: " + _orientation);
            if (!_ismoving)
            {
                Sprite s = null;
                bool moveAccept = true;

                if (_orientation == "left")
                {
                    if (_context.GetMapObject.GetMap.TryGetValue(new Position((float)Math.Round(_position.X - _monsterMove, MidpointRounding.ToNegativeInfinity), (float)Math.Round(_position.Y, MidpointRounding.ToNegativeInfinity)), out s)) // Block is solid?
                    {
                        if (!s.IsSolid)
                        {
                            for (int i = 1; (float)(Math.Round(_position.Y, MidpointRounding.ToNegativeInfinity) - i) >= _context.GetMapObject.GetLimitMin.Y; i++)
                            {
                                if (_context.GetMapObject.GetMap.TryGetValue(new Position((float)Math.Round(_position.X - _monsterMove, MidpointRounding.ToNegativeInfinity), (float)Math.Round(_position.Y, MidpointRounding.ToNegativeInfinity) - i), out s)) // Check if he have a ground under
                                {
                                    if (s.IsDangerous)
                                        moveAccept = false;
                                }
                            }
                            if (moveAccept)
                                _position.X -= _monsterMove;
                        }
                    }

                }
                else
                {
                    if (_context.GetMapObject.GetMap.TryGetValue(new Position((float)Math.Round(_position.X + _monsterMove, MidpointRounding.ToPositiveInfinity), (float)Math.Round(_position.Y, MidpointRounding.ToNegativeInfinity)), out s)) // Block is solid?
                    {
                        if (!s.IsSolid)
                        {
                            for (int i = 1; (float)(Math.Round(_position.Y, MidpointRounding.ToNegativeInfinity) - i) >= _context.GetMapObject.GetLimitMin.Y; i++)
                            {
                                if (_context.GetMapObject.GetMap.TryGetValue(new Position((float)Math.Round(_position.X + _monsterMove, MidpointRounding.ToPositiveInfinity), (float)Math.Round(_position.Y, MidpointRounding.ToNegativeInfinity) - i), out s)) // Check if he have a ground under
                                {
                                    if (s.IsDangerous)
                                        moveAccept = false;
                                }
                            }
                            if (moveAccept)
                                _position.X += _monsterMove;
                        }
                    }
                }

                _ismoving = true;
                _sprite.MonsterMoveAnimation(this);

            }

        }

        internal void MonsterMove2()
        {
            Console.WriteLine("Monster position: [ " + _position2.X2 + " ; " + _position2.Y2 + " ] Orientation: " + _orientation2);
            if (!_ismoving2)
            {
                Sprite s = null;

                if (_orientation2 == "left")
                {
                    if (_context2.GetMapObject2.GetMap2.TryGetValue(new Position2((float)Math.Round(_position2.X2 - _monsterMove2, MidpointRounding.ToNegativeInfinity), (float)Math.Round(_position2.Y2, MidpointRounding.ToNegativeInfinity)), out s)) // Block is solid?
                        if (!s.IsSolid)
                            _position2.X2 -= _monsterMove2;
                }
                else
                {
                    if (_context2.GetMapObject2.GetMap2.TryGetValue(new Position2((float)Math.Round(_position2.X2 + _monsterMove2, MidpointRounding.ToPositiveInfinity), (float)Math.Round(_position2.Y2, MidpointRounding.ToNegativeInfinity)), out s)) // Block is solid?
                    {
                        if (!s.IsSolid2)
                            _position2.X2 += _monsterMove2;
                    }
                }

                _ismoving2 = true;
                _sprite2.MonsterMoveAnimation(this);

            }

        }



        internal void MonsterAttack()
        {
            _attack.AttackMonster();
        }

        internal void MonsterAttack2()
        {
            _attack2.AttackMonster2();
        }


        internal void MonsterDead()
        {
            _sprite.MonsterDeadAnimation(this);
            _sprite.IsSolid = false;
        }

        internal void MonsterDead2()
        {
            _sprite2.MonsterDeadAnimation2(this);
            _sprite2.IsSolid2 = false;
        }
        internal void Gravity()
        {
            if (_ismoving && _origin != null && _position.Y <= _origin.Y + JUMPING_LIMIT) // Jump
            {
                _position.Y += JUMPING_SPEED;
            }
            else // Fall
            {
                _origin = null;
                _position.Y -= GRAVITY_SPEED;
            }
        }
        internal void Gravity2()
        {
            if (_ismoving2 && _origin2 != null && _position2.Y2 <= _origin2.Y2 + JUMPING_LIMIT) // Jump
            {
                _position2.Y2 += JUMPING_SPEED;
            }
            else // Fall
            {
                _origin2 = null;
                _position2.Y2 -= GRAVITY_SPEED;
            }
        }

        internal void RoundY()
        {
            _position.Y = (int)Math.Round(_position.Y);
        }

        internal void RoundX() // To recalibrate X (because precision of float)
        {
            _position.X = (float)Math.Round(_position.X, 2);
        }
        internal void RoundY2()
        {
            _position2.Y2 = (int)Math.Round(_position2.Y2);
        }

        internal void RoundX2() // To recalibrate X (because precision of float)
        {
            _position2.X2 = (float)Math.Round(_position2.X2, 2);
        }
        private Game context
        {
            get { return _context; }
        }
        internal String Name
        {
            get { return _name; }
        }
        internal Position Position
        {
            get => _position;
            set { _position = value; }
        }

        internal Position2 Position2
        {
            get => _position2;
            set { _position2 = value; }
        }


        internal string Orientation
        {
            get => _orientation;
            set { _orientation = value; }
        }
        internal string Orientation2
        {
            get => _orientation2;
            set { _orientation2 = value; }
        }

        internal Life life
        {
            get { return _life; }
            set { _life = value; }
        }
        internal Life life2
        {
            get { return _life2; }
            set { _life2 = value; }
        }
        internal bool isAlive
        {
            get
            {
                return _life.GetCurrentPoint > 0;
            }
        }

        internal bool isAlive2
        {
            get
            {
                return _life2.GetCurrentPoint2() > 0;
            }
            set { _isAlive2 = value; }
        }
        internal Attack GetAttack
        {
            get { return _attack; }
            set { _attack = value; }
        }
        internal Attack GetAttack2
        {
            get { return _attack2; }
            set { _attack2 = value; }
        }

        internal Sprite GetMonsterSprite
        {
            get => _sprite;
        }

        internal Sprite GetMonsterSprite2
        {
            get => _sprite2;
        }
        internal bool IsMoving
        {
            get => _ismoving;
            set => _ismoving = value;
        }
        internal bool IsMoving2
        {
            get => _ismoving2;
            set => _ismoving2 = value;

        }

        internal float MoveSpeed
        {
            get => _monsterMove;
        }

        internal float Range
        {
            get => _range;
        }

        internal float Range2
        {
            get => _range2;
        }
        internal string AttackCombo
        {
            get => _attackCombo;
        }
    }
}
