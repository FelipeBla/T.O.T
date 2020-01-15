using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    class Map
    {
        Dictionary<Position, Sprite> _map;
        Dictionary<Position, Sprite> _map2;
        List<Sprite> _sprites;
        string _backgroundPath;
        string _lifebarPath = "..\\..\\..\\..\\Assets\\HUD\\lifebar.png";
        string _mapPath;
        Position _limitMin;
        Position _limitMax;
        List<Position> _checkpointPosition;
        Game _context;

        internal Map(Game context, string mapPath)
        {
            if (String.IsNullOrEmpty(mapPath)) throw new ArgumentException("mapPath is null or empty!");
            if (context == null) throw new ArgumentNullException("Context null!");

            _context = context;
            _map = new Dictionary<Position, Sprite>();
            _map2 = new Dictionary<Position, Sprite>();
            _sprites = new List<Sprite>();
            _mapPath = mapPath;
            _checkpointPosition = new List<Position>();

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
        /// MONSTER
        /// id name [x;y] hp imgDirPath range attackcombo
        /// MONSTER
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
            for (int i = 0; i < blocks.Length; i++)
            {
                string s = (string)blocks[i];
                string[] str = s.Split(" ");
                _sprites.Add(new Sprite(str[0], str[1], str[2], Convert.ToBoolean(str[3]), this));
                if (str.Length > 4)
                {
                    //Console.WriteLine(str[4]);
                    //if (str[4] == "DANGEROUS")
                    //{
                        _sprites[i].IsDangerous = true;
                        //Console.WriteLine(str[1] + " IS DANGEROUS");
                    //}
                }
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

        internal List<Monster> GenerateMonsters()
        {
            //Verify if it's a map file
            if (!_mapPath.EndsWith(".totmap")) throw new ArgumentException("The map file is not correct (.totmap)");
            // Open map file
            string text = File.ReadAllText(_mapPath);
            if (String.IsNullOrEmpty(text)) throw new FileLoadException("File is empty ?");

            // Get monsters
            // name x y hp
            string[] strmonsters = StringBetweenString(text, "MONSTER", "MONSTEREND").Split("\n");
            List<Monster> monsters = new List<Monster>();
            foreach (string s in strmonsters)
            {
                string[] str = s.Split(" ");

                monsters.Add(new Monster(_context, str[0], new Position(Convert.ToSingle(str[1]), Convert.ToSingle(str[2])), new Life(Convert.ToUInt16(str[3])), Convert.ToUInt16(str[4]), float.Parse(str[5])/100, Convert.ToSingle(str[6]), str[7]));
            }

            return monsters;
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

        internal Dictionary<Position, Sprite> GetMap2
        {
            get => _map2;
        }

        internal Position GetLimitMax
        {
            get => _limitMax;
        }

        internal Position GetLimitMin
        {
            get => _limitMin;
        }

        internal string GetBackground
        {
            get => _backgroundPath;
        }
        internal string GetLifeBar
        {
            get => _lifebarPath;
        }

        internal Position GetEndPosition
        {
            get
            {
                foreach(KeyValuePair<Position, Sprite> s in _map)
                {
                    if (s.Value.IsEnd) return s.Key;
                }
                
                throw new InvalidOperationException("END NOT FOUND!");
            }
        }

        internal List<Position> GetCheckpointPosition
        {
            get => _checkpointPosition;
        }
        internal Game GetGame
        {
            get => _context;
        }
    }
}
