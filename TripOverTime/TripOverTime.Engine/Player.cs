using System;
using System.Collections.Generic;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    class Player
    {

        readonly Game _context;
        readonly String _name;
        Double _position;
        Life _life;
        bool _isAlive;
        int _attack;

        public Player(Game context, String name, Double position, Life life, int attack)
        {
            _context = context;
            _name = name;
            _position = position;
            _life = life;
            _isAlive = true;
            _attack = attack;

        }
        private Game context
        {
            get { return _context; }
        }
        private String name
        {
            get { return _name; }
        }
        private Double position
        {
            get { return _position; }
            set { _position = value; }
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

    }
}
