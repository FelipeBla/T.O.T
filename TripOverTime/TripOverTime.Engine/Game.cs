using System;
using System.Collections.Generic;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    public class Game
    {
        Player _player;
        Monster _monster;
        Map _map;

        internal Game()
        {
        }

        public void StartGame(string mapPath)
        {
            _player = new Player();
            _monster = new Monster();
            _map = new Map(mapPath);
        }

    }
}
