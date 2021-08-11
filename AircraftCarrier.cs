using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lodaky
{
    class AircraftCarrier : Ship
    {
        public AircraftCarrier()
        {
            Width = 2;
            Lenght = 3;
            type = FieldTypes.CV;
        }
        public override void attack(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
