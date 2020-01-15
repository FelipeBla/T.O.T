using System;
using System.IO;
using System.Text;
using System.Diagnostics;



namespace TripOverTime.EngineNamespace
{
    internal class Attack
    {
        Game _context;
        Monster _monster;
        Boss _boss;
        ushort _attack;
        Stopwatch _timer;
        string[] _schemaAttack;
        int _incrementationAttack;
        string _mapPath;
        int _speedAttack;
        int _incrementationSpeed;

        internal Attack(Game context, Boss boss, ushort attack)
            :this (context, null, boss, attack) { }
        internal Attack(Game context, Monster monster, ushort attack)
            : this(context, monster, null, attack) { }
        internal Attack(Game context, Monster monster, Boss boss, ushort attack)
        {
            _context = context;
            _monster = monster;
            _boss = boss;
            _attack = attack;
            _timer = new Stopwatch();
            _timer.Start();
            _mapPath = @"..\..\..\..\Maps\attackMonster.totMonster";

            //Verify if it's a map file
            if (!_mapPath.EndsWith(".totMonster")) throw new ArgumentException("The map file is not correct (.totMonster)");
            string text = File.ReadAllText(_mapPath);
            // Open map file
            if (String.IsNullOrEmpty(text)) throw new FileLoadException("File is empty ?");
            if (_monster != null)
            {
                _schemaAttack = StringBetweenString(text, _monster.Name + "UP", _monster.Name + "END").Split("\n");
            }
            else
            {
                _schemaAttack = StringBetweenString(text, _boss.Name + "UP", _boss.Name + "END").Split("\n");
            }
            _incrementationAttack = 0;
        }

        internal void AttackMonster()
        {
            if (_incrementationAttack == _schemaAttack[0].Length -1 )
            {
                _incrementationAttack = 0;
            }

            if (_schemaAttack[0][_incrementationAttack] == 'S')
            {
                _speedAttack = 50;
                SlidingAttack();
                if (_timer.ElapsedMilliseconds >= _speedAttack*12 && _monster.IsAlive)
                {
                    if (_monster.Position.X + 2 > _context.GetPlayer.RealPosition.X && _monster.Position.X - 2 < _context.GetPlayer.RealPosition.X && _monster.Position.Y == _context.GetPlayer.RealPosition.Y)
                    {
                        HurtPlayer();
                    }
                    _incrementationAttack++;
                    _timer.Restart();
                }
            }
            else if (_schemaAttack[0][_incrementationAttack] == 'A')
            {
                _speedAttack = 80;
                NormalAttack();

                if (_timer.ElapsedMilliseconds >= _speedAttack*12 && _monster.IsAlive)
                {
                    if (_monster.Position.X + 2 > _context.GetPlayer.RealPosition.X && _monster.Position.X - 2 < _context.GetPlayer.RealPosition.X && _monster.Position.Y == _context.GetPlayer.RealPosition.Y)
                    {
                        HurtPlayer();
                    }
                    _incrementationAttack++;
                    _timer.Restart();
                }
            }

        }

        internal void AttackBoss()
        {
            if (_incrementationAttack == _schemaAttack[0].Length - 1)
            {
                _incrementationAttack = 0;
            }
            if (_schemaAttack[0][_incrementationAttack] == 'R')//run slashing
            {
                _speedAttack = 40;
                if (_incrementationSpeed == 0) { _boss.BossSpeed *= 5; _incrementationSpeed++; }
                RunSlashingBoss();
                if (_timer.ElapsedMilliseconds >= _speedAttack*12 && _boss.IsAlive)
                {
                    if (_boss.Position.X + 3 > _context.GetPlayer.RealPosition.X && _boss.Position.X - 3 < _context.GetPlayer.RealPosition.X && (_boss.Position.Y == _context.GetPlayer.RealPosition.Y || _boss.Position.Y + 1 == _context.GetPlayer.RealPosition.Y))
                    {
                        HurtPlayer();
                    }
                    _boss.BossSpeed /= 5;
                    _incrementationSpeed = 0;
                    _incrementationAttack++;
                    _timer.Restart();
                }
            }
            else if (_schemaAttack[0][_incrementationAttack] == 'A')//slashing
            {
                _speedAttack = 40;
                SlashingBoss();
                if (_timer.ElapsedMilliseconds >= _speedAttack * 12 && _boss.IsAlive)
                {
                    if (_boss.Position.X + 3 > _context.GetPlayer.RealPosition.X && _boss.Position.X - 3 < _context.GetPlayer.RealPosition.X && (_boss.Position.Y == _context.GetPlayer.RealPosition.Y || _boss.Position.Y + 1 == _context.GetPlayer.RealPosition.Y))
                    {
                        HurtPlayer();
                    }
                    _incrementationAttack++;
                    _timer.Restart();
                }
            }
            else if (_schemaAttack[0][_incrementationAttack] == 'T')//throwing
            {
                _speedAttack = 60;
                ThrowingBoss();
                if (_timer.ElapsedMilliseconds >= _speedAttack * 12 && _boss.IsAlive)
                {
                    if (_boss.Position.X + 3 > _context.GetPlayer.RealPosition.X && _boss.Position.X - 3 < _context.GetPlayer.RealPosition.X && (_boss.Position.Y == _context.GetPlayer.RealPosition.Y || _boss.Position.Y + 1 == _context.GetPlayer.RealPosition.Y))
                    {
                        _attack *= 2;
                        HurtPlayer();
                        _attack /= 2;
                    }
                    _incrementationAttack++;
                    _timer.Restart();
                }
            }
            else if (_schemaAttack[0][_incrementationAttack] == 'K')//kicking
            {
                _speedAttack = 40;
                KickingBoss();
                if (_timer.ElapsedMilliseconds >= _speedAttack * 12 && _boss.IsAlive)
                {
                    if (_boss.Position.X + 3 > _context.GetPlayer.RealPosition.X && _boss.Position.X - 3 < _context.GetPlayer.RealPosition.X && (_boss.Position.Y == _context.GetPlayer.RealPosition.Y || _boss.Position.Y + 1 == _context.GetPlayer.RealPosition.Y))
                    {
                        HurtPlayer();
                    }
                    _incrementationAttack++;
                    _timer.Restart();
                }
            }
            else if (_schemaAttack[0][_incrementationAttack] == 'S')//sliding
            {
                _speedAttack = 60;
                SlidingBoss();
                if (_timer.ElapsedMilliseconds >= _speedAttack * 6 && _boss.IsAlive)
                {
                    if (_boss.Position.X + 3 > _context.GetPlayer.RealPosition.X && _boss.Position.X - 3 < _context.GetPlayer.RealPosition.X && (_boss.Position.Y == _context.GetPlayer.RealPosition.Y || _boss.Position.Y + 1 == _context.GetPlayer.RealPosition.Y))
                    {
                        HurtPlayer();
                    }
                    _incrementationAttack++;
                    _timer.Restart();
                }
            }
        }

        // AttackBossAnimation (int nbrAction,string name Action, Boss boss, int speed sprite) 
        internal void RunSlashingBoss()
        {
            _boss.GetBossSprite.BossAttackAnimation(12, "runslashing (", _speedAttack);
        }
        internal void SlashingBoss()
        {
            _boss.GetBossSprite.BossAttackAnimation(12, "slashing (", _speedAttack);
        }
        internal void ThrowingBoss()
        {
            _boss.GetBossSprite.BossAttackAnimation(12, "throwing (", _speedAttack);
        }
        internal void KickingBoss()
        {
            _boss.GetBossSprite.BossAttackAnimation(12, "kicking (", _speedAttack);
        }
        internal void SlidingBoss()
        {
            _boss.GetBossSprite.BossAttackAnimation(6, "sliding (", _speedAttack);
        }


        internal void NormalAttack()
        {
            _monster.GetMonsterSprite.MonsterAttackAnimation(12, "attack", _speedAttack);
        }

        internal void SlidingAttack()
        {
            _monster.GetMonsterSprite.MonsterAttackAnimation(6, "sliding", _speedAttack);
        }
        internal void HurtPlayer ()
        {
            _context.GetPlayer.GetLife.DecreasedPoint(_attack);
            _context.GetPlayer.HurtPlayer = true;
            HurtPlayerAnimation();
        }

        internal void HurtPlayerAnimation()
        {
            _context.GetPlayer.GetPlayerSprite.PlayerAnimation(4, "hurt", 60);
        }

        private string StringBetweenString(string original, string str1, string str2)
        {
            int firstStringPosition = original.IndexOf(str1);
            int secondStringPosition = original.IndexOf(str2);
            return original.Substring(firstStringPosition + str1.Length + 2, secondStringPosition - firstStringPosition - str2.Length);
        }

        internal ushort GetAttack
        {
            get => _attack;
            set => _attack = value;
        }
    }
}
