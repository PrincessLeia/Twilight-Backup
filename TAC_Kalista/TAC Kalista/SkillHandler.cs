using LeagueSharp;
using LeagueSharp.Common;
namespace TAC_Kalista
{
    class SkillHandler
    {
        public static Spell Q, W, E, R;
        public static Spell[] SpellList = { Q, W, E, R };
        internal static double[] BaseRendDamage = { 20, 30, 40, 50, 60};
        internal static double[] RendDamageBonusPerSpear = { 10, 14, 19, 25, 32};
        internal static double[] RendDamageBonusPerSpearMultiplier = { 0.2, 0.225, 0.25, 0.275, 0.3 };
        internal static double AttackDamage = ObjectManager.Player.BaseAttackDamage + ObjectManager.Player.FlatPhysicalDamageMod;
        public static void Init()
        {
            Q = new Spell(SpellSlot.Q, 1450);
            W = new Spell(SpellSlot.W, 5500);
            E = new Spell(SpellSlot.E, 1000);
            R = new Spell(SpellSlot.R, 1200);
            Q.SetSkillshot(0.25f, 60f, 2000f, true, SkillshotType.SkillshotLine);
        }
    }
}
