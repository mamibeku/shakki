using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jypeli;

namespace Mscee
{
    public class Pelilauta
    {
        //private Peli peli;

        public Nappula[,] lauta;
        public List<NappulaTracker> valkoiset = new List<NappulaTracker>();
        public List<NappulaTracker> mustat = new List<NappulaTracker>();

        public Pelilauta(int xkoko, int ykoko)
        {
            //this.peli = peli;
            lauta = new Nappula[xkoko, ykoko];
        }

        // Getter. Haetaan nappuloita.
        public Nappula Lauta(int x, int y)
        {
            try
            {
                return lauta[x, y];
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        // Getter. Haetaan nappuloita.
        public Nappula Lauta(Vektori paikka)
        {
            return Lauta(paikka.x, paikka.y);
        }

        // Setter. Muutetaan nappuloita.
        public void Lauta(int x, int y, Nappula uusi)
        {
            lauta[x, y] = uusi;
        }

        // Setter. Muutetaan nappuloita.
        public void Lauta(Vektori paikka, Nappula uusi)
        {
            lauta[paikka.x, paikka.y] = uusi;
        }

        public void Lisaa(Nappula nappula)
        {
            lauta[nappula.paikkaX, nappula.paikkaY] = nappula;
            if (nappula.vari)
            {
                valkoiset.Add(new NappulaTracker(nappula));
            }
            else
            {
                mustat.Add(new NappulaTracker(nappula));
            }
            Peli.Instance.Add(nappula);
        }

        // Poista nappula taulukosta ja listoilta
        public bool Poista(int x, int y)
        {
            for (int i = 0; i < valkoiset.Count; i++)
            {
                if (valkoiset[i].x == x && valkoiset[i].y == y)
                {
                    valkoiset.RemoveAt(i);
                    lauta[x, y].Destroy();
                    lauta[x, y] = null;

                }

            }
            if (valkoiset.Count == 0)
            {
                Peli.Instance.MessageDisplay.Add("Musta voitti! huutista");
            }
            for (int i = 0; i < mustat.Count; i++)
            {
                if (mustat[i].x == x && mustat[i].y == y)
                {
                    mustat.RemoveAt(i);
                    lauta[x, y].Destroy();
                    lauta[x, y] = null;

                }

            }
            if (mustat.Count == 0)
            {
                Peli.Instance.MessageDisplay.Add("Valkoinen voitti! huutista");
            }
            return true;
        }

        // Poista nappula taulukosta ja listoilta
        public bool Poista(Vektori paikka)
        {
            return Poista(paikka.x, paikka.y);
        }

    }

}
