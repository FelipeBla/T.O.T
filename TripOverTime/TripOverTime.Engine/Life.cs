using System;
using System.Collections.Generic;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    public class Life
    {
        readonly ushort _maxPoint;
        readonly ushort _minPoint;
        ushort _currentPoint;
        ushort _currentPoint2;

        public Life()
            : this(1000, 0)
        {
        }

        public Life(ushort maxPoint, ushort minPoint = 0)
        {
            _maxPoint = maxPoint;
            _minPoint = minPoint;
            _currentPoint = maxPoint;
            _currentPoint2 = maxPoint;
        }

        public ushort CurrentPoint
        {
            get { return _currentPoint; }
            internal set => _currentPoint = value;
        }
        public ushort CurrentPoint2
        {
            get { return _currentPoint2; }
            internal set => _currentPoint2 = value;
        }

        public ushort MaxPoint
        {
            get => _maxPoint;
        }

        public float PerCent
        {
            get => (float)_currentPoint / (float)_maxPoint;
        }
        public float PerCent2
        {
            get => (float)_currentPoint2 / (float)_maxPoint;
        }

        public bool IsFull
        {
            get { return _currentPoint == _maxPoint; }
        }

        public bool IsEmpty
        {
            get { return _currentPoint == 0; }
        }

        public void Empty(ushort point)
        {
            if (point > _currentPoint) throw new InvalidOperationException("The amount of point to remove is to big.");

            _currentPoint -= point;
        }

        public void Empty()
        {
            _currentPoint = _minPoint;
            _currentPoint = 0;
        }

        public void Fill(ushort point)
        {
            if (_currentPoint + point > _maxPoint) throw new InvalidOperationException("The amount of point to add is to big.");

            _currentPoint += point;
        }

        public void Fill()
        {
            _currentPoint = _maxPoint;
            _currentPoint = 1000;
        }

        public ushort GetCurrentPoint
        {
            get => _currentPoint;
            set => _currentPoint = value;
        }
        public ushort GetCurrentPoint2()
        {
            return _currentPoint2;
        }


        public ushort GetMinPoint()
        {
            return _maxPoint;
        }
        public ushort GetMaxPoint()
        {
            return _maxPoint;
        }

        public void DecreasedPoint(ushort damage)
        {
            if (damage < 0 ) return;
            if (damage >= _currentPoint) 
            { 
                _currentPoint = 0;
            }
            else
            {
                _currentPoint -= damage;
            }
        }

        public void DecreasedPoint2(ushort damage)
        {
            if (damage < 0) return;
            if (damage >= _currentPoint2)
            {
                _currentPoint2 = 0;
                Console.WriteLine("The player is dead.");
            }
            else
            {
                _currentPoint2 -= damage;
            }
        }

        public void BonusPoint(ushort bonus)
        {
            if (bonus < 0) return;
            if (bonus + _currentPoint >= _maxPoint)
            {
                _currentPoint = _maxPoint;
            }
            else
            {
                _currentPoint += bonus;
            }
        }

        public void BonusPoint2(ushort bonus)
        {
            if (bonus < 0) return;
            if (bonus + _currentPoint2 >= _maxPoint)
            {
                _currentPoint2 = _maxPoint;
            }
            else
            {
                _currentPoint2 += bonus;
            }
        }

    }

}
