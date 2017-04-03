using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mscee
{
    public class Siirrot
    {
         static Peli peli;
        public static void Init(Peli reference)
        {
            peli = reference;
        }

        // palauttaa truen, jos laiton
        public static bool Sotilas(bool onkoLiikutettu, bool vari, int vanhaX, int vanhaY, int uusiX, int uusiY)
        {
            // TODO: lisaa onkoLiikutetun tarkoitus
            // lisätty, lol
            // TODO: jokin virheteksti esim. "yritit tehdä laittoman siirron. huutista" samaan tyyliin kuin voittamisteksti
            if (vari) // on valkoinen
            {
                if (uusiY <= vanhaY || uusiY - vanhaY > 2)
                {
                    return true;
                }
                if (onkoLiikutettu && uusiY - vanhaY > 1)
                {
                    return true;
                }
                if (uusiX == vanhaX && peli.lauta.Lauta(uusiX, uusiY) != null)
                {
                    return true;
                }
                if (uusiY == 3 && peli.lauta.Lauta(uusiX, 2) != null && vanhaY == 1)
                {
                    return true;
                }
                if (uusiX - vanhaX < -1 || uusiX - vanhaX > 1)
                {
                    return true;
                }
                if (uusiX != vanhaX && uusiY - vanhaY == 2)
                {
                    return true;
                }
                if (uusiX != vanhaX && peli.lauta.Lauta(uusiX, uusiY) == null)
                {
                    return true;
                }

            }
            if (!vari) // on musta
            {
                if (uusiY >= vanhaY || uusiY - vanhaY < -2)
                {
                    return true;
                }
                if (onkoLiikutettu && uusiY - vanhaY < -1)
                {
                    return true;
                }
                if (uusiX == vanhaX && peli.lauta.Lauta(uusiX, uusiY) != null)
                {
                    return true;
                }
                if (uusiY == 4 && peli.lauta.Lauta(uusiX, 5) != null && vanhaY == 6)
                {
                    return true;
                }
                if (uusiX - vanhaX < -1 || uusiX - vanhaX > 1)
                {
                    return true;
                }
                if (uusiX != vanhaX && uusiY - vanhaY == -2)
                {
                    return true;
                }
                if (uusiX != vanhaX && peli.lauta.Lauta(uusiX, uusiY) == null)
                {
                    return true;
                }

            }
            return false;
        }


        //// palauttaa truen, jos laiton
        public static bool Torni(int vanhaX, int vanhaY, int uusiX, int uusiY) // Määritellään tornin laittomat siirrot
        {

            if (uusiX - vanhaX != 0 && uusiY - vanhaY != 0)
            {
                return true;
            }
            if (vanhaX < uusiX) // katsotaan onko reitillä nappuloita, jos on niin palauttaa true
            {
                for (int i = vanhaX + 1; i < uusiX; i++)
                {
                    if (peli.lauta.Lauta(i, uusiY) != null)
                    {
                        return true;
                    }
                }
            }
            if (vanhaX > uusiX) // katsotaan onko reitillä nappuloita, jos on niin palauttaa true
            {
                for (int i = vanhaX - 1; i > uusiX; i--)
                {
                    if (peli.lauta.Lauta(i, uusiY) != null)
                    {
                        return true;
                    }
                }
            }
            if (vanhaY < uusiY) // nyt kun siirrot tapahtuvat y-akselilla
            {
                for (int i = vanhaY + 1; i < uusiY; i++)
                {
                    if (peli.lauta.Lauta(uusiX, i) != null)
                    {
                        return true;
                    }
                }
            }
            if (vanhaY > uusiY) // nyt kun siirrot tapahtuvat y-akselilla
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

        // palauttaa truen, jos laiton
        public static bool Ratsu(int vanhaX, int vanhaY, int uusiX, int uusiY)
        {
            if (uusiX - vanhaX > 2 || uusiX - vanhaX < -2)
            {
                return true;
            }
            if (uusiY - vanhaY > 2 || uusiY - vanhaY < -2)
            {
                return true;
            }
            if (uusiY == vanhaY || uusiX == vanhaX)
            {
                return true;
            }
            if (uusiX - vanhaX == uusiY - vanhaY || uusiX - vanhaX == -1 * (uusiY - vanhaY))
            {
                return true;
            }
            return false;
        }
        // palauttaa truen, jos laiton
        public static bool Lahetti(int vanhaX, int vanhaY, int uusiX, int uusiY)
        {
            if (uusiX - vanhaX != uusiY - vanhaY && uusiX - vanhaX != -1 * (uusiY - vanhaY))
            {
                return true;
            }
            if (uusiY > vanhaY && uusiX > vanhaX)
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
            if (uusiY > vanhaY && uusiX < vanhaX)
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
            if (uusiY < vanhaY && uusiX < vanhaX)
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
            if (uusiY < vanhaY && uusiX > vanhaX)
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
        // palauttaa truen, jos laiton
        public static bool Kuningas(int vanhaX, int vanhaY, int uusiX, int uusiY)
        {
            if (Math.Abs(uusiX - vanhaX) > 1 || Math.Abs(uusiY - vanhaY) > 1)
            {
                return true;
            }
            return false;
        }
        // palauttaa truen, jos laiton
        public static bool Kuningatar(int vanhaX, int vanhaY, int uusiX, int uusiY)
        {
            if (uusiX - vanhaX != 0 && uusiY - vanhaY != 0 && uusiX - vanhaX != uusiY - vanhaY && uusiX - vanhaX != -1 * (uusiY - vanhaY))
            {
                return true;
            }
            if (uusiY > vanhaY && uusiX > vanhaX)
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
            if (uusiY > vanhaY && uusiX < vanhaX)
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
            if (uusiY < vanhaY && uusiX < vanhaX)
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
            if (uusiY < vanhaY && uusiX > vanhaX)
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
            if (vanhaX < uusiX && uusiY == vanhaY) // katsotaan onko reitillä nappuloita, jos on niin palauttaa true
            {
                for (int i = vanhaX + 1; i < uusiX; i++)
                {
                    if (peli.lauta.Lauta(i, uusiY) != null)
                    {
                        return true;
                    }
                }
            }
            if (vanhaX > uusiX && uusiY == vanhaY) // katsotaan onko reitillä nappuloita, jos on niin palauttaa true
            {
                for (int i = vanhaX - 1; i > uusiX; i--)
                {
                    if (peli.lauta.Lauta(i, uusiY) != null)
                    {
                        return true;
                    }
                }
            }
            if (vanhaY < uusiY && uusiX == vanhaX) // nyt kun siirrot tapahtuvat y-akselilla
            {
                for (int i = vanhaY + 1; i < uusiY; i++)
                {
                    if (peli.lauta.Lauta(uusiX, i) != null)
                    {
                        return true;
                    }
                }
            }
            if (vanhaY > uusiY && uusiX == vanhaX) // nyt kun siirrot tapahtuvat y-akselilla
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


        public void Virheteksti(bool Sotilas, bool Torni, bool Ratsu, bool Lahetti, bool Kuningatar, bool Kuningas)
        {
            if (Sotilas || Torni || Ratsu || Lahetti || Kuningatar || Kuningas)
            {
                Peli.Instance.MessageDisplay.Add("Yritit tehdä laittoman siirron! huutista");
            }
        }
    }
}
