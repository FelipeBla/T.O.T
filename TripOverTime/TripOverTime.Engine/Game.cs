using System;
using System.Collections.Generic;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    public class Game
    {
        Engine _context;
        Player _player;
        List<Monster> _monsters;
        Map _map;

        internal Game(Engine context, string mapPath, string playerPath, Position startPosition)
        {
            _context = context;
            _map = new Map(this, mapPath);
            _player = new Player(this, "player", startPosition, new Life(100, 1), 5, playerPath);
            _monsters = _map.GenerateMonsters();
        }

        internal Map GetMapObject
        {
            get => _map;
        }
        internal Player GetPlayer
        {
            get => _player;
        }
        internal Engine GetEngine
        {
            get => _context;
        }

        internal List<Monster> GetMonsters
        {
            get => _monsters;
            set => _monsters = value;
        }
    }
}
