using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace LibRPHG.PlayerClasses
{
    public class PKnight : Abstractplayer
    {
        public PKnight(Point startPoint)
        {
            base.SetDefaultStats(Namegen.GenerateRandomName(), startPoint,
                1, 120, 40, 4, 1, 80, 1, 45, -1);
            base.SetDefault();
            AddBuff(new LibRPHG.BuffsDebuffs.KnightPassive(this, this));
        }
        public override string Prof { get { return "Knight"; } }

        public override void OnHitRecievedEvent(int damage)
        {
            base.OnHitRecievedEvent(damage);

            foreach (BuffsDebuffs.KnightPassive kp in this.GetBuffs("sturdy armor"))
                kp.GetHit();
        }
    }
    public class PBarbarian : Abstractplayer
    {
        public PBarbarian(Point startPoint)
        {
            base.SetDefaultStats(Namegen.GenerateRandomName(), startPoint,
                1, 100, 20, 5, 2, 60, 1, 35, -1);
            base.SetDefault();
            AddBuff(new LibRPHG.BuffsDebuffs.BarbarianPassive(this, this));
        }
        public override string Prof { get { return "Barbarian"; } }

        public override void OnHealthChangedEvent()
        {
            base.OnHealthChangedEvent();

            foreach (BuffsDebuffs.BarbarianPassive bp in this.GetBuffs("blood rage"))
                bp.OnHealthChange();
        }

        public override void AfterAttacked(Iunit bywho)
        {
            base.AfterAttacked(bywho);
            if (this.DistanceTo((Abstraceunit)bywho, false) <= 1)
            {
                

                int dmg_buff = -(_att_dmg + _att_dmg_mod) / 2;
                _att_dmg_mod += dmg_buff;
                LOGS.Add(String.Format("{0} counterattacked {1} for {2} DMG!", this.NameFull, bywho.NameFull, CurrentDamage));
                if (CurrentDamage > 10 && !bywho.isDead)
                    this.Attack((Abstraceunit)bywho);
                _att_dmg -= dmg_buff;
            }
        }
    }

    public class PPaladin : Abstractplayer
    {
        public PPaladin(Point startPoint)
        {
            base.SetDefaultStats(Namegen.GenerateRandomName(), startPoint,
                1, 160, 65, 3, 1, 75, 1, 40, -1);
            base.SetDefault();
        }
        public override string Prof { get { return "Paladin"; } }
    }

    public class PDruid : Abstractplayer
    {
        public PDruid(Point startPoint)
        {
            base.SetDefaultStats(Namegen.GenerateRandomName(), startPoint,
                1, 180, 80, 3, 2, 40, 1, 30, -1);
            base.SetDefault();
        }
        public override string Prof { get { return "Druid"; } }

        public void PartyRegen(Battlefield bf)
        {
            if (!this.SpendManaFor(40))
                return;
            LOGS.Add(String.Format("{0} uses ACTIVE ABILITY \"Party HP MP regen\"", NameFull));
            foreach (Abstraceunit ab in bf.getUnitsInObl(this.GetPosition, 4, false, this.getTeamNumber))
                ab.AddBuff(new LibRPHG.BuffsDebuffs.DruidSplash(ab, this));
        }

        public void SelfHeal()
        {
            if (!this.SpendManaFor(10))
                return;
            LOGS.Add(String.Format("{0} uses ACTIVE ABILITY \"Self heal + DMG buff\"", NameFull));
            this.HealFor(LibRPHG.BuffsDebuffs.BuffsConsts.DruidSelfHeal);
            this.AddBuff(new LibRPHG.BuffsDebuffs.DruidDamageBuff(this, this));
        }
    }

    public class PSpearman : Abstractplayer
    {
        public PSpearman(Point startPoint)
        {
            base.SetDefaultStats(Namegen.GenerateRandomName(), startPoint,
                1, 140, 25, 4, 1, 100, 2, 25, 40);
            base.SetDefault();
        }
        public override string Prof { get { return "Spearman"; } }
    }

    public class PDeathKnight : Abstractplayer
    {
        public PDeathKnight(Point startPoint)
        {
            base.SetDefaultStats(Namegen.GenerateRandomName(), startPoint,
                1, 160, 20, 3, 1, 160, 1, 40, -1);
            base.SetDefault();
        }
        public override string Prof { get { return "Death knight"; } }
    }

    public class PBrawler : Abstractplayer
    {
        public PBrawler(Point startPoint)
        {
            base.SetDefaultStats(Namegen.GenerateRandomName(), startPoint,
                1, 100, 60, 5, 2, 35, 1, 20, -1);
            base.SetDefault();
        }
        public override string Prof { get { return "Brawler"; } }
    }

    public class PGuard : Abstractplayer
    {
        public PGuard(Point startPoint)
        {
            base.SetDefaultStats(Namegen.GenerateRandomName(), startPoint,
                1, 125, 70, 4, 1, 60, 1, 60, -1);
            base.SetDefault();
        }
        public override string Prof { get { return "Guard"; } }
    }
}
