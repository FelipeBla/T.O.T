using System;
using System.Collections.Generic;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    class Sprite
    {
        ushort _id;
        string _name;
        string _imgPath;
        bool _isSolid;
        Map _context;

        internal Sprite(ushort id, string name, string imgPath, bool isSolid, Map context)
        {
            if (String.IsNullOrEmpty(imgPath)) throw new ArgumentException("imgPath is null or empty!");
            if (context == null) throw new ArgumentNullException("context is null!");
            if (String.IsNullOrEmpty(name)) throw new ArgumentException("name is null or empty!");

            _id = id;
            _name = name;
            _imgPath = imgPath;
            _isSolid = isSolid;
            _context = context;
        }

        internal bool IsSolid
        {
            get => _isSolid;
        }
        internal ushort Id
        {
            get => _id;
        }
        
    }
}
