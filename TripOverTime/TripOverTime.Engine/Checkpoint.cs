using System;
using System.Collections.Generic;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    class Checkpoint
    {
        Position _position;
        Position _lastActivatedCheckpoint;

        public Checkpoint()
        {
            
        }
        internal Position LastActivatedCheckpoint
        {
            set { _lastActivatedCheckpoint = value; }
        }
        
    }
}
