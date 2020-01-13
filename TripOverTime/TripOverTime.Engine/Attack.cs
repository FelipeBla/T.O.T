using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TripOverTime.EngineNamespace
{
    internal class Attack
    {
        Game _context;
        Monster _monster;
        ushort _attack;
        Stopwatch _timer;
        int _specialAttack;

        internal Attack(Game context, Monster monster, ushort attack)
        {
            _context = context;
            _monster = monster;
            _attack = attack;
            _timer = new Stopwatch();
            _timer.Start();
            _specialAttack = 0;
        }

        internal void AttackMonster()
        {
            if (_specialAttack < 2 && (_monster.Name == "Golem1" || _monster.Name == "Golem2"))
            {
                SlidingAttack();
                if (_timer.ElapsedMilliseconds >= 700)
                {
                    _specialAttack++;
                    _timer.Restart();
                }
            }
            else
            {
                NormalAttack();
            }

            if (_timer.ElapsedMilliseconds >= 910 && _monster.Position.X + 2 > _context.GetPlayer.RealPosition.X && _monster.Position.X - 2 < _context.GetPlayer.RealPosition.X && _monster.isAlive)
            {
                _context.GetPlayer.GetLife.DecreasedPoint(_attack);
                _context.GetPlayer.HurtPlayer = true;
                HurtPlayer();
                _timer.Restart();
            }
        }
        internal void NormalAttack()
        {
            _monster.GetMonsterSprite.MonsterAttackAnimation(12, "attack", _monster);
        }

        internal void SlidingAttack()
        {
            _monster.GetMonsterSprite.MonsterAttackAnimation(6, "sliding", _monster);
        }

        internal void HurtPlayer()
        {
            _context.GetPlayer.GetPlayerSprite.PlayerAnimation(4, "hurt", 40);
        }

        internal void HurtPlayer2()
        {
            _context.GetPlayer2.GetPlayerSprite.PlayerAnimation(4, "hurt", 40);
        }

        internal ushort GetAttack
        {
            get => _attack;
            set => _attack = value;
        }
    }
}
