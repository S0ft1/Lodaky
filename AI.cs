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
        public int getRndPos()
        {
            return rd.Next(0, 10);
        }
        
        public bool getAiRotation()
        {
            bool x = rd.Next(0, 2) == 1;

            return x;
        }
    }
}
