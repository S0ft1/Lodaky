using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lodaky
{
    class Battleship : Ship
    {
        public Battleship(Position _position, bool _rotation)
        {
            Lenght = 4;
            type = FieldTypes.BB;
            baseReloadTime = 4;
            hitPoints = Enumerable.Repeat<bool>(true, Lenght).ToArray();
            rotated = _rotation;
            position = _position;
        }

        private Position fixedPosition(Position _position)
        {
            if (_position.X == 9)
            {
                _position.X = 8;
            }
            if (_position.Y == 9)
            {
                _position.Y = 8;
            }
            return _position;
        }

        public override Position[] attack(Position _position, bool _rotation)
        {
            Position[] area = new Position[4];
            _position = fixedPosition(_position);
            area[0] = _position;
            area[1] = new Position(_position.X, _position.Y + 1);
            area[2] = new Position(_position.X + 1, _position.Y);
            area[3] = new Position(_position.X + 1, _position.Y + 1);
            return area;
        }
    }
}
