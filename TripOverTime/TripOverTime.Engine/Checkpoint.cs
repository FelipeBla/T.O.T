﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    public class Checkpoint
    {
        Position _position;
        Position _lastActivatedCheckpoint;
        Position _lastActivatedCheckpoint2;

        public Checkpoint(Position lastActivatedCheckpoint)
        {
            _lastActivatedCheckpoint = lastActivatedCheckpoint;
            _lastActivatedCheckpoint2 = lastActivatedCheckpoint;
        }
        internal Position LastActivatedCheckpoint
        {
            get { return _lastActivatedCheckpoint; }
            set { _lastActivatedCheckpoint = value; }
        }
        internal Position LastActivatedCheckpoint2
        {
            get { return _lastActivatedCheckpoint2; }
            set { _lastActivatedCheckpoint2 = value; }
        }

    }
}
