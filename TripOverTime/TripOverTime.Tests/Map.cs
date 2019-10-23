using NUnit.Framework;
using TripOverTime.EngineNamespace;

namespace TripOverTime.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MapTest()
        {
            Engine sut = new Engine();

            sut.GetGame.StartGame(@"D:\PI_S3\T.O.T\TripOverTime\Maps\tutorial.totmap");

            Assert.That(false);
        }
    }
}