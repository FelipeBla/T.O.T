using System;
using System.Collections.Generic;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    public class Position
    {
        float _x;
        float _y;
        float _x2;
        float _y2;

        /// <summary>
        /// Init position
        /// </summary>
        /// <param name="x">Default 0</param>
        /// <param name="y">Default 0</param>
        public Position(float x = 0, float y = 0)
        {
            _x = x;
            _y = y;
            _x2 = x;
            _y2 = y;
        }

        public float X
        {
            get => _x;
            internal set => _x = value;
        }
        public float Y
        {
            get => _y;
            internal set => _y = value;
        }
        public float X2
        {
            get => _x2;
            internal set => _x2 = value;
        }
        public float Y2
        {
            get => _y2;
            internal set => _y2 = value;
        }

        // For TryGetValue
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Position)) return false;
            return ((Position)obj).X == this.X && ((Position)obj).Y == this.Y;
        }

        public override int GetHashCode()
        {
            return (this.X + this.Y).GetHashCode();
        }

    }
}
