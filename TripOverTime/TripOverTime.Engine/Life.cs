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

        public ushort MaxPoint
        {
            get => _maxPoint;
        }

        public float PerCent
        {
            get => (float)_currentPoint / (float)_maxPoint;
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

        public ushort GetCurrentPoint()
        {
            return _currentPoint;
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
                Console.WriteLine("The player is dead.");
            }
            else
            {
                _currentPoint -= damage;
            }
        }

        public ushort BonusPoint(ushort bonus)
        {
            _currentPoint += Convert.ToUInt16(Math.Max(Convert.ToInt32(bonus), 0));
            int valeur = bonus;
            if (valeur + CurrentPoint > 1000)
                return 0;
            return _maxPoint;

        }
    }

}
