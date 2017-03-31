using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jypeli;

namespace Mscee
{
    public class Nappula : GameObject
    {
        public bool tyhja = false;
        public bool onkoLiikutettu;
        public bool vari;
        public char arvo;
        public int paikkaX;
        public int paikkaY;

        Peli peli;

        // Siirrä nappula uuteen paikkaan (x,y).
        public bool Siirra(int x, int y)
        {
            Position = new Vector(Asetukset.ruudunKoko * x + Asetukset.origo, Asetukset.ruudunKoko * y + Asetukset.origo);

            Nappula kenka = peli.lauta.Lauta(x, y);

            // tarkistetaan siirtojen laillisuus
            switch (arvo)
            {
                //case 's': if (Siirrot.Sotilas(onkoLiikutettu, vari, paikkaX, paikkaY, x, y)) { return false; } break;
                //case 't': if (Siirrot.Torni(paikkaX, paikkaY, x, y)) { return false; } break;
                case 'r': if (Siirrot.Ratsu(paikkaX, paikkaY, x, y)) { return false; } break;
                case 'l': if (Siirrot.Lahetti(paikkaX, paikkaY, x, y)) { return false; } break;
                case 'k': if (Siirrot.Kuningas(paikkaX, paikkaY, x, y)) { return false; } break;
                case 'q': if (Siirrot.Kuningatar(paikkaX, paikkaY, x, y)) { return false; } break;
                default:
                    break;
            }


            if (kenka != null && kenka.vari && !vari) // uudessa ruudussa valkoinen, ja musta syö
            {
                peli.lauta.Poista(x, y);
            }
            if (kenka != null && !kenka.vari && vari) // uudessa musta, ja valkoinen syö
            {
                peli.lauta.Poista(x, y);
            }
            if (kenka == null || kenka.vari != vari)
            {
                peli.lauta.Lisaa(new Nappula(arvo, vari, x, y, true));
                peli.lauta.Poista(paikkaX, paikkaY);
            }



            return true; // TODO: PLACEHOLDER, FIX THIS
        }

        public bool Siirra(Vektori paikka)
        {
            return Siirra(paikka.x, paikka.y);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Mscee.Nappula"/> class.
        /// </summary>
        /// <param name="arvo">Onko nappula sotilas, lähetti tai tms.</param>
        /// <param name="vari">Nappula on valkoinen, kun vari on <c>true</c>.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public Nappula(char arvo, bool vari, int x, int y, bool onkoLiikutettu) : base(20, 20) // TODO: defaultoi joskus myöhemmin
        {
            peli = (Peli)Game.Instance;

            this.arvo = arvo;
            this.vari = vari;

            paikkaX = x;
            paikkaY = y;
            Position = new Vector(Asetukset.ruudunKoko * x + Asetukset.origo, Asetukset.ruudunKoko * y + Asetukset.origo);
            //
            string prefix = "musta";
            if (vari) { prefix = "valkoinen"; } // valitaan väri

            switch (arvo)
            {
                case 's': this.Image = Game.LoadImage(prefix + "sotilas"); break;
                case 't': this.Image = Game.LoadImage(prefix + "torni"); break;
                case 'r': this.Image = Game.LoadImage(prefix + "ratsu"); break;
                case 'l': this.Image = Game.LoadImage(prefix + "lahetti"); break;
                case 'k': this.Image = Game.LoadImage(prefix + "kuningas"); break;
                case 'q': this.Image = Game.LoadImage(prefix + "kuningatar"); break;
                default: break;
            }
        }

    }
}
