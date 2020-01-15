using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    public class MapEditor
    {
        uint _charSize;
        Font _font;

        public MapEditor()
        {
            _font = new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf");
            _charSize = 32;
        }

        public void Run(RenderWindow window)
        {
            //    Choisis tous les blocks de la map
            //    Les différents type de monstres
            //    La taille max (editable apres)
            //    MAP EDITOR (bloc transparent = position)

            //Liste en haut des blocks et monstres, coordonnées

            window.SetKeyRepeatEnabled(false);

            // Choose Background
            string bgPath = "";
            bool again = true;
            do
            {
                // Graphic
                DisplayBackground(window);
                Text t = new Text("Select a background", _font, _charSize);
                t.FillColor = Color.White;
                t.Position = new SFML.System.Vector2f(window.Size.X / 2 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 2);

                RectangleShape r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 2) - (t.GetGlobalBounds().Height / 3));
                r.FillColor = Color.Black;

                window.Draw(r);
                window.Draw(t);

                t = new Text(bgPath, _font, _charSize);
                t.FillColor = Color.White;
                t.Position = new SFML.System.Vector2f(window.Size.X / 2 - (t.GetGlobalBounds().Width) / 2, window.Size.Y / 3);

                r = new RectangleShape(new SFML.System.Vector2f(t.GetGlobalBounds().Width * 1.5f, t.GetGlobalBounds().Height * 2));
                r.Position = new SFML.System.Vector2f(window.Size.X / 2 - (r.GetGlobalBounds().Width) / 2, (window.Size.Y / 3) - (t.GetGlobalBounds().Height / 3));
                r.FillColor = Color.Black;

                window.Draw(r);
                window.Draw(t);


                window.Display();

                // Events
                window.DispatchEvents();

                window.TextEntered += (s, a) =>
                {
                    bgPath += a.Unicode;
                    Console.WriteLine(a.Unicode);
                };

                window.KeyReleased += (s, a) =>
                {
                    if (a.Code == SFML.Window.Keyboard.Key.Enter) again = false;
                    else if (a.Code == SFML.Window.Keyboard.Key.Backspace) bgPath.Remove(bgPath.Length - 1);
                };

                System.Threading.Thread.Sleep(1);
            } while (again);
            
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
