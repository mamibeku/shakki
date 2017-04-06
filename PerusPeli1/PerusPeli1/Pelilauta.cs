using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jypeli;

namespace Mscee
{
    /// <summary>
    /// laudalla olevat asiat, kuten eriväriset nappulat
    /// </summary>
    public class Pelilauta
    {
        //private Peli peli;

        public Nappula[,] lauta;
        public List<NappulaTracker> valkoiset = new List<NappulaTracker>();
        public List<NappulaTracker> mustat = new List<NappulaTracker>();

        private bool flipattu = false;

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

        /// <summary>
        /// haetaan Nappuloita
        /// </summary>
        /// <param name="paikka"> paikka laudalla</param>
        /// <returns>Nappula joka siinä paikassa on</returns>
        public Nappula Lauta(Vektori paikka)
        {
            return Lauta(paikka.x, paikka.y);
        }

        /// <summary>
        /// Muutetaan nappuloita.
        /// </summary>
        /// <param name="x">laudan x koordianatti</param>
        /// <param name="y">laudan y koordinaatti</param>
        /// <param name="uusi">Nappula, joka laitetaan paikkaan x,y</param>
        public void Lauta(int x, int y, Nappula uusi)
        {
            lauta[x, y] = uusi;
        }

        /// <summary>
        /// Sama kuin äsken, mutta paikka on kokonaisluku vektori
        /// </summary>
        /// <param name="paikka">laudan x ja y koordianatti</param>
        /// <param name="uusi">Nappula, joka laitetaan paikkaan x,y</param>
        public void Lauta(Vektori paikka, Nappula uusi)
        {
            lauta[paikka.x, paikka.y] = uusi;
        }
        /// <summary>
        /// Lisätään nappuloita
        /// </summary>
        /// <param name="nappula"> nappula joka lisätään</param>
        public void Lisaa(Nappula nappula)
        {
            lauta[nappula.paikkaX, nappula.paikkaY] = nappula;
            if (flipattu) { nappula.FlipImage(); }
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

        /// <summary>
        /// Poistaa nappulan taulukosta ja listoilta
        /// </summary>
        /// <param name="x">laudan x koordinaatti</param>
        /// <param name="y">laudan y koordinaatti</param>
        /// <returns></returns>
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

        /// <summary>
        /// sama kuin äskeinen, mutta ottaa argumenttina kokonaislukuvektorin
        /// </summary>
        /// <param name="paikka">paikka laudalla</param>
        /// <returns></returns>
        public bool Poista(Vektori paikka)
        {
            return Poista(paikka.x, paikka.y);
        }
        /// <summary>
        /// Käännetään lauta
        /// </summary>
        public void FlippaaLauta()
        {
            flipattu = !flipattu;
            foreach (Nappula nappula in lauta)
            {
                if (nappula != null)
                {
                    nappula.FlipImage();
                }
            }
        }
    }

}
