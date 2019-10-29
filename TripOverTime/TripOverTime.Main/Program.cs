using System;
using TripOverTime.EngineNamespace;

namespace TripOverTime.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Graphics tests !");

            Engine engine = new Engine();

            //Menu


            // Start a game
            engine.StartGame(@"..\..\..\..\Maps\test.totmap", @"..\..\..\..\Assets\Players\128x256\Green\alienGreen_stand.png");
            engine.GetGUI.InitGame();

            bool playerAlive = true;
            // GameLoop
            while(!engine.Close && playerAlive)
            {
                playerAlive = engine.GameTick();
                engine.GetGUI.ShowMap();
            }
        }
    }
}
