using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jypeli;

namespace Mscee
{
    /// <summary>
    /// nappulaan liittyviä asioita
    /// </summary>
    public class Nappula : GameObject
    {
        public bool onkoLiikutettu;
        public bool vari;
        public char arvo;
        public int paikkaX;
        public int paikkaY;

        Peli peli;

        /// <summary>
        /// Siirrä nappula uuteen paikkaan (x,y)
        /// </summary>
        /// <param name="x">uuden paikan x-koordinaatti</param>
        /// <param name="y">uuden paikan y-koordinaatti</param>
        /// <returns></returns>
        public bool Siirra(int x, int y)
        {
            Debug.Prt(1, "Siirto: " + (paikkaX + 1) + ", " + (paikkaY + 1) + " -> " + (x + 1) + ", " + (y + 1));

            Nappula uudenpaikannappula = peli.lauta.Lauta(x, y);
            Debug.Prt(2, "Uudessa ruudussa: " + uudenpaikannappula);

            // tarkistetaan siirtojen laillisuus
            switch (arvo)
            {
                case 's': if (Siirrot.Sotilas(onkoLiikutettu, vari, paikkaX, paikkaY, x, y)) { return false; } break;
                case 't': if (Siirrot.Torni(paikkaX, paikkaY, x, y)) { return false; } break;
                case 'r': if (Siirrot.Ratsu(paikkaX, paikkaY, x, y)) { return false; } break;
                case 'l': if (Siirrot.Lahetti(paikkaX, paikkaY, x, y)) { return false; } break;
                case 'k': if (Siirrot.Kuningas(paikkaX, paikkaY, x, y)) { return false; } break;
                case 'q': if (Siirrot.Kuningatar(paikkaX, paikkaY, x, y)) { return false; } break;
                default:
                    break;
            }

            Position = new Vector(peli.asetukset.ruudunKoko * x + peli.asetukset.origo, peli.asetukset.ruudunKoko * y + peli.asetukset.origo);
            Debug.Prt(2, "Position: " + Position.X + ", " + Position.Y);

            if (uudenpaikannappula != null)
            {
                peli.lauta.Poista(x, y); // jos uudessa paikassa on erivärinen nappula, niin se poistetaan
            }

            peli.lauta.Lisaa(new Nappula(this.arvo, this.vari, x, y, true));
            peli.lauta.Poista(paikkaX, paikkaY);

            return true;
        }
        /// <summary>
        /// siirtofunktio, mutta kokonaislukujen sijasta ottaa argumenttina vektorin joka sisältää uuden paikan koordinaatit
        /// </summary>
        /// <param name="paikka">uusi paikka</param>
        /// <returns>menee tekemään saman kuin edellisessä </returns>
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
        public Nappula(char arvo, bool vari, int x, int y, bool onkoLiikutettu) : base(peli.asetukset.nappulanKokoX, peli.asetukset.nappulanKokoY) // TODO: defaultoi joskus myöhemmin
        {
            peli = (Peli)Game.Instance;

            this.arvo = arvo;
            this.vari = vari;

            paikkaX = x;
            paikkaY = y;
            Position = new Vector(peli.asetukset.ruudunKoko * x + peli.asetukset.origo, peli.asetukset.ruudunKoko * y + peli.asetukset.origo);
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
