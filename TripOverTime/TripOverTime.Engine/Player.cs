using System;
using System.Collections.Generic;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    class Player
    {
        internal const string PLAYER_ID = "PLAYER420";
        internal const float pw = 128;
        internal const float ph = 256;
        internal const float JUMPING_SPEED = 0.1f;
        internal const float GRAVITY_SPEED = 0.1f;
        internal const float JUMPING_LIMIT = 1.5f;
        internal const float PPLAYER_MOVE = 0.15f;

        readonly Game _context;
        readonly String _name;
        Position _position; // Graphical position
        Position _realPosition;
        Life _life;
        int _attack;
        Sprite _sprite;
        bool _isJumping;
        Position _origin;

        internal Player(Game context, String name, Position position, Life life, int attack, string imgPath)
        {
            _context = context;
            _name = name;
            _position = position;
            _realPosition = new Position(_position.X, _position.Y);
            _life = life;
            _attack = attack;
            _isJumping = false;
            _sprite = new Sprite(PLAYER_ID, _name, imgPath, true, _context.GetMapObject, false, true);
        }

        internal void Jump()
        {
            if (!_isJumping)
            {
                _origin = new Position(_position.X, _position.Y);
                _realPosition.Y += JUMPING_SPEED;
                _position.Y += JUMPING_SPEED;
                _isJumping = true;
            }
        }

        internal void Gravity()
        {
            if(_isJumping && _origin != null && _position.Y <= _origin.Y + JUMPING_LIMIT) // Jump
            {
                _realPosition.Y += JUMPING_SPEED;
                _position.Y += JUMPING_SPEED;
            }
            else // Fall
            {
                _origin = null;
                _realPosition.Y -= GRAVITY_SPEED;
                _position.Y -= GRAVITY_SPEED;
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

        internal bool IsJumping
        {
            get => _isJumping;
            set => _isJumping = value;
        }
        internal bool IsAlive
        {
            get => _life.CurrentPoint > 0;
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
    }
}
