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
            baseReloadTime = 3;
            reloadTime = baseReloadTime;
            hitPoints = Enumerable.Repeat<bool>(true, Lenght).ToArray();
            rotated = _rotation;
            position = _position;
        }

        public override void attack(Position _position)
        {
            throw new NotImplementedException();
        }
    }
}
