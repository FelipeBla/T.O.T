﻿using System;
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
        Position _position2;
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
        Position _origin2;
        string _orientation;
        string _orientation2;
        float _monsterMove;
        float _monsterMove2;
        float _range;
        string _attackCombo;

        internal Monster(Game context, String name, Position position, Life life, ushort attack, float monsterMove, float range, string attackCombo)
        {
            _context = context;
            _name = name;
            _position = position;
            _life = life;
            _isAlive = true;
            _attack = new Attack(context, this, attack, attackCombo);
            _sprite = new Sprite(MONSTER_ID, _name, $@"..\..\..\..\Assets\Monster\{name}", true, _context.GetMapObject, true, false);
            pw = _sprite.GetSprite.TextureRect.Width;
            ph = _sprite.GetSprite.TextureRect.Height;
            _monsterMove = monsterMove;
            _range = range + 0.2f;
            _attackCombo = attackCombo;
            _context2 = context;
            _name2 = name;
            _position2 = position;
            _life2 = life;
            _isAlive2 = true;
            _attack2 = new Attack(context, this, attack);
            _sprite2 = new Sprite(MONSTER_ID, _name, $@"..\..\..\..\Assets\Monster\{name}", true, _context.GetMapObject, true, false);
            pw2 = _sprite.GetSprite.TextureRect.Width;
            ph2 = _sprite.GetSprite.TextureRect.Height;
            _monsterMove2 = monsterMove;
        }

        internal void MonsterMove()
        {
            Console.WriteLine("Monster position: [ " + _position.X + " ; " + _position.Y + " ] Orientation: " + _orientation);
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

        internal void MonsterMove2()
        {
            Console.WriteLine("Monster position: [ " + _position2.X + " ; " + _position2.Y + " ] Orientation: " + _orientation2);
            if (!_ismoving2)
            {
                Sprite s = null;

                if (_orientation2 == "left")
                {
                    if (_context2.GetMapObject2.GetMap2.TryGetValue(new Position((float)Math.Round(_position2.X - _monsterMove2, MidpointRounding.ToNegativeInfinity), (float)Math.Round(_position2.Y, MidpointRounding.ToNegativeInfinity)), out s)) // Block is solid?
                        if (!s.IsSolid)
                            _position2.X -= _monsterMove2;
                }
                else
                {
                    if (_context2.GetMapObject2.GetMap2.TryGetValue(new Position((float)Math.Round(_position2.X + _monsterMove2, MidpointRounding.ToPositiveInfinity), (float)Math.Round(_position2.Y, MidpointRounding.ToNegativeInfinity)), out s)) // Block is solid?
                    {
                        if (!s.IsSolid)
                            _position2.X += _monsterMove2;
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
        internal void Gravity2()
        {
            if (_ismoving2 && _origin2 != null && _position2.Y <= _origin.Y + JUMPING_LIMIT) // Jump
            {
                _position2.Y += JUMPING_SPEED;
            }
            else // Fall
            {
                _origin = null;
                _position2.Y -= GRAVITY_SPEED;
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
            _position2.Y = (int)Math.Round(_position2.Y);
        }

        internal void RoundX2() // To recalibrate X (because precision of float)
        {
            _position2.X = (float)Math.Round(_position2.X, 2);
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

        internal Position Position2
        {
            get => _position2;
            set { _position2 = value; }
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
        internal Life life2
        {
            get { return _life2; }
            set { _life2 = value; }
        }
        internal bool isAlive
        {
            get
            {
                return _life.GetCurrentPoint() > 0;
            }
            set { _isAlive = value; }
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

        internal float Range
        {
            get => _range;
        }

        internal string AttackCombo
        {
            get => _attackCombo;
        }
    }
}
