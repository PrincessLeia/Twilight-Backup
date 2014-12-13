using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using Color = System.Drawing.Color;
namespace TAC_Jax
{
    class MenuHandler
    {
        internal static Menu Config;
        internal static void load()
        {
            Config = new Menu("TAC Jax", "TAC_Jax", true);
            Menu targetSelector = new Menu("Target selector", "ts");
            SimpleTs.AddToMenu(targetSelector);
            Config.AddSubMenu(targetSelector);
            try
            {
                Config.AddSubMenu(new Menu("Orbwalker", "Orbwalker"));
                GameHandler.Orbwalker = new Orbwalking.Orbwalker(Config.SubMenu("Orbwalker"));
            }
            catch(Exception ex)
            {
                Game.PrintChat("Could not load orbwalker! " + ex);
            }

            Config.AddSubMenu(new Menu("Auto Carry", "ac"));
            Config.SubMenu("ac").AddSubMenu(new Menu("Use Q", "q_menu"));
            Config.SubMenu("ac").SubMenu("q_menu").AddItem(new MenuItem("acQ_useIfWorth", "Use F+Q if worth").SetValue(true));
            Config.SubMenu("ac").SubMenu("q_menu").AddItem(new MenuItem("acQ_useIfWorthEnemy", "Maximum enemies in range: ").SetValue(new Slider(2,1,5)));
            Config.SubMenu("ac").SubMenu("q_menu").AddItem(new MenuItem("acQ", "Enabled").SetValue(true));

            Config.SubMenu("ac").AddItem(new MenuItem("acW", "Smart W").SetValue(true));
            Config.SubMenu("ac").AddItem(new MenuItem("acE", "Smart E").SetValue(true));

            Config.AddSubMenu(new Menu("Mixed", "mx"));
            Config.SubMenu("mx").AddItem(new MenuItem("about", "This is automatic"));
            Config.SubMenu("mx").AddItem(new MenuItem("about1", "Hold mixed key and if"));
            Config.SubMenu("mx").AddItem(new MenuItem("about3", "you dont have lvl 6 it will"));
            Config.SubMenu("mx").AddItem(new MenuItem("about4", "use E+W+Q or if you have lvl 6"));
            Config.SubMenu("mx").AddItem(new MenuItem("about5", "then you need 2 auto's and then"));
            Config.SubMenu("mx").AddItem(new MenuItem("about6", "hold mixed button for high burst dmg"));

            Config.AddSubMenu(new Menu("Lane/Jungle clear", "cl"));
            Config.SubMenu("cl").AddItem(new MenuItem("clear_w", "Use W").SetValue(true));
            Config.SubMenu("cl").AddItem(new MenuItem("clear_e", "Use E").SetValue(true));

            Config.AddSubMenu(new Menu("Advanced", "advanced"));
            Config.SubMenu("advanced").AddSubMenu(new Menu("Smart E", "e_menu"));
            Config.SubMenu("advanced").SubMenu("e_menu").AddItem(new MenuItem("interruptE", "Interrupt (E) enemy cast").SetValue(true));
            Config.SubMenu("advanced").SubMenu("e_menu").AddItem(new MenuItem("gapclose_E", "Prevent gap-closing").SetValue(true));
            Config.SubMenu("advanced").SubMenu("e_menu").AddItem(new MenuItem("gapcloseRange_E", "Gap-close range").SetValue(new Slider(250, 200, 400)));
            Config.SubMenu("advanced").AddItem(new MenuItem("smartR", "Smart R").SetValue(true));
            Config.SubMenu("advanced").AddItem(new MenuItem("Ward", "Ward Jump")).SetValue(new KeyBind('T', KeyBindType.Press));
            Config.SubMenu("advanced").AddItem(new MenuItem("packetCast", "Packet Casting").SetValue(true));
            Config.SubMenu("advanced").AddItem(new MenuItem("debug", "Debugging").SetValue(true));

            Config.AddSubMenu(new Menu("Drawings", "Drawings"));
            Config.SubMenu("Drawings").AddItem(new MenuItem("rangeQ", "Q range").SetValue(new Circle(true, Color.FromArgb(100, Color.Red))));
            Config.SubMenu("Drawings").AddItem(new MenuItem("rangeE", "E range").SetValue(new Circle(true, Color.FromArgb(100, Color.BlueViolet))));
            Config.SubMenu("Drawings").AddItem(new MenuItem("drawHp", "Draw combo damage").SetValue(true));
            Config.SubMenu("Drawings").AddItem(new MenuItem("drawWard", "Draw jumping radius").SetValue(true));
            Config.AddToMainMenu();
        }
    }
}
