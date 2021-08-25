using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lodaky
{
    class Cruiser : Ship
    {
        public Cruiser(Position _position, bool _rotation)
        {
            Lenght = 3;
            type = FieldTypes.CA;
            baseReloadTime = 1;
            hitPoints = Enumerable.Repeat<bool>(true, Lenght).ToArray();
            rotated = _rotation;
            position = _position;
        }
        public override Position[] attack(Position _position, bool _rotation)
        {
            return new Position[] { new Position(_position.X, _position.Y) };           
        }
    }
}
