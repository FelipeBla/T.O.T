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

        internal Game(string mapPath, string playerPath)
        {
            _player = new Player(this, "player", new Position(), new Life(100, 100), 1, playerPath);
            _monsters = new List<Monster>();
            _map = new Map(this, mapPath);
        }

        internal Map GetMapObject
        {
            get => _map;
        }
        internal Player GetPlayer
        {
            get => _player;
        }
    }
}
