using System;
using System.Collections.Generic;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    public class Game
    {
        Player _player;
        List<Monster> _monsters;
        Map _map;

        internal Game()
        {
        }

        public void StartGame(string mapPath)
        {
            _player = new Player(this, "player", new Position(), new Life(), 1);
            _monsters = new List<Monster>();
            _map = new Map(mapPath);
        }

        internal Map GetMapObject
        {
            get => _map;
        }
    }
}
