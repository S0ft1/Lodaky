using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lodaky
{
    class Report
    {
        public bool firstTime;
        public bool result;
    public Report(bool _result, bool _firstTime)
        {
            result = _result;
            firstTime = _firstTime;
        }
    }
}
