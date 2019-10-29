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

            engine.StartGame(@"..\..\..\..\Maps\test.totmap", @"..\..\..\..\Assets\Players\128x256\Green\alienGreen_stand.png");
            engine.GetGUI.ShowMap();

        }
    }
}
