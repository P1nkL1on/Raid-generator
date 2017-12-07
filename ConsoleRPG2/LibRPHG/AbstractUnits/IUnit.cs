using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using MatrixFields;
using LibRPHG.AbstractUnits;

namespace LibRPHG
{
    public interface Iunit
    {
        int Level { get; }
        string Name { get; }
        string NameFull { get; }
        int getCurrentHP { get; }
        int getMaxHP { get; }
        int getCurrentMP { get; }
        int getMaxMP { get; }
        Point GetPosition { get; }
        void MoveTo(Point where);
        int GetSpd { get; }

        void HealFor(int x);
        void DamageFor(int x);
        void FillManaFor(int x);
        bool SpendManaFor(int x);
        void Die();
        bool isDead { get; }

        void SetDefaultStats(string name, Point location,
            int level, int hpmax, int mpmax,
            int spd, int attpoints, int attdamage, int attdist, int def, int acc);

        string TraceMoveStats();
        string TraceBars();
        int getTeamNumber { get; }
        List<Prio> CalculateSituation(Battlefield bf);

        // buffs
        void AddBuff(Ibuff buff);
        void TickBuffs();
        // trigger spells

        void OnHitRecievedEvent(int damage);
        void OnHealthChangedEvent();
        void OnAttacking(Iunit who);
        void OnKillUnit(Iunit who);
        void OnDie();
        void OnAttacked(Iunit bywho);

    }
}