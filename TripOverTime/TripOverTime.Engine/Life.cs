using System;
using System.Collections.Generic;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    public class Life
    {
        readonly Game _context;
        Monster _monster;
        Player _player;
        readonly ushort _maxPoint;
        readonly ushort _minPoint;
        ushort _currentPoint;

        public Life()
            : this(1000)
        {
        }

        public Life(ushort maxPoint, ushort minPoint)
        {
            _maxPoint = maxPoint;
            _minPoint = minPoint;
        }

        public ushort CurrentPoint
        {
            get { return _currentPoint; }
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
    }
}
