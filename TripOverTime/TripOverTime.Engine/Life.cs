using System;
using System.Collections.Generic;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    //
    public class Life
    {
        readonly Game _context;
        Monster _monster;
        Player _player;
        readonly ushort _maxPoint;
        readonly ushort _minPoint;
        ushort _currentPoint;
        ushort _damage;

        public Life()
            : this(1000, 0)
        {
        }

        public Life(ushort maxPoint, ushort minPoint)
        {
            _maxPoint = maxPoint;
            _minPoint = minPoint;
            _currentPoint = maxPoint;
        }

        public ushort CurrentPoint
        {
            get { return _currentPoint; }
            internal set => _currentPoint = value;
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

        public void BonusPoint(ushort bonus)
        {
            _currentPoint += Convert.ToUInt16(Math.Max(Convert.ToInt32(bonus), 0));
        }
    }
}
