using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    class Map
    {
        Dictionary<Position, Sprite> _map;
        List<Sprite> _sprites;
        string _backgroundPath;
        string _mapPath;
        Position _limitMin;
        Position _limitMax;
        Game _context;

        internal Map(Game context, string mapPath)
        {
            if (String.IsNullOrEmpty(mapPath)) throw new ArgumentException("mapPath is null or empty!");

            _context = context;
            _map = new Dictionary<Position, Sprite>();
            _sprites = new List<Sprite>();
            _mapPath = mapPath;

            GenerateMap();
        }

        /// <summary>
        /// Map file format :
        /// LIMIT
        /// min
        /// max
        /// LIMIT
        /// BLOCKS
        /// id name imgPath
        /// BLOCKS
        /// MAP
        /// 
        /// MAP
        /// </summary>
        void GenerateMap()
        {
            //Verify if it's a map file
            if (!_mapPath.EndsWith(".totmap")) throw new ArgumentException("The map file is not correct (.totmap)");
            // Open map file
            string text = File.ReadAllText(_mapPath);
            if (String.IsNullOrEmpty(text)) throw new FileLoadException("File is empty ?");

            // Get background path
            _backgroundPath = StringBetweenString(text, "BACKGROUNDPATH", "BACKGROUNDPATHEND");
            _backgroundPath = _backgroundPath.Replace("\r", "");

            // Get limits
            string[] limits = StringBetweenString(text, "LIMIT", "LIMITEND").Split("\n");
            string[] limitMin = limits[0].Split(" ");
            _limitMin = new Position(Convert.ToSingle(limitMin[0]), Convert.ToSingle(limitMin[1]));
            string[] limitMax = limits[1].Split(" ");
            _limitMax = new Position(Convert.ToSingle(limitMax[0]), Convert.ToSingle(limitMax[1]));

            // Get all blocks in level (id, name, path, isSolid)
            string[] blocks = StringBetweenString(text, "BLOCKS", "BLOCKSEND").Split("\n");
            foreach(string s in blocks)
            {
                string[] str = s.Split(" ");
                _sprites.Add(new Sprite(str[0], str[1], str[2], Convert.ToBoolean(str[3]), this));
            }

            // Get map
            string[] mapParsed = StringBetweenString(text, "MAP", "MAPEND").Split("\n");

            //Boucle Y
            int indexTemp = 0;
            for(int y = Convert.ToInt32(_limitMax.Y); y >= Convert.ToInt32(_limitMin.Y); y-- )
            {
                for(int x = Convert.ToInt32(_limitMin.X); x <= Convert.ToInt32(_limitMax.X); x++)
                {
                    _map.Add(new Position(x, y), RetrieveSpriteWithId( mapParsed[indexTemp].Substring(x, 1) ));
                }
                indexTemp++;
            }
        }

        private Sprite RetrieveSpriteWithId(string strId)
        {
            foreach(Sprite spr in _sprites)
            {
                if (spr.Id == strId) return spr;
            }

            throw new Exception("ID INCONNU, ERREUR DANS LA MAP!");
        }

        private string StringBetweenString(string original, string str1, string str2)
        {
            int firstStringPosition = original.IndexOf(str1);
            int secondStringPosition = original.IndexOf(str2);
            return original.Substring(firstStringPosition + str1.Length + 2, secondStringPosition - firstStringPosition - str2.Length);
        }


        // Getters & Setters
        internal Dictionary<Position, Sprite> GetMap
        {
            get => _map;
        }

        internal Position GetLimitMax
        {
            get => _limitMax;
        }

        internal string GetBackground
        {
            get => _backgroundPath;
        }
    }
}
