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

            engine.GetGame.StartGame(@"..\..\..\..\Maps\test.totmap");
            engine.GetGUI.ShowMap();

        }
    }
}
