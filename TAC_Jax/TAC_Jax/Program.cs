using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace TAC_Jax
{
    class Jax
    {
        internal static bool PacketCast = true;
        internal static bool Debug = false;

        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
            Game.PrintChat("[" + Utils.FormatTime(Game.ClockTime) + "] <font color='#00ff00'>System (Kappa):</font> <font color='#FF0000'>[v1.4]</font> <font color='#7A6EFF'>Twilight's Auto Carry: </font> <font color='#86E5E1'>Jax</font>");
            Game.PrintChat("[" + Utils.FormatTime(Game.ClockTime) + "] <font color='#00ff00'>System (Kappa):</font> Thank you for using my assembly!");
        }

        static void Game_OnGameLoad(EventArgs args)
        {
            if (ObjectManager.Player.BaseSkinName != "Jax") return;
            SkillHandler.Load();
            MenuHandler.Load();
            Game.OnGameUpdate += Game_OnGameUpdate;
            Obj_AI_Hero.OnProcessSpellCast += EventHandler.Game_OnProcessSpell;
            AntiGapcloser.OnEnemyGapcloser += EventHandler.AntiGapCloser;
            Interrupter.OnPossibleToInterrupt += EventHandler.OnInterrupt;
            Orbwalking.BeforeAttack += EventHandler.Orbwalking_BeforeAttack;
            Orbwalking.AfterAttack += EventHandler.Orbwalking_AfterAttack;
            Drawing.OnDraw += DrawingHandler.Load;
            Drawing.OnEndScene += DrawingHandler.OnEndScene;
        }

        static void Game_OnGameUpdate(EventArgs args)
        {
            PacketCast = MenuHandler.Config.Item("packetCast").GetValue<bool>();
            Debug = MenuHandler.Config.Item("debug").GetValue<bool>();
            switch (GameHandler.Orbwalker.ActiveMode)
            {
                case Orbwalking.OrbwalkingMode.Combo:
                    EventHandler.OnCombo();
                    break;
                case Orbwalking.OrbwalkingMode.Mixed:
                    EventHandler.OnHarass();
                    break;
                case Orbwalking.OrbwalkingMode.LaneClear:
                    EventHandler.OnLaneClear();
                    break;
                case Orbwalking.OrbwalkingMode.None:
                    if (MenuHandler.Config.Item("Flee").GetValue<bool>())
                    {
                        Orbwalking.Orbwalk(null, Game.CursorPos);
                        EventHandler.WardJump();
                    }
                    break;
            }
            if (MenuHandler.Config.Item("smartR").GetValue<bool>())
                EventHandler.SmartR();

            if (MenuHandler.Config.Item("ks_enabled").GetValue<bool>())
                EventHandler.KillSteal();

            if (MenuHandler.Config.Item("Ward").GetValue<KeyBind>().Active) 
                EventHandler.WardJump();
            GameHandler.UpdateCount();
        }
    }
}
