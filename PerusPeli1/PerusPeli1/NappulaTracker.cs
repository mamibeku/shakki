using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jypeli;

namespace Mscee
{
    public class NappulaTracker
    {
        public bool vari;
        public char arvo;
        public int x;
        public int y;
        public Vector paikka;
        public bool liikutettu = false;

        public NappulaTracker(Nappula nappula)
        {
            vari = nappula.vari;
            arvo = nappula.arvo;
            x = nappula.paikkaX;
            y = nappula.paikkaY;
            paikka = new Vector(nappula.paikkaX, nappula.paikkaY);
        }

        public void XY(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void XY(Vector paikka)
        {
            x = (int)paikka.X;
            y = (int)paikka.Y;
        }
    }
}
