using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lodaky
{
    class AircraftCarrier : Ship
    {
        bool[,] cvHitPoints = new bool[3, 2];
        public AircraftCarrier(Position _position, bool _rotation)
        {
            Lenght = 3;
            type = FieldTypes.CV;
            baseReloadTime = 4;
            reloadTime = baseReloadTime;
            fillCvHitPoints();
            rotated = _rotation;
            position = _position;
        }
        private void fillCvHitPoints()
        {
            for(int i = 0; i < Lenght;++i)
            {
                cvHitPoints[i, 0] = true;
                cvHitPoints[i, 1] = true;
            }
        }
        public override bool checkIfDestroyed()
        {
            for (int i = 0; i < Lenght; ++i)
            {
                if (cvHitPoints[i, 0]|| cvHitPoints[i, 1])
                {
                    return false;
                }
            }
            return true;
        }
        public override void isHitted(int index)
        {
            //dodelat
        }
        public override void attack(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
