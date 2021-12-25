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
        protected ushort reloadCooldown = 0;
        protected bool[] hitPoints;
        protected bool rotated = false;



        public virtual Report checkIfDestroyed()
        {
            if (destroyed)
            {
                return new Report(true,false);
            }
            for(int i = 0; i < hitPoints.Length; ++i)
            {
                if (hitPoints[i])
                {
                    return new Report(false, false);
                }
            }
           // Console.WriteLine(type + "destroyed");
            destroyed = true;
            return new Report(true, true);
        }

        public virtual void isHitted(int index,bool cv)
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

        public ushort getReloadCooldown()
        {
            return reloadCooldown;
        }
        public ushort getBaseReload()
        {
            return baseReloadTime;
        }
        public void shoot()
        {
            reloadCooldown = baseReloadTime;
        }
        public void reload()
        {
            if (reloadCooldown != 0)
            {
                --reloadCooldown;
            }
        }
        public bool getDestroyed()
        {
            return destroyed;
        }

        public abstract Position[] attack(Position _position,bool _rotation);
    }
}
