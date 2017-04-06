using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;
using System.Collections;
/// @author  Markus Kulmala, Jyri Eerola
/// @version 7.4.2017

namespace Mscee
{
    /// <summary>
    /// Oma vektoriluokka kokonaisluvuille, vrt. Jypeli.Vector double-pareille.
    /// </summary>
    public class Vektori
    {
        /// <summary>
        /// Vektorin x-komponentti
        /// </summary>
        /// <value>
        /// Asettaa x-komponentin suuruuden
        /// </value>
        public int x;

        /// <summary>
        /// Vektorin y-komponentti
        /// </summary>
        /// <value>
        /// Asettaa y-komponentin suuruuden
        /// </value>
        public int y;

        /// <summary>
        /// Vektorin konstruktori
        /// </summary>
        /// <param name="x">Uuden vektorin x-komponentti</param>
        /// <param name="y">Uuden vektorin y-komponentti</param>
        /// <returns>
        /// Palauttaa uuden vektorin
        /// </returns>
        public Vektori(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary cref="Jypeli.Vector">
        /// Muunnos Jypeli.Vector:ksi
        /// </summary>
        /// <returns>
        /// Palauttaa doublea käyttävän Vectorin.
        /// </returns>
        public Vector AsVector()
        {
            return new Vector(x, y);
        }

    }
    /// <summary>
    /// Vakioita joita käytetään usein, jotka liittyvät laudan ja nappulan asetuksiin
    /// </summary>
    public class Asetukset
    {
        // TODO: isoilla alkukirjaimilla
        public const int LAUDANKOKO = 208;
        public const int RUUDUNKOKO = LAUDANKOKO / 8;
        public const int origo = RUUDUNKOKO / 2 - LAUDANKOKO / 2;
        public const double ZOOMNOPEUS = 0.1;
        public const int NAPPULANKOKO_X = 20;
        public const int NAPPULANKOKO_Y = 20;

        public int debugLevel = 0;
    }
    /// <summary>
    /// itse peliin liittyvät asiat, kuten siirtäminen 
    /// </summary>
    public class Peli : Game
    {
        public Asetukset asetukset;

        /// <summary>
        /// lauta sisältää shakkipelin tilan tarkkailuun käytettävän objektin
        /// </summary>
        public Pelilauta lauta = new Pelilauta(8, 8);

        // ladataan taustakuva tiedostosta
        private Image taustaKuva = LoadImage("gridi");

        private Vektori edellinenKlikkaus; //edellisen klikatun ruudun koordinaatit
        private Vektori viimeisinNappula = new Vektori(-1, -1); // ruutuX ja ruutuY alkutilassa

        /// <summary>
        /// mitä tapahtuu kun peli käynnistetään
        /// </summary>
        public override void Begin()
        {
            // asetetaan järkeviä aloitusarvoja Jypelin asetuksille
            IsFullScreen = false;
            Camera.ZoomFactor = 4.0;
            Mouse.IsCursorVisible = true;
            Title = "MSCEE";

            // asetetaan näytölle tulevien viestien näkymisajaksi 10 sekuntia
            MessageDisplay.MessageTime = new TimeSpan(0, 0, 10);

            // annetaan siirrontarkistusluokalle referenssi käytävään peliin
            Siirrot.Init(this);

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

            // TÄSTÄ ETEENPÄIN VAIN KIVAA

            // asetetaan taustakuva eli kuva shakkilaudasta
            Level.Background.Image = taustaKuva;

            // sulkemiset
            PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
            Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");

            // koko näytön tila <--> ikkunatila
            Keyboard.Listen(Key.K, ButtonState.Pressed, delegate { IsFullScreen = !IsFullScreen; }, "Ikkunatilan vaihto");

            // zoomaus
            Keyboard.Listen(Key.Add, ButtonState.Down, delegate { Camera.ZoomFactor += peli.asetukset.ZOOMNOPEUS; }, "Zoom+");
            Keyboard.Listen(Key.Subtract, ButtonState.Down, delegate { Camera.ZoomFactor -= peli.asetukset.ZOOMNOPEUS; }, "Zoom-");

            // flippaus
            Keyboard.Listen(Key.F, ButtonState.Pressed, delegate { Camera.ZoomFactor = -Camera.ZoomFactor; lauta.FlippaaLauta(); }, "Flippaa lauta");

            // debugin aktivointi
            Keyboard.Listen(Key.D, ButtonState.Pressed, delegate { this.DebugToggle(); }, "Debug");

            // itse pelaamisen hiirella handlaus
            Mouse.Listen(MouseButton.Left, ButtonState.Pressed, KlikkausPelissa, "Siirtele nappuloita");

        }

        /// <summary>
        /// DebugToggle käsittelee debugLevelin muuttamiseen käytettävät näppäinpainallukset.
        /// </summary>
        private void DebugToggle()
        {
            // Jos shift on painettuna, vähennetään debugLeveliä.
            // Pidetään myös huoli, ettei mennä nollasta pienemmäksi.
            if (Keyboard.IsShiftDown() && peli.asetukset.debugLevel > 0)
            {
                peli.asetukset.debugLevel -= 1;
            }
            else  // Muuten nostetaan debugLeveliä.
            {
                peli.asetukset.debugLevel += 1;
            }
            // Huudellaan uudesta levelistä.
            System.Diagnostics.Debug.WriteLine("debugLevel = " + peli.asetukset.debugLevel);
            MessageDisplay.Add("debugLevel = " + peli.asetukset.debugLevel);
        }

        /// <summary>
        /// KlikkausPelissa käsittelee nappuloiden siirtoihin liittyvät klikkaukset pelilaudalla.
        /// </summary>
        private void KlikkausPelissa()
        {
            Debug.Prt(Mouse.PositionOnWorld.X);
            Debug.Prt(Mouse.PositionOnWorld.Y);

            int uusiX = (int)Math.Floor(Mouse.PositionOnWorld.X + peli.asetukset.LAUDANKOKO / 2) / peli.asetukset.RUUDUNKOKO; // FIXME: poista "peli."?
            int uusiY = (int)Math.Floor(Mouse.PositionOnWorld.Y + peli.asetukset.LAUDANKOKO / 2) / peli.asetukset.RUUDUNKOKO; // FIXME: poista "peli."?

            // tarkistetaan, että koordinaatit ovat laudan sisällä
            if (uusiX < 0 || uusiX > 7 || uusiY < 0 || uusiY > 7)
            {
                return;
            }

            // katsotaan mitä klikatusta ruudusta löytyy
            Nappula uusiNappula = lauta.Lauta(uusiX, uusiY);

            // Onko edellistä nappulaa valittu?
            if (viimeisinNappula.x == -1 && viimeisinNappula.y == -1)
            {
                if (uusiNappula != null) // valittiin nappula
                {
                    viimeisinNappula.x = uusiX;
                    viimeisinNappula.y = uusiY;
                }
            }
            else // NAPPULA ON VALITTU
            {
                // tarkitetaan onko nappula oma tai paikka sama kuin edellinen
                if ((viimeisinNappula.x == uusiX && viimeisinNappula.y == uusiY) || (uusiNappula != null && lauta.Lauta(viimeisinNappula.x, viimeisinNappula.y).vari == uusiNappula.vari))
                {
                    return;
                }
                lauta.Lauta(edellinenKlikkaus).Siirra(uusiX, uusiY);

                // nollataan viimeksi valitun nappulan paikka
                viimeisinNappula.x = -1;
                viimeisinNappula.y = -1;
            }

            // tallennetaan tapahtuneen klikkauksen ruudun koordinaatit
            edellinenKlikkaus = new Vektori(uusiX, uusiY);
        }
    }
}
