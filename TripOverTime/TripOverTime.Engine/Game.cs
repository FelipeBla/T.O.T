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
        Player _player2;
        List<Monster> _monsters;
        List<Monster> _monsters2;
        Map _map;
        Stopwatch _timer;
        string _mapPath;

        internal Game(Engine context, string mapPath, string playerPath, Position startPosition, ushort lifePoint, ushort atk)
        {
            _mapPath = mapPath;
            _context = context;
            _map = new Map(this, mapPath);
            _player = new Player(this, "player", startPosition, new Life(lifePoint), atk, playerPath);
            _player2 = new Player(this, "player", startPosition, new Life(100, 1), 5, playerPath);
            _monsters = _map.GenerateMonsters();
            _monsters2 = _map.GenerateMonsters();
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
        public Player GetPlayer2
        {
            get => _player2;
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
        internal List<Monster> GetMonsters2
        {
            get => _monsters2;
            set => _monsters2 = value;
        }
    }
}
