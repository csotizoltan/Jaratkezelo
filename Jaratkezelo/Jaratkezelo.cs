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
                if (item.JaratSzam.Equals(jaratSzam))
                {
                    if ((item.Keses + keses) > 0)
                    {
                        item.Keses += keses; //keso = keso + keses;
                    }

                    else
                    {
                        throw new NegativKesesException(keses);
                    }
                }
            }
        }


        // Keses --> TimeSpan
        public void KesesTS(string jaratSzam, TimeSpan keses)
        {
            foreach (var item in jaratok)
            {
                if (item.JaratSzam.Equals(jaratSzam))
                {
                    if ((item.KesesTS + keses) > new TimeSpan(0,0,0))
                    {
                        item.KesesTS += keses;
                    }

                    else
                    {
                        throw new NegativKesesException(keses);
                    }
                }
            }
        }


        public DateTime MikorIndul(string jaratSzam)
        {
            foreach (var item in jaratok)
            {
                if (item.JaratSzam.Equals(jaratSzam))
                {
                    return item.Indulas + item.KesesTS;
                }
            }

            throw new ArgumentException(jaratSzam);
        }


        // MikorIndul --> TimeSpan
        public DateTime MikorIndulTS(string jaratSzam)
        {
            foreach (var item in jaratok)
            {
                if (item.JaratSzam.Equals(jaratSzam))
                {
                    return item.Indulas + item.KesesTS;
                }
            }

            throw new ArgumentException(jaratSzam);
        }

        
        public List<string> JaratokRepuloterrol(string repter)
        {
            List<string> JaratokSzama = new List<string>();

            foreach (var item in jaratok)
            {
                if (item.HonnanRepter.Equals(repter))
                {
                    JaratokSzama.Add(item.JaratSzam);
                    return JaratokSzama;
                }
            }

            throw new ArgumentException(repter);
        }


        public Jarat JaratInfo(string jaratSzam)
        {
            foreach (var item in jaratok)
            {
                if (item.JaratSzam.Equals(jaratSzam))
                {
                    return item;
                }
            }

            throw new ArgumentException(jaratSzam);
        }
    }
}
