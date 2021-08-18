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
            baseReloadTime = 5;
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
