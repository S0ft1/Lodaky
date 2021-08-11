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
        protected int Width;
        protected int Lenght;
        protected Image image;
        protected Position position = new Position(0, 0);
        protected bool destroyed = false;
        protected FieldTypes type;

        public FieldTypes getType()
        {
            return type;
        }
        public void setPosition(Position _position)
        {
            position.X = _position.X;
            position.Y = _position.Y;
        }
        public int getLenght()
        {
            return Lenght;
        }

        public int getWidth()
        {
            return Width;
        }
        public abstract void attack(int x, int y);
    }
}
