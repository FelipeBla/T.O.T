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

        internal Game(string mapPath, string playerPath, Position startPosition, string monsterPath)
        {
            _map = new Map(this, mapPath);
            _player = new Player(this, "player", startPosition, new Life(100, 100), 1, playerPath);
            _monsters = new List<Monster>();
            _monsters.Add(new Monster(this, "frog", new Position(0, 3), new Life(100, 100), 1, monsterPath));

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
        }
    }
}
