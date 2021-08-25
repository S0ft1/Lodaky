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
            return chooseShipByIndex(rd.Next(0, 4));
        }

        public FieldTypes chooseShipByIndex(int index)
        {
            switch (index)
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
        public FieldTypes chooseAttack(Ship[]fleet)
        {
            int randomN = 0;
            FieldTypes chosenShip = FieldTypes.SEA;
            while (chosenShip == FieldTypes.SEA)
            {
                randomN = rd.Next(0, 4);
                if (fleet[randomN].getReloadCooldown() == 0 && !fleet[randomN].getDestroyed())
                {
                    chosenShip = chooseShipByIndex(randomN);
                }
            }
            return chosenShip;
        }

        public bool getAiRotation()
        {
            bool x = rd.Next(0, 2) == 1;

            return x;
        }
    }
}
