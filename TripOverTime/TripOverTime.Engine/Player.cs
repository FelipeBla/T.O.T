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

        internal Player(Game context, String name, Position position, Life life, int attack, string imgPath)
        {
            _context = context;
            _name = name;
            _position = position;
            _realPosition = new Position(_position.X, _position.Y);
            _life = life;
            _attack = attack;
            _sprite = new Sprite(PLAYER_ID, _name, imgPath, true, _context.GetMapObject, false, true);
        }

        internal bool IsAlive
        {
            get => _life.CurrentLife > 0;
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
