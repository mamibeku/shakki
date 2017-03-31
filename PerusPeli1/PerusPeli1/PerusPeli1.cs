using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;
using System.Collections;

namespace Mscee
{
    public class Vektori
    {
        public int x, y;
        public Vektori(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector AsVector()
        {
            return new Vector(x, y);
        }

    }

    public class Asetukset
    {
        public const int laudanKoko = 208;
        public const int ruudunKoko = laudanKoko / 8;
        public const int origo = ruudunKoko / 2 - laudanKoko / 2;
        public const double ZoomNopeus = 0.1;
        public const int nappulanKokoX = 20;
        public const int nappulanKokoY = 20;

        public static int debugLevel = 0;
    }

    public class Peli : Game
    {
        public Pelilauta lauta = new Pelilauta(8, 8);

        Image taustaKuva = LoadImage("gridi");

        private Vektori edellinenKlikkaus;
        private Vektori viimeisinNappula = new Vektori(-1, -1); // ruutuX, ruutuY

        public override void Begin()
        {
            IsFullScreen = false;
            Camera.ZoomFactor = 4.0;
            Mouse.IsCursorVisible = true;
            Title = "MSCEE";
            MessageDisplay.MessageTime = new TimeSpan(0, 0, 10);

            // lisätään valkoiset sotilaat
            for (int i = 1; i < 9; i++)
            {
                lauta.Lisaa(new Nappula('s', true, i - 1, 1, false));
            }

            // lisätään mustat sotilaat
            for (int i = 1; i < 9; i++)
            {
                lauta.Lisaa(new Nappula('s', false, i - 1, 6, false));
            }

            // tornit
            // kaikkiin -1 ja i-1
            lauta.Lisaa(new Nappula('t', true, 0, 0, false));
            lauta.Lisaa(new Nappula('t', true, 7, 0, false));

            lauta.Lisaa(new Nappula('t', false, 0, 7, false));
            lauta.Lisaa(new Nappula('t', false, 7, 7, false));

            //// ratsut
            lauta.Lisaa(new Nappula('r', true, 1, 0, false));
            lauta.Lisaa(new Nappula('r', true, 6, 0, false));
            lauta.Lisaa(new Nappula('r', false, 1, 7, false));
            lauta.Lisaa(new Nappula('r', false, 6, 7, false));

            //// lähetit
            lauta.Lisaa(new Nappula('l', true, 2, 0, false));
            lauta.Lisaa(new Nappula('l', true, 5, 0, false));
            lauta.Lisaa(new Nappula('l', false, 2, 7, false));
            lauta.Lisaa(new Nappula('l', false, 5, 7, false));

            ////kuninkaat
            lauta.Lisaa(new Nappula('k', true, 4, 0, false));
            lauta.Lisaa(new Nappula('k', false, 4, 7, false));

            //// kuningattaret
            lauta.Lisaa(new Nappula('q', true, 3, 0, false));
            lauta.Lisaa(new Nappula('q', false, 3, 7, false));

            //TÄSTÄ ETEENPÄIN VAIN KIVAA

            Level.Background.Image = taustaKuva;

            // sulkemiset
            PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
            Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");

            // koko näytön tila <--> ikkunatila
            Keyboard.Listen(Key.F, ButtonState.Pressed, delegate { IsFullScreen = !IsFullScreen; }, "Ikkunatilan vaihto");

            // zoomaus
            Keyboard.Listen(Key.Add, ButtonState.Down, delegate { Camera.ZoomFactor += Asetukset.ZoomNopeus; }, "Zoom+");
            Keyboard.Listen(Key.Subtract, ButtonState.Down, delegate { Camera.ZoomFactor -= Asetukset.ZoomNopeus; }, "Zoom-");

            // debugin aktivointi
            Keyboard.Listen(Key.D, ButtonState.Pressed, delegate { this.DebugToggle(); }, "Debug");

            // itse pelaamisen hiirella handlaus
            Mouse.Listen(MouseButton.Left, ButtonState.Pressed, KlikkausPelissa, "kissa");

        }

        private void DebugToggle()
        {
            if (Keyboard.IsShiftDown() && Asetukset.debugLevel > 0)
            {
                Asetukset.debugLevel -= 1;
            }
            else
            {
                Asetukset.debugLevel += 1;
            }
            Console.WriteLine("debugLevel = " + Asetukset.debugLevel);
            MessageDisplay.Add("debugLevel = " + Asetukset.debugLevel);
        }

        private void KlikkausPelissa()
        {
            Console.WriteLine(Mouse.PositionOnWorld.X);
            Console.WriteLine(Mouse.PositionOnWorld.Y);

            int uusiX = (int)Math.Floor(Mouse.PositionOnWorld.X + Asetukset.laudanKoko / 2) / Asetukset.ruudunKoko;
            int uusiY = (int)Math.Floor(Mouse.PositionOnWorld.Y + Asetukset.laudanKoko / 2) / Asetukset.ruudunKoko;

            if (uusiX < 0 || uusiX > 7 || uusiY < 0 || uusiY > 7)
            {
                return;
            }

            Nappula uusiNappula = lauta.Lauta(uusiX, uusiY);

            if (viimeisinNappula.x == -1 && viimeisinNappula.y == -1) // Onko nappulaa valittu?
            {
                if (uusiNappula != null) // valittiin nappula
                {
                    viimeisinNappula.x = uusiX;
                    viimeisinNappula.y = uusiY;
                }
            }
            else // NAPPULA ON VALITTU
            {
                // tarkitetaan onko nappula oma tai paikka sama kuin edellinen // FIXME: tarkista
                if ((viimeisinNappula.x == uusiX && viimeisinNappula.y == uusiY) || (uusiNappula != null && lauta.Lauta(viimeisinNappula.x, viimeisinNappula.y).vari == uusiNappula.vari))
                {
                    return;
                }
                lauta.Lauta(edellinenKlikkaus).Siirra(uusiX, uusiY);


                viimeisinNappula.x = -1;
                viimeisinNappula.y = -1;
            }

            edellinenKlikkaus = new Vektori(uusiX, uusiY);
        }
    }
}