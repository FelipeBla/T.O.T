using System;
using System.Collections.Generic;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    class Sprite
    {
        string _imgPath;
        bool _isSolid;
        Position _position;
        Map _context;

        internal Sprite(string imgPath, bool isSolid, Position position, Map context)
        {
            if (String.IsNullOrEmpty(imgPath)) throw new ArgumentException("imgPath is null or empty!");
            if (position == null) throw new ArgumentNullException("position is null!");
            if (context == null) throw new ArgumentNullException("context is null!");

            _imgPath = imgPath;
            _isSolid = isSolid;
            _position = position;
            _context = context;
        }

        internal bool IsSolid
        {
            get => _isSolid;
        }
        internal Position Pos
        {
            get => _position;
            set
            {
                if (value == null) throw new ArgumentNullException("position is null!");
                _position = value;
            }
        }
    }
}
