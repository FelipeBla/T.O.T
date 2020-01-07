using System;
using System.Collections.Generic;
using System.Diagnostics;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace TripOverTime.EngineNamespace
{
    public class Engine
    {
        bool CLOSE = false;
        Stopwatch _timer;
        Checkpoint _checkpoint;

        SFML.Graphics.RenderWindow _window;
        Menu _menu;
        Game _game; // Contient Map, Player, Monster
        Settings _settings;
        GUI _gui;

        public Engine(SFML.Graphics.RenderWindow window)
        {
            _window = window;
            _menu = new Menu(window);
            _settings = new Settings(this, window);
            _gui = new GUI(this, window);
            _timer = new Stopwatch();
            _timer.Start();
        }

        public void StartGame(string mapPath, string playerPath)
        {
            _game = new Game(this, mapPath, playerPath, new Position(0, 3)); //0, 3
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>True if player is alive</returns>
        public short GameTick()
        {
            if (_game == null) throw new Exception("Game not started!");

            //Events
            _gui.Events();

            Console.Write("Player : " + _game.GetPlayer.RealPosition.X + ";" + _game.GetPlayer.RealPosition.Y);
            Console.WriteLine(" | Monster1 : " + _game.GetMonsters[0].Position.X + ";" + _game.GetMonsters[0].Position.Y + " " + _game.GetMonsters[0].life.GetCurrentPoint() + "HP.");
            //Gravity 4 player
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
                if ((sToPositive != null && sToNegative != null) && (sToPositive.IsDangerous || sToNegative.IsDangerous))
                {
                    //DIE
                    _game.GetPlayer.KilledBy = "Trap";
                    return -1;
                }
                else
                {
                    _game.GetPlayer.RoundY(); // Don't stuck player in ground
                }
            }

            if(!_game.GetPlayer.IsAlive)
            {
                //DIE
                _game.GetPlayer.KilledBy = "Monster";
                return -1;
            }

            //Monsters move + Attack
            foreach (Monster m in _game.GetMonsters)
            {
                if ( m.Position.X > _game.GetPlayer.RealPosition.X && m.isAlive) //left
                {
                    m.Orientation = "left";
                }
                else if(m.Position.X < _game.GetPlayer.RealPosition.X && m.isAlive) //right
                {
                    m.Orientation = "right";
                }

                if (!m.isAlive)
                {
                    m.MonsterDead();
                }
                else if (m.Position.X - 4 < _game.GetPlayer.RealPosition.X && m.Position.X - 1 > _game.GetPlayer.RealPosition.X || m.Position.X + 4 > _game.GetPlayer.RealPosition.X && m.Position.X + 1 < _game.GetPlayer.RealPosition.X)
                {
                    m.MonsterMove();
                }

                if (m.Position.X + 2 > _game.GetPlayer.RealPosition.X && m.Position.X - 2 < _game.GetPlayer.RealPosition.X && m.isAlive) //attack
                {
                    m.MonsterAttack();
                }
            }

            //Gravity 4 monsters
            foreach (Monster m in _game.GetMonsters)
            {
                _game.GetMapObject.GetMap.TryGetValue(new Position((float)Math.Round(m.Position.X, MidpointRounding.ToPositiveInfinity), (float)Math.Round(m.Position.Y - 1, MidpointRounding.ToPositiveInfinity)), out sToPositive);
                _game.GetMapObject.GetMap.TryGetValue(new Position((float)Math.Round(m.Position.X, MidpointRounding.ToNegativeInfinity), (float)Math.Round(m.Position.Y - 1, MidpointRounding.ToPositiveInfinity)), out sToNegative);
                if (sToPositive != null && !sToPositive.IsSolid && sToNegative != null && !sToNegative.IsSolid)
                {
                    //Block under monster isn't solid
                    m.Gravity();
                }
                else
                {
                    m.IsMoving = false;
                    m.RoundY(); // Don't stuck monster in ground
                }
            }

            // Recalibrate float
            _game.GetPlayer.RoundX();
            // WIN !!!
            Position end = _game.GetMapObject.GetEndPosition;
            if (end.X <= _game.GetPlayer.RealPosition.X)
            {
                Console.WriteLine("YOUWINNNNNNNNNN");
                // SHOW WIN MENU !
                return 0;
            }

            // Dead return -1;

            return 1;
        }

        public void WinMenu()
        {
            SFML.Graphics.Sprite background = new SFML.Graphics.Sprite(new Texture(@"..\..\..\..\Assets\Backgrounds\colored_desert.png"));
            if (background == null) throw new Exception("Sprite null!");

            background.Scale = new SFML.System.Vector2f(_window.Size.X / 550, _window.Size.Y / 550);
            _window.Draw(background);

            List<Text> lines = new List<Text>();
            //Lines
            lines.Add(new Text("YOU WIN !", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), 64));
            lines.Add(new Text("in : " + _game.TimeElapsed / 1000 + " seconds !", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), 48));
            lines.Add(new Text("With " + _game.GetPlayer.GetLife.GetCurrentPoint() + " HP.", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), 32));
            lines.Add(new Text("Press ENTER to QUIT", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), 32));

            lines[0].Color = Color.Green;
            lines[1].Color = Color.Yellow;
            lines[2].Color = Color.Red;
            lines[3].Color = Color.Black;

            for (int i = 0; i < lines.Count; i++)
            {
                lines[i].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (lines[i].GetGlobalBounds().Width)/2, (_window.Size.Y / 6) * i);
                _window.Draw(lines[i]);
            }

            _window.Display();

            bool quit = false;

            while(!quit)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
                    quit = true;
                System.Threading.Thread.Sleep(1);
            }

            // QUAND QUITTE LE MENU
            _menu = new Menu(_window);
            _settings = new Settings(this, _window);
            _gui = new GUI(this, _window);
            _timer = new Stopwatch();
            _timer.Start();
            _game = null;
        }

        public void DieMenu()
        {
            SFML.Graphics.Sprite background = new SFML.Graphics.Sprite(new Texture(@"..\..\..\..\Assets\Backgrounds\colored_desert.png"));
            if (background == null) throw new Exception("Sprite null!");

            background.Scale = new SFML.System.Vector2f(_window.Size.X / 550, _window.Size.Y / 550);
            _window.Draw(background);

            List<Text> lines = new List<Text>();
            //Lines
            lines.Add(new Text("YOU DIIIIE !", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), 64));
            lines.Add(new Text("Killed by : " + _game.GetPlayer.KilledBy, new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), 48));
            lines.Add(new Text("in : " + _game.TimeElapsed / 1000 + " seconds !", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), 32));
            lines.Add(new Text("Press ENTER to QUIT", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), 32));

            lines[0].Color = Color.Green;
            lines[1].Color = Color.Yellow;
            lines[2].Color = Color.Red;
            lines[3].Color = Color.Black;

            for (int i = 0; i < lines.Count; i++)
            {
                lines[i].Position = new SFML.System.Vector2f(_window.Size.X / 2 - (lines[i].GetGlobalBounds().Width) / 2, (_window.Size.Y / 6) * i);
                _window.Draw(lines[i]);
            }

            _window.Display();

            bool quit = false;

            while (!quit)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
                    quit = true;
                System.Threading.Thread.Sleep(1);
            }

            // QUAND QUITTE LE MENU
            _menu = new Menu(_window);
            _settings = new Settings(this, _window);
            _gui = new GUI(this, _window);
            _timer = new Stopwatch();
            _timer.Start();
            _game = null;
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
