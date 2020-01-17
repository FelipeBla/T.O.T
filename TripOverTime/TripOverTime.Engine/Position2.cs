using System;
using System.Collections.Generic;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    public class Position2
    {
        float _x2;
        float _y2;

        /// <summary>
        /// Init position
        /// </summary>
        /// <param name="x">Default 0</param>
        /// <param name="y">Default 0</param>
        public Position2(float x = 0, float y = 0)
        {
            _x2 = x;
            _y2 = y;
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
            return ((Position2)obj).X2 == this.X2 && ((Position2)obj).Y2 == this.Y2;
        }

        public override int GetHashCode()
        {
            return (this.X2 + this.Y2).GetHashCode();
        }

    }
}
