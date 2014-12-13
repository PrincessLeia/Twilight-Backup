using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using Color = System.Drawing.Color;

namespace TAC_Kalista
{
    class MenuHandler
    {
        public static Menu Config;
        internal static Orbwalking.Orbwalker Orb;
        public static void Init()
        {
            Config = new Menu("Twilight Kalista Rework", "Kalista", true);

            var targetselectormenu = new Menu("Target Selector", "Common_TargetSelector");
            SimpleTs.AddToMenu(targetselectormenu);
            Config.AddSubMenu(targetselectormenu);

            Menu orbwalker = new Menu("Orbwalker", "orbwalker");
            Orb = new Orbwalking.Orbwalker(orbwalker);
            Config.AddSubMenu(orbwalker);

            Config.AddSubMenu(new Menu("AutoCarry options", "ac"));
            
            Config.SubMenu("ac").AddSubMenu(new Menu("Skills","skillUsage"));
            Config.SubMenu("ac").SubMenu("skillUsage").AddItem(new MenuItem("UseQAC", "Use Q").SetValue(true));
            Config.SubMenu("ac").SubMenu("skillUsage").AddItem(new MenuItem("UseEAC", "Use E").SetValue(true));
            
            Config.SubMenu("ac").AddSubMenu(new Menu("Skill settings","skillConfiguration"));
            Config.SubMenu("ac").SubMenu("skillConfiguration").AddItem(new MenuItem("UseQACM", "Use Q when range is ").SetValue(new StringList(new[] { "Far", "Medium", "Close" }, 2)));
            Config.SubMenu("ac").SubMenu("skillConfiguration").AddItem(new MenuItem("E4K", "Use E to kill").SetValue(true));
            Config.SubMenu("ac").SubMenu("skillConfiguration").AddItem(new MenuItem("UseEACSlow", "Use E to slow target").SetValue(false));
            Config.SubMenu("ac").SubMenu("skillConfiguration").AddItem(new MenuItem("UseEACSlowT", "Slow target if enemy <=").SetValue(new Slider(1, 1, 5)));
            Config.SubMenu("ac").SubMenu("skillConfiguration").AddItem(new MenuItem("minE", "Use E at X stacks").SetValue(new Slider(1, 1, 20)));
            Config.SubMenu("ac").SubMenu("skillConfiguration").AddItem(new MenuItem("minEE", "Enable E at X stacks").SetValue(false));
            Config.SubMenu("ac").SubMenu("skillConfiguration").AddItem(new MenuItem("stickToTarget", "Stick to target with minions").SetValue(true));
            
            Config.SubMenu("ac").AddSubMenu(new Menu("Item settings","itemsAC"));
            Config.SubMenu("ac").SubMenu("itemsAC").AddItem(new MenuItem("useItems", "Use Items").SetValue(new KeyBind("G".ToCharArray()[0], KeyBindType.Toggle)));

            Config.SubMenu("ac").SubMenu("itemsAC").AddItem(new MenuItem("allIn", "All in mode").SetValue(new KeyBind("U".ToCharArray()[0], KeyBindType.Toggle)));
//            Config.SubMenu("ac").SubMenu("itemsAC").AddItem(new MenuItem("allInAt", "Auto All in when X hero").SetValue(new Slider(2, 1, 5)));
            
            Config.SubMenu("ac").SubMenu("itemsAC").AddItem(new MenuItem("BOTRK", "Use BOTRK").SetValue(true));
            Config.SubMenu("ac").SubMenu("itemsAC").AddItem(new MenuItem("GHOSTBLADE", "Use Ghostblade").SetValue(true));
            Config.SubMenu("ac").SubMenu("itemsAC").AddItem(new MenuItem("SWORD", "Use SOTD").SetValue(true));

            Config.SubMenu("ac").SubMenu("itemsAC").AddSubMenu(new Menu("QSS settings", "QSS"));
            Config.SubMenu("ac").SubMenu("itemsAC").SubMenu("QSS").AddItem(new MenuItem("AnyStun", "Any Stun").SetValue(true));
            Config.SubMenu("ac").SubMenu("itemsAC").SubMenu("QSS").AddItem(new MenuItem("AnySnare", "Any Snare").SetValue(true));
            Config.SubMenu("ac").SubMenu("itemsAC").SubMenu("QSS").AddItem(new MenuItem("AnyTaunt", "Any Taunt").SetValue(true));
            foreach (var t in ItemHandler.BuffList)
            {
                foreach (var enemy in ObjectManager.Get<Obj_AI_Hero>().Where(enemy => enemy.IsEnemy))
                {
                    if (t.ChampionName == enemy.ChampionName)
                        Config.SubMenu("ac").SubMenu("itemsAC").SubMenu("QSS").AddItem(new MenuItem(t.BuffName, t.DisplayName).SetValue(t.DefaultValue));
                }
            }

            Config.AddSubMenu(new Menu("Misc", "misc"));
            Config.SubMenu("misc").AddItem(new MenuItem("saveSould", "Save soulbound").SetValue(true));
            Config.SubMenu("misc").AddItem(new MenuItem("soulHP", "Save soul at HP%").SetValue(new Slider(15,1,100)));
            Config.SubMenu("misc").AddItem(new MenuItem("soulEnemyCount", "and at X enemy around").SetValue(new Slider(3, 1, 5)));
            Config.SubMenu("misc").AddItem(new MenuItem("antiGap", "Anti-Gap Closer").SetValue(false));
            Config.SubMenu("misc").AddItem(new MenuItem("antiGapRange", "Gap close range").SetValue(new Slider(300, 300, 400)));
            Config.SubMenu("misc").AddItem(new MenuItem("antiGapPrevent", "Prevent Anti-Gap in Combo mode").SetValue(true));

            Config.AddSubMenu(new Menu("Harass settings", "harass"));
            Config.SubMenu("harass").AddItem(new MenuItem("harassQ", "Use Q").SetValue(true));
            Config.SubMenu("harass").AddItem(new MenuItem("stackE", "Use E at").SetValue(new Slider(1, 1, 10)));
            Config.SubMenu("harass").AddItem(new MenuItem("manaPercent", "Mana %").SetValue(new Slider(40, 1, 100)));

            Config.AddSubMenu(new Menu("Wave Clear settings", "wc"));
            Config.SubMenu("wc").AddItem(new MenuItem("wcQ", "Use Q").SetValue(true));
            Config.SubMenu("wc").AddItem(new MenuItem("wcE", "Use E").SetValue(true));
            Config.SubMenu("wc").AddItem(new MenuItem("enableClear", "WaveClear enabled?").SetValue(false));
            
            Config.AddSubMenu(new Menu("Smite settings", "smite"));
            Config.SubMenu("smite").AddItem(new MenuItem("SRU_Baron", "Baron Enabled").SetValue(true));
            Config.SubMenu("smite").AddItem(new MenuItem("SRU_Dragon", "Dragon Enabled").SetValue(true));
            Config.SubMenu("smite").AddItem(new MenuItem("SRU_Gromp", "Gromp Enabled").SetValue(false));
            Config.SubMenu("smite").AddItem(new MenuItem("SRU_Murkwolf", "Murkwolf Enabled").SetValue(false));
            Config.SubMenu("smite").AddItem(new MenuItem("SRU_Krug", "Krug Enabled").SetValue(false));
            Config.SubMenu("smite").AddItem(new MenuItem("SRU_Razorbeak", "Razorbeak Enabled").SetValue(false));
            Config.SubMenu("smite").AddItem(new MenuItem("Sru_Crab", "Crab Enabled").SetValue(false));
            Config.SubMenu("smite").AddItem(new MenuItem("smite", "Auto-Smite enabled").SetValue(new KeyBind("G".ToCharArray()[0], KeyBindType.Toggle)));

            Config.AddSubMenu(new Menu("Wall Hop settings", "wh"));
            Config.SubMenu("wh").AddItem(new MenuItem("JumpTo", "Jump key (HOLD)").SetValue(new KeyBind("T".ToCharArray()[0], KeyBindType.Press)));

            Config.AddSubMenu(new Menu("Drawings", "Drawings"));
            Config.SubMenu("Drawings").AddSubMenu(new Menu("Ranges", "range"));

            Config.SubMenu("Drawings").SubMenu("range").AddItem(new MenuItem("QRange", "Q range").SetValue(new Circle(true, Color.FromArgb(100, Color.Red))));
            Config.SubMenu("Drawings").SubMenu("range").AddItem(new MenuItem("WRange", "W range").SetValue(new Circle(false, Color.FromArgb(100, Color.Coral))));
            Config.SubMenu("Drawings").SubMenu("range").AddItem(new MenuItem("ERange", "E range").SetValue(new Circle(true, Color.FromArgb(100, Color.BlueViolet))));
            Config.SubMenu("Drawings").SubMenu("range").AddItem(new MenuItem("drawESlow", "E slow range").SetValue(true));
            Config.SubMenu("Drawings").SubMenu("range").AddItem(new MenuItem("RRange", "R range").SetValue(new Circle(false, Color.FromArgb(100, Color.Blue))));
            Config.SubMenu("Drawings").AddItem(new MenuItem("drawHp", "Draw damage on HP bar")).SetValue(true);
            Config.SubMenu("Drawings").AddItem(new MenuItem("drawStacks", "Draw total stacks")).SetValue(true);
            Config.SubMenu("Drawings").AddItem(new MenuItem("enableDrawings", "Enable all drawings").SetValue(true));          

            Config.AddItem(new MenuItem("Packets", "Packet Casting").SetValue(true));

            Config.AddItem(new MenuItem("debug", "Debug").SetValue(false));
            
            Config.AddToMainMenu();

        }
    }
}
