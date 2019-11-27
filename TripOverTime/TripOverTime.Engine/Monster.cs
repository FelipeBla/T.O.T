using System;
using System.Collections.Generic;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    internal class Monster
    {
        const string MONSTER_ID = "MONSTER420";
        readonly Game _context;
        readonly String _name;
        Position _position;
        Life _life;
        bool _isAlive;
        int _attack;
        Sprite _sprite;
        float pw;
        float ph;

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
            set
            {
                _position = value;
            }
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
    }
}
