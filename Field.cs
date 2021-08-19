using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lodaky
{
    class Field
    {
        public FieldTypes type;
        public bool spotted;
        public Field( FieldTypes _type)
        {
            spotted = false;
            type = _type;
        }

    }
}
