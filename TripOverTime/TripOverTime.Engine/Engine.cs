using System;
using System.Diagnostics;
using SFML;

namespace TripOverTime.EngineNamespace
{
    public class Engine
    {
        bool CLOSE = false;
        Stopwatch _timer;
        Checkpoint _checkpoint;

        Menu _menu;
        Game _game; // Contient Map, Player, Monster
        Settings _settings;
        GUI _gui;

        public Engine(SFML.Graphics.RenderWindow window)
        {
            _menu = new Menu(window);
            _settings = new Settings();
            _gui = new GUI(this, window);
            _timer = new Stopwatch();
            _timer.Start();
        }

        public void StartGame(string mapPath, string playerPath, string monsterPath)
        {
            _game = new Game(mapPath, playerPath, new Position(0, 3), monsterPath); //0, 3
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>True if player is alive</returns>
        public bool GameTick()
        {
            if (_game == null) throw new Exception("Game not started!");

            //Events
            _gui.Events();

            //Gravity
            Sprite sToPositive = null;
            Sprite sToNegative = null;
            _game.GetMapObject.GetMap.TryGetValue(new Position((float)Math.Round(_game.GetPlayer.RealPosition.X, MidpointRounding.ToPositiveInfinity), (float)Math.Round(_game.GetPlayer.RealPosition.Y - 1, MidpointRounding.ToPositiveInfinity)), out sToPositive);
            _game.GetMapObject.GetMap.TryGetValue(new Position((float)Math.Round(_game.GetPlayer.RealPosition.X, MidpointRounding.ToNegativeInfinity), (float)Math.Round(_game.GetPlayer.RealPosition.Y - 1, MidpointRounding.ToPositiveInfinity)), out sToNegative);
            if (sToPositive != null && !sToPositive.IsSolid && sToNegative != null && !sToNegative.IsSolid)
            {
                //Block under player isn't solid
                _game.GetPlayer.Gravity();
            }
            else
            {
                _game.GetPlayer.IsJumping = false;
                _game.GetPlayer.RoundY(); // Don't stuck player in ground
            }

            //Gravity
            foreach (Monster m in _game.GetMonsters)
            {
                _game.GetMapObject.GetMap.TryGetValue(new Position((float)Math.Round(m.Position.X, MidpointRounding.ToPositiveInfinity), (float)Math.Round(m.Position.Y - 1, MidpointRounding.ToPositiveInfinity)), out sToPositive);
                _game.GetMapObject.GetMap.TryGetValue(new Position((float)Math.Round(m.Position.X, MidpointRounding.ToNegativeInfinity), (float)Math.Round(m.Position.Y - 1, MidpointRounding.ToPositiveInfinity)), out sToNegative);
                if (sToPositive != null && !sToPositive.IsSolid && sToNegative != null && !sToNegative.IsSolid)
                {
                    //Block under player isn't solid
                    m.Gravity();
                }
                else
                {
                    m.RoundY(); // Don't stuck player in ground
                }
            }

            // Recalibrate float
            _game.GetPlayer.RoundX();
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
            set => CLOSE = value;
        }

        internal Stopwatch Timer
        {
            get => _timer;
        }
        public Checkpoint GetCheckpoint
        {
            get => _checkpoint;
        }
    }
}
