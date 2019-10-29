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

            engine.GetGame.StartGame(@"D:\PI_S3\T.O.T\TripOverTime\Maps\test.totmap");
            engine.GetGUI.ShowMap();

        }
    }
}
