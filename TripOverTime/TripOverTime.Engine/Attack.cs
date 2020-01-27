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
        string _schemaAttack;
        int _incrementationAttack;
        int _speedAttack;
        Game _context2;
        Monster _monster2;
        ushort _attack2;
        float _range;
        int _incrementationSpeed;

        
        Boss _boss2;
        Stopwatch _timer2;
        string _schemaAttack2;
        int _incrementationAttack2;
        int _speedAttack2;
        int _incrementationSpeed2;

        internal Attack(Game context, Monster monster, ushort attack, string attackCombo, Boss boss = null)
        {
            _context = context;
            _monster = monster;
            _boss = boss;
            _attack = attack;
            _timer = new Stopwatch();
            _timer.Start();

            _boss2 = boss;
            _context2 = context;
            _timer2 = new Stopwatch();
            _timer2.Start();
            _monster2 = monster;
            _schemaAttack = attackCombo;
            _schemaAttack2 = attackCombo;
            _incrementationAttack = 0;
            _incrementationAttack2 = 0;

        }

        internal void AttackMonster()
        {
            if (_incrementationAttack >= _schemaAttack.Length - 1)
            {
                _incrementationAttack = 0;
            }

            if (_schemaAttack[_incrementationAttack] == 'S')
            {
                _speedAttack = 50;
                SlidingAttack();
                if (_timer.ElapsedMilliseconds >= _speedAttack * 12 && _monster.isAlive)
                {
                    if (_monster.Position.X + _monster.Range > _context.GetPlayer.RealPosition.X && _monster.Position.X - _monster.Range < _context.GetPlayer.RealPosition.X && _monster.Position.Y == _context.GetPlayer.RealPosition.Y)
                    {
                        HurtPlayer();
                    }
                    _incrementationAttack++;
                    _timer.Restart();
                }
            }
            else if (_schemaAttack[_incrementationAttack] == 'A')
            { 
                _speedAttack = 80;
                NormalAttack();

                if (_timer.ElapsedMilliseconds >= _speedAttack * 12 && _monster.isAlive)
                {
                    if (_monster.Position.X + _monster.Range > _context.GetPlayer.RealPosition.X && _monster.Position.X - _monster.Range < _context.GetPlayer.RealPosition.X && _monster.Position.Y == _context.GetPlayer.RealPosition.Y)
                    {
                        HurtPlayer();
                    }
                    _incrementationAttack++;
                    _timer.Restart();
                }
            }

        }

         internal void AttackMonster2()
        {
            if (_incrementationAttack2 >= _schemaAttack2.Length - 1)
            {
                _incrementationAttack2 = 0;
            }

            if (_schemaAttack2[_incrementationAttack2] == 'S')
            {
                _speedAttack2 = 50;
                SlidingAttack2();
                if (_timer2.ElapsedMilliseconds >= _speedAttack2 * 12 && _monster2.isAlive2)
                {
                    if (_monster2.Position2.X2 + _monster2.Range2 > _context2.GetPlayer2.RealPosition2.X2 && _monster2.Position2.X2 - _monster2.Range2 < _context2.GetPlayer2.RealPosition2.X2 && _monster2.Position2.Y2 == _context2.GetPlayer2.RealPosition2.Y2)
                    {
                        HurtPlayer2();
                    }
                    _incrementationAttack2++;
                    _timer2.Restart();
                }
            }
            else if (_schemaAttack2[_incrementationAttack2] == 'A')
            {
                _speedAttack2 = 80;
                NormalAttack2();

                if (_timer2.ElapsedMilliseconds >= _speedAttack2 * 12 && _monster2.isAlive2)
                {
                    if (_monster2.Position2.X2 + _monster2.Range2 > _context2.GetPlayer2.RealPosition2.X2 && _monster2.Position2.X2 - _monster2.Range2 < _context2.GetPlayer2.RealPosition2.X2 && _monster2.Position2.Y2 == _context2.GetPlayer2.RealPosition2.Y2)
                    {
                        HurtPlayer2();
                    }
                    _incrementationAttack2++;
                    _timer2.Restart();
                }
            }

        }
        internal void AttackBoss()
        {
            if (_incrementationAttack >= _schemaAttack.Length - 1)
            {
                _incrementationAttack = 0;
            }
            if (_schemaAttack[_incrementationAttack] == 'R')//run slashing
            {
                _speedAttack = 40;
                if (_incrementationSpeed == 0) { _boss.BossSpeed *= 5; _incrementationSpeed++; }
                RunSlashingBoss();
                if (_timer.ElapsedMilliseconds >= _speedAttack * 12 && _boss.IsAlive)
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
            else if (_schemaAttack[_incrementationAttack] == 'A')//slashing
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
            else if (_schemaAttack[_incrementationAttack] == 'T')//throwing
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
            else if (_schemaAttack[_incrementationAttack] == 'K')//kicking
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
            else if (_schemaAttack[_incrementationAttack] == 'S')//sliding
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
        internal void AttackBoss2()
        {
            if (_incrementationAttack2 >= _schemaAttack2.Length - 1)
            {
                _incrementationAttack2 = 0;
            }
            if (_schemaAttack2[_incrementationAttack2] == 'R')//run slashing
            {
                _speedAttack2 = 40;
                if (_incrementationSpeed2 == 0) { _boss2.BossSpeed2 *= 5; _incrementationSpeed2++; }
                RunSlashingBoss2();
                if (_timer2.ElapsedMilliseconds >= _speedAttack2 * 12 && _boss2.IsAlive2)
                {
                    if (_boss2.Position2.X2 + 3 > _context2.GetPlayer2.RealPosition2.X2 && _boss2.Position2.X2 - 3 < _context2.GetPlayer2.RealPosition2.X2 && (_boss2.Position2.Y2 == _context2.GetPlayer2.RealPosition2.Y2 || _boss2.Position2.Y2 + 1 == _context2.GetPlayer2.RealPosition2.Y2))
                    {
                        HurtPlayer2();
                    }
                    _boss2.BossSpeed2 /= 5;
                    _incrementationSpeed2 = 0;
                    _incrementationAttack2++;
                    _timer2.Restart();
                }
            }
            else if (_schemaAttack2[_incrementationAttack2] == 'A')//slashing
            {
                _speedAttack2 = 40;
                SlashingBoss2();
                if (_timer2.ElapsedMilliseconds >= _speedAttack2 * 12 && _boss2.IsAlive2)
                {
                    if (_boss2.Position2.X2 + 3 > _context2.GetPlayer2.RealPosition2.X2 && _boss2.Position2.X2 - 3 < _context2.GetPlayer2.RealPosition2.X2 && (_boss2.Position2.Y2 == _context2.GetPlayer2.RealPosition2.Y2 || _boss2.Position2.Y2 + 1 == _context2.GetPlayer2.RealPosition2.Y2))
                    {
                        HurtPlayer2();
                    }
                    _incrementationAttack2++;
                    _timer2.Restart();
                }
            }
            else if (_schemaAttack2[_incrementationAttack2] == 'T')//throwing
            {
                _speedAttack = 60;
                ThrowingBoss2();
                if (_timer2.ElapsedMilliseconds >= _speedAttack2 * 12 && _boss2.IsAlive2)
                {
                    if (_boss2.Position2.X2 + 3 > _context2.GetPlayer2.RealPosition2.X2 && _boss2.Position2.X2 - 3 < _context2.GetPlayer2.RealPosition2.X2 && (_boss2.Position2.Y2 == _context2.GetPlayer2.RealPosition2.Y2 || _boss2.Position2.Y2 + 1 == _context2.GetPlayer2.RealPosition2.Y2))
                    {
                        _attack2 *= 2;
                        HurtPlayer2();
                        _attack2 /= 2;
                    }
                    _incrementationAttack2++;
                    _timer2.Restart();
                }
            }
            else if (_schemaAttack2[_incrementationAttack2] == 'K')//kicking
            {
                _speedAttack2 = 40;
                KickingBoss2();
                if (_timer2.ElapsedMilliseconds >= _speedAttack2 * 12 && _boss2.IsAlive2)
                {
                    if (_boss2.Position2.X2 + 3 > _context2.GetPlayer2.RealPosition2.X2 && _boss2.Position2.X2 - 3 < _context2.GetPlayer2.RealPosition2.X2 && (_boss2.Position2.Y2 == _context2.GetPlayer2.RealPosition2.Y2 || _boss2.Position2.Y2 + 1 == _context2.GetPlayer2.RealPosition2.Y2))
                    {
                        HurtPlayer2();
                    }
                    _incrementationAttack2++;
                    _timer2.Restart();
                }
            }
            else if (_schemaAttack2[_incrementationAttack2] == 'S')//sliding
            {
                _speedAttack2 = 60;
                SlidingBoss2();
                if (_timer2.ElapsedMilliseconds >= _speedAttack2 * 6 && _boss2.IsAlive2)
                {
                    if (_boss2.Position2.X2 + 3 > _context2.GetPlayer2.RealPosition2.X2 && _boss2.Position2.X2 - 3 < _context2.GetPlayer2.RealPosition2.X2 && (_boss2.Position2.Y2 == _context2.GetPlayer2.RealPosition2.Y2 || _boss2.Position2.Y2 + 1 == _context2.GetPlayer2.RealPosition2.Y2))
                    {
                        HurtPlayer2();
                    }
                    _incrementationAttack2++;
                    _timer2.Restart();
                }
            }
        }

        // AttackBossAnimation (int nbrAction,string name Action, Boss boss, int speed sprite) 
        internal void RunSlashingBoss()
        {
            _boss.GetBossSprite.BossAttackAnimation(12, "runslashing (", _speedAttack);
        }
        internal void RunSlashingBoss2()
        {
            _boss2.GetBossSprite2.BossAttackAnimation2(12, "runslashing (", _speedAttack);
        }
        internal void SlashingBoss()
        {
            _boss.GetBossSprite.BossAttackAnimation(12, "slashing (", _speedAttack);
        }
        internal void SlashingBoss2()
        {
            _boss2.GetBossSprite2.BossAttackAnimation2(12, "slashing (", _speedAttack);
        }
        internal void ThrowingBoss()
        {
            _boss.GetBossSprite.BossAttackAnimation(12, "throwing (", _speedAttack);
        }
        internal void ThrowingBoss2()
        {
            _boss2.GetBossSprite2.BossAttackAnimation2(12, "throwing (", _speedAttack);
        }
        internal void KickingBoss()
        {
            _boss.GetBossSprite.BossAttackAnimation(12, "kicking (", _speedAttack);
        }
        internal void KickingBoss2()
        {
            _boss2.GetBossSprite2.BossAttackAnimation2(12, "kicking (", _speedAttack);
        }
        internal void SlidingBoss()
        {
            _boss.GetBossSprite.BossAttackAnimation(6, "sliding (", _speedAttack);
        }
        internal void SlidingBoss2()
        {
            _boss2.GetBossSprite2.BossAttackAnimation2(6, "sliding (", _speedAttack);
        }

        internal void NormalAttack()
        {
            _monster.GetMonsterSprite.MonsterAttackAnimation(12, "attack", _speedAttack);
        }

        internal void NormalAttack2()
        {
            _monster2.GetMonsterSprite2.MonsterAttackAnimation2(12, "attack", _monster2);
        }

        internal void SlidingAttack()
        {
            _monster.GetMonsterSprite.MonsterAttackAnimation(6, "sliding", _speedAttack);
        }
        internal void SlidingAttack2()
        {
            _monster2.GetMonsterSprite2.MonsterAttackAnimation2(6, "sliding", _monster2);
        }
        internal void HurtPlayer()
        {
            _context.GetPlayer.GetLife.DecreasedPoint(_attack);
            _context.GetPlayer.HurtPlayer = true;
            _context.GetPlayer.DegatsToShow.Add(0 - Convert.ToInt32(_attack));
            HurtPlayerAnimation();
        }

        internal void HurtPlayerAnimation()
        {
            //Hurt animation
            _context.GetPlayer.GetPlayerSprite.PlayerAnimation(4, "hurt", 60);
        }
        internal void HurtPlayerAnimation2()
        {
            _context2.GetPlayer2.GetPlayerSprite2.PlayerAnimation2(4, "hurt", 60);
        }

        internal void HurtPlayer2()
        {
            _context2.GetPlayer2.GetLife2.DecreasedPoint2(_attack);
            _context2.GetPlayer2.HurtPlayer2 = true;
            HurtPlayerAnimation2();
        }

        internal ushort GetAttack
        {
            get => _attack;
            set => _attack = value;
        }
    }
}
