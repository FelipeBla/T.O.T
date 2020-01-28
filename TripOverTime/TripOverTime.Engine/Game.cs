using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    public class Game
    {
        Engine _context;
        Engine _context2;
        Player _player;
        Boss _boss;
        Boss _boss2;
        Player _player2;
        List<Monster> _monsters;
        List<Monster> _monsters2;
        Map _map;
        Map _map2;
        Stopwatch _timer;
        Stopwatch _timer2;
        string _mapPath;
        string _mapPath2;
        string _music;

        internal Game(Engine context, string mapPath, string playerPath, Position startPosition, Position2 startPosition2, ushort lifePoint, ushort atk, bool multiplayer)
        {
            _mapPath = mapPath;
            _context = context;
            _map = new Map(this, mapPath, multiplayer);
            _context.GetGUI.ShowLoading(15);
            Console.WriteLine("map charged");
            _player = new Player(this, "player", startPosition, new Life(lifePoint), atk, playerPath, multiplayer);
            _context.GetGUI.ShowLoading(25);
            Console.WriteLine("player charged");
            _monsters = _map.GenerateMonsters();
            _context.GetGUI.ShowLoading(60);
            Console.WriteLine("monsters charged"); // Prend + de temps
            _boss = _map.GenerateBoss();
            _context.GetGUI.ShowLoading(85);
            Console.WriteLine("boss charged"); // Prends + de temps
            _timer = new Stopwatch();
            _timer.Start();

            if (multiplayer == true)
            {
                _map2 = new Map(this, mapPath, multiplayer);
                _boss2 = _map2.GenerateBoss2();
                _mapPath2 = mapPath;
                _context2 = context;
                _player2 = new Player(this, "player", startPosition, new Life(lifePoint), atk, playerPath, true);
                _monsters2 = _map.GenerateMonsters2();
                _timer2 = new Stopwatch();
                _timer2.Start();
            }
        }

        internal Map GetMapObject
        {
            get => _map;
        }
        internal Map GetMapObject2
        {
            get => _map;
        }
        public Player GetPlayer
        {
            get => _player;
        }
        public Player GetPlayer2
        {
            get => _player2;
        }
        internal Engine GetEngine
        {
            get => _context;
        }

        internal Engine GetEngine2
        {
            get => _context2;
        }
        internal long TimeElapsed
        {
            get => _timer.ElapsedMilliseconds;
        }
        internal long TimeElapsed2
        {
            get => _timer2.ElapsedMilliseconds;
        }
        internal List<Monster> GetMonsters
        {
            get => _monsters;
            set => _monsters = value;
        }

        internal Boss GetBoss
        {
            get => _boss;
        }
        internal Boss GetBoss2
        {
            get => _boss2;
        }
        internal List<Monster> GetMonsters2
        {
            get => _monsters2;
            set => _monsters2 = value;
        }
        internal string GetMusic
        {
            get => _music;
        }
    }
}
