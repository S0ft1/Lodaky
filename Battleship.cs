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
        public Battleship()
        { 
            Width = 1;
            Lenght = 4;
            type = FieldTypes.BB;
        }

        public override void attack(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
