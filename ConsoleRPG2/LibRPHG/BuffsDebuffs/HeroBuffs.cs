using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibRPHG.AbstractUnits;

namespace LibRPHG.BuffsDebuffs
{
    static class BuffsConsts
    {
        public static int[] KnightArmor = new int[3] { 50, -20, 10 };
        
        public static int BarbarianRageATPplus = 1;
        public static int BarbarianDamagePerHPmiss = 1;

        public static int PaladinAuraDef = 15;
        public static int PaladinAuraMVPplus = 1;
        public static int PaladinAuraRadius = 3;

        public static int DruidAttackBuff = 20;
        public static int DruidSelfHeal = 50;
        public static int DruidManaRegenplus = 5;
        public static int DruidHealthRegenplus = 20;

        public static int SpearmanNextAttackMoreDamagePercent = 200;
        public static int SpearmanSprintMVPplus = 6;
        public static int SpearmanSprintDamagePerMovePoint = 20;

        // ---------------------------------------

        public static int WoodoManaRegenAura = 2;
        public static int WoodoSlowMinimumMVP = 2;
        public static int WoodoSlowStep = 1;
        public static int WoodoDamageMinimum = 20;
        public static int WoodoDamageMinStep = 50;

        public static int ArcherAuraAccplus = 15;
        public static int ArcherRushATPplus = 8;
    }

    public class SpearManNextAttackBuff : Abstractbuff
    {
        int _recievedDamage;
        public override void ApplyFor(Abstraceunit who, Abstraceunit bywho)
        {
            base.ApplyFor(who, bywho);
            _recievedDamage = (int)(_bufftarget.CurrentDamage * BuffsConsts.SpearmanNextAttackMoreDamagePercent / 100.0);
            _bufftarget._att_dmg_mod += _recievedDamage;

        }
        public override void Dissaply()
        {
            _bufftarget._att_dmg_mod -= _recievedDamage;
        }
        public SpearManNextAttackBuff(Abstraceunit who, Abstraceunit bywho)
        {
            _buffname = "mortal blow";
            _turnsleft = 1;
            _recievedDamage = 0;
            ApplyFor(who, bywho);
        }
        public override void Tick()
        {
            //base.Tick();
        }
        public void OnAttackedEvent (){
            _turnsleft--;
        }
        public override string NameFull
        {
            get
            {
                return base.NameFull + String.Format("This unit's next attack will deal {0} more DMG", _recievedDamage);
            }
        }
    }

    public class DruidDamageBuff : Abstractbuff
    {
        public override void ApplyFor(Abstraceunit who, Abstraceunit bywho)
        {
            base.ApplyFor(who, bywho);
            _bufftarget._att_dmg_mod += BuffsConsts.DruidAttackBuff;

        }
        public override void Dissaply()
        {
            _bufftarget._att_dmg_mod -= BuffsConsts.DruidAttackBuff;
        }
        public DruidDamageBuff(Abstraceunit who, Abstraceunit bywho)
        {
            _buffname = "brute pose";
            _turnsleft = 2;
            ApplyFor(who, bywho);
        }
        public override string NameFull
        {
            get
            {
                return base.NameFull + String.Format("This unit has +{0} DMG for a short time", BuffsConsts.DruidAttackBuff);
            }
        }
    }

    public class DruidSplash : Abstractbuff
    {
        public override void ApplyFor(Abstraceunit who, Abstraceunit bywho)
        {
            base.ApplyFor(who, bywho);
            _bufftarget._hp_regen_mod += BuffsConsts.DruidHealthRegenplus;
            _bufftarget._mp_regen_mod += BuffsConsts.DruidManaRegenplus;

        }
        public override void Dissaply()
        {
            _bufftarget._hp_regen_mod -= BuffsConsts.DruidHealthRegenplus;
            _bufftarget._mp_regen_mod -= BuffsConsts.DruidManaRegenplus;
        }
        public DruidSplash(Abstraceunit who, Abstraceunit bywho)
        {
            _buffname = "natures restoration";
            _turnsleft = 5;
            ApplyFor(who, bywho);
        }
        public override string NameFull
        {
            get
            {
                return base.NameFull + String.Format("This unit recieved a {0} HP REGEN and {1} MP REGEN per turn from Druid", BuffsConsts.DruidHealthRegenplus, BuffsConsts.DruidManaRegenplus);
            }
        }
    }

    public class PaladinsAura : Abstractbuff
    {

        public override void ApplyFor(Abstraceunit who, Abstraceunit bywho)
        {
            base.ApplyFor(who, bywho);
            _bufftarget._def_mod += BuffsConsts.PaladinAuraDef;
            _bufftarget._mvp_mod += BuffsConsts.PaladinAuraMVPplus;
        }
        public override void Dissaply()
        {
            _bufftarget._def_mod -= BuffsConsts.PaladinAuraDef;
            _bufftarget._mvp_mod -= BuffsConsts.PaladinAuraMVPplus;
        }
        public PaladinsAura(Abstraceunit who, Abstraceunit bywho)
        {
            _buffname = "holy might";
            _turnsleft = 1;
            ApplyFor(who, bywho);
        }
        public override void Tick()
        {
            if (_bufftarget.DistanceTo(_buffhost, true) > BuffsConsts.PaladinAuraRadius)
                base.Tick();
        }
        public override string NameFull
        {
            get
            {
                return base.NameFull + String.Format("This unit is under Paladin's aura, has +{0} DEF, +{1} MVP", BuffsConsts.PaladinAuraDef, BuffsConsts.PaladinAuraMVPplus);
            }
        }
    }

    public class BarbarianSplash : Abstractbuff
    {
        public override void ApplyFor(Abstraceunit who, Abstraceunit bywho)
        {
            base.ApplyFor(who, bywho);
            _bufftarget._atp_mod += BuffsConsts.BarbarianRageATPplus;
        }
        public override void Dissaply()
        {
            _bufftarget._atp_mod -= BuffsConsts.BarbarianRageATPplus;
        }
        public BarbarianSplash(Abstraceunit who, Abstraceunit bywho)
        {
            _buffname = "fighting rage";
            _turnsleft = 3;
            ApplyFor(who, bywho);
        }
        public override string NameFull
        {
            get
            {
                return base.NameFull + String.Format("This unit has +{0} ATP for a short time", BuffsConsts.BarbarianRageATPplus);
            }
        }
    }

    public class BarbarianPassive : Abstractbuff
    {
        int _currentDamageGiven;

        public override void ApplyFor(Abstraceunit who, Abstraceunit bywho)
        {
            base.ApplyFor(who, bywho);
        }

        public override void Dissaply()
        {
            // never
        }

        public override void Tick()
        {
            // nothing
        }

        public void OnHealthChange()
        {
            int newDamageGiven = (_bufftarget.getMaxHP - _bufftarget.getCurrentHP) * BuffsConsts.BarbarianDamagePerHPmiss;
            _bufftarget._att_dmg_mod += newDamageGiven - _currentDamageGiven;
            _currentDamageGiven = newDamageGiven;
        }
        public BarbarianPassive(Abstraceunit who, Abstraceunit bywho)
        {
            _buffname = "blood rage";
            _turnsleft = 1;
            _currentDamageGiven = 0;
            ApplyFor(who, bywho);
            _bufftarget._att_dmg_mod += 0;
        }
        public override string NameFull
        {
            get
            {
                return base.NameFull + String.Format("This unit has +{0} DMG cause of missing health", _currentDamageGiven);
            }
        }
    }

    public class KnightPassive : Abstractbuff
    {
        int _currentDefenseBuff;
        int _nothit;

        public override void ApplyFor(Abstraceunit who, Abstraceunit bywho)
        {
            base.ApplyFor(who, bywho);
        }

        public override void Dissaply()
        {
            // never
        }

        public override void Tick()
        {
            _nothit++;
            // do nothing
            if (_nothit >= 2 && _currentDefenseBuff < BuffsConsts.KnightArmor[0])
            {
                _currentDefenseBuff += BuffsConsts.KnightArmor[2];
                _bufftarget._def_mod += BuffsConsts.KnightArmor[2];
            }
        }

        public void GetHit()
        {
            if (_currentDefenseBuff > BuffsConsts.KnightArmor[1])
            {
                _currentDefenseBuff -= BuffsConsts.KnightArmor[2];
                _bufftarget._def_mod -= BuffsConsts.KnightArmor[2];
            }
            _nothit = 0;
        }
        public KnightPassive(Abstraceunit who, Abstraceunit bywho)
        {
            _buffname = "sturdy armor";
            _turnsleft = 1;
            _nothit = 0;
            _currentDefenseBuff = BuffsConsts.KnightArmor[0];
            ApplyFor(who, bywho);
            _bufftarget._def_mod += _currentDefenseBuff;
        }

        public override string NameFull
        {
            get
            {
                return base.NameFull + String.Format("This unit has +{1} DEF (up to +{0}), but each taken hit remove {2} DEF down to {3} DEF\n", BuffsConsts.KnightArmor[0], _currentDefenseBuff, BuffsConsts.KnightArmor[2], BuffsConsts.KnightArmor[1]);
            }
        }
        
    }
}
