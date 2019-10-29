using System;
using SFML;

namespace TripOverTime.EngineNamespace
{
    public class Engine
    {
        internal bool CLOSE = false;

        Menu _menu;
        Game _game; // Contient Map, Player, Monster
        Settings _settings;
        GUI _gui;

        public Engine()
        {
            _menu = new Menu();
            _settings = new Settings();
            _gui = new GUI(this);
        }

        public void StartGame(string mapPath, string playerPath)
        {
            _game = new Game(mapPath, playerPath);
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

        public bool Close
        {
            get => CLOSE;
        }
    }
}
