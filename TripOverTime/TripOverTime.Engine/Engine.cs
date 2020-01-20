﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        Game _game2;
        Settings _settings;
        GUI _gui;
        Font _globalFont;

        public Engine(SFML.Graphics.RenderWindow window)
        {
            _window = window;
            _menu = new Menu(window);
            _settings = new Settings(this, window);
            _gui = new GUI(this, window);
            _globalFont = new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf");
            _timer = new Stopwatch();
            _timer.Start();
        }

        public void StartGame(string mapPath)
        {
            //Verify if it's a map file
                if (!mapPath.EndsWith(".totmap")) throw new ArgumentException("The map file is not correct (.totmap)");
            // Open map file
            string text = File.ReadAllText(mapPath);
            if (String.IsNullOrEmpty(text)) throw new FileLoadException("File is empty ?");

            // Get player
            // path x y life atk
            string[] strPlayer = StringBetweenString(text, "PLAYER", "PLAYEREND").Split(" ");

            _game = new Game(this, mapPath, strPlayer[0], new Position(Convert.ToSingle(strPlayer[1]), Convert.ToSingle(strPlayer[2])), Convert.ToUInt16(strPlayer[3]), Convert.ToUInt16(strPlayer[4])); //0, 3
            _game2 = new Game(this, mapPath, strPlayer[0], new Position(Convert.ToSingle(strPlayer[1]), Convert.ToSingle(strPlayer[2])), Convert.ToUInt16(strPlayer[3]), Convert.ToUInt16(strPlayer[4])); //0, 3

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


            //Console.Write("Player : " + _game.GetPlayer.RealPosition.X + ";" + _game.GetPlayer.RealPosition.Y);
            //Console.WriteLine(" | Monster1 : " + _game.GetMonsters[0].Position.X + ";" + _game.GetMonsters[0].Position.Y + " " + _game.GetMonsters[0].life.GetCurrentPoint() + "HP.");
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
                else if (m.Position.X - 4 < _game.GetPlayer.RealPosition.X && m.Position.X - 1.2 > _game.GetPlayer.RealPosition.X || m.Position.X + 4 > _game.GetPlayer.RealPosition.X && m.Position.X + 1.2 < _game.GetPlayer.RealPosition.X)
                {
                    m.MonsterMove();
                }

                if (m.Position.X + m.Range > _game.GetPlayer.RealPosition.X && m.Position.X - m.Range < _game.GetPlayer.RealPosition.X && m.isAlive) //attack
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


            // boss
            if (!_game.GetBoss.IsAlive)
            {
                _game.GetBoss.BossDead();
            }
            else
            {
                if (_game.GetBoss.Position.X > _game.GetPlayer.RealPosition.X) //left
                {
                    _game.GetBoss.Orientation = "left";
                }
                else if (_game.GetBoss.Position.X < _game.GetPlayer.RealPosition.X) //right
                {
                    _game.GetBoss.Orientation = "right";
                }

                _game.GetBoss.GetBossSprite.BossOrientation(_game.GetBoss);

                if (_game.GetBoss.Position.X + 3 > _game.GetPlayer.RealPosition.X && _game.GetBoss.Position.X - 3 < _game.GetPlayer.RealPosition.X) //attack
                {
                    if (_game.GetBoss.Position.X - 1 > _game.GetPlayer.RealPosition.X || _game.GetBoss.Position.X + 1 < _game.GetPlayer.RealPosition.X)
                    {
                        _game.GetBoss.BossMove();
                    }
                    _game.GetBoss.BossAttack();
                }

                else if (_game.GetBoss.Position.X - 6 < _game.GetPlayer.RealPosition.X && _game.GetBoss.Position.X - 1 > _game.GetPlayer.RealPosition.X || _game.GetBoss.Position.X + 6 > _game.GetPlayer.RealPosition.X && _game.GetBoss.Position.X + 1 < _game.GetPlayer.RealPosition.X)
                {
                    _game.GetBoss.BossMove();
                    _game.GetBoss.GetBossSprite.BossMoveAnimation(_game.GetBoss);
                }
            }

            _game.GetMapObject.GetMap.TryGetValue(new Position((float)Math.Round(_game.GetBoss.Position.X, MidpointRounding.ToPositiveInfinity), (float)Math.Round(_game.GetBoss.Position.Y - 1, MidpointRounding.ToPositiveInfinity)), out sToPositive);
            _game.GetMapObject.GetMap.TryGetValue(new Position((float)Math.Round(_game.GetBoss.Position.X, MidpointRounding.ToNegativeInfinity), (float)Math.Round(_game.GetBoss.Position.Y - 1, MidpointRounding.ToPositiveInfinity)), out sToNegative);
            if (sToPositive != null && !sToPositive.IsSolid && sToNegative != null && !sToNegative.IsSolid)
            {
                //Block under monster isn't solid
                _game.GetBoss.Gravity();
            }
            else
            {
                _game.GetBoss.IsMoving = false;
                _game.GetBoss.RoundY(); // Don't stuck monster in ground
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

        public short GameTick2()
        {
            if (_game == null) throw new Exception("Game not started!");
            if (_game2 == null) throw new Exception("Game 2 not started!");

            //Events
            _gui.Events2();


            //Console.Write("Player : " + _game.GetPlayer.RealPosition.X + ";" + _game.GetPlayer.RealPosition.Y);
            //Console.WriteLine(" | Monster1 : " + _game.GetMonsters[0].Position.X + ";" + _game.GetMonsters[0].Position.Y + " " + _game.GetMonsters[0].life.GetCurrentPoint() + "HP.");
            //Gravity 4 player
            Sprite sToPositive = null;
            Sprite sToNegative = null;
            Sprite sToPositive2 = null;
            Sprite sToNegative2 = null;
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

            if (!_game.GetPlayer.IsAlive)
            {
                //DIE
                _game.GetPlayer.KilledBy = "Monster";
                return -1;
            }


            //Monsters move + Attack
            foreach (Monster m in _game.GetMonsters)
            {
                

                if (!m.isAlive)
                {
                    m.MonsterDead();
                }
                else
                {
                    if (m.Position.X > _game.GetPlayer.RealPosition.X) //left
                    {
                        m.Orientation = "left";
                    }
                    else if (m.Position.X < _game.GetPlayer.RealPosition.X) //right
                    {
                        m.Orientation = "right";
                    }

                    if (m.Position.X - 4 < _game.GetPlayer.RealPosition.X && m.Position.X - 1 > _game.GetPlayer.RealPosition.X || m.Position.X + 4 > _game.GetPlayer.RealPosition.X && m.Position.X + 1 < _game.GetPlayer.RealPosition.X)
                    {
                        m.MonsterMove();
                    }

                    {
                        m.MonsterAttack();
                    }
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

            // boss
            if (!_game.GetBoss.IsAlive)
            {
                _game.GetBoss.BossDead();
            }
            else
            {
                if (_game.GetBoss.Position.X > _game.GetPlayer.RealPosition.X) //left
                {
                    _game.GetBoss.Orientation = "left";
                }
                else if (_game.GetBoss.Position.X < _game.GetPlayer.RealPosition.X) //right
                {
                    _game.GetBoss.Orientation = "right";
                }

                _game.GetBoss.GetBossSprite.BossOrientation(_game.GetBoss);

                if (_game.GetBoss.Position.X + 3 > _game.GetPlayer.RealPosition.X && _game.GetBoss.Position.X - 3 < _game.GetPlayer.RealPosition.X) //attack
                {
                    if (_game.GetBoss.Position.X - 1 > _game.GetPlayer.RealPosition.X || _game.GetBoss.Position.X + 1 < _game.GetPlayer.RealPosition.X)
                    {
                        _game.GetBoss.BossMove();
                    }
                    _game.GetBoss.BossAttack();
                }

                else if (_game.GetBoss.Position.X - 6 < _game.GetPlayer.RealPosition.X && _game.GetBoss.Position.X - 1 > _game.GetPlayer.RealPosition.X || _game.GetBoss.Position.X + 6 > _game.GetPlayer.RealPosition.X && _game.GetBoss.Position.X + 1 < _game.GetPlayer.RealPosition.X)
                {
                    _game.GetBoss.BossMove();
                    _game.GetBoss.GetBossSprite.BossMoveAnimation(_game.GetBoss);
                }
            }

            _game.GetMapObject.GetMap.TryGetValue(new Position((float)Math.Round(_game.GetBoss.Position.X, MidpointRounding.ToPositiveInfinity), (float)Math.Round(_game.GetBoss.Position.Y - 1, MidpointRounding.ToPositiveInfinity)), out sToPositive);
            _game.GetMapObject.GetMap.TryGetValue(new Position((float)Math.Round(_game.GetBoss.Position.X, MidpointRounding.ToNegativeInfinity), (float)Math.Round(_game.GetBoss.Position.Y - 1, MidpointRounding.ToPositiveInfinity)), out sToNegative);
            if (sToPositive != null && !sToPositive.IsSolid && sToNegative != null && !sToNegative.IsSolid)
            {
                //Block under monster isn't solid
                _game.GetBoss.Gravity();
            }
            else
            {
                _game.GetBoss.IsMoving = false;
                _game.GetBoss.RoundY(); // Don't stuck monster in ground
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


            //-------------------------------------------------------player 2
            _game2.GetMapObject.GetMap.TryGetValue(new Position((float)Math.Round(_game2.GetPlayer2.RealPosition2.X, MidpointRounding.ToPositiveInfinity), (float)Math.Round(_game2.GetPlayer2.RealPosition2.Y - 1, MidpointRounding.ToPositiveInfinity)), out sToPositive);
            _game2.GetMapObject.GetMap.TryGetValue(new Position((float)Math.Round(_game2.GetPlayer2.RealPosition2.X, MidpointRounding.ToNegativeInfinity), (float)Math.Round(_game2.GetPlayer2.RealPosition2.Y - 1, MidpointRounding.ToPositiveInfinity)), out sToNegative);


            if (sToPositive2 != null && !sToPositive2.IsSolid && sToNegative2 != null && !sToNegative2.IsSolid)
            {
                //Block under player isn't solid
                _game2.GetPlayer2.Gravity();
            }
            else
            {
                _game.GetPlayer2.IsJumping = false;
                if ((sToPositive2 != null && sToNegative2 != null) && (sToPositive2.IsDangerous || sToNegative2.IsDangerous))
                {
                    //DIE
                    _game.GetPlayer2.KilledBy = "Trap";
                    return -1;
                }
                else
                {
                    _game2.GetPlayer2.RoundY(); // Don't stuck player in ground
                }
            }

            if (!_game.GetPlayer2.IsAlive)
            {
                //DIE
                _game.GetPlayer2.KilledBy = "Monster";
                return -1;
            }

            //Monsters move + Attack
            foreach (Monster m2 in _game.GetMonsters2)
            {
                if (m2.Position.X > _game.GetPlayer2.RealPosition2.X && m2.isAlive) //left
                {
                    m2.Orientation = "left";
                }
                else if (m2.Position.X < _game.GetPlayer2.RealPosition2.X && m2.isAlive) //right
                {
                    m2.Orientation = "right";
                }

                if (!m2.isAlive)
                {
                    m2.MonsterDead();
                }
                else if (m2.Position.X - 4 < _game2.GetPlayer2.RealPosition2.X && m2.Position2.X - 1 > _game2.GetPlayer2.RealPosition.X || m2.Position.X + 4 > _game2.GetPlayer2.RealPosition2.X && m2.Position2.X + 1 < _game2.GetPlayer2.RealPosition.X)
                {
                    m2.MonsterMove();
                }

                if (m2.Position2.X + 2 > _game2.GetPlayer2.RealPosition2.X && m2.Position2.X - 2 < _game.GetPlayer2.RealPosition2.X && m2.isAlive) //attack
                {
                    m2.MonsterAttack();
                }
            }

            //Gravity 4 monsters
            foreach (Monster m2 in _game.GetMonsters2)
            {
                _game2.GetMapObject.GetMap2.TryGetValue(new Position((float)Math.Round(m2.Position2.X, MidpointRounding.ToPositiveInfinity), (float)Math.Round(m2.Position2.Y - 1, MidpointRounding.ToPositiveInfinity)), out sToPositive2);
                _game2.GetMapObject.GetMap2.TryGetValue(new Position((float)Math.Round(m2.Position2.X, MidpointRounding.ToNegativeInfinity), (float)Math.Round(m2.Position2.Y - 1, MidpointRounding.ToPositiveInfinity)), out sToNegative2);
                if (sToPositive2 != null && !sToPositive2.IsSolid && sToNegative2 != null && !sToNegative2.IsSolid)
                {
                    //Block under monster isn't solid
                    m2.Gravity();
                }
                else
                {
                    m2.IsMoving = false;
                    m2.RoundY(); // Don't stuck monster in ground
                }



            }

            // Recalibrate float
            _game.GetPlayer2.RoundX();
            // WIN !!!
            Position end2 = _game2.GetMapObject.GetEndPosition;
            if (end2.X <= _game2.GetPlayer2.RealPosition2.X)
            {
                Console.WriteLine("YOUWINNNNNNNNNN");
                // SHOW WIN MENU !
                return 0;
            }


            // Dead return -1;

            return 1;
        }

        public bool WinMenu()
        {
            SFML.Graphics.Sprite background = new SFML.Graphics.Sprite(new Texture(@"..\..\..\..\Assets\Backgrounds\colored_desert.png"));
            if (background == null) throw new Exception("Sprite null!");

            background.Scale = new SFML.System.Vector2f(_window.Size.X / 550, _window.Size.Y / 550);
            _window.Draw(background);

            List<Text> lines = new List<Text>();
            //Lines
            lines.Add(new Text("YOU WIN !", _globalFont, 64));
            lines.Add(new Text("in : " + _game.TimeElapsed / 1000 + " seconds !", _globalFont, 48));
            lines.Add(new Text("With " + _game.GetPlayer.GetLife.GetCurrentPoint() + " HP.", _globalFont, 32));
            lines.Add(new Text("Press ENTER/A to QUIT", _globalFont, 32));

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
            bool loadNextLevel = false;
            while(!quit)
            {
                Joystick.Update();
                if (Keyboard.IsKeyPressed(Keyboard.Key.Enter) || (Joystick.IsConnected(0) && Joystick.IsButtonPressed(0, 0)))
                {
                    quit = true;
                    loadNextLevel = true;
                }

                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape) /*|| (Joystick.IsConnected(0) && Joystick.IsButtonPressed(0, 0))*/) //TODO touche de la manette pour quitter
                {
                    loadNextLevel = false;
                    quit = true;
                }

                System.Threading.Thread.Sleep(1);
            }

            // QUAND QUITTE LE MENU
            _menu = new Menu(_window);
            _settings = new Settings(this, _window);
            _gui = new GUI(this, _window);
            _timer = new Stopwatch();
            _timer.Start();
            _game = null;

            return loadNextLevel;
        }

        public void DieMenu()
        {
            SFML.Graphics.Sprite background = new SFML.Graphics.Sprite(new Texture(@"..\..\..\..\Assets\Backgrounds\colored_desert.png"));
            if (background == null) throw new Exception("Sprite null!");

            background.Scale = new SFML.System.Vector2f(_window.Size.X / 550, _window.Size.Y / 550);
            _window.Draw(background);

            List<Text> lines = new List<Text>();
            //Lines
            lines.Add(new Text("YOU DIIIIE !", _globalFont, 64));
            lines.Add(new Text("Killed by : " + _game.GetPlayer.KilledBy, _globalFont, 48));
            lines.Add(new Text("in : " + _game.TimeElapsed / 1000 + " seconds !", _globalFont, 32));
            lines.Add(new Text("Press ENTER/A to QUIT", _globalFont, 32));

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
                Joystick.Update();
                if (Keyboard.IsKeyPressed(Keyboard.Key.Enter) || (Joystick.IsConnected(0) && Joystick.IsButtonPressed(0, 0)))
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

        private string StringBetweenString(string original, string str1, string str2)
        {
            int firstStringPosition = original.IndexOf(str1);
            int secondStringPosition = original.IndexOf(str2);
            return original.Substring(firstStringPosition + str1.Length + 2, secondStringPosition - firstStringPosition - str2.Length);
        }

        public Menu GetMenu
        {
            get => _menu;
        }
        public Game GetGame
        {
            get => _game;
        }
        public Game GetGame2
        {
            get => _game2;
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

        public Font GetFont
        {
            get => _globalFont;
        }
    }
}
