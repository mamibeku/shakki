using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jypeli;

namespace Mscee
{
    /// <summary>
    /// trackataan nappuloita
    /// </summary>
    public class NappulaTracker
    {
        public bool vari;
        public char arvo;
        public int x;
        public int y;
        public Vector paikka;
        public bool liikutettu = false;

        /// <summary>
        /// nappulaan liittyvät attribuutit
        /// </summary>
        /// <param name="nappula">laudalla oleva nappula</param>
        public NappulaTracker(Nappula nappula)
        {
            vari = nappula.vari;
            arvo = nappula.arvo;
            x = nappula.paikkaX;
            y = nappula.paikkaY;
            paikka = new Vector(nappula.paikkaX, nappula.paikkaY);
        }

        /// <summary>
        /// Nappulan paikka 
        /// </summary>
        /// <param name="x">x-koordinaatti laudalla</param>
        /// <param name="y">y-kordinaatti laudalla</param>
        public void XY(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Nappulan paikka
        /// </summary>
        /// <param name="paikka">paikka vektorina</param>
        public void XY(Vector paikka)
        {
            x = (int)paikka.X;
            y = (int)paikka.Y;
        }
    }
}
