using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

//JaxRelentlessAssaultAS -- When jax is auto attacking
//JaxEvasion -- When Jax is casting E
//EmpoweredTwo --When Jax presses W
//MasterySpellWeaving -- When Jax is using W on target
//ItemPhageSpeed
// TODO: add items and get theyre damage
namespace TAC_Jax
{
    class EventHandler
    {
        internal static void AntiGapCloser(ActiveGapcloser gapcloser)
        {
            if (ObjectManager.Player.Distance(gapcloser.Sender) < MenuHandler.Config.Item("gapcloseRange_E").GetValue<Slider>().Value && MenuHandler.Config.Item("gapclose_E").GetValue<bool>())
                SkillHandler.E.Cast(Jax.packetCast);
        }
        internal static void onInterrupt(Obj_AI_Base unit, InterruptableSpell spell)
        {
            if (!MenuHandler.Config.Item("interruptE").GetValue<bool>() 
                || ObjectManager.Player.IsDead
                    || !GameHandler.isCastingE || GameHandler.canCastE) return;
            if (GameHandler.canCastE && GameHandler.isCastingE)
            {
                float distance = ObjectManager.Player.Distance(unit);
                if (SkillHandler.Q.IsReady() && distance < SkillHandler.Q.Range)
                    SkillHandler.Q.Cast(Jax.packetCast);
                if(distance < SkillHandler.E.Range)
                    SkillHandler.E.Cast(Jax.packetCast);
            }
        }
        
        internal static void Game_OnProcessSpell(Obj_AI_Base unit, GameObjectProcessSpellCastEventArgs spell)
        {
            // Here i need to add if I need to survive.
            //if (!unit.IsMe) return;
            //Game.PrintChat("spell.SData.Name.ToLower: -- " + spell.SData.Name.ToLower());
            //if(spell.SData.Name.ToLower() == "jaxcounterstrike")
            //{
            //}
            //jaxcounterstrike
            //jaxleapstrike
        }
        internal static bool canDieFromLeaping(Obj_AI_Hero target)
        {
            return target.Health < (ObjectManager.Player.GetSpellDamage(target, SpellSlot.Q) + ObjectManager.Player.GetSpellDamage(target,SpellSlot.W));
        }
        internal static void onHarass()
        {
            Obj_AI_Hero target = SimpleTs.GetTarget(SkillHandler.Q.Range, SimpleTs.DamageType.Physical);
            if(target != null)
            {
                /**
                 * Check if my Q is available
                 * Check if my 3'rd R is available
                 * Check if my W is available
                 */
                if(SkillHandler.Q.IsReady() && SkillHandler.W.IsReady())
                {
                    if (ObjectManager.Player.Level >= 6 && GameHandler.buffCount > 0 && GameHandler.buffCount % 3 == 0)
                    {
                        GameHandler.isCastingE = true;
                        GameHandler.lastTickE = Environment.TickCount;
                        SkillHandler.E.Cast(Jax.packetCast);
                        SkillHandler.Q.Cast(target,Jax.packetCast);
                        Utility.DelayAction.Add(5, () =>
                        {
                            SkillHandler.W.Cast(Jax.packetCast);
                        });
                    }
                    else
                    {
                        GameHandler.isCastingE = true;
                        GameHandler.lastTickE = Environment.TickCount;
                        SkillHandler.E.Cast(Jax.packetCast);
                        SkillHandler.W.Cast(Jax.packetCast);
                        SkillHandler.Q.Cast(Jax.packetCast);
                    }
                }
                if (GameHandler.isCastingE && GameHandler.canCastE)
                {
                    SkillHandler.E.Cast(Jax.packetCast);
                    GameHandler.canCastE = false;
                    GameHandler.isCastingE = false;
                }
            }
        }
        internal static void onCombo()
        {
            // TODO: Make E cast when in range
            // make better w logic
            // need more tests on E out of range

            // TODO: Make mixed damage in TS and orbwalker
            Obj_AI_Hero target = SimpleTs.GetTarget(SkillHandler.Q.Range,SimpleTs.DamageType.Physical);

            if(target != null)
            {
                if(SkillHandler.E.IsReady() && SkillHandler.Q.IsReady() && ObjectManager.Player.Distance(target) < SkillHandler.Q.Range)
                {
                    GameHandler.lastTickE = Environment.TickCount;
                    GameHandler.isCastingE = true;
                    SkillHandler.E.Cast(Jax.packetCast);
                    GameHandler.buffCountBeforeQ = GameHandler.buffCount;
                    SkillHandler.Q.Cast(target, Jax.packetCast);
                }
                if (ObjectManager.Player.Distance(target) < SkillHandler.E.Range && !GameHandler.isCastingE && SkillHandler.E.IsReady())
                {
                    GameHandler.lastTickE = Environment.TickCount;
                    GameHandler.isCastingE = true;
                    GameHandler.canCastE = false;
                }
                if (GameHandler.isCastingE && GameHandler.canCastE && ObjectManager.Player.Distance(target) < (SkillHandler.E.Range - 30f))
                {
                    SkillHandler.E.Cast(Jax.packetCast);
                    GameHandler.canCastE = false;
                    GameHandler.isCastingE = false;
                }


                // if we are out of distance we need to cast our Q to get near him.
                // if he is not valid target in our range we have to cast q to get near him.
                if(!target.IsValidTarget(SkillHandler.E.Range))
                {
                    GameHandler.buffCountBeforeQ = GameHandler.buffCount;
                    SkillHandler.Q.Cast(target,Jax.packetCast);
                }

                /* Check if player Health is below W damage,
                 * so then we can use it without auto-attacking
                 * or else it's useless to waste W + auto-attack,
                 * since W is a Auto-Attack reset.
                 * Also check if I just used Q and auto-attacked 
                 * for biggest damage possible
                 */
                if ((target.Health < ObjectManager.Player.GetSpellDamage(target,SpellSlot.W))
                    || (!ObjectManager.Player.IsDashing() && SkillHandler.W.IsReady() && GameHandler.buffCountBeforeQ != GameHandler.buffCount))
                {
                    SkillHandler.W.Cast(Jax.packetCast);
                }
                if (canDieFromLeaping(target) && SkillHandler.Q.IsReady() && SkillHandler.W.IsReady())
                {
                    SkillHandler.W.Cast(Jax.packetCast);
                    SkillHandler.Q.Cast(target, Jax.packetCast);
                }
                // Check if our flash and q is ready 
                if(SkillHandler.Q.IsReady() && SkillHandler.Flash.IsReady())
                {
                    // Check if it is actually worth the flash
                    // Only Flash Q if target is going to die, or else it's not worth
                    // TODO: Do another checkup on priority stuff
                    if (MenuHandler.Config.Item("acQ_useIfWorth").GetValue<bool>()
                            && target.ChampionsKilled > target.Deaths
                                && target.CountEnemysInRange((int)(SkillHandler.Q.Range + SkillHandler.Flash.Range)) < MenuHandler.Config.Item("acQ_useIfWorthEnemy").GetValue<Slider>().Value)
                    {
                        SkillHandler.Flash.Cast(target.Position,Jax.packetCast);
                        Utility.DelayAction.Add(10, () =>
                        {
                            GameHandler.buffCountBeforeQ = GameHandler.buffCount;
                            // Since flash is packet casting, we need to delay it for atleast 0.01 second
                            SkillHandler.Q.Cast(target.Position, Jax.packetCast);
                        });
                    }
                }
            }
        }
        internal static void Orbwalking_BeforeAttack(Orbwalking.BeforeAttackEventArgs args)
        {
            var dBuffBro = ObjectManager.Player.Buffs.FirstOrDefault(b => b.DisplayName == "JaxRelentlessAssaultAS").Count;
            if (dBuffBro > 0)
            {
                GameHandler.buffCount++;
                if (GameHandler.buffCount != dBuffBro && dBuffBro < 6 && dBuffBro > 1)
                    GameHandler.buffCount = dBuffBro;
                GameHandler.lastTick = Environment.TickCount;
                if (Jax.debug)
                    Game.PrintChat("(" + GameHandler.buffCount + ") Buff: JaxRelentlessAssaultAS Count: " + dBuffBro);
                GameHandler.hasResetBuffCount = false;
            }
        }
        internal static void onLaneClear()
        {
            foreach (var minions in ObjectManager.Get<Obj_AI_Minion>().Where(minion => minion.IsValidTarget(SkillHandler.Q.Range)).OrderBy(minion => minion.Health))
            {
                if (MenuHandler.Config.Item("clear_e").GetValue<bool>() && SkillHandler.E.IsReady() && minions.IsValidTarget(SkillHandler.E.Range) && !GameHandler.isCastingE)
                {
                    GameHandler.isCastingE = true;
                    GameHandler.lastTickE = Environment.TickCount;
                    GameHandler.canCastE = false;
                    SkillHandler.E.Cast(Jax.packetCast);
                }
                if (MenuHandler.Config.Item("clear_w").GetValue<bool>() && SkillHandler.W.IsReady())
                {
                    SkillHandler.W.Cast(Jax.packetCast);
                }
            }
        }

        /**
         * @author xSalice
         */

        internal static void WardJump()
        {
            foreach (Obj_AI_Minion ward in ObjectManager.Get<Obj_AI_Minion>().Where(ward =>
                ward.Name.ToLower().Contains("ward") && ward.Distance(Game.CursorPos) < 250))
            {
                if (SkillHandler.Q.IsReady())
                {
                    SkillHandler.Q.CastOnUnit(ward,Jax.packetCast);
                    return;
                }
            }

            foreach (
                Obj_AI_Hero hero in ObjectManager.Get<Obj_AI_Hero>().Where(hero => hero.Distance(Game.CursorPos) < 250 && !hero.IsDead))
            {
                if (SkillHandler.Q.IsReady())
                {
                    SkillHandler.Q.CastOnUnit(hero, Jax.packetCast);
                    return;
                }
            }

            foreach (Obj_AI_Minion minion in ObjectManager.Get<Obj_AI_Minion>().Where(minion =>
                minion.Distance(Game.CursorPos) < 250))
            {
                if (SkillHandler.Q.IsReady())
                {
                    SkillHandler.Q.CastOnUnit(minion, Jax.packetCast);
                    return;
                }
            }

            if (Environment.TickCount <= GameHandler.lastPlaced + 3000 || !SkillHandler.E.IsReady()) return;

            Vector3 cursorPos = Game.CursorPos;
            Vector3 myPos = ObjectManager.Player.Position;

            Vector3 delta = cursorPos - myPos;
            delta.Normalize();

            Vector3 wardPosition = myPos + delta * (600 - 5);

            InventorySlot invSlot = FindBestWardItem();
            if (invSlot == null) return;

            Items.UseItem((int)invSlot.Id, wardPosition);
            GameHandler.lastWardPos = wardPosition;
            GameHandler.lastPlaced = Environment.TickCount;
        }

        private static InventorySlot FindBestWardItem()
        {
            InventorySlot slot = Items.GetWardSlot();
            if (slot == default(InventorySlot)) return null;
            return slot;
        }
    }
}
