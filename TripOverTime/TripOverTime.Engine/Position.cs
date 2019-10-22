using System;
using System.Collections.Generic;
using System.Text;

namespace TripOverTime.Engine
{
    class Position
    {
        double _x;
        double _y;

        /// <summary>
        /// Init position
        /// </summary>
        /// <param name="x">Default 0</param>
        /// <param name="y">Default 0</param>
        public Position(double x = 0, double y = 0)
        {
            _x = x;
            _y = y;
        }

        public double X
        {
            get => _x;
            internal set => _x = value;
        }
        public double Y
        {
            get => _y;
            internal set => _y = value;
        }
    }
}
