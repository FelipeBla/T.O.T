using System;
using System.Collections.Generic;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    class Player
    {
        const string PLAYER_ID = "420";

        readonly Game _context;
        readonly String _name;
        Position _position;
        Life _life;
        int _attack;
        Sprite _sprite;

        internal Player(Game context, String name, Position position, Life life, int attack, string imgPath)
        {
            _context = context;
            _name = name;
            _position = position;
            _life = life;
            _attack = attack;
            _sprite = new Sprite(PLAYER_ID, _name, imgPath, true, _context.GetMapObject, false, true);
        }

        private String Name
        {
            get => _name;
        }

        internal bool IsAlive
        {
            get => _life.CurrentLife > 0;
        }

        private Position position
        {
            get => _position;
            set { _position = value; }
        }
        private Life life
        {
            get => _life;
            set { _life = value; }
        }

        private int attack
        {
            get { return _attack; }
            set { _attack = value; }
        }

    }
}
