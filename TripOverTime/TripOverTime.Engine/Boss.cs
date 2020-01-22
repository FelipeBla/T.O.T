using System;
using System.Collections.Generic;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    internal class Boss
    {
        const float JUMPING_SPEED = 0.1f;
        const float GRAVITY_SPEED = 0.1f;
        const float JUMPING_LIMIT = 1.5f;
        float pw;
        float ph;

        const string BOSS_ID = "BOSS420";
        readonly Game _context;
        readonly Game _context2;
        readonly String _name;
        Position _position;
        Position2 _position2;
        Life _life;
        Life _life2;
        bool _isAlive;
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
        float _bossMove;
        float _bossMove2;
        float _range;

        internal Boss(Game context, String name, Position position, Position2 position2, Life life, ushort attack, float bossMove, float range, string attackCombo)
        {
            _context = context;
            _context2 = context;
            _name = name;
            _position = position;
            _life = life;
            _attack = new Attack(_context, null, attack, attackCombo, this);
            _attack2 = new Attack(_context2, null, attack, attackCombo, this);
            _sprite = new Sprite(BOSS_ID, _name, $@"..\..\..\..\Assets\Boss\{name}\All", true, _context.GetMapObject, false, false, true);
            _sprite2 = new Sprite(BOSS_ID, _name, $@"..\..\..\..\Assets\Boss\{name}\All", true, _context.GetMapObject, false, false, true);
            pw = _sprite.GetSprite.TextureRect.Width;
            ph = _sprite.GetSprite.TextureRect.Height;
            _bossMove = bossMove;
            _bossMove2 = bossMove;
            _range = range + 0.2f;
          
            _life2 = life;
            _position2 = position2;
        }
        internal void BossMove()
        {
            if (!_ismoving)
            {
                Sprite s = null;

                _origin = new Position(_position.X, _position.Y);
                if (_orientation == "left")
                {
                    if (_context.GetMapObject.GetMap.TryGetValue(new Position((float)Math.Round(_position.X - _bossMove, MidpointRounding.ToNegativeInfinity), (float)Math.Round(_position.Y, MidpointRounding.ToNegativeInfinity)), out s)) // Block is solid?
                        if (!s.IsSolid)
                            _position.X -= _bossMove;
                }
                else
                {
                    if (_context.GetMapObject.GetMap.TryGetValue(new Position((float)Math.Round(_position.X + _bossMove, MidpointRounding.ToPositiveInfinity), (float)Math.Round(_position.Y, MidpointRounding.ToNegativeInfinity)), out s)) // Block is solid?
                    {
                        if (!s.IsSolid)
                            _position.X += _bossMove;
                    }
                }

                _ismoving = true;
            }

        }

        internal void BossMove2()
        {
            if (!_ismoving2)
            {
                Sprite s = null;

                _origin2 = new Position2(_position2.X2, _position2.Y2);
                if (_orientation2 == "left")
                {
                    if (_context2.GetMapObject2.GetMap2.TryGetValue(new Position2((float)Math.Round(_position2.X2 - _bossMove2, MidpointRounding.ToNegativeInfinity), (float)Math.Round(_position2.Y2, MidpointRounding.ToNegativeInfinity)), out s)) // Block is solid?
                        if (!s.IsSolid2)
                            _position2.X2 -= _bossMove2;
                }
                else
                {
                    if (_context2.GetMapObject2.GetMap2.TryGetValue(new Position2((float)Math.Round(_position2.X2 + _bossMove2, MidpointRounding.ToPositiveInfinity), (float)Math.Round(_position2.Y2, MidpointRounding.ToNegativeInfinity)), out s)) // Block is solid?
                    {
                        if (!s.IsSolid2)
                            _position2.X2 += _bossMove2;
                    }
                }

                _ismoving2 = true;
            }

        }

        internal void BossAttack()
        {
            _attack.AttackBoss();
        }
        internal void BossAttack2()
        {
            _attack.AttackBoss2();
        }


        internal void BossDead()
        {
            _sprite.BossDeadAnimation(this);
        }

        internal void BossDead2()
        {
            _sprite2.BossDeadAnimation2(this);
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

        internal void RoundY2()
        {
            _position2.Y2 = (int)Math.Round(_position2.Y2);
        }

        internal void RoundX() // To recalibrate X (because precision of float)
        {
            _position.X = (float)Math.Round(_position.X, 2);
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
        internal bool IsAlive
        {
            get
            {
                return _life.GetCurrentPoint > 0;
            }
        }

        internal bool IsAlive2
        {
            get
            {
                return _life2.GetCurrentPoint2() > 0;
            }
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

        internal Sprite GetBossSprite
        {
            get => _sprite;
        }
        internal Sprite GetBossSprite2
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
        internal float BossSpeed
        {
            get => _bossMove;
            set => _bossMove = value;
        }
        internal float BossSpeed2
        {
            get => _bossMove2;
            set => _bossMove2 = value;
        }
    }
}
