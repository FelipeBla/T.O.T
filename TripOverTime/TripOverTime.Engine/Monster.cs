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

        const string MONSTER_ID = "MONSTER420";
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
        float _monsterMove;

        internal Monster(Game context, String name, Position position, Life life, ushort attack, float monsterMove)
        {
            _context = context;
            _name = name;
            _position = position;
            _life = life;
            _isAlive = true;
            _attack = new Attack(context, this, attack);
            _sprite = new Sprite(MONSTER_ID, _name, $@"..\..\..\..\Assets\Monster\{name}", true, _context.GetMapObject, true, false, false);
            pw = _sprite.GetSprite.TextureRect.Width;
            ph = _sprite.GetSprite.TextureRect.Height;
            _monsterMove = monsterMove;
        }

        internal void MonsterMove()
        {
            //Console.WriteLine("Monster position: [ " + _position.X + " ; " + _position.Y + " ] Orientation: " + _orientation);
            if (!_ismoving)
            {
                Sprite s = null;

                if (_orientation == "left")
                {
                    if (_context.GetMapObject.GetMap.TryGetValue(new Position((float)Math.Round(_position.X - _monsterMove, MidpointRounding.ToNegativeInfinity), (float)Math.Round(_position.Y, MidpointRounding.ToNegativeInfinity)), out s)) // Block is solid?
                        if (!s.IsSolid)
                            _position.X -= _monsterMove;
                }
                else
                {
                    if (_context.GetMapObject.GetMap.TryGetValue(new Position((float)Math.Round(_position.X + _monsterMove, MidpointRounding.ToPositiveInfinity), (float)Math.Round(_position.Y, MidpointRounding.ToNegativeInfinity)), out s)) // Block is solid?
                    {
                        if (!s.IsSolid)
                            _position.X += _monsterMove;
                    }
                }

                _ismoving = true;
                _sprite.MonsterMoveAnimation(this);
           
            }
       
        }

        internal void MonsterAttack()
        {
            _attack.AttackMonster();
        }


        internal void MonsterDead()
        {
            _sprite.MonsterDeadAnimation(this);
            _sprite.IsSolid = false;
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

        internal Sprite GetMonsterSprite
        {
            get => _sprite;
        }
        internal bool IsMoving
        {
            get => _ismoving;
            set => _ismoving = value;
        }
    }
}
