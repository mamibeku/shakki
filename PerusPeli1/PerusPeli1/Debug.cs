using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jypeli;

namespace Mscee
{

    /// <summary>
    /// Äärimmäisen hyödyllinen debuggausluokka.
    /// </summary>
    class Debug
    {
        /// <summary>
        /// Tulosta tällä kaikkea kivaa.
        /// </summary>
        /// <param name="level">Viestin tärkeys</param>
        /// <param name="msg">Nönnönnöö</param>
        public static void Prt(int level, string msg)
        {
            if (Asetukset.debugLevel >= level)
            {
                System.Diagnostics.Debug.WriteLine(msg);
                Game.Instance.MessageDisplay.Add(msg);
            }
           
        }
    }
}
