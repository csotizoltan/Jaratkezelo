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
        public int Keses;
        public TimeSpan KesesTS { get; set; }


        public Jarat(string jaratSzam, string honnanRepter, string hovaRepter, DateTime indulas)
        {
            JaratSzam = jaratSzam;
            HonnanRepter = honnanRepter;
            HovaRepter = hovaRepter;
            Indulas = indulas;
            Keses = 0;
            KesesTS = TimeSpan.Zero; // Értékadás 15 perc: TimeSpan.FromMinutes(15);
        }
    }
}
