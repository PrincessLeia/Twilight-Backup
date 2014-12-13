using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using Color = System.Drawing.Color;

namespace TAC_Kassadin
{
    class MenuHandler
    {
        internal static Menu menu;
        internal static void load()
        {
            menu = new Menu("TAC Kassadin","tac_kassadin",true);

            Menu targetSelector = new Menu("Target selector","ts");
            SimpleTs.AddToMenu(targetSelector);
            menu.AddSubMenu(targetSelector);

            Menu orbwalker = new Menu("Orbwalker", "orbwalker");
            Program.orb = new Orbwalking.Orbwalker(orbwalker);
            menu.AddSubMenu(orbwalker);

            menu.AddSubMenu(new Menu("AutoCarry", "ac"));
            menu.SubMenu("ac").AddItem(new MenuItem("acQ", "Use Q").SetValue(true));
            menu.SubMenu("ac").AddItem(new MenuItem("acW", "Use W").SetValue(true));
            menu.SubMenu("ac").AddItem(new MenuItem("acE", "Use E").SetValue(true));
            menu.SubMenu("ac").AddItem(new MenuItem("acR", "Use R").SetValue(true));

            menu.AddSubMenu(new Menu("Harass (Mixed)", "mx"));
            menu.SubMenu("mx").AddItem(new MenuItem("mxQ", "Use Q").SetValue(true));
            menu.SubMenu("mx").AddItem(new MenuItem("mxE", "Use E").SetValue(true));

            menu.AddSubMenu(new Menu("Misc", "misc"));
            menu.SubMenu("misc").AddItem(new MenuItem("blockMD", "Block incoming AP damage").SetValue(true));
            menu.SubMenu("misc").AddItem(new MenuItem("antiGap", "Anti-Gap closer").SetValue(true));
            menu.SubMenu("misc").AddItem(new MenuItem("interruptSpells", "Interrupt spells").SetValue(true));
            menu.SubMenu("misc").AddItem(new MenuItem("useShield", "Seraph's Embrace").SetValue(true));
            menu.SubMenu("misc").AddItem(new MenuItem("useShieldHP", "Use Seraph at X HP").SetValue(new Slider(30, 10, 100)));
            menu.SubMenu("misc").AddItem(new MenuItem("useDFG", "Use DFG").SetValue(true));
            menu.SubMenu("misc").AddItem(new MenuItem("useDFGFull", "Use DFG on full combo only").SetValue(true));
            menu.SubMenu("misc").AddItem(new MenuItem("chargeE", "Smart E charging").SetValue(true));
            menu.SubMenu("misc").AddItem(new MenuItem("chargeEto", "Charge E to").SetValue(new Slider(4,1,5)));


            menu.AddSubMenu(new Menu("Kill-Secure", "ks"));
            menu.SubMenu("ks").AddItem(new MenuItem("ksQ", "Use Q").SetValue(true));
            menu.SubMenu("ks").AddItem(new MenuItem("ksE", "Use E").SetValue(true));
            menu.SubMenu("ks").AddItem(new MenuItem("ksR", "Use R").SetValue(true));
            menu.SubMenu("ks").AddItem(new MenuItem("ksActive", "Enabled").SetValue(true));

            menu.AddSubMenu(new Menu("Sexy Drawings", "d"));
            menu.SubMenu("d").AddItem(new MenuItem("QRange", "Q range").SetValue(new Circle(true, Color.FromArgb(100, Color.Red))));
            menu.SubMenu("d").AddItem(new MenuItem("WRange", "W range").SetValue(new Circle(false, Color.FromArgb(100, Color.Coral))));
            menu.SubMenu("d").AddItem(new MenuItem("ERange", "E range").SetValue(new Circle(true, Color.FromArgb(100, Color.BlueViolet))));
            menu.SubMenu("d").AddItem(new MenuItem("RRange", "R range").SetValue(new Circle(false, Color.FromArgb(100, Color.Blue))));
            menu.SubMenu("d").AddItem(new MenuItem("drawHp", "Draw combo damage bar").SetValue(true));
            menu.SubMenu("d").AddItem(new MenuItem("drawings", "Enabled").SetValue(true));

            menu.AddItem(new MenuItem("packetCast", "Packet casting").SetValue(true));
            menu.AddToMainMenu();
        }
    }
}
