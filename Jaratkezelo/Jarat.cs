using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jaratkezelo
{
    class Jarat
    {
        public string JaratSzam;
        public string HonnanRepter;
        public string HovaRepter;
        public DateTime Indulas;
        //public int Keses; // vagy public TimeSpan Keses { get; set; }
        public TimeSpan Keses { get; set; }


        public Jarat(string jaratSzam, string honnanRepter, string hovaRepter, DateTime indulas)
        {
            JaratSzam = jaratSzam;
            HonnanRepter = honnanRepter;
            HovaRepter = hovaRepter;
            Indulas = indulas;
            //Keses = 0; //  Keses = TimeSpan.Zero;
            Keses = TimeSpan.Zero;
        }
    }
}
