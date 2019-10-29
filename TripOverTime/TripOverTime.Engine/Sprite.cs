using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;

namespace TripOverTime.EngineNamespace
{
    class Sprite
    {
        ushort _id;
        string _name;
        string _imgPath;
        bool _isSolid;
        Map _context;
        Texture _texture; //img
        SFML.Graphics.Sprite _sprite;

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

            // For GUI (texture, srpite)
            _texture = new Texture(_imgPath);
            if (_texture == null) throw new Exception("Texture null!");

            _sprite = new SFML.Graphics.Sprite(_texture, new IntRect(0, 0, 128, 128));
            if (_sprite == null) throw new Exception("Sprite null!");
        }

        internal Texture GetTexture
        {
            get => _texture;
        }

        internal SFML.Graphics.Sprite GetSprite
        {
            get => _sprite;
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
