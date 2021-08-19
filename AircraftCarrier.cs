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
        private Position fixedPosition(Position _position)
        {
            if (_position.X == 0)
            {
                _position.X = 1;
            }
            if (_position.Y == 0)
            {
                _position.Y = 1;
            }
            if (_position.X == 9)
            {
                _position.X = 8;
            }
            if (_position.Y == 9)
            {
                _position.Y = 8;
            }
            _position.X = _position.X - 1;
            _position.Y = _position.Y - 1;
            return _position;
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
            Console.WriteLine(type + " destroyed");
            return true;
        }
        public override void isHitted(int index,bool lower)
        {
            if (lower)
            {
                cvHitPoints[index, 1] = false;
            }
            else
            {
                cvHitPoints[index, 0] = false;
            }
        }
        public override Position[] attack(Position _position,bool _rotation)
        {
            _position = fixedPosition(_position);
            //dodelat checknuti hranice      
            Position[] area = new Position[9];
            ushort counter = 0;
            for(int i = 0; i < 3; ++i)
            {
                for(int j = 0; j < 3; ++j)
                {
                    area[counter] = new Position(_position.X + j, _position.Y + i);
                    ++counter;
                }
            }
            return area;
        }
    }
}
