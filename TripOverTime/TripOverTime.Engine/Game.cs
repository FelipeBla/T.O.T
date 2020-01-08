using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    public class Game
    {
        Engine _context;
        Player _player;
        List<Monster> _monsters;
        Map _map;
        Stopwatch _timer;
        string _mapPath;

        internal Game(Engine context, string mapPath, string playerPath, Position startPosition)
        {
            _mapPath = mapPath;
            _context = context;
            _map = new Map(this, mapPath);
            _player = new Player(this, "player", startPosition, new Life(10, 1), 5, playerPath);
            _monsters = _map.GenerateMonsters();
            _timer = new Stopwatch();
            _timer.Start();
        }

        internal Map GetMapObject
        {
            get => _map;
        }
        public Player GetPlayer
        {
            get => _player;
        }
        internal Engine GetEngine
        {
            get => _context;
        }

        internal long TimeElapsed
        {
            get => _timer.ElapsedMilliseconds;
        }
        internal List<Monster> GetMonsters
        {
            get => _monsters;
            set => _monsters = value;
        }
        internal string MapPath
        {
            get => _mapPath;
        }
    }
}
