using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    class Map
    {
        Dictionary<Position, Sprite> _map;
        List<Position> _heart;
        List<Position> _star;
        List<Position> _trap;
        List<Position2> _trap2;
        Sprite _spriteChange;
        Sprite _spriteChange2;
        List<Position2> _heart2;
        List<Position2> _star2;
        List<Sprite> _sprites;
        string _backgroundPath;
        string _lifebarPath = "..\\..\\..\\..\\Assets\\HUD\\lifebar.png";
        string _mapPath;
        Position _limitMin;
        Position _limitMax;
        List<Position> _checkpointPosition;
        Game _context;
        bool _multiplayer;

        //multipayer
        Dictionary<Position2, Sprite> _map2;
        List<Sprite> _sprites2;
        string _backgroundPath2;
        string _lifebarPath2 = "..\\..\\..\\..\\Assets\\HUD\\lifebar.png";
        string _mapPath2;
        Position2 _limitMin2;
        Position2 _limitMax2;
        List<Position2> _checkpointPosition2;
        Game _context2;

        internal Map(Game context, string mapPath, bool multiplayer)
        {
            if (String.IsNullOrEmpty(mapPath)) throw new ArgumentException("mapPath is null or empty!");
            if (context == null) throw new ArgumentNullException("Context null!");

            _context = context;
            _map = new Dictionary<Position, Sprite>();
            _heart = new List<Position>();
            _star = new List<Position>();
            _trap = new List<Position>();
            _sprites = new List<Sprite>();
            _mapPath = mapPath;
            _checkpointPosition = new List<Position>();
            _multiplayer = multiplayer;

            if (multiplayer)
            {
                _heart2 = new List<Position2>();
                _star2 = new List<Position2>();
                _context2 = context;
                _map2 = new Dictionary<Position2, Sprite>();
                _sprites2 = new List<Sprite>();
                _mapPath2 = mapPath;
                _checkpointPosition2 = new List<Position2>();
                _heart2 = new List<Position2>();
                _star2 = new List<Position2>();
                _trap2 = new List<Position2>();

            }

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

            // Get limits2
            if (_multiplayer)
            {
                string[] limits2 = StringBetweenString(text, "LIMIT", "LIMITEND").Split("\n");
                string[] limitMin2 = limits[0].Split(" ");
                _limitMin2 = new Position2(Convert.ToSingle(limitMin[0]), Convert.ToSingle(limitMin[1]));
                string[] limitMax2 = limits[1].Split(" ");
                _limitMax2 = new Position2(Convert.ToSingle(limitMax[0]), Convert.ToSingle(limitMax[1]));
            }
            

            // Get all blocks in level (id, name, path, isSolid)
            string[] blocks = StringBetweenString(text, "BLOCKS", "BLOCKSEND").Split("\n");
            for (int i = 0; i < blocks.Length; i++)
            {
                string s = (string)blocks[i];
                string[] str = s.Split(" ");
                Sprite SpriteToAdd = new Sprite(str[0], str[1], str[2], Convert.ToBoolean(str[3]),true, this, false, false, false);
                _sprites.Add(SpriteToAdd);
                if (_context2 != null) _sprites2.Add(SpriteToAdd);
                if (str[1] == "TRAP")
                {
                    //Console.WriteLine(str[4]);
                    //if (str[4] == "DANGEROUS")
                    //{
                    _sprites[i].IsDangerous = true;
                    if (_context2 != null) _sprites2[i].IsDangerous2 = true;
                    Console.WriteLine(str[1] + " IS DANGEROUS");
                    //}
                }
                if (str[1] == "AIR")
                {
                    _spriteChange = new Sprite(str[0], str[1], str[2], Convert.ToBoolean(str[3]), true, this, false, false, false);
                    if (_context2 != null) _spriteChange2 = _spriteChange;
                }
            }

            // Get map
            string[] mapParsed = StringBetweenString(text, "MAP", "MAPEND").Split("\n");
            for (int y = 0; y < mapParsed.Length; y++)
            {
                for (int x = 0; x < mapParsed[y].Length; x++)
                {
                    if (mapParsed[y][x] == 'H')
                    {
                        _heart.Add(new Position((float)x, (float)_limitMax.Y - y));
                        if (_multiplayer)
                        {
                            _heart2.Add(new Position2((float)x, (float)_limitMax.Y - y));
                        }
                    }
                    if (mapParsed[y][x] == 'S')
                    {
                        _star.Add(new Position((float)x, (float)_limitMax.Y - y));
                        if (_multiplayer)
                        {
                            _star2.Add(new Position2((float)x, (float)_limitMax.Y - y));
                        }
                    }
                    if (mapParsed[y][x] == '2')
                    {
                        _trap.Add(new Position((float)x, (float)_limitMax.Y - y));
                        if (_multiplayer)
                        {
                            _trap2.Add(new Position2((float)x, (float)_limitMax.Y - y));
                        }
                    }
                }
            }

            //Boucle Y
            int indexTemp = 0;
            for (int y = Convert.ToInt32(_limitMax.Y); y >= Convert.ToInt32(_limitMin.Y); y--)
            {
                for (int x = Convert.ToInt32(_limitMin.X); x <= Convert.ToInt32(_limitMax.X); x++)
                {
                    _map.Add(new Position(x, y), RetrieveSpriteWithId(mapParsed[indexTemp].Substring(x, 1)));
                    if (_context2 != null) _map2.Add(new Position2(x, y), RetrieveSpriteWithId(mapParsed[indexTemp].Substring(x, 1)));
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
            Dictionary<string, Sprite> allMonsters = new Dictionary<string, Sprite>();
            List<Monster> monsters = new List<Monster>();

            foreach (string s in strmonsters)
            {
                string[] str = s.Split(" ");

                if (!(allMonsters.ContainsKey(str[0]))) // Ne contient pas deja le monstre
                {
                    allMonsters.Add(str[0], new Sprite("MONSTER420", str[0], $@"..\..\..\..\Assets\Monster\{str[0]}", true, true, _context.GetMapObject, true, false, false));
                }
            }

            foreach (string s in strmonsters)
            {
                string[] str = s.Split(" ");

                monsters.Add(new Monster(_context, str[0], new Position(Convert.ToSingle(str[1]), Convert.ToSingle(str[2])), new Life(Convert.ToUInt16(str[3])), Convert.ToUInt16(str[4]), float.Parse(str[5]), Convert.ToSingle(str[6]), str[7], allMonsters[str[0]].Clone()));
            }

            return monsters;
        }
        internal List<Monster> GenerateMonsters2()
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

                monsters.Add(new Monster(_context2, str[0], new Position(Convert.ToSingle(str[1]), Convert.ToSingle(str[2])), new Life(Convert.ToUInt16(str[3])), Convert.ToUInt16(str[4]), float.Parse(str[5]), Convert.ToSingle(str[6]), str[7], null, true));

            }

            return monsters;
        }
        internal Boss GenerateBoss()
        {
            //Verify if it's a map file
            if (!_mapPath.EndsWith(".totmap")) throw new ArgumentException("The map file is not correct (.totmap)");
            // Open map file
            string text = File.ReadAllText(_mapPath);
            if (String.IsNullOrEmpty(text)) throw new FileLoadException("File is empty ?");

            Boss boss = null;

            if (text.Contains("BOSS"))
            {
                // Get boss
                // name x y hp
                string[] strBoss = StringBetweenString(text, "BOSS", "BOSSEND").Split("\n");
                foreach (string s in strBoss)
                {
                    string[] str = s.Split(" ");

                    boss = new Boss(_context, str[0], new Position(Convert.ToSingle(str[1]), Convert.ToSingle(str[2])), new Position2(Convert.ToSingle(str[1]), Convert.ToSingle(str[2])), new Life(Convert.ToUInt16(str[3])), Convert.ToUInt16(str[4]), float.Parse(str[5]) / 100, float.Parse(str[6]), str[7]);
                }
            }

            return boss;

        }
        internal Boss GenerateBoss2()
        {
            //Verify if it's a map file
            if (!_mapPath.EndsWith(".totmap")) throw new ArgumentException("The map file is not correct (.totmap)");
            // Open map file
            string text = File.ReadAllText(_mapPath);
            if (String.IsNullOrEmpty(text)) throw new FileLoadException("File is empty ?");

            Boss boss = null;

            if (text.Contains("BOSS"))
            {
                // Get boss
                // name x y hp
                string[] strBoss = StringBetweenString(text, "BOSS", "BOSSEND").Split("\n");

                foreach (string s in strBoss)
                {
                    string[] str = s.Split(" ");

                    boss = new Boss(_context2, str[0], new Position(Convert.ToSingle(str[1]), Convert.ToSingle(str[2])), new Position2(Convert.ToSingle(str[1]), Convert.ToSingle(str[2])), new Life(Convert.ToUInt16(str[3])), Convert.ToUInt16(str[4]), float.Parse(str[5]) / 100, float.Parse(str[6]), str[7]);
                }
            }


            return boss;


        }
        private Sprite RetrieveSpriteWithId(string strId)
        {
            foreach (Sprite spr in _sprites)
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

        internal Dictionary<Position2, Sprite> GetMap2
        {
            get => _map2;
        }

        internal Position GetLimitMax
        {
            get => _limitMax;
        }

        internal Position2 GetLimitMax2
        {
            get => _limitMax2;
        }
        internal Position GetLimitMin
        {
            get => _limitMin;
        }

        internal Position2 GetLimitMin2
        {
            get => _limitMin2;
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
                foreach (KeyValuePair<Position, Sprite> s in _map)
                {
                    if (s.Value.IsEnd) return s.Key;
                }

                throw new InvalidOperationException("END NOT FOUND!");
            }
        }
        internal Position2 GetEndPosition2
        {
            get
            {
                foreach (KeyValuePair<Position2, Sprite> s in _map2)
                {
                    if (s.Value.IsEnd2) return s.Key;
                }

                throw new InvalidOperationException("END NOT FOUND!");
            }
        }

        internal List<Position> GetCheckpointPosition
        {
            get => _checkpointPosition;
        }
        internal List<Position2> GetCheckpointPosition2
        {
            get => _checkpointPosition2;
        }
        internal Game GetGame
        {
            get => _context;
        }
        internal Game GetGame2
        {
            get => _context2;
        }
        internal List<Position> GetHeart
        {
            get => _heart;
        }
        internal List<Position> GetStar
        {
            get => _star;
        }
        internal List<Position2> GetStar2
        {
            get => _star2;
        }
        internal List<Position> GetTrap
        {
            get => _trap;
        }
        internal List<Position2> GetHeart2
        {
            get => _heart2;
            set => _heart2 = value;
        }
        internal Sprite GetSpriteChange
        {
            get => _spriteChange;
        }
        internal Sprite GetSpriteChange2
        {
            get => _spriteChange2;
        }
    }
}
