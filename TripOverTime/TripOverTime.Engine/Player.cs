using System;
using System.Collections.Generic;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    class Player
    {
        internal const string PLAYER_ID = "420";
        internal const float pw = 128;
        internal const float ph = 256;

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
                _realPosition.Y += 0.125f;
                _position.Y += 0.125f;
                _isJumping = true;
            }
        }

        internal void Gravity()
        {
            if(_isJumping && _origin != null && _position.Y <= _origin.Y + 1.5f)
            {
                _realPosition.Y += 0.125f;
                _position.Y += 0.125f;
            }
            else
            {
                _origin = null;
                _realPosition.Y -= 0.125f;
                _position.Y -= 0.125f;
            }
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
    }
}
