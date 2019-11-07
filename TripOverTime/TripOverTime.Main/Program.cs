using System;
using System.Diagnostics;
using TripOverTime.EngineNamespace;

namespace TripOverTime.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Graphics tests !");

            // To manage FramePS and TicksPS
            Stopwatch spGui = new Stopwatch();
            Stopwatch spGame = new Stopwatch();
            Engine engine = new Engine();

            //Menu


            // Start a game
            engine.StartGame(@"..\..\..\..\Maps\test.totmap", @"..\..\..\..\Assets\Players\Variable sizes\Pink\alienPink_stand.png");
            engine.GetGUI.InitGame();

            bool playerAlive = true;
            // GameLoop
            spGui.Start();
            spGame.Start();
            while(!engine.Close && playerAlive)
            {
                if (spGame.ElapsedMilliseconds >= 1000/120)
                {
                    // GameTick
                    playerAlive = engine.GameTick();
                    spGame.Restart();
                }

                if (spGui.ElapsedMilliseconds >= 1000/120)
                {
                    //GUI
                    engine.GetGUI.ShowMap();
                    spGui.Restart();
                }

            }
        }
    }
}
