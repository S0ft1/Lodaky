using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lodaky
{
    class AI
    {
        private Random rd = new Random();

        public Position getRndPos()
        {
            Position pos = new Position(0, 0);
            pos.X = rd.Next(0, 10);
            pos.Y = rd.Next(0, 10);
            return pos;
        }
        public FieldTypes getShip()
        {
            switch (rd.Next(0, 4))
            {
                case 0:
                    return FieldTypes.BB;
                case 1:
                    return FieldTypes.CV;
                case 2:
                    return FieldTypes.CA;
                case 3:
                    return FieldTypes.DD;
                default:
                    return FieldTypes.SEA;
            }
        }
        public bool getAiRotation()
        {
            bool x = rd.Next(0, 2) == 1;

            return x;
        }
    }
}
