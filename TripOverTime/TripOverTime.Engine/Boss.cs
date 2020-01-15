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
        readonly String _name;
        Position _position;
        Life _life;
        bool _isAlive;
        Attack _attack;
        Sprite _sprite;
        bool _ismoving;
        Position _origin;
        string _orientation;
        float _bossMove;

        internal Boss(Game context, String name, Position position, Life life, ushort attack, float bossMove)
        {
            _context = context;
            _name = name;
            _position = position;
            _life = life;
            _attack = new Attack(_context, this, attack);
            _sprite = new Sprite(BOSS_ID, _name, $@"..\..\..\..\Assets\Boss\{name}\All", true, _context.GetMapObject, false, false, true);
            pw = _sprite.GetSprite.TextureRect.Width;
            ph = _sprite.GetSprite.TextureRect.Height;
            _bossMove = bossMove;
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

        internal void BossAttack()
        {
            _attack.AttackBoss();
        }


        internal void BossDead()
        {
            _sprite.BossDeadAnimation(this);
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

        internal void RoundY()
        {
            _position.Y = (int)Math.Round(_position.Y);
        }

        internal void RoundX() // To recalibrate X (because precision of float)
        {
            _position.X = (float)Math.Round(_position.X, 2);
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

        internal string Orientation
        {
            get => _orientation;
            set { _orientation = value; }
        }

        internal Life life
        {
            get { return _life; }
            set { _life = value; }
        }
        internal bool IsAlive
        {
            get
            {
                return _life.GetCurrentPoint() > 0;
            }
        }
        internal Attack GetAttack
        {
            get { return _attack; }
            set { _attack = value; }
        }

        internal Sprite GetBossSprite
        {
            get => _sprite;
        }
        internal bool IsMoving
        {
            get => _ismoving;
            set => _ismoving = value;
        }
        internal float BossSpeed
        {
            get => _bossMove;
            set => _bossMove = value;
        }
    }
}
