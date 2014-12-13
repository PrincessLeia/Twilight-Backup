using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;

namespace TAC_Kalista
{
    class MathHandler
    {
        public static float GetDamageToTarget(Obj_AI_Base target)
        {
            double damage = 0;
            if (SkillHandler.Q.IsReady() && ObjectManager.Player.Distance(target) < SkillHandler.Q.Range)
                damage += ObjectManager.Player.GetSpellDamage(target, SpellSlot.Q);
            if (SkillHandler.E.IsReady() && ObjectManager.Player.Distance(target) < SkillHandler.E.Range)
                damage += GetRealDamage(target);
            return (float)damage;
        }

        internal static int CheckBuff(Obj_AI_Base target)
        {
            var buff = target.Buffs.FirstOrDefault(b => b.DisplayName == "KalistaExpungeMarker");
            return buff != null ? buff.Count : 0;
        }
        internal static void CastMinionE(Obj_AI_Base target)
        {
            if (ObjectManager.Get<Obj_AI_Hero>().Any(
                        hero => hero.IsValidTarget(SkillHandler.E.Range)
                            &&
                                CheckBuff(hero) >= 1
                            ))
            {
                List<Obj_AI_Base> minions = MinionManager.GetMinions(ObjectManager.Player.Position, SkillHandler.E.Range,MinionTypes.All,MinionTeam.Enemy,MinionOrderTypes.Health);
                foreach (var minion in minions)
                {
                    if (MathHandler.GetRealDamage(minion) > minion.Health)
                    {
                        SkillHandler.E.Cast(Kalista.PacketCast);
                        break;
                    }
                }
            }        
        }

        public static float GetPlayerHealthPercentage()
        {
            return ObjectManager.Player.Health * 100 / ObjectManager.Player.MaxHealth;
        }
        public static float GetPlayerManaPercentage()
        {
            return ObjectManager.Player.Mana * 100 / ObjectManager.Player.MaxMana;
        }
        #region Hellsing E calculation
        internal static Obj_AI_Hero Player = ObjectManager.Player;
        public static double GetRealDamage(Obj_AI_Base target, int customStacks = -1)
        {
            return Player.CalcDamage(target, Damage.DamageType.Physical, GetRawRendDamage(target, customStacks));
        }
        internal static double GetRawRendDamage(Obj_AI_Base target, int customStacks = -1)
        {
            return SkillHandler.BaseRendDamage[SkillHandler.E.Level - 1] + 0.6*SkillHandler.AttackDamage
                   +
                   (CheckBuff(target) - 1)*
                   (SkillHandler.RendDamageBonusPerSpear[SkillHandler.E.Level - 1] +
                    SkillHandler.RendDamageBonusPerSpearMultiplier[SkillHandler.E.Level - 1]*SkillHandler.AttackDamage);
        }
        #endregion
    }
}
