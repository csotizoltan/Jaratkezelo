using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jaratkezelo
{
    class NegativKesesException : Exception
    {
        public NegativKesesException(int time)
            : base("Nem lehet negatív a késés végösszege: " + time.ToString())
        {

        }

        public NegativKesesException(TimeSpan time)
           : base("Nem lehet negatív a késés végösszege: " + time.ToString())
        {

        }
    }
}
