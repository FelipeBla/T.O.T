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
        const float MMONSTER_MOVE = 0.15f;
        float pw;
        float ph;

        const string MONSTER_ID = "MONSTER420";
        readonly Game _context;
        readonly String _name;
        Position _position;
        Life _life;
        bool _isAlive;
        int _attack;
        Sprite _sprite;
        bool _isJumping;
        Position _origin;
        string _orientation;

        internal Monster(Game context, String name, Position position, Life life, int attack, string imgPath)
        {
            _context = context;
            _name = name;
            _position = position;
            _life = life;
            _isAlive = true;
            _attack = attack;
            _sprite = new Sprite(MONSTER_ID, _name, imgPath, true, _context.GetMapObject, false, true);
            pw = _sprite.GetSprite.TextureRect.Width;
            ph = _sprite.GetSprite.TextureRect.Height;

        }

        internal void Jump()
        {
            if (!_isJumping)
            {
                _origin = new Position(_position.X, _position.Y);
                _position.Y += JUMPING_SPEED;
                _isJumping = true;

                _sprite.JumpAnimation();
            }
        }
        internal void Gravity()
        {
            if (_isJumping && _origin != null && _position.Y <= _origin.Y + JUMPING_LIMIT) // Jump
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
        private String name
        {
            get { return _name; }
        }
        internal Position Position
        {
            get => _position;
            set { _position = value;}
        }


        private Life life
        {
            get { return _life; }
            set { _life = value; }
        }
        private bool isAlive
        {
            get { return _isAlive; }
            set { _isAlive = value; }
        }
        private int attack
        {
            get { return _attack; }
            set { _attack = value; }
        }

        internal Sprite GetMonsterSprite
        {
            get => _sprite;
        }
        internal bool IsJumping
        {
            get => _isJumping;
            set => _isJumping = value;
        }
    }
}
