using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        char _blockId;

        public MapEditor()
        {
            _font1 = new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf");
            _font2 = new Font(@"..\..\..\..\Assets\Fonts\Comfortaa-Regular.ttf");
            _charSize = 32;
            _blockId = 'A';
            _blockId--;

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
            Position posMax = new Position(15, 5);
            Position posMin = new Position(0, 1);
            ushort playerLife = 10;
            ushort playerAtk = 5;
            List<Sprite> blocks = new List<Sprite>();
            List<Monster> monsters = new List<Monster>();

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
            bgPath = bgPath.Replace("\r", "");

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
            playerPath = playerPath.Replace("\r", "");

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
                else if (selected < lines.Count && a.Unicode == "\b")
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

            //Limit map

            display = true;

            window.DispatchEvents();

            _eventsText.Add((s, a) =>
            {
                if (a.Unicode != "\b" && int.TryParse(a.Unicode, out int n)) //Verify if it's a number
                {
                    switch (selected)
                    {
                        case 0:
                            if (posMax.X == 0) posMax.X = Convert.ToSingle(a.Unicode);
                            else posMax.X = Convert.ToSingle(Convert.ToString(posMax.X) + a.Unicode);
                            break;
                        case 1:
                            if (posMin.X == 0) posMin.X = Convert.ToSingle(a.Unicode);
                            else posMin.X = Convert.ToSingle(Convert.ToString(posMin.X) + a.Unicode);
                            break;
                        case 2:
                            if (posMax.Y == 0) posMax.Y = Convert.ToSingle(a.Unicode);
                            else posMax.Y = Convert.ToSingle(Convert.ToString(posMax.Y) + a.Unicode);
                            break;
                        case 3:
                            if (posMin.Y == 0) posMin.Y = Convert.ToSingle(a.Unicode);
                            else posMin.Y = Convert.ToSingle(Convert.ToString(posMin.Y) + a.Unicode);
                            break;
                    }

                    display = true;
                }
                else if (selected < lines.Count && a.Unicode == "\b")
                {
                    switch (selected)
                    {
                        case 0:
                            if (posMax.X >= 10)
                                posMax.X = Convert.ToSingle(Convert.ToString(posMax.X).Remove(Convert.ToString(posMax.X).Length - 1));
                            else
                                posMax.X = 0;
                            break;
                        case 1:
                            if (posMin.X >= 10)
                                posMin.X = Convert.ToSingle(Convert.ToString(posMin.X).Remove(Convert.ToString(posMin.X).Length - 1));
                            else
                                posMin.X = 0;
                            break;
                        case 2:
                            if (posMax.Y >= 10)
                                posMax.Y = Convert.ToSingle(Convert.ToString(posMax.Y).Remove(Convert.ToString(posMax.Y).Length - 1));
                            else
                                posMax.Y = 0;
                            break;
                        case 3:
                            if (posMin.Y >= 10)
                                posMin.Y = Convert.ToSingle(Convert.ToString(posMin.Y).Remove(Convert.ToString(posMin.Y).Length - 1));
                            else
                                posMin.Y = 0;
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

            window.TextEntered += _eventsText[3];
            window.KeyPressed += _eventsKey[3];

            lines.Clear();

            lines.Add(new Text("X MAX : ", _font1, _charSize));
            lines.Add(new Text("X MIN : ", _font1, _charSize));
            lines.Add(new Text("Y MAX : ", _font1, _charSize));
            lines.Add(new Text("Y MIN : ", _font1, _charSize));

            do
            {
                again = true;

                if (display)
                {
                    DisplayBackground(window);

                    // Title
                    t = new Text("MAX LIMITS", _font1, _charSize);
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
                                t = new Text(Convert.ToString(posMax.X), _font2, _charSize);
                                break;
                            case 1: //Y
                                t = new Text(Convert.ToString(posMin.X), _font2, _charSize);
                                break;
                            case 2: //Life
                                t = new Text(Convert.ToString(posMax.Y), _font2, _charSize);
                                break;
                            case 3: //Atk
                                t = new Text(Convert.ToString(posMin.Y), _font2, _charSize);
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

            window.TextEntered -= _eventsText[3];
            window.KeyPressed -= _eventsKey[3];

            // Choose all blocks
            selected = 0;
            bool addingBlock = false;
            ushort startIndex = 0;
            int j = 0;
            display = true;

            window.DispatchEvents();

            _eventsKey.Add((s, a) =>
            {
                if (!addingBlock)
                {
                    if (a.Code == Keyboard.Key.Enter)
                    {
                        if (selected == lines.Count + 1) // Next
                        {
                            display = true;
                            again = false;
                        }
                        else if (selected == lines.Count) // Add Block
                        {
                            addingBlock = true;

                            Sprite ss = AddBlock(window);

                            // G GRASS ..\..\..\..\Assets\Ground\Snow\snowMid.png true

                            if (ss != null)
                            {
                                blocks.Add(ss);
                                string tempPath = blocks[blocks.Count - 1].ImgPath;

                                while (tempPath.Length > 55) // A TESTER
                                {
                                    tempPath = tempPath.Remove(tempPath.Length - 1, 1);
                                }

                                lines.Add(new Text(blocks[blocks.Count - 1].Id + " - " + blocks[blocks.Count - 1].Name + " - " + tempPath + " - " + blocks[blocks.Count - 1].IsSolid, _font2, _charSize));
                            }

                            selected++;
                            addingBlock = false;

                            display = true;
                        }
                        else if (selected < lines.Count && selected > 0) // Enter on a block line
                        {
                            // Edit
                        }
                    }
                    else if (a.Code == Keyboard.Key.Delete)
                    {
                        if (lines.Count > selected && selected > 0)
                        {
                            lines.RemoveAt(selected);
                            blocks.RemoveAt(selected - 1);
                            display = true;
                        }
                    }
                    else if (a.Code == Keyboard.Key.Up && selected > 0)
                    {
                        display = true;
                        selected--;

                        if (lines.Count > 5 && startIndex > 0 && selected <= 4)
                        {
                            startIndex--;
                        }

                    }
                    else if (a.Code == Keyboard.Key.Down && selected < lines.Count + 1)
                    {
                        display = true;
                        selected++;

                        if (lines.Count > 5 && selected > 4 && selected < lines.Count)
                        {
                            startIndex++;
                        }

                    }
                }
            });

            window.KeyPressed += _eventsKey[4];

            lines.Clear();

            // 2 TRAP ..\..\..\..\Assets\Tiles\spikes.png true DANGEROUS
            lines.Add(new Text("ID - NAME - FILEPATH - SOLID - (OPTIONAL: TRAP)", _font1, _charSize));

            do
            {
                again = true;

                if (display)
                {
                    DisplayBackground(window);

                    // Title
                    t = new Text("All blocks", _font1, _charSize);
                    t.FillColor = Color.White;
                    t.Position = new SFML.System.Vector2f(window.Size.X / 2 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8);

                    r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                    r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) - (t.GetGlobalBounds().Height / 3));
                    r.FillColor = Color.Black;

                    window.Draw(r);
                    window.Draw(t);

                    // Block Label
                    j = 0;
                    for (int i = startIndex; i < startIndex + 5 && i < lines.Count; i++)
                    {
                        lines[i].Position = new SFML.System.Vector2f(window.Size.X / 2 - (lines[i].GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * (j + 2));

                        r = new RectangleShape(new SFML.System.Vector2f(lines[i].GetGlobalBounds().Width * 1.5f, lines[i].GetGlobalBounds().Height * 2));
                        r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * (j + 2) - (lines[i].GetGlobalBounds().Height / 3));

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
                        j++;
                    }

                    // Add Block
                    t = new Text("Add block...", _font1, _charSize);
                    t.Position = new SFML.System.Vector2f(window.Size.X / 4 - (t.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 7);

                    r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                    r.Position = new SFML.System.Vector2f(window.Size.X / 4 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 7 - (t.GetGlobalBounds().Height / 3));

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

                    // Next
                    t = new Text("Next", _font1, _charSize);
                    t.Position = new SFML.System.Vector2f((window.Size.X / 4) * 3 - (t.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 7);

                    r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                    r.Position = new SFML.System.Vector2f((window.Size.X / 4) * 3 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 7 - (t.GetGlobalBounds().Height / 3));

                    if (lines.Count + 1 == selected)
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

            window.KeyPressed -= _eventsKey[4];

            // All type of monster

            selected = 0;
            bool addingMonster = false;
            startIndex = 0;
            j = 0;
            display = true;

            window.DispatchEvents();

            _eventsKey.Add((s, a) =>
            {
                if (!addingMonster)
                {
                    if (a.Code == Keyboard.Key.Enter)
                    {
                        if (selected == lines.Count + 1) // Next
                        {
                            display = true;
                            again = false;
                        }
                        else if (selected == lines.Count) // Add Block
                        {
                            addingMonster = true;

                            Monster ss = AddMonster(window);

                            // G GRASS ..\..\..\..\Assets\Ground\Snow\snowMid.png true

                            if (ss != null)
                            {
                                monsters.Add(ss);

                                // Golem1 3 3 10 1 3 1 SAAAAA
                                //lines.Add(new Text("NAME - HP - ATTACK - MOVESPEED - RANGE - COMBO", _font1, _charSize));
                                lines.Add(new Text(monsters[monsters.Count - 1].Name + " - " + monsters[monsters.Count - 1].life.GetMaxPoint() + " - " + monsters[monsters.Count - 1].GetAttack.GetAttack + " - " + monsters[monsters.Count - 1].MoveSpeed * 100 + " - " + monsters[monsters.Count - 1].Range + " - " + monsters[monsters.Count - 1].AttackCombo, _font2, _charSize));
                            }

                            selected++;
                            addingMonster = false;

                            display = true;
                        }
                        else if (selected < lines.Count && selected > 0) // Enter on a block line
                        {
                            // Edit
                        }
                    }
                    else if (a.Code == Keyboard.Key.Delete)
                    {
                        if (lines.Count > selected && selected > 0)
                        {
                            lines.RemoveAt(selected);
                            monsters.RemoveAt(selected - 1);
                            display = true;
                        }
                    }
                    else if (a.Code == Keyboard.Key.Up && selected > 0)
                    {
                        display = true;
                        selected--;

                        if (lines.Count > 5 && startIndex > 0 && selected <= 4)
                        {
                            startIndex--;
                        }

                    }
                    else if (a.Code == Keyboard.Key.Down && selected < lines.Count + 1)
                    {
                        display = true;
                        selected++;

                        if (lines.Count > 5 && selected > 4 && selected < lines.Count)
                        {
                            startIndex++;
                        }

                    }
                }
            });

            window.KeyPressed += _eventsKey[5];

            lines.Clear();

            // Golem1 3 3 10 1 3 1 SAAAAA
            lines.Add(new Text("NAME - HP - ATTACK - MOVESPEED - RANGE - COMBO", _font1, _charSize));

            do
            {
                again = true;

                if (display)
                {
                    DisplayBackground(window);

                    // Title
                    t = new Text("All monsters", _font1, _charSize);
                    t.FillColor = Color.White;
                    t.Position = new SFML.System.Vector2f(window.Size.X / 2 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8);

                    r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                    r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) - (t.GetGlobalBounds().Height / 3));
                    r.FillColor = Color.Black;

                    window.Draw(r);
                    window.Draw(t);

                    // Block Label
                    j = 0;
                    for (int i = startIndex; i < startIndex + 5 && i < lines.Count; i++)
                    {
                        lines[i].Position = new SFML.System.Vector2f(window.Size.X / 2 - (lines[i].GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * (j + 2));

                        r = new RectangleShape(new SFML.System.Vector2f(lines[i].GetGlobalBounds().Width * 1.5f, lines[i].GetGlobalBounds().Height * 2));
                        r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * (j + 2) - (lines[i].GetGlobalBounds().Height / 3));

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
                        j++;
                    }

                    // Add Block
                    t = new Text("Add monster...", _font1, _charSize);
                    t.Position = new SFML.System.Vector2f(window.Size.X / 4 - (t.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 7);

                    r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                    r.Position = new SFML.System.Vector2f(window.Size.X / 4 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 7 - (t.GetGlobalBounds().Height / 3));

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

                    // Next
                    t = new Text("Next", _font1, _charSize);
                    t.Position = new SFML.System.Vector2f((window.Size.X / 4) * 3 - (t.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 7);

                    r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                    r.Position = new SFML.System.Vector2f((window.Size.X / 4) * 3 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 7 - (t.GetGlobalBounds().Height / 3));

                    if (lines.Count + 1 == selected)
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

            window.KeyPressed -= _eventsKey[4];

            // EDITORRRRRR
            ShowMap(window, bgPath, playerPath, playerPos, posMax, posMin, playerLife, playerAtk, blocks, monsters);

            // Add "AIR" on null sprite position
            // Save

        }

        private Sprite AddBlock(RenderWindow window)
        {
            Sprite s = null;
            bool again = true;
            bool display = true;
            Text t = null;
            RectangleShape r = null;
            int selected = 0;
            bool complete = false;

            string name = "";
            string path = "";
            bool isSolid = true;

            EventHandler<KeyEventArgs> kEvent = (s, a) =>
            {
                if (a.Code == Keyboard.Key.Enter)
                {
                    if (selected == 4) //back
                    {
                        again = false;
                        complete = false;
                    }
                    else if (selected == 3) // valid
                    {
                        again = false;
                        complete = true;
                    }
                    else if (selected == 2) //isSolid
                    {
                        isSolid = !isSolid;
                    }

                    display = true;
                }
                else if (a.Control && a.Code == Keyboard.Key.V) //Paste
                {
                    if (selected == 0) name += Clipboard.Contents;
                    else path += Clipboard.Contents;
                    display = true;
                }
                else if (a.Code == Keyboard.Key.Up && selected > 0)
                {
                    selected--;
                    display = true;
                }
                else if (a.Code == Keyboard.Key.Down && selected < 4)
                {
                    selected++;
                    display = true;
                }
            };

            EventHandler<TextEventArgs> tEvent = (s, a) =>
            {
                if (selected == 0) //Name
                {
                    if (a.Unicode != "\b")
                    {
                        name += a.Unicode;
                    }
                    else if (name.Length > 0)
                    {
                        name = name.Remove(name.Length - 1);
                    }
                }
                else if (selected == 1) //Path
                {
                    if (a.Unicode != "\b")
                    {
                        path += a.Unicode;
                    }
                    else if (path.Length > 0)
                    {
                        path = path.Remove(path.Length - 1);
                    }
                }


                display = true;
            };

            window.TextEntered += tEvent;

            window.KeyPressed += kEvent;

            do
            {
                again = true;

                if (display)
                {
                    DisplayBackground(window);

                    // Title
                    t = new Text("Add block", _font1, _charSize);
                    t.FillColor = Color.White;
                    t.Position = new SFML.System.Vector2f(window.Size.X / 2 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8);

                    r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                    r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) - (t.GetGlobalBounds().Height / 3));
                    r.FillColor = Color.Black;

                    window.Draw(r);
                    window.Draw(t);

                    // Name 0
                    if (selected == 0)
                    {
                        t = new Text("Name : " + name, _font2, _charSize);
                        t.FillColor = Color.Black;
                        t.Position = new SFML.System.Vector2f(window.Size.X / 2 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8 * 3);

                        r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                        r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 3 - (t.GetGlobalBounds().Height / 3));
                        r.FillColor = Color.White;

                        window.Draw(r);
                        window.Draw(t);
                    }
                    else
                    {
                        t = new Text("Name : " + name, _font2, _charSize);
                        t.FillColor = Color.White;
                        t.Position = new SFML.System.Vector2f(window.Size.X / 2 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8 * 3);

                        r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                        r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 3 - (t.GetGlobalBounds().Height / 3));
                        r.FillColor = Color.Black;

                        window.Draw(r);
                        window.Draw(t);
                    }


                    // Path 1
                    if (selected == 1)
                    {
                        t = new Text("Path : " + path, _font2, _charSize);
                        t.FillColor = Color.Black;
                        t.Position = new SFML.System.Vector2f(window.Size.X / 2 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8 * 4);

                        r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                        r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 4 - (t.GetGlobalBounds().Height / 3));
                        r.FillColor = Color.White;

                        window.Draw(r);
                        window.Draw(t);
                    }
                    else
                    {
                        t = new Text("Path : " + path, _font2, _charSize);
                        t.FillColor = Color.White;
                        t.Position = new SFML.System.Vector2f(window.Size.X / 2 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8 * 4);

                        r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                        r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 4 - (t.GetGlobalBounds().Height / 3));
                        r.FillColor = Color.Black;

                        window.Draw(r);
                        window.Draw(t);
                    }

                    // IsSolid 2
                    if (selected == 2)
                    {
                        if (isSolid)
                        {
                            t = new Text("Block is solid", _font1, _charSize);
                            t.FillColor = Color.Green;
                            t.Position = new SFML.System.Vector2f(window.Size.X / 2 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8 * 5);

                            r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                            r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 5 - (t.GetGlobalBounds().Height / 3));
                            r.FillColor = Color.White;

                            window.Draw(r);
                            window.Draw(t);
                        }
                        else
                        {
                            t = new Text("Block isn't solid", _font1, _charSize);
                            t.FillColor = Color.Red;
                            t.Position = new SFML.System.Vector2f(window.Size.X / 2 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8 * 5);

                            r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                            r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 5 - (t.GetGlobalBounds().Height / 3));
                            r.FillColor = Color.White;

                            window.Draw(r);
                            window.Draw(t);
                        }
                    }
                    else
                    {
                        if (isSolid)
                        {
                            t = new Text("Block is solid", _font1, _charSize);
                            t.FillColor = Color.Green;
                            t.Position = new SFML.System.Vector2f(window.Size.X / 2 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8 * 5);

                            r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                            r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 5 - (t.GetGlobalBounds().Height / 3));
                            r.FillColor = Color.Black;

                            window.Draw(r);
                            window.Draw(t);
                        }
                        else
                        {
                            t = new Text("Block isn't solid", _font1, _charSize);
                            t.FillColor = Color.Red;
                            t.Position = new SFML.System.Vector2f(window.Size.X / 2 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8 * 5);

                            r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                            r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 5 - (t.GetGlobalBounds().Height / 3));
                            r.FillColor = Color.Black;

                            window.Draw(r);
                            window.Draw(t);
                        }
                    }

                    // Valid 3
                    if (selected == 3)
                    {
                        t = new Text("Valid", _font1, _charSize);
                        t.FillColor = Color.Black;
                        t.Position = new SFML.System.Vector2f(window.Size.X / 2 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8 * 6);

                        r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                        r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 6 - (t.GetGlobalBounds().Height / 3));
                        r.FillColor = Color.White;

                        window.Draw(r);
                        window.Draw(t);
                    }
                    else
                    {
                        t = new Text("Valid", _font1, _charSize);
                        t.FillColor = Color.White;
                        t.Position = new SFML.System.Vector2f(window.Size.X / 2 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8 * 6);

                        r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                        r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 6 - (t.GetGlobalBounds().Height / 3));
                        r.FillColor = Color.Black;

                        window.Draw(r);
                        window.Draw(t);
                    }


                    // Back 4
                    if (selected == 4)
                    {
                        t = new Text("Back", _font1, _charSize);
                        t.FillColor = Color.Black;
                        t.Position = new SFML.System.Vector2f(window.Size.X / 2 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8 * 7);

                        r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                        r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 7 - (t.GetGlobalBounds().Height / 3));
                        r.FillColor = Color.White;

                        window.Draw(r);
                        window.Draw(t);
                    }
                    else
                    {
                        t = new Text("Back", _font1, _charSize);
                        t.FillColor = Color.White;
                        t.Position = new SFML.System.Vector2f(window.Size.X / 2 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8 * 7);

                        r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                        r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 7 - (t.GetGlobalBounds().Height / 3));
                        r.FillColor = Color.Black;

                        window.Draw(r);
                        window.Draw(t);
                    }

                    window.Display();
                    display = false;
                }

                window.DispatchEvents();

                System.Threading.Thread.Sleep(1000 / 60);
            } while (again);


            if (complete)
            {
                _blockId++;
                if (path.ToLower().EndsWith(".png") || path.ToLower().EndsWith(".jpg"))
                {
                    s = new Sprite(Convert.ToString(_blockId), name, path, isSolid);
                }
            }

            window.TextEntered -= tEvent;

            window.KeyPressed -= kEvent;

            return s;
        }

        private Monster AddMonster(RenderWindow window)
        {
            Monster m = null;
            bool again = true;
            bool display = true;
            Text t = null;
            RectangleShape r = null;
            int selected = 0;
            bool complete = false;

            // Golem1 3 3 10 1 3 1 SAAAAA
            //lines.Add(new Text("NAME - [ X ; Y ] - HP - ATTACK - MOVESPEED - RANGE - COMBO", _font1, _charSize));
            //internal Monster(Game context, String name, Position position, Life life, ushort attack, float monsterMove, float range, string attackCombo)
            string name = "";
            Position pos = new Position(0, 0);
            string life = "10";
            string attack = "1";
            string monsterMove = "3";
            string range = "1";
            string attackCombo = "SAAA";

            EventHandler<KeyEventArgs> kEvent = (s, a) =>
            {
                if (a.Code == Keyboard.Key.Enter)
                {
                    if (selected == 7) //back
                    {
                        again = false;
                        complete = false;
                    }
                    else if (selected == 6) // valid
                    {
                        again = false;
                        complete = true;
                    }

                    display = true;
                }
                else if (a.Code == Keyboard.Key.Up && selected > 0)
                {
                    selected--;
                    display = true;
                }
                else if (a.Code == Keyboard.Key.Down && selected < 7)
                {
                    selected++;
                    display = true;
                }
            };

            EventHandler<TextEventArgs> tEvent = (s, a) =>
            {
                //lines.Add(new Text("NAME - HP - ATTACK - MOVESPEED - RANGE - COMBO", _font1, _charSize));

                switch (selected)
                {
                    case 0: //Name
                        if (a.Unicode != "\b")
                        {
                            name += a.Unicode;
                        }
                        else if (name.Length > 0)
                        {
                            name = name.Remove(name.Length - 1);
                        }
                        break;
                    case 1: //HP
                        if (a.Unicode != "\b")
                        {
                            life += a.Unicode;
                        }
                        else if (life.Length > 0)
                        {
                            life = life.Remove(life.Length - 1);
                        }
                        break;
                    case 2: //Attack
                        if (a.Unicode != "\b")
                        {
                            attack += a.Unicode;
                        }
                        else if (attack.Length > 0)
                        {
                            attack = attack.Remove(attack.Length - 1);
                        }
                        break;
                    case 3: //MoveSpeed
                        if (a.Unicode != "\b")
                        {
                            monsterMove += a.Unicode;
                        }
                        else if (monsterMove.Length > 0)
                        {
                            monsterMove = monsterMove.Remove(monsterMove.Length - 1);
                        }
                        break;
                    case 4: //Range
                        if (a.Unicode != "\b")
                        {
                            range += a.Unicode;
                        }
                        else if (range.Length > 0)
                        {
                            range = range.Remove(range.Length - 1);
                        }
                        break;
                    case 5: //Combo
                        if (a.Unicode != "\b")
                        {
                            attackCombo += a.Unicode;
                        }
                        else if (attackCombo.Length > 0)
                        {
                            attackCombo = attackCombo.Remove(attackCombo.Length - 1);
                        }
                        break;
                }


                display = true;
            };

            window.TextEntered += tEvent;

            window.KeyPressed += kEvent;

            do
            {
                again = true;

                if (display)
                {
                    DisplayBackground(window);

                    // Title
                    t = new Text("Add monster", _font1, _charSize);
                    t.FillColor = Color.White;
                    t.Position = new SFML.System.Vector2f(window.Size.X / 2 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8);

                    r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                    r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) - (t.GetGlobalBounds().Height / 3));
                    r.FillColor = Color.Black;

                    window.Draw(r);
                    window.Draw(t);

                    // Name 0
                    if (selected == 0)
                    {
                        t = new Text("Name (in Monster Dir.): " + name, _font2, _charSize);
                        t.FillColor = Color.Black;
                        t.Position = new SFML.System.Vector2f(window.Size.X / 2 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8 * 3);

                        r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                        r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 3 - (t.GetGlobalBounds().Height / 3));
                        r.FillColor = Color.White;

                        window.Draw(r);
                        window.Draw(t);
                    }
                    else
                    {
                        t = new Text("Name (in Monster Dir.): " + name, _font2, _charSize);
                        t.FillColor = Color.White;
                        t.Position = new SFML.System.Vector2f(window.Size.X / 2 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8 * 3);

                        r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                        r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 3 - (t.GetGlobalBounds().Height / 3));
                        r.FillColor = Color.Black;

                        window.Draw(r);
                        window.Draw(t);
                    }

                    //lines.Add(new Text("NAME - [ X ; Y ] - HP - ATTACK - MOVESPEED - RANGE - COMBO", _font1, _charSize));

                    // HP 1
                    if (selected == 1)
                    {
                        t = new Text("HP : " + life, _font2, _charSize);
                        t.FillColor = Color.Black;
                        t.Position = new SFML.System.Vector2f(window.Size.X / 4 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8 * 4);

                        r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                        r.Position = new SFML.System.Vector2f(window.Size.X / 4 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 4 - (t.GetGlobalBounds().Height / 3));
                        r.FillColor = Color.White;

                        window.Draw(r);
                        window.Draw(t);
                    }
                    else
                    {
                        t = new Text("HP : " + life, _font2, _charSize);
                        t.FillColor = Color.White;
                        t.Position = new SFML.System.Vector2f(window.Size.X / 4 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8 * 4);

                        r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                        r.Position = new SFML.System.Vector2f(window.Size.X / 4 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 4 - (t.GetGlobalBounds().Height / 3));
                        r.FillColor = Color.Black;

                        window.Draw(r);
                        window.Draw(t);
                    }

                    // Attack 2
                    if (selected == 2)
                    {
                        t = new Text("Attack : " + attack, _font2, _charSize);
                        t.FillColor = Color.Black;
                        t.Position = new SFML.System.Vector2f(window.Size.X / 4 * 3 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8 * 4);

                        r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                        r.Position = new SFML.System.Vector2f(window.Size.X / 4 * 3 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 4 - (t.GetGlobalBounds().Height / 3));
                        r.FillColor = Color.White;

                        window.Draw(r);
                        window.Draw(t);
                    }
                    else
                    {
                        t = new Text("Attack : " + attack, _font2, _charSize);
                        t.FillColor = Color.White;
                        t.Position = new SFML.System.Vector2f(window.Size.X / 4 * 3 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8 * 4);

                        r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                        r.Position = new SFML.System.Vector2f(window.Size.X / 4 * 3 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 4 - (t.GetGlobalBounds().Height / 3));
                        r.FillColor = Color.Black;

                        window.Draw(r);
                        window.Draw(t);
                    }

                    // MoveSpeed 3
                    if (selected == 3)
                    {
                        t = new Text("MoveSpeed : " + monsterMove, _font2, _charSize);
                        t.FillColor = Color.Black;
                        t.Position = new SFML.System.Vector2f(window.Size.X / 4 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8 * 5);

                        r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                        r.Position = new SFML.System.Vector2f(window.Size.X / 4 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 5 - (t.GetGlobalBounds().Height / 3));
                        r.FillColor = Color.White;

                        window.Draw(r);
                        window.Draw(t);
                    }
                    else
                    {
                        t = new Text("MoveSpeed : " + monsterMove, _font2, _charSize);
                        t.FillColor = Color.White;
                        t.Position = new SFML.System.Vector2f(window.Size.X / 4 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8 * 5);

                        r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                        r.Position = new SFML.System.Vector2f(window.Size.X / 4 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 5 - (t.GetGlobalBounds().Height / 3));
                        r.FillColor = Color.Black;

                        window.Draw(r);
                        window.Draw(t);
                    }

                    // Range 4
                    if (selected == 4)
                    {
                        t = new Text("Range : " + range, _font2, _charSize);
                        t.FillColor = Color.Black;
                        t.Position = new SFML.System.Vector2f(window.Size.X / 4 * 3 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8 * 5);

                        r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                        r.Position = new SFML.System.Vector2f(window.Size.X / 4 * 3 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 5 - (t.GetGlobalBounds().Height / 3));
                        r.FillColor = Color.White;

                        window.Draw(r);
                        window.Draw(t);
                    }
                    else
                    {
                        t = new Text("Range : " + range, _font2, _charSize);
                        t.FillColor = Color.White;
                        t.Position = new SFML.System.Vector2f(window.Size.X / 4 * 3 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8 * 5);

                        r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                        r.Position = new SFML.System.Vector2f(window.Size.X / 4 * 3 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 5 - (t.GetGlobalBounds().Height / 3));
                        r.FillColor = Color.Black;

                        window.Draw(r);
                        window.Draw(t);
                    }

                    // AttackCombo 5
                    if (selected == 5)
                    {
                        t = new Text("COMBO : " + attackCombo, _font2, _charSize);
                        t.FillColor = Color.Black;
                        t.Position = new SFML.System.Vector2f(window.Size.X / 2 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8 * 6);

                        r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                        r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 6 - (t.GetGlobalBounds().Height / 3));
                        r.FillColor = Color.White;

                        window.Draw(r);
                        window.Draw(t);
                    }
                    else
                    {
                        t = new Text("COMBO : " + attackCombo, _font2, _charSize);
                        t.FillColor = Color.White;
                        t.Position = new SFML.System.Vector2f(window.Size.X / 2 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8 * 6);

                        r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                        r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 6 - (t.GetGlobalBounds().Height / 3));
                        r.FillColor = Color.Black;

                        window.Draw(r);
                        window.Draw(t);
                    }

                    // Valid 6
                    if (selected == 6)
                    {
                        t = new Text("Valid", _font1, _charSize);
                        t.FillColor = Color.Black;
                        t.Position = new SFML.System.Vector2f(window.Size.X / 4 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8 * 7);

                        r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                        r.Position = new SFML.System.Vector2f(window.Size.X / 4 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 7 - (t.GetGlobalBounds().Height / 3));
                        r.FillColor = Color.White;

                        window.Draw(r);
                        window.Draw(t);
                    }
                    else
                    {
                        t = new Text("Valid", _font1, _charSize);
                        t.FillColor = Color.White;
                        t.Position = new SFML.System.Vector2f(window.Size.X / 4 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8 * 7);

                        r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                        r.Position = new SFML.System.Vector2f(window.Size.X / 4 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 7 - (t.GetGlobalBounds().Height / 3));
                        r.FillColor = Color.Black;

                        window.Draw(r);
                        window.Draw(t);
                    }


                    // Back 7
                    if (selected == 7)
                    {
                        t = new Text("Back", _font1, _charSize);
                        t.FillColor = Color.Black;
                        t.Position = new SFML.System.Vector2f(window.Size.X / 4 * 3 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8 * 7);

                        r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                        r.Position = new SFML.System.Vector2f(window.Size.X / 4 * 3 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 7 - (t.GetGlobalBounds().Height / 3));
                        r.FillColor = Color.White;

                        window.Draw(r);
                        window.Draw(t);
                    }
                    else
                    {
                        t = new Text("Back", _font1, _charSize);
                        t.FillColor = Color.White;
                        t.Position = new SFML.System.Vector2f(window.Size.X / 4 * 3 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 8 * 7);

                        r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                        r.Position = new SFML.System.Vector2f(window.Size.X / 4 * 3 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 8) * 7 - (t.GetGlobalBounds().Height / 3));
                        r.FillColor = Color.Black;

                        window.Draw(r);
                        window.Draw(t);
                    }

                    window.Display();
                    display = false;
                }

                window.DispatchEvents();

                System.Threading.Thread.Sleep(1000 / 60);
            } while (again);


            if (complete)
            {
                name = name.Replace("\r", "");
                m = new Monster(null, name, pos, new Life(Convert.ToUInt16(life)), Convert.ToUInt16(attack), Convert.ToSingle(monsterMove), Convert.ToSingle(range), attackCombo);
            }

            window.TextEntered -= tEvent;

            window.KeyPressed -= kEvent;

            return m;
        }

        private void DisplayBackground(RenderWindow window, string bgPath = @"..\..\..\..\Assets\Backgrounds\time-travel-background.png")
        {
            window.Clear();

            Texture backgroundTexture = new Texture(bgPath);
            if (backgroundTexture == null) throw new Exception("Texture null!");

            SFML.Graphics.Sprite background = new SFML.Graphics.Sprite(backgroundTexture);
            if (background == null) throw new Exception("Sprite null!");

            window.Draw(background);
        }

        private void ShowMap(RenderWindow window,
            string bgPath,
            string playerPath,
            Position playerPos,
            Position posMax,
            Position posMin,
            ushort playerLife,
            ushort playerAtk,
            List<Sprite> blocks,
            List<Monster> monsters)
        {
            window.Clear();



            Dictionary<Position, Sprite> map = new Dictionary<Position, Sprite>();
            Player player = new Player(null, "Player", playerPos, new Life(playerLife), playerAtk, playerPath);
            Position posActual = new Position(0, 0);
            bool showMapAgain = true;
            Stopwatch spTime = new Stopwatch();
            spTime.Start();

            player.GetPlayerSprite.GetSprite.Color = new Color(player.GetPlayerSprite.GetSprite.Color.R, player.GetPlayerSprite.GetSprite.Color.G, player.GetPlayerSprite.GetSprite.Color.B, 128); // Transparency

            EventHandler<KeyEventArgs> events = (s, a) =>
            {
                if (a.Code == Keyboard.Key.Escape)
                {
                    showMapAgain = false;
                }
            };

            window.KeyPressed += events;

            do
            {
                // Affichage
                if (spTime.ElapsedMilliseconds >= 1000/60)
                {
                    // Background
                    DisplayBackground(window, bgPath);

                    // Player
                    player.GetPlayerSprite.GetSprite.Position = new SFML.System.Vector2f(player.RealPosition.X * 128, window.Size.Y + player.RealPosition.Y * -128 - 65);
                    window.Draw(player.GetPlayerSprite.GetSprite);

                    // Actual Pos / block


                    // All blocks




                    // Events and Display

                    window.DispatchEvents();

                    window.Display();

                    spTime.Restart();
                }

                //System.Threading.Thread.Sleep(1);
            } while (showMapAgain);

            window.KeyPressed -= events;

        }
    }
}
