using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    public class Settings
    {
        const ushort MAX_LINES = 3;

        RenderWindow _window;
        ushort _selected;
        Text[] _lines;
        SFML.Graphics.Sprite _background;
        uint _charSize = 32;

        internal Settings(RenderWindow window)
            {
             if (window == null) throw new ArgumentException("Window is null");

            _window = window;
            _selected = 0;
            _lines = new Text[MAX_LINES];
            }

        public void StartSettings()
        {
            //Background
            // Set background
            _background = new SFML.Graphics.Sprite(new Texture(@"..\..\..\..\Assets\Backgrounds\colored_desert.png"));
            if (_background == null) throw new Exception("Sprite null!");

            _background.Position = new SFML.System.Vector2f(0, -(float)_window.Size.Y / 2);
            _window.Draw(_background);

            //Lines
            _lines[0] = new Text("Start", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _lines[1] = new Text("Settings", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);
            _lines[2] = new Text("Quit", new Font(@"..\..\..\..\Assets\Fonts\Blanka-Regular.ttf"), _charSize);

            _lines[0].Position = new SFML.System.Vector2f(_window.Size.X / 2, (_window.Size.Y / 6) * 1);
            _lines[1].Position = new SFML.System.Vector2f(_window.Size.X / 2, (_window.Size.Y / 6) * 2);
            _lines[2].Position = new SFML.System.Vector2f(_window.Size.X / 2, (_window.Size.Y / 6) * 3);

            _window.Display();
        }
    }
}
