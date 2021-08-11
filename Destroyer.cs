using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lodaky
{
    class Destroyer : Ship
    {
        public Destroyer()
        {
            Width = 1;
            Lenght = 2;
            type = FieldTypes.DD;
        }
        public override void attack(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
