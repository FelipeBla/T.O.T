using System;
using System.Collections.Generic;
using System.Text;

namespace TripOverTime.EngineNamespace
{
    internal class Items
    {
        string _name;
        Position _position;
        readonly Game _context;
        bool _isValid;
        internal Items(Game context, string name, Position position)
        {
            _name = name;
            _position = position;
            _context = context;
            _isValid = true;
        }

        internal void BonusPoint()
        {
            if (_position.X == _context.GetPlayer.Position.X && _name == "Heart" && _isValid)
            {
                _context.GetPlayer.GetLife.BonusPoint(1);
                _isValid = false;
            }
        }

        internal string GetName
        {
            get => _name;
        }
        internal Position Position
        {
            get => _position;
        }

        internal bool IsValid
        {
            get => _isValid;
        }
    }
}
