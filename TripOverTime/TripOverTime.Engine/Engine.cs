using System;
using SFML;


namespace TripOverTime.Engine
{
    public class Engine
    {
        Menu _menu;
        Game _game; // Contient Map, Player, Monster
        Settings _settings;
        GUI _gui;

        public Engine()
        {
            _menu = new Menu();
            _game = new Game();
            _settings = new Settings();
            _gui = new GUI();


        }
    }
}
