using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    class Map
    {
        Dictionary<Sprite, Position> _sprites;
        string _backgroundPath;
        string _mapPath;
        Position _limitMin;
        Position _limitMax;

        internal Map(string mapPath)
        {
            if (String.IsNullOrEmpty(mapPath)) throw new ArgumentException("mapPath is null or empty!");

            _sprites = new Dictionary<Sprite, Position>();
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

            

            //From _limitMin.X to _limitMax.X

        }

        private string StringBetweenString(string original, string str1, string str2)
        {
            int firstStringPosition = original.IndexOf(str1);
            int secondStringPosition = original.IndexOf(str2);
            return original.Substring(firstStringPosition + str1.Length + 2, secondStringPosition - firstStringPosition - str2.Length);
        }
    }
}
