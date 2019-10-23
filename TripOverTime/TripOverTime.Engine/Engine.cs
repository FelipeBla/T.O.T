using System;
using SFML;


namespace TripOverTime.EngineNamespace
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

        public Menu GetMenu
        {
            get => _menu;
        }
        public Game GetGame
        {
            get => _game;
        }
        public Settings GetSettings
        {
            get => _settings;
        }
        public GUI GetGUI
        {
            get => _gui;
        }

    }
}
