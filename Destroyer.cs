using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lodaky
{
    class Destroyer : Ship
    {
        public Destroyer(Position _position, bool _rotation)
        {
            Lenght = 2;
            type = FieldTypes.DD;
            baseReloadTime = 6;
            hitPoints = Enumerable.Repeat<bool>(true, Lenght).ToArray();
            rotated = _rotation;
            position = _position;
        }
        public override Position[] attack(Position _position, bool _rotation)
        {
            Position[] area = new Position[10];
            for(int i = 0; i < 10; ++i)
            {
                if (!_rotation)
                {
                    area[i] = new Position(i, _position.Y);
                }
                else 
                {
                    area[i] = new Position(_position.X, i);
                }
            }
            return area;
        }
    }
}
