using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lodaky
{
    class Cruiser : Ship
    {
        public Cruiser()
        {
            Width = 1;
            Lenght = 3;
            type = FieldTypes.CA;
        }
        public override void attack(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
