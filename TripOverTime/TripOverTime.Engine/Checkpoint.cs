using System;
using System.Collections.Generic;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    public class Checkpoint
    {
        Position _position;
        Position _lastActivatedCheckpoint;

        public Checkpoint(Position lastActivatedCheckpoint)
        {
            _lastActivatedCheckpoint = lastActivatedCheckpoint;
        }
        internal Position LastActivatedCheckpoint
        {
            get { return _lastActivatedCheckpoint; }
            set { _lastActivatedCheckpoint = value; }
        }

    }
}
