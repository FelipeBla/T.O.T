using System;
using System.Collections.Generic;
using System.Text;

namespace TripOverTime.Engine
{
    class Game
    {
        Player _player;
        Monster _monster;
        Map _map;

        internal Game()
        {
            _player = new Player();
            _monster = new Monster();
            _map = new Map();
        }

    }
}
