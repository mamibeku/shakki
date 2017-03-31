using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mscee
{
    public class Siirrot
    {
        // palauttaa truen, jos laiton
        //public static bool Sotilas(bool onkoLiikutettu, bool vari, int vanhaX, int vanhaY, int uusiX, int uusiY)
        //{
        //    // TODO: lisaa onkoLiikutetun tarkoitus
        //    // lisätty, lol
        //    // TODO: jokin virheteksti esim. "yritit tehdä laittoman siirron. huutista" samaan tyyliin kuin voittamisteksti
        //    if (vari) // on valkoinen
        //    {
        //        if (uusiY < vanhaY || uusiY - vanhaY > 2)
        //        {
        //            return true;
        //        }
        //        if (onkoLiikutettu && uusiY - vanhaY > 1)
        //        {
        //            return true;
        //        }
        //        if (uusiX == vanhaX && !Peli.lauta.Lauta(uusiX, uusiY).vari)
        //        {
        //            return true;
        //        }
        //        if (uusiY == 3 && Peli.lauta.Lauta(uusiX, 2) != null && uusiX - vanhaX == 0)
        //        {
        //            return true;
        //        }
        //        if (uusiX - vanhaX < -1 || uusiX - vanhaX > 1)
        //        {
        //            return true;
        //        }
        //        if (uusiY - vanhaY == 0)
        //        {
        //            return true;
        //        }
        //        if (uusiX - vanhaX != 0 && uusiY - vanhaY == 2)
        //        {
        //            return true;
        //        }
        //        if (uusiX - vanhaX != 0 && Pelilauta.lauta.Lauta(uusiX, uusiY) == null)
        //        {
        //            return true;
        //        }

        //    }
        //    if (!vari) // on musta
        //    {
        //        if (uusiY > vanhaY || uusiY - vanhaY < -2)
        //        {
        //            return true;
        //        }
        //        if (onkoLiikutettu && uusiY - vanhaY < -1)
        //        {
        //            return true;
        //        }
        //        if (uusiX == vanhaX && Peli.lauta.Lauta(uusiX, uusiY).vari)
        //        {
        //            return true;
        //        }
        //        if (uusiY == 4 && Peli.lauta.Lauta(uusiX, 5) != null && uusiX - vanhaX == 0)
        //        {
        //            return true;
        //        }
        //        if (uusiX - vanhaX < -1 || uusiX - vanhaX > 1)
        //        {
        //            return true;
        //        }
        //        if (uusiY - vanhaY == 0)
        //        {
        //            return true;
        //        }
        //        if (uusiX - vanhaX != 0 && uusiY - vanhaY == -2)
        //        {
        //            return true;
        //        }
        //        if (uusiX - vanhaX != 0 && Peli.lauta.Lauta(uusiX, uusiY) == null)
        //        {
        //            return true;
        //        }

        //    }
        //    return false;
        //}


        //// palauttaa truen, jos laiton
        //public static bool Torni(int vanhaX, int vanhaY, int uusiX, int uusiY)
        //{
        //    int siirronpituus = Math.Abs(uusiX - vanhaX);
        //    if (uusiX - vanhaX != 0 && uusiY - vanhaY != 0)
        //    {
        //        return true;
        //    }
        //    if (uusiY - vanhaY == 0 && vanhaX < uusiX)
        //    {
        //        for (int i = 1 < siirronpituus + 1; i++)
        //        {
        //            if (Peli.lauta.Lauta(uusiX + i, uusiY) != null)
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    if (uusiY - vanhaY == 0 && vanhaX > uusiX)
        //    {
        //        for (int i = 1 < siirronpituus + 1; i++)
        //        {
        //            if (Peli.lauta.Lauta(uusiX - i, uusiY) != null)
        //            {
        //                return true;
        //            }
        //        }
        //    }

        //    return false;
        //}

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
            if (uusiY - vanhaY == 0 || uusiX - vanhaX == 0)
            {
                return true;
            }
            if (uusiX - vanhaX == uusiY - vanhaY)
            {
                return true;
            }
            return false;
        }
        // palauttaa truen, jos laiton
        public static bool Lahetti(int vanhaX, int vanhaY, int uusiX, int uusiY) { return false; }
        // palauttaa truen, jos laiton
        public static bool Kuningas(int vanhaX, int vanhaY, int uusiX, int uusiY) { return false; }
        // palauttaa truen, jos laiton
        public static bool Kuningatar(int vanhaX, int vanhaY, int uusiX, int uusiY) { return false; }

        public void Virheteksti(bool Sotilas, bool Torni, bool Ratsu, bool Lahetti, bool Kuningatar, bool Kuningas)
        {
            if (Sotilas || Torni || Ratsu || Lahetti || Kuningatar || Kuningas)
            {
                Peli.Instance.MessageDisplay.Add("Yritit tehdä laittoman siirron! huutista");
            }
        }
    }
}
