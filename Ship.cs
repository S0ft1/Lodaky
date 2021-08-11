using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lodaky
{
    abstract class Ship
    {
        protected ushort Lenght;
        protected Image image;
        protected Position position;
        protected bool destroyed = false;
        protected FieldTypes type;
        protected ushort baseReloadTime;
        protected ushort reloadTime;
        protected bool[] hitPoints;
        protected bool rotated = false;



        public virtual bool checkIfDestroyed()
        {
            if (destroyed)
            {
                return true;
            }
            for(int i = 0; i < hitPoints.Length; ++i)
            {
                if (hitPoints[i])
                {
                    return false;
                }
            }
            Console.WriteLine(type + "destroyed");
            destroyed = true;
            return true;
        }

        public virtual void isHitted(int index)
        {
            hitPoints[index] = false;
        }
        public FieldTypes getType()
        {
            return type;
        }
        public bool getRotation()
        {
            return rotated;
        }
        public Position getPosition()
        {
            return position;
        }
        public ushort getLenght()
        {
            return Lenght;
        }

        public abstract void attack(int x, int y);
    }
}
