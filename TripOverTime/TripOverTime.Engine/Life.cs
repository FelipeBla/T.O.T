using System;
using System.Collections.Generic;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    public class Life
    {
        int _maxLife;
        int _currentLife;

        public Life(int maxLife, int currentLife)
        {
            _maxLife = maxLife;
            _currentLife = currentLife;
        }

        public int MaxLife
        {
            get { return _maxLife; }
        }

        public int CurrentLife
        {
            get { return _currentLife; }
        }
    }
}
