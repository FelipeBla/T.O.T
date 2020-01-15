using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    public class MapEditor
    {
        uint _charSize;
        Font _font1;
        Font _font2;
        List<EventHandler<KeyEventArgs>> _eventsKey;
        List<EventHandler<TextEventArgs>> _eventsText;

        public MapEditor()
        {
            _font1 = new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf");
            _font2 = new Font(@"..\..\..\..\Assets\Fonts\Comfortaa-Regular.ttf");
            _charSize = 32;

            _eventsKey = new List<EventHandler<KeyEventArgs>>();
            _eventsText = new List<EventHandler<TextEventArgs>>();
        }

        public void Run(RenderWindow window)
        {
            //    Choisis tous les blocks de la map
            //    Les différents type de monstres
            //    La taille max (editable apres)
            //    MAP EDITOR (bloc transparent = position)

            //Liste en haut des blocks et monstres, coordonnées

            // Choose Background
            string bgPath = " ";
            string playerPath = " ";
            Position playerPos = new Position(0, 0);
            ushort playerLife = 10;
            ushort playerAtk = 5;

            bool display = true;
            bool again = true;
            int selected = 0;
            Text t;
            RectangleShape r;
            List<Text> lines = new List<Text>();

            window.SetKeyRepeatEnabled(false);

            _eventsText.Add((s, a) =>
            {
               if (a.Unicode != "\b")
               {
                   bgPath = bgPath + a.Unicode;
                   display = true;
               }
               else if (bgPath.Length - 1 > 0)
               {
                   bgPath = bgPath.Remove(bgPath.Length - 1);
                   display = true;
               }
            });

            _eventsKey.Add((s, a) =>
            {
                if (a.Code == Keyboard.Key.Enter)
                {
                    again = false;
                    display = true;
                }
                else if (a.Control && a.Code == Keyboard.Key.V) //Paste
                {
                    bgPath += Clipboard.Contents;
                    display = true;
                }
            });

            window.TextEntered += _eventsText[0];
            window.KeyPressed += _eventsKey[0];

            window.DispatchEvents();

            do
            {
                again = true;

                if (display)
                {
                    // Graphic
                    DisplayBackground(window);
                    t = new Text("Select a background", _font1, _charSize);
                    t.FillColor = Color.White;
                    t.Position = new SFML.System.Vector2f(window.Size.X / 2 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 2);

                    r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                    r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 2) - (t.GetGlobalBounds().Height / 3));
                    r.FillColor = Color.Black;

                    window.Draw(r);
                    window.Draw(t);

                    t = new Text(bgPath, _font2, _charSize);
                    t.FillColor = Color.White;
                    t.Position = new SFML.System.Vector2f(window.Size.X / 2 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 3);

                    r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                    r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 3) - (t.GetGlobalBounds().Height / 3));
                    r.FillColor = Color.Black;

                    window.Draw(r);
                    window.Draw(t);

                    window.Display();
                    display = false;
                }

                // Events
                window.DispatchEvents();

                System.Threading.Thread.Sleep(1000 / 60);
            } while (again);

            bgPath = bgPath.Remove(0, 1);

            //Console.WriteLine(bgPath);
            //Console.WriteLine(@"..\..\..\..\Assets\Backgrounds\game_background_1.png");

            window.TextEntered -= _eventsText[0];
            window.KeyPressed -= _eventsKey[0];

            // Player Path
            display = true;

            _eventsText.Add((s, a) =>
            {
                if (a.Unicode != "\b")
                {
                    playerPath = playerPath + a.Unicode;
                    display = true;
                }
                else if (playerPath.Length - 1 > 0)
                {
                    playerPath = playerPath.Remove(playerPath.Length - 1);
                    display = true;
                }
            });

            _eventsKey.Add((s, a) =>
            {
                if (a.Code == Keyboard.Key.Enter)
                {
                    display = true;
                    again = false;
                }
                else if (a.Control && a.Code == Keyboard.Key.V) //Paste
                {
                    playerPath += Clipboard.Contents;
                    display = true;
                }
            });

            window.TextEntered += _eventsText[1];
            window.KeyPressed += _eventsKey[1];

            do
            {
                again = true;

                if (display)
                {
                    DisplayBackground(window);

                    t = new Text("Select a player", _font1, _charSize);
                    t.FillColor = Color.White;
                    t.Position = new SFML.System.Vector2f(window.Size.X / 2 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 2);

                    r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                    r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 2) - (t.GetGlobalBounds().Height / 3));
                    r.FillColor = Color.Black;

                    window.Draw(r);
                    window.Draw(t);

                    t = new Text(playerPath, _font2, _charSize);
                    t.FillColor = Color.White;
                    t.Position = new SFML.System.Vector2f(window.Size.X / 2 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 3);

                    r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                    r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 3) - (t.GetGlobalBounds().Height / 3));
                    r.FillColor = Color.Black;

                    window.Draw(r);
                    window.Draw(t);

                    window.Display();
                    display = false;
                }

                // Events
                window.DispatchEvents();

                System.Threading.Thread.Sleep(1000 / 60);
            } while (again);

            playerPath = playerPath.Remove(0, 1);
            //Console.WriteLine(playerPath);

            window.TextEntered -= _eventsText[1];
            window.KeyPressed -= _eventsKey[1];

            // Stats Player
            display = true;

            window.DispatchEvents();

            _eventsText.Add((s, a) =>
            {
                if (a.Unicode != "\b" && int.TryParse(a.Unicode, out int n)) //Verify if it's a number
                {
                    switch (selected)
                    {
                        case 0:
                            if (playerPos.X == 0) playerPos.X = Convert.ToSingle(a.Unicode);
                            else playerPos.X = Convert.ToSingle(Convert.ToString(playerPos.X) + a.Unicode);
                            break;
                        case 1:
                            if (playerPos.Y == 0) playerPos.Y = Convert.ToSingle(a.Unicode);
                            else playerPos.Y = Convert.ToSingle(Convert.ToString(playerPos.Y) + a.Unicode);
                            break;
                        case 2:
                            if (playerLife == 0) playerLife = Convert.ToUInt16(a.Unicode);
                            else playerLife = Convert.ToUInt16(Convert.ToString(playerLife) + a.Unicode);
                            break;
                        case 3:
                            if (playerAtk == 0) playerAtk = Convert.ToUInt16(a.Unicode);
                            else playerAtk = Convert.ToUInt16(Convert.ToString(playerAtk) + a.Unicode);
                            break;
                    }

                    display = true;
                }
                else if (selected < lines.Count)
                {
                    switch (selected)
                    {
                        case 0:
                            if (playerPos.X >= 10)
                                playerPos.X = Convert.ToSingle(Convert.ToString(playerPos.X).Remove(Convert.ToString(playerPos.X).Length - 1));
                            else
                                playerPos.X = 0;
                            break;
                        case 1:
                            if (playerPos.Y >= 10)
                                playerPos.Y = Convert.ToSingle(Convert.ToString(playerPos.Y).Remove(Convert.ToString(playerPos.Y).Length - 1));
                            else
                                playerPos.Y = 0;
                            break;
                        case 2:
                            if (playerLife >= 10)
                                playerLife = Convert.ToUInt16(Convert.ToString(playerLife).Remove(Convert.ToString(playerLife).Length - 1));
                            else
                                playerLife = 0;
                            break;
                        case 3:
                            if (playerAtk >= 10)
                                playerAtk = Convert.ToUInt16(Convert.ToString(playerAtk).Remove(Convert.ToString(playerAtk).Length - 1));
                            else
                                playerAtk = 0;
                            break;
                    }

                    display = true;
                }
            });

            _eventsKey.Add((s, a) =>
            {
                if (a.Code == Keyboard.Key.Enter)
                {
                    if (selected == lines.Count)
                    {
                        display = true;
                        again = false;
                    }
                }
                else if (a.Control && a.Code == Keyboard.Key.V) //Paste
                {
                    playerPath += Clipboard.Contents;
                    display = true;
                }
                else if (a.Code == Keyboard.Key.Up && selected > 0)
                {
                    display = true;
                    selected--;
                }
                else if (a.Code == Keyboard.Key.Down && selected < lines.Count)
                {
                    display = true;
                    selected++;
                }
            });

            window.TextEntered += _eventsText[2];
            window.KeyPressed += _eventsKey[2];

            lines.Add(new Text("X : ", _font1, _charSize));
            lines.Add(new Text("Y : ", _font1, _charSize));
            lines.Add(new Text("HP : ", _font1, _charSize));
            lines.Add(new Text("ATK : ", _font1, _charSize));

            do
            {
                again = true;

                if (display)
                {
                    DisplayBackground(window);

                    // Title
                    t = new Text("Player Stats & Position", _font1, _charSize);
                    t.FillColor = Color.White;
                    t.Position = new SFML.System.Vector2f(window.Size.X / 2 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8);

                    r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                    r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) - (t.GetGlobalBounds().Height / 3));
                    r.FillColor = Color.Black;

                    window.Draw(r);
                    window.Draw(t);

                    // Inputs Label
                    for (int i = 0; i < lines.Count; i++)
                    {
                        lines[i].Position = new SFML.System.Vector2f(window.Size.X / 4 - (lines[i].GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * (i + 2));

                        r = new RectangleShape(new SFML.System.Vector2f(lines[i].GetGlobalBounds().Width * 1.5f, lines[i].GetGlobalBounds().Height * 2));
                        r.Position = new SFML.System.Vector2f(window.Size.X / 4 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * (i + 2) - (lines[i].GetGlobalBounds().Height / 3));

                        if (i == selected)
                        {
                            lines[i].FillColor = Color.Black;
                            r.FillColor = Color.White;
                        }
                        else
                        {
                            lines[i].FillColor = Color.White;
                            r.FillColor = Color.Black;
                        }

                        window.Draw(r);
                        window.Draw(lines[i]);
                    }

                    // Inputs
                    for (int i = 0; i < lines.Count; i++)
                    {
                        switch (i)
                        {
                            case 0: //X
                                t = new Text(Convert.ToString(playerPos.X), _font2, _charSize);
                                break;
                            case 1: //Y
                                t = new Text(Convert.ToString(playerPos.Y), _font2, _charSize);
                                break;
                            case 2: //Life
                                t = new Text(Convert.ToString(playerLife), _font2, _charSize);
                                break;
                            case 3: //Atk
                                t = new Text(Convert.ToString(playerAtk), _font2, _charSize);
                                break;

                        }

                        t.Position = new SFML.System.Vector2f(window.Size.X / 2.8f - (t.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * (i + 2));

                        r = new RectangleShape(new SFML.System.Vector2f(150, t.GetGlobalBounds().Height * 2));
                        r.Position = new SFML.System.Vector2f(window.Size.X / 2.8f - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * (i + 2) - (t.GetGlobalBounds().Height / 3));

                        if (i == selected)
                        {
                            t.FillColor = Color.Black;
                            r.FillColor = Color.White;
                        }
                        else
                        {
                            t.FillColor = Color.White;
                            r.FillColor = Color.Black;
                        }

                        window.Draw(r);
                        window.Draw(t);
                    }

                    // Next
                    t = new Text("Next", _font1, _charSize);
                    t.Position = new SFML.System.Vector2f(window.Size.X / 4 - (t.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * (lines.Count + 2));

                    r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                    r.Position = new SFML.System.Vector2f(window.Size.X / 4 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * (lines.Count + 2) - (t.GetGlobalBounds().Height / 3));

                    if (lines.Count == selected)
                    {
                        r.FillColor = Color.White;
                        t.FillColor = Color.Black;
                    }
                    else
                    {
                        r.FillColor = Color.Black;
                        t.FillColor = Color.White;
                    }

                    window.Draw(r);
                    window.Draw(t);


                    window.Display();
                    display = false;
                }

                // Events
                window.DispatchEvents();

                System.Threading.Thread.Sleep(1000 / 60);
            } while (again);


            window.TextEntered -= _eventsText[2];
            window.KeyPressed -= _eventsKey[2];

            // Choose all blocks


        }

        private void DisplayBackground(RenderWindow window)
        {
            window.Clear();

            Texture backgroundTexture = new Texture(@"..\..\..\..\Assets\Backgrounds\time-travel-background.png");
            if (backgroundTexture == null) throw new Exception("Texture null!");

            SFML.Graphics.Sprite background = new SFML.Graphics.Sprite(backgroundTexture);
            if (background == null) throw new Exception("Sprite null!");

            window.Draw(background);
        }

    }
}
