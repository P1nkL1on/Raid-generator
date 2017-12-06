using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using MatrixFields;

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
    }

    public interface IUnit
    {
        int Level { get; }
        string Name { get; }
        string NameFull { get; }
        float Health { get; set; }
        float Mana { get; set; }
        Point GetPosition { get; }
        void MoveTo(Point where);

        int GetSpd { get; }

        void HealFor(float x);
        void DamageFor(float x);
        void Die();
        bool isDead();

        void SetDefault(string nam, Point location, int spd, int att_spd, float dmg, int dist, float hpmax, float mpmax);

        void TraceStats();
        void TraceMoreStats();

        void TraceBars();
        int GetTeamNumber { get; }
        List<Prio> CalculateSituation(Battlefield bf);
    }
}