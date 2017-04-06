using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mscee
{
    public class Siirrot
    {
        private static Peli peli;
        private static void Init(Peli reference)
        {
            peli = reference;
        }

        /// <summary>
        /// Katsotaan onko sotilaan siirto laillinen
        /// </summary>
        /// <param name="onkoLiikutettu">onko nappulaa liikutettu</param>
        /// <param name="vari">true valkoinen, musta false</param>
        /// <param name="vanhaX">X-kordinaatti, jossa nappula on kun sitä lähdetään liikuttamaan</param>
        /// <param name="vanhaY">Y-kordinaatti, jossa nappula on kun sitä lähdetään liikuttamaan</param>
        /// <param name="uusiX">nappulan uuden paikan X-koordinaatti</param>
        /// <param name="uusiY">nappulan uuden paikan Y-koordinaatti</param>
        /// <returns>Palauttaa true, jos siirto on laiton. False, jos siirto on laillinen.</returns>
        public bool Sotilas(bool onkoLiikutettu, bool vari, int vanhaX, int vanhaY, int uusiX, int uusiY)
        {            
            if (vari) // on valkoinen
            {
                if (uusiY <= vanhaY || uusiY - vanhaY > 2) // ei voi liikkua eteenpäin kuin max2, eikä taaksepäin yhtään
                {
                    return true;
                }
                if (onkoLiikutettu && uusiY - vanhaY > 1) // jos on liikutettu niin eteenpäin vain yksi
                {
                    return true;
                }
                if (uusiX == vanhaX && peli.lauta.Lauta(uusiX, uusiY) != null) // ei voi syödä samalla x koordinaatilla olevaa
                {
                    return true;
                }
                if (uusiY == 3 && peli.lauta.Lauta(uusiX, 2) != null && vanhaY == 1) // ei voi mennä alkuruudusta toisen nappulan läpi kaksi ruutua eteenpäin
                {
                    return true;
                }
                if (uusiX - vanhaX < -1 || uusiX - vanhaX > 1) // ei voi mennä vaakasuunnassa pidemmälle sivulle kuin yksi ruutu
                {
                    return true;
                }
                if (uusiX != vanhaX && uusiY - vanhaY == 2) // ei voi mennä kaksi ruutua eteenpäin ja sivulle samaan aikaan
                {
                    return true;
                }
                if (uusiX != vanhaX && peli.lauta.Lauta(uusiX, uusiY) == null) // ei voi mennä sivulle ellei syö nappulaa
                {
                    return true;
                }
            }
            if (!vari) // on musta
            {
                if (uusiY >= vanhaY || uusiY - vanhaY < -2) // ei voi liikkua eteenpäin kuin max2, eikä taaksepäin yhtään
                {
                    return true;
                }
                if (onkoLiikutettu && uusiY - vanhaY < -1) // jos on liikutettu niin eteenpäin vain yksi
                {
                    return true;
                }
                if (uusiX == vanhaX && peli.lauta.Lauta(uusiX, uusiY) != null) // ei voi syödä samalla x koordinaatilla olevaa// ei voi syödä samalla x koordinaatilla olevaa
                {
                    return true;
                }
                if (uusiY == 4 && peli.lauta.Lauta(uusiX, 5) != null && vanhaY == 6) // ei voi mennä alkuruudusta toisen nappulan läpi kaksi ruutua eteenpäin
                {
                    return true;
                }
                if (uusiX - vanhaX < -1 || uusiX - vanhaX > 1) // ei voi mennä vaakasuunnassa pidemmälle sivulle kuin yksi ruutu
                {
                    return true;
                }
                if (uusiX != vanhaX && uusiY - vanhaY == -2)  // ei voi mennä kaksi ruutua eteenpäin ja sivulle samaan aikaan
                {
                    return true;
                }
                if (uusiX != vanhaX && peli.lauta.Lauta(uusiX, uusiY) == null) // ei voi mennä sivulle ellei syö nappulaa
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Katsotaan onko tornin siirto laillinen
        /// </summary>
        /// <param name="vanhaX">X-kordinaatti, jossa nappula on kun sitä lähdetään liikuttamaan</param>
        /// <param name="vanhaY">Y-kordinaatti, jossa nappula on kun sitä lähdetään liikuttamaan</param>
        /// <param name="uusiX">nappulan uuden paikan X-koordinaatti</param>
        /// <param name="uusiY">nappulan uuden paikan Y-koordinaatti</param>
        /// <returns>Palauttaa true, jos siirto on laiton. False, jos siirto on laillinen.</returns>
        public bool Torni(int vanhaX, int vanhaY, int uusiX, int uusiY) // Määritellään tornin laittomat siirrot
        {
            if (uusiX - vanhaX != 0 && uusiY - vanhaY != 0) // voi liikkua vain vaaka- tai pystysuunnassa
            {
                return true;
            }
            if (vanhaX < uusiX) // katsotaan onko reitillä nappuloita, liikutaan oikealle
            {
                for (int i = vanhaX + 1; i < uusiX; i++)
                {
                    if (peli.lauta.Lauta(i, uusiY) != null)
                    {
                        return true;
                    }
                }
            }
            if (vanhaX > uusiX) // katsotaan onko reitillä nappuloita, liikutaan vasemmalle
            {
                for (int i = vanhaX - 1; i > uusiX; i--)
                {
                    if (peli.lauta.Lauta(i, uusiY) != null)
                    {
                        return true;
                    }
                }
            }
            if (vanhaY < uusiY) // katsotaan onko reitillä nappuloita, liikutaan ylös
            {
                for (int i = vanhaY + 1; i < uusiY; i++)
                {
                    if (peli.lauta.Lauta(uusiX, i) != null)
                    {
                        return true;
                    }
                }
            }
            if (vanhaY > uusiY) // katsotaan onko reitillä nappuloita, liikutaan alas
            {
                for (int i = vanhaY - 1; i > uusiY; i--)
                {
                    if (peli.lauta.Lauta(uusiX, i) != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Katsotaan onko ratsun siirto laillinen
        /// </summary>
        /// <param name="vanhaX">X-kordinaatti, jossa nappula on kun sitä lähdetään liikuttamaan</param>
        /// <param name="vanhaY">Y-kordinaatti, jossa nappula on kun sitä lähdetään liikuttamaan</param>
        /// <param name="uusiX">nappulan uuden paikan X-koordinaatti</param>
        /// <param name="uusiY">nappulan uuden paikan Y-koordinaatti</param>
        /// <returns>Palauttaa true, jos siirto on laiton. False, jos siirto on laillinen.</returns>
        public bool Ratsu(int vanhaX, int vanhaY, int uusiX, int uusiY)
        {
            if (uusiX - vanhaX > 2 || uusiX - vanhaX < -2) // sivuille ei voi liikkua enemmän kuin kaksi ruutua
            {
                return true;
            }
            if (uusiY - vanhaY > 2 || uusiY - vanhaY < -2) // eikä ylös tai alas
            {
                return true;
            }
            if (uusiY == vanhaY || uusiX == vanhaX) // kummallakin akselilla on tapahduttava muutosta
            {
                return true;
            }
            if (uusiX - vanhaX == uusiY - vanhaY || uusiX - vanhaX == -1 * (uusiY - vanhaY)) // ei saa liikkua diagonaalilla
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Katsotaan onko lähetin siirto laillinen
        /// </summary>
        /// <param name="vanhaX">X-kordinaatti, jossa nappula on kun sitä lähdetään liikuttamaan</param>
        /// <param name="vanhaY">Y-kordinaatti, jossa nappula on kun sitä lähdetään liikuttamaan</param>
        /// <param name="uusiX">nappulan uuden paikan X-koordinaatti</param>
        /// <param name="uusiY">nappulan uuden paikan Y-koordinaatti</param>
        /// <returns>Palauttaa true, jos siirto on laiton. False, jos siirto on laillinen.</returns>
        public bool Lahetti(int vanhaX, int vanhaY, int uusiX, int uusiY)
        {
            if (uusiX - vanhaX != uusiY - vanhaY && uusiX - vanhaX != -1 * (uusiY - vanhaY)) // tapahtuuko siirto ylipäätään diagonaalilla
            {
                return true;
            }
            if (uusiY > vanhaY && uusiX > vanhaX) // onko reitillä muita nappuloita
            {
                int j = vanhaY + 1;
                for (int i = vanhaX + 1; i < uusiX; i++, j++)
                {
                    if (peli.lauta.Lauta(i, j) != null)
                    {
                        return true;
                    }
                }
            }
            if (uusiY > vanhaY && uusiX < vanhaX) // onko reitillä muita nappuloita
            {
                int j = vanhaY + 1;
                for (int i = vanhaX - 1; i > uusiX; i--, j++)
                {
                    if (peli.lauta.Lauta(i, j) != null)
                    {
                        return true;
                    }
                }
            }
            if (uusiY < vanhaY && uusiX < vanhaX) // onko reitillä muita nappuloita
            {
                int j = vanhaY - 1;
                for (int i = vanhaX - 1; i > uusiX; i--, j--)
                {
                    if (peli.lauta.Lauta(i, j) != null)
                    {
                        return true;
                    }
                }
            }
            if (uusiY < vanhaY && uusiX > vanhaX) // onko reitillä muita nappuloita
            {
                int j = vanhaY - 1;
                for (int i = vanhaX + 1; i < uusiX; i++, j--)
                {
                    if (peli.lauta.Lauta(i, j) != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Katsotaan onko kuninkaan siirto laillinen
        /// </summary>
        /// <param name="vanhaX">X-kordinaatti, jossa nappula on kun sitä lähdetään liikuttamaan</param>
        /// <param name="vanhaY">Y-kordinaatti, jossa nappula on kun sitä lähdetään liikuttamaan</param>
        /// <param name="uusiX">nappulan uuden paikan X-koordinaatti</param>
        /// <param name="uusiY">nappulan uuden paikan Y-koordinaatti</param>
        /// <returns>Palauttaa true, jos siirto on laiton. False, jos siirto on laillinen.</returns>
        public bool Kuningas(int vanhaX, int vanhaY, int uusiX, int uusiY)
        {
            if (Math.Abs(uusiX - vanhaX) > 1 || Math.Abs(uusiY - vanhaY) > 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Katsotaan onko kuningattaren siirto laillinen
        /// </summary>
        /// <param name="vanhaX">X-kordinaatti, jossa nappula on kun sitä lähdetään liikuttamaan</param>
        /// <param name="vanhaY">Y-kordinaatti, jossa nappula on kun sitä lähdetään liikuttamaan</param>
        /// <param name="uusiX">nappulan uuden paikan X-koordinaatti</param>
        /// <param name="uusiY">nappulan uuden paikan Y-koordinaatti</param>
        /// <returns>Palauttaa true, jos siirto on laiton. False, jos siirto on laillinen.</returns>
        public bool Kuningatar(int vanhaX, int vanhaY, int uusiX, int uusiY)
        {
            if (uusiX - vanhaX != 0 && uusiY - vanhaY != 0 && uusiX - vanhaX != uusiY - vanhaY && uusiX - vanhaX != -1 * (uusiY - vanhaY)) // tapahtuuko siirto diagonaalin tai pysty-tai vaakasuunnan ulkopuolella
            {
                return true;
            }
            if (uusiY > vanhaY && uusiX > vanhaX) // onko reitillä muita nappuloita
            {
                int j = vanhaY + 1;
                for (int i = vanhaX + 1; i < uusiX; i++, j++)
                {
                    if (peli.lauta.Lauta(i, j) != null)
                    {
                        return true;
                    }
                }
            }
            if (uusiY > vanhaY && uusiX < vanhaX) // onko reitillä muita nappuloita
            {
                int j = vanhaY + 1;
                for (int i = vanhaX - 1; i > uusiX; i--, j++)
                {
                    if (peli.lauta.Lauta(i, j) != null)
                    {
                        return true;
                    }
                }
            }
            if (uusiY < vanhaY && uusiX < vanhaX) // onko reitillä muita nappuloita
            {
                int j = vanhaY - 1;
                for (int i = vanhaX - 1; i > uusiX; i--, j--)
                {
                    if (peli.lauta.Lauta(i, j) != null)
                    {
                        return true;
                    }
                }
            }
            if (uusiY < vanhaY && uusiX > vanhaX) // onko reitillä muita nappuloita
            {
                int j = vanhaY - 1;
                for (int i = vanhaX + 1; i < uusiX; i++, j--)
                {
                    if (peli.lauta.Lauta(i, j) != null)
                    {
                        return true;
                    }
                }
            }
            if (vanhaX < uusiX && uusiY == vanhaY) // onko reitillä muita nappuloita
            {
                for (int i = vanhaX + 1; i < uusiX; i++)
                {
                    if (peli.lauta.Lauta(i, uusiY) != null)
                    {
                        return true;
                    }
                }
            }
            if (vanhaX > uusiX && uusiY == vanhaY) // onko reitillä muita nappuloita
            {
                for (int i = vanhaX - 1; i > uusiX; i--)
                {
                    if (peli.lauta.Lauta(i, uusiY) != null)
                    {
                        return true;
                    }
                }
            }
            if (vanhaY < uusiY && uusiX == vanhaX) // onko reitillä muita nappuloita
            {
                for (int i = vanhaY + 1; i < uusiY; i++)
                {
                    if (peli.lauta.Lauta(uusiX, i) != null)
                    {
                        return true;
                    }
                }
            }
            if (vanhaY > uusiY && uusiX == vanhaX) // onko reitillä muita nappuloita
            {
                for (int i = vanhaY - 1; i > uusiY; i--)
                {
                    if (peli.lauta.Lauta(uusiX, i) != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Jos pelaaja yrittää tehdä laitonta siirtoa, niin tulostetaan siitä ilmoittava virheviesti
        /// </summary>
        /// <param name="Sotilas">sotilaan siirron laillisuus</param>
        /// <param name="Torni">tornin siirron laillisuus</param>
        /// <param name="Ratsu">ratsun siirron laillisuus</param>
        /// <param name="Lahetti">lähetin siirron laillisuus</param>
        /// <param name="Kuningatar">kuningattaren siirron laillisuus</param>
        /// <param name="Kuningas">kuninkaan siirron laillisuus</param>
        public void Virheteksti(bool Sotilas, bool Torni, bool Ratsu, bool Lahetti, bool Kuningatar, bool Kuningas)
        {
            if (Sotilas || Torni || Ratsu || Lahetti || Kuningatar || Kuningas)
            {
                Peli.Instance.MessageDisplay.Add("Yritit tehdä laittoman siirron! huutista");
            }
        }
    }
}
