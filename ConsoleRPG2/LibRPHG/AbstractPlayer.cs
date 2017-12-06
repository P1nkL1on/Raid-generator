using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

using MatrixFields;

namespace LibRPHG
{

    public abstract class AbstractPlayer : AbstractUnit
    {
        protected int _expirience;
        protected float _exp_mod;


        public void LevelUp()
        {
            LOGS.Add(String.Format("{0} archieved {1} level", NameFull, Level));
            _hp = _hp_max;
            _mp = _mp_max;
            _exp_mod = Math.Max(_exp_mod - .1f, 0.0f);

            // random stat gain
            // ...
        }
        public void RecieveExp(int exp)
        {

            _expirience += (int)(exp * _exp_mod);
            LOGS.Add(String.Format("{0} gain {1} exp.", Name, (int)(exp * _exp_mod)));
            int _level_prev = _level;
            _level = _expirience / 100 + 1;
            if (_level != _level_prev)
                LevelUp();
        }
        public abstract string Prof { get; }

        public override string NameFull { get { return String.Format("{0}, the {1} ( lvl.{2} )", Name, Prof, Level); } }

        public void SetDefault()
        {
            _exp_mod = 1;
            _expirience = 0;
        }

        public override void TraceStats()
        {
            base.TraceStats();
            Console.WriteLine(String.Format("\t({0}/{1} exp.)", _expirience, _level * 100));
        }

        public override void DamageFor(float x)
        {
            LOGS.Add(String.Format("{0} was damaged for {1} ( -> {2} / {3} )", Name, x, _hp, _hp_max));
        }

        public override void HealFor(float x)
        {
            LOGS.Add(String.Format("{0} was healed for {1} health ( -> {2} / {3} )", Name, x, _hp, _hp_max));
        }
    }


    public abstract class AbstractUnit : IUnit
    {
        Thread mainThread;

        protected Point _pos;

        public int TeamID;
        protected bool isDead;
        protected int _level;
        protected string _name;
        protected float _hp;
        protected float _mp;
        protected float _hp_regen;
        protected float _mp_regen;
        protected float _hp_max;
        protected float _mp_max;
        protected int _att_spd;
        protected int _move_spd;
        protected int _att_dist;
        protected float _att_dmg;

        // innver buffs
        protected float _income_damage_mod;
        protected float _income_heal_mod;
        protected float _dmg_mod;
        protected int _att_spd_mod;
        protected int _move_spd_mod;
        protected float _hp_regen_mod;
        protected float _mp_regen_mod;

        public virtual void TraceStats()
        {
            Console.WriteLine(String.Format("\t{0}\nHP:{1}/{2}\t++{3} per second\nMP:{4}/{5}\t++{6} per second",
                                            NameFull, _hp, _hp_max, _hp_regen * _hp_regen_mod, _mp, _mp_max, _mp_regen * _mp_regen_mod));
        }

        public void TraceMoreStats()
        {
            TraceStats();
            Console.WriteLine(String.Format("\nDMG: {0}  x{1} per second\nATTDST: {2}\nDEF: {3}%\nMOVSPD: {4} per second",
                _att_dmg * _dmg_mod, Math.Round(100.0 / (_att_spd * _att_spd_mod)) / 100.0, _att_dist, (100 - 100 * _income_damage_mod), _move_spd * _move_spd_mod));
        }

        public void SetDefault(string nam, Point location, int spd, int att_spd, float dmg, int dist, float hpmax, float mpmax)
        {
            TeamID = -1;
            _pos = location;
            _name = nam; _att_dmg = dmg; _move_spd = spd;
            _att_spd = att_spd; _att_dist = dist;
            _hp_max = hpmax; _mp_max = mpmax;
            isDead = false; _level = 1;
            _hp = _hp_max; _mp = _mp_max;
            _hp_regen = _mp_regen = .0f;   // per second

            _income_damage_mod = _income_heal_mod = _dmg_mod = _hp_regen_mod = _mp_regen_mod = 1;
            _att_spd_mod = _move_spd_mod = 0;
        }

        public int Level { get { return _level; } }
        public string Name { get { return _name; } }
        public abstract string NameFull { get; }
        public float Health
        {
            get { return _hp; }
            set
            {
                float _hpprev = _hp;
                _hp = Math.Min(Math.Max(0f, value), _hp_max);
                float _hpdif = _hp - _hpprev, _hpabs = Math.Abs(_hpdif);
                if (_hpdif > 0)
                    HealFor(_hpabs);
                if (_hpdif < 0)
                    DamageFor(_hpabs);
                if (_hp <= 0)
                {
                    isDead = true;
                    Die();
                }
            }
        }
        public float Mana
        {
            get { return _mp; }
            set
            {
                _mp = Math.Min(Math.Max(0f, value), _mp_max);
            }
        }
        public Point GetPosition { get { return _pos; } }

        public abstract void DamageFor(float x);

        public void Die()
        {
            LOGS.Add(String.Format("{0} died.", NameFull));
        }

        public abstract void HealFor(float x);

        bool IUnit.isDead()
        {
            return isDead;
        }

        public virtual void TraceBars()
        {
            Console.WriteLine(String.Format("\n  {0}:\n  HP:{1}\n  MP:{2}", NameFull, LOGS.TraceBar(_hp, _hp_max), LOGS.TraceBar(_mp, _mp_max)));
        }

        public int GetTeamNumber { get { return TeamID; } }

        public virtual List<Prio> CalculateSituation(Battlefield bf)
        {
            bool inverse = (GetTeamNumber % 2) == 0;
            List<IUnit> units = bf.getUnits;

            List<Prio> res = new List<Prio>();
            for (int i = 0; i < units.Count; i++)
                if (units[i] != this)
                {
                    if (inverse)
                    {   
                        // victim
                        if (units[i].GetTeamNumber != GetTeamNumber)
                            res.Add(new Prio(Calculator.GetOblast(units[i].GetPosition, units[i].GetSpd * 2 - GetSpd, true, true), -5.0f));
                        else
                            res.Add(new Prio(Calculator.GetOblast(units[i].GetPosition, 2, true, true), 1.0f)); // 1.0f
                    }
                    else
                    {
                        // hunter
                        if (units[i].GetTeamNumber != GetTeamNumber)
                            res.Add(new Prio(Calculator.GetOblast(units[i].GetPosition, units[i].GetSpd * 2 - GetSpd, true, true), 5.0f));
                    }
                }
            return res;
        }
        public virtual int GetSpd {get {return _move_spd + _move_spd_mod;}}

        public void MoveTo(Point where) {
            LOGS.Add(String.Format("#{0} moves from ({1},{2}) to ({3},{4})", Name, _pos.X, _pos.Y, where.X, where.Y));
            _pos = where; 
        }
    }
}