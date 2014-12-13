using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;

namespace Windup_Detector
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.PrintChat("============================");
            Game.PrintChat("TwiligtLoL - Windup Detector");
            Game.PrintChat("Windup should be: " + detectWindup());
            Game.PrintChat("============================");
        }
        private static int detectWindup()
        {
            var additional = 0;
            if (Game.Ping >= 100)
                additional = Game.Ping / 10;
            else if (Game.Ping > 40 && Game.Ping < 100)
                additional = Game.Ping / 100 * 20;
            else if (Game.Ping <= 40)
                additional += 20;
            var windUp = Game.Ping + additional;
            if (windUp < 40)
                windUp = 40;
            return windUp < 200 ? windUp : 200;
        }
    }
}
