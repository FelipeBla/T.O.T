using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    public class MapEditor
    {
        MapEditor()
        {

        }

        public static void Run(RenderWindow window)
        {
            //    Choisis tous les blocks de la map
            //    Les différents type de monstres
            //    La taille max (editable apres)
            //    MAP EDITOR (bloc transparent = position)

            //Liste en haut des blocks et monstres, coordonnées

            // Choose Background
            string bgPath = "_BACKGROUNDPATH_";




            // Choose all blocks



        }

        private void DisplayBackground(RenderWindow window)
        {
            window.Clear();
            /*
            // Set background
            Texture backgroundTexture = new Texture("");
            if (backgroundTexture == null) throw new Exception("Texture null!");
            backgroundTexture.Repeated = true;

            _background = new SFML.Graphics.Sprite(backgroundTexture);
            if (_background == null) throw new Exception("Sprite null!");

            _background.Scale = new SFML.System.Vector2f(_window.Size.X / 550, _window.Size.Y / 550);
            _window.Draw(_background);*/
        }

    }
}
