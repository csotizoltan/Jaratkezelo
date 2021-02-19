using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jaratkezelo
{
    class Jaratkezelo
    {
        public List<Jarat> jaratok = new List<Jarat>();

        public void UjJarat(string jaratSzam, string repterHonnan, string repterHova, DateTime indulas)
        {
            foreach (var item in jaratok)
            {
                if (item.JaratSzam.Equals(jaratSzam))
                {
                    throw new ArgumentException(jaratSzam);
                }
            }

            jaratok.Add(new Jarat(jaratSzam, repterHonnan, repterHova, indulas));
        }


        public void Keses(string jaratSzam, int keses)
        {
            foreach (var item in jaratok)
            {
                int keso = 0;

                if (item.JaratSzam.Equals(jaratSzam))
                {
                    if ((keso + keses) < keso)
                    {
                        keso += keses; //keso = keso + keses;
                    }

                    else
                    {
                        throw new NegativKesesException(keses);
                    }

                    keso += keses; //keso = keso + keses;
                }
            }
        }


        public DateTime MikorIndul(string jaratSzam)
        {
            foreach (var item in jaratok)
            {
                if (item.JaratSzam.Equals(jaratSzam))
                {
                    return item.Indulas + item.Keses;
                }
            }

            throw new ArgumentException(jaratSzam);
        }


        public List<string> JaratokRepuloterrol(string repter)
        {
            List<string> repulokSzama = new List<string>();

            foreach (var item in jaratok)
            {
                if (item.HonnanRepter.Equals(repter))
                {
                    repulokSzama.Add(item.JaratSzam);
                    return repulokSzama;
                }
            }

            throw new ArgumentException(repter);
        }
    }
}
