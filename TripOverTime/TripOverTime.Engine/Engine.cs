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

        /// <summary>
        /// 
        /// </summary>
        /// <returns>True if player is alive</returns>
        public bool GameTick()
        {
            if (_game == null) throw new Exception("Game not started!");

            //Gravity
            Sprite s = null;
            if (_game.GetMapObject.GetMap.TryGetValue(new Position(_game.GetPlayer.Position.X, _game.GetPlayer.Position.Y - 1), out s)) Console.WriteLine("ok");
            if (s != null && !s.IsSolid)
            {
                //Block under player isn't solid
                _game.GetPlayer.Position.Y--;
                Console.WriteLine("descends!");
            }

            return _game.GetPlayer.IsAlive;
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
