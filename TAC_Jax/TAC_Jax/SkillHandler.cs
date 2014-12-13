using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;

namespace TAC_Jax
{
    class SkillHandler
    {
        internal static Spell Q, W, E, R, Flash, Ignite;
        
        internal static void load()
        {
            Q = new Spell(SpellSlot.Q, 700f);
            W = new Spell(SpellSlot.W);
            E = new Spell(SpellSlot.E, 187.5f);
            R = new Spell(SpellSlot.R);
            Q.SetTargetted(0.50f, 75f);
            Flash = new Spell(ObjectManager.Player.GetSpellSlot("SummonerFlash"),500f);
            Ignite = new Spell(ObjectManager.Player.GetSpellSlot("SummonerDot"), 600f);
        }
    }
}
