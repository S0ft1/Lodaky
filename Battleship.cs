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
            Lenght = 4;
            type = FieldTypes.BB;
            baseReloadTime = 3;
            reloadTime = baseReloadTime;
        }

        public override void attack(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
