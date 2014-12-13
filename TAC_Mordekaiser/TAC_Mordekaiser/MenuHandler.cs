using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using Color = System.Drawing.Color;

namespace TAC_Mordekaiser
{
    class MenuHandler
    {
        internal static Menu Config;
        internal static void loadMe()
        {
            Config = new Menu("TAC Mordekaiser", "tac_mordekaiser",true);

            Menu targetSelector = new Menu("Target selector", "ts");
            SimpleTs.AddToMenu(targetSelector);
            Config.AddSubMenu(targetSelector);
            Menu orbwalker = new Menu("Orbwalker", "orbwalker");
            Program.orb = new Orbwalking.Orbwalker(orbwalker);
            Config.AddSubMenu(orbwalker);

            Config.AddSubMenu(new Menu("Auto Carry", "ac"));

            Config.SubMenu("ac").AddSubMenu(new Menu("Skills","ss"));
            Config.SubMenu("ac").SubMenu("ss").AddItem(new MenuItem("acQ", "Use Q").SetValue(true));
            Config.SubMenu("ac").SubMenu("ss").AddItem(new MenuItem("acW", "Use W").SetValue(true));
            Config.SubMenu("ac").SubMenu("ss").AddItem(new MenuItem("acE", "Use E").SetValue(true));

            Config.SubMenu("ac").AddSubMenu(new Menu("Use R", "useR"));
            Config.SubMenu("ac").SubMenu("useR").AddItem(new MenuItem("acR", "Use R").SetValue(true));
            Config.SubMenu("ac").SubMenu("useR").AddItem(new MenuItem("about0", "---Targets---"));
            foreach (var target in ObjectManager.Get<Obj_AI_Hero>().Where(h => h.IsEnemy))
            {
                Config.SubMenu("ac").SubMenu("useR").AddItem(new MenuItem("no"+target.BaseSkinName, target.BaseSkinName).SetValue(true));
            }
            Config.SubMenu("ac").AddItem(new MenuItem("useIgnite", "Use Ignite").SetValue(true));

            Config.SubMenu("ac").AddSubMenu(new Menu("Items", "item"));
            Config.SubMenu("ac").SubMenu("item").AddItem(new MenuItem("dfg", "Deathfire Grasp").SetValue(true));
            Config.SubMenu("ac").SubMenu("item").AddItem(new MenuItem("hg", "Hextech Gunblade").SetValue(true));

            Config.AddSubMenu(new Menu("Mixed", "mixed"));
            Config.SubMenu("mixed").AddItem(new MenuItem("mxQ", "Use Q").SetValue(true));
            Config.SubMenu("mixed").AddItem(new MenuItem("mxW", "Use W").SetValue(true));
            Config.SubMenu("mixed").AddItem(new MenuItem("mxE", "Use E").SetValue(true));

            Config.AddSubMenu(new Menu("Lane Clear", "lc"));
            Config.SubMenu("lc").AddItem(new MenuItem("lcQ", "Use Q").SetValue(true));
            Config.SubMenu("lc").AddItem(new MenuItem("lcW", "Use W").SetValue(true));
            Config.SubMenu("lc").AddItem(new MenuItem("lcE", "Use E").SetValue(true));
            Config.SubMenu("lc").AddItem(new MenuItem("lcActive", "Enabled").SetValue(false));

            Config.AddSubMenu(new Menu("Misc", "misc"));
            Config.SubMenu("misc").AddItem(new MenuItem("shieldself", "Use shield self").SetValue(true));
            Config.SubMenu("misc").AddItem(new MenuItem("gap", "Anti-Gap").SetValue(true));

            Config.AddSubMenu(new Menu("Drawings", "draw"));
            Config.SubMenu("draw").AddItem(new MenuItem("drawQ", "Draw Q Range").SetValue(new Circle(true, Color.FromArgb(100, Color.Red))));
            Config.SubMenu("draw").AddItem(new MenuItem("drawW", "W Range").SetValue(new Circle(false, Color.FromArgb(100, Color.Coral))));
            Config.SubMenu("draw").AddItem(new MenuItem("drawE", "E Range").SetValue(new Circle(true, Color.FromArgb(100, Color.BlueViolet))));
            Config.SubMenu("draw").AddItem(new MenuItem("drawR", "R Range").SetValue(new Circle(false, Color.FromArgb(100, Color.Blue))));
            Config.SubMenu("draw").AddItem(new MenuItem("drawings", "Enable drawings").SetValue(true));

            Config.SubMenu("draw").AddItem(new MenuItem("drawClone", "Draw Clone Range").SetValue(true));
            Config.SubMenu("draw").AddItem(new MenuItem("drawFC", "Draw Flash+Combo range").SetValue(true));
            Config.SubMenu("draw").AddItem(new MenuItem("drawHp", "Draw combo damage bar").SetValue(true));


            Config.AddItem(new MenuItem("packetCast", "Packet Casting").SetValue(true));


            Config.AddToMainMenu();
        }
    }
}