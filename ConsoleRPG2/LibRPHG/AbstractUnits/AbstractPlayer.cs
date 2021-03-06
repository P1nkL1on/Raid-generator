﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

using MatrixFields;
using LibRPHG.AbstractUnits;

namespace LibRPHG
{

    public abstract class Abstractplayer : Abstraceunit
    {
        protected int _expirience;
        protected float _exp_mod;
        public void LevelUp()
        {
            LOGS.Add(String.Format("{0} archieved {1} level", NameFull, Level));
            _hp = _hpmax;
            _mp = _mpmax;
            _exp_mod = Math.Max(_exp_mod - .25f, 0.0f);

            // random stat gain
            // ...
        }
        public void RecieveExp(int exp)
        {
            _expirience += Math.Max(1, (int)(exp * _exp_mod));
            LOGS.Add(String.Format("{0} gain {1} exp", NameFull, (int)(exp * _exp_mod)));
            int _level_prev = _level;
            _level = Math.Min(5, _expirience / 100 + 1);
            if (_level != _level_prev)
                LevelUp();
        }
        public abstract string Prof { get; }

        public override string NameFull { get { return String.Format("{2}\"{0}, the {1}\"", _name, Prof, NameID); } }

        public void SetDefault()
        {
            _exp_mod = 1.0f;
            _expirience = 0;
        }

    }


    public abstract class Abstraceunit : Iunit
    {
        public int TeamNumber;

        protected Point _pos;
        protected bool _isDead;
        protected int _level;
        protected string _name;

        protected int _hpmax;
        protected int _hp;
        protected int _mpmax;
        protected int _mp;
        protected int _hp_shield;

        protected int _hp_regen_per_turn;
        protected int _mp_regen_per_turn;

        protected int _mvp;
        protected int _mvpmax;

        protected int _atp;
        protected int _atpmax;
        protected int _att_dmg;
        protected int _att_dist;

        protected int _def;
        protected int _acc;

        public int _atp_mod;
        public int _mvp_mod;
        public int _def_mod;
        public int _acc_mod;
        public int _att_dmg_mod;
        public int _hp_regen_mod;
        public int _mp_regen_mod;

        protected List<Abstractbuff> buffs;
        public string description;

        protected List<Abstractbuff> GetBuffs(string name)
        {
            List<Abstractbuff> res = new List<Abstractbuff>();
            for (int i = 0; i < buffs.Count; i++)
                if ((buffs[i].Name == name))
                    res.Add(buffs[i]);
            return res;
        }

        public virtual void SetDefaultStats(string name, Point location, int level, int hpmax, int mpmax,
            int spd, int attpoints, int attdamage, int attdist, int def, int acc)
        {
            _unitID = ++BFConst.currentUnitID;
            _isDead = false; _hp_shield = _hp_regen_mod = _hp_regen_per_turn = _mp_regen_per_turn = _mp_regen_mod = 0;
            _atp_mod = _mvp_mod = _def_mod = _acc_mod = _att_dmg_mod = _hp_regen_mod = _mp_regen_mod = 0;
            description = "Abstract unit as itself.";
            buffs = new List<Abstractbuff>();

            _level = level;
            _name = name;
            _pos = location;
            _hpmax = _hp = hpmax;
            _mpmax = _mp = mpmax;
            _mvpmax = _mvp = spd;
            _atpmax = _atp = attpoints;
            _att_dmg = attdamage;
            _att_dist = attdist;
            _def = def;
            _acc = acc;

            // 
            _hp_regen_per_turn = 1;
            // automaticly enemy
            TeamNumber = 0;
        }
        protected string StrPlus(int X, int Y)
        {
            return (Y > 0) ? String.Format("({0}+{1})", X, Y) : X + "";
        }

        public virtual string TraceMoveStats()
        {
            string s = String.Format("\n{0} (lvl.{1})\n{8}\nHP: {15}  {2} / {3}\t +{4}\nMP: {16}  {5} / {6}\t +{7}\nATT: {9}x{10} at range {11}\nMV: {12}\tDEF: {13}\tACC: {14}\n"
                , NameFull, Level, StrPlus(_hp, _hp_shield), _hpmax, StrPlus(_hp_regen_per_turn, _hp_regen_mod),
                 _mp, _mpmax, StrPlus(_mp_regen_per_turn, _mp_regen_mod), description, StrPlus(_atpmax, _atp_mod),
                 StrPlus(_att_dmg, _att_dmg_mod), _att_dist, StrPlus(_mvpmax, _mvp_mod), StrPlus(_def, _def_mod), (_acc == -1) ? "no"
                 : StrPlus(_acc, _acc_mod), LOGS.TraceBar(_hp + _hp_shield, _hpmax + _hp_shield), LOGS.TraceBar(_mp, _mpmax));
            for (int i = 0; i < buffs.Count; i++)
                s += buffs[i].NameFull + "\n";
            return s;
        }
        public virtual int Level { get { return _level; } }
        //public virtual string Name { get { return _name; } }
        public abstract string NameFull { get; }
        //
        public int getCurrentHP { get { return _hp + _hp_shield; } }
        public int getMaxHP { get { return _hpmax + _hp_shield; } }
        public int getCurrentMP { get { return _mp; } }
        public int getMaxMP { get { return _mpmax; } }
        public virtual Point GetPosition { get { return _pos; } }
        public virtual int GetSpd { get { return _mvpmax; } }
        public virtual int getTeamNumber { get { return TeamNumber; } }

        public virtual Abstraceunit CalculateAttack(List<Abstraceunit> whocan)
        {
            //
            if (whocan.Count == 0)
                return null;
            int minHealh = int.MaxValue; int maxInd = -1;
            for (int i = 0; i < whocan.Count; i++)
                if (whocan[i].getCurrentHP < minHealh)
                { minHealh = whocan[i].getCurrentHP; maxInd = i; }
            return whocan[maxInd];
        }

        public virtual List<Prio> CalculateSituation(Battlefield bf)
        {
            bool inverse = false;// (TeamNumber % 2) == 0;
            List<Abstraceunit> units = bf.getUnits;

            List<Prio> res = new List<Prio>();
            for (int i = 0; i < units.Count; i++)
                if (units[i] != this)
                {
                    if (inverse)
                    {
                        // victim
                        if (units[i].getTeamNumber != TeamNumber)
                            res.Add(new Prio(Calculator.GetOblast(units[i].GetPosition, units[i].GetSpd * 2 - GetSpd, true, true), -5.0f));
                        else
                            res.Add(new Prio(Calculator.GetOblast(units[i].GetPosition, 2, true, true), 1.0f)); // 1.0f
                    }
                    else
                    {
                        // hunter
                        if (units[i].getTeamNumber != getTeamNumber)
                        {
                            res.Add(new Prio(Calculator.GetOblast(units[i].GetPosition, _att_dist - 1, true, true), -2.0f));
                            res.Add(new Prio(Calculator.GetOblast(units[i].GetPosition, _att_dist - 1, true, false), 8.0f));
                        }
                    }
                }
            return res;
        }
        public void Die()
        {
            for (int i = 0; i < buffs.Count; i++)
                buffs[i].Dissaply();
            OnDie();
            LOGS.Add(String.Format("{0} died.", NameFull));
            _isDead = true;
        }
        public virtual void DamageFor(int x)
        {
            float defPercent = 1.0f - .01f * (_def + _def_mod);
            int wasHP = getCurrentHP;
            int recievedDamage = (int)Math.Max(1.0f, defPercent * x);
            OnHitRecievedEvent(x);
            _hp = Math.Max(0, _hp - recievedDamage);
            LOGS.Add(String.Format("{0} was damaged for {1} (blocked {2}), {5} -> {3}/{4} HP left", NameFull, x, x - recievedDamage, getCurrentHP, getMaxHP, wasHP));
            OnHealthChangedEvent();
        }

        public virtual void HealFor(int x)
        {
            int _hpwas = _hp;
            _hp = Math.Min(_hpmax, _hp + x);
            if (_hp != _hpwas)
            {
                OnHealthChangedEvent();
                LOGS.Add(String.Format("{0} was healed for {1} health up to {2}/{3}", NameFull, _hp - _hpwas, getCurrentHP, getMaxHP));
            }
        }
        public virtual void FillManaFor(int x)
        {
            _mp = Math.Min(_mpmax, _mp + x);
            LOGS.Add(String.Format("{0} refilled mana for {1} up to {2}/{3}", NameFull, x, getCurrentMP, getMaxMP));
        }
        public virtual bool SpendManaFor(int x)
        {
            if (_mp < x)
                return false;
            _mp = _mp - x;
            LOGS.Add(String.Format("{0} spend {1} mana, {2}/{3} MP left", NameFull, x, getCurrentMP, getMaxMP));
            return true;
        }
        public virtual bool isDead { get { return _isDead; } }
        public void MoveTo(Point where)
        {
            LOGS.Add(String.Format("{0} moves ({1},{2}) -> ({3},{4})", NameFull, _pos.X, _pos.Y, where.X, where.Y));
            _pos = where;
        }
        public void Attack(Abstraceunit who)
        {
            this.OnAttacking(who);
            LOGS.Add(String.Format("{0} attacks {1}", this.NameFull, who.NameFull));
            who.OnAttacked(this);
            who.DamageFor(this.CurrentDamage);
            if (!who.isDead)
                who.AfterAttacked(this);
        }

        public virtual string TraceBars()
        {
            return (String.Format("\n  {0}:\n  HP:{1}\n  MP:{2}", NameFull, LOGS.TraceBar(_hp + _hp_shield, _hpmax + _hp_shield), LOGS.TraceBar(_mp, _mpmax)));
        }
        public string TraceBar(bool health)
        {
            if (health)
                return String.Format("{0}  {1}/{2}", LOGS.TraceBar(_hp + _hp_shield, _hpmax + _hp_shield), _hp + _hp_shield, _hpmax + _hp_shield);
            return String.Format("{0}  {1}/{2}", LOGS.TraceBar(_mp, _mpmax), _mp, _mpmax);
        }

        public virtual void AddBuff(Ibuff buff)
        {
            //
            buffs.Add((Abstractbuff)buff);
        }

        public void TickBuffs()
        {
            for (int i = 0; i < buffs.Count; i++)
                if (buffs[i].TurnsLeft <= 0)
                {
                    buffs[i].Dissaply();
                    buffs.RemoveAt(i);
                    i--;
                }
                else
                    buffs[i].Tick();
        }

        public virtual void OnHitRecievedEvent(int damage)
        {
            LOGS.Add(String.Format("{0} was hited", this.NameFull));
        }

        public virtual void OnHealthChangedEvent()
        {
            LOGS.Add(String.Format("{0}'s HP changed", this.NameFull));
            if (this.getCurrentHP <= 0)
                Die();
        }

        public virtual void OnTurnEnd()
        {
            int regenHP = _hp_regen_mod + _hp_regen_per_turn,
                regenMP = _mp_regen_mod + _mp_regen_per_turn;

            if (regenHP > 0) HealFor(regenHP);
            if (regenHP < 0) DamageFor(regenHP);
            if (regenMP > 0) FillManaFor(regenMP);

            TickBuffs();
        }

        public virtual void OnAttacking(Iunit who) { LOGS.Add(String.Format("{0} will attack {1} in a second", this.NameFull, who.NameFull)); }

        public virtual void OnKillUnit(Iunit who) { LOGS.Add(String.Format("{0} fragged {1}", this.NameFull, who.NameFull)); }

        public virtual void OnDie() { LOGS.Add(String.Format("{0} will die", this.NameFull)); }

        public virtual void OnAttacked(Iunit bywho) { LOGS.Add(String.Format("{0} was attacked by {1}", this.NameFull, bywho.NameFull)); }

        public virtual void AfterAttacked(Iunit bywho) { LOGS.Add(String.Format("{0} was already attacked by {1}", this.NameFull, bywho.NameFull)); }

        public int DistanceTo(Abstraceunit another, bool isMovement)
        {
            if (isMovement)
                return Math.Abs(GetPosition.X - another.GetPosition.X) + Math.Abs(GetPosition.Y - another.GetPosition.Y);
            return Math.Max(Math.Abs(GetPosition.X - another.GetPosition.X), Math.Abs(GetPosition.Y - another.GetPosition.Y));
        }

        public int DistanceTo(Point where, bool isMovement)
        {
            if (isMovement)
                return Math.Abs(GetPosition.X - where.X) + Math.Abs(GetPosition.Y - where.Y);
            return Math.Max(Math.Abs(GetPosition.X - where.X), Math.Abs(GetPosition.Y - where.Y));
        }

        public int CurrentDamage
        {
            get { return _att_dmg + _att_dmg_mod; }
        }

        public int CurrentMVP
        {
            get { return _mvpmax + _mvp_mod; }
        }

        public int CurrentAttackRange
        {
            get { return _att_dist; }
        }

        public int CurrentATP
        {
            get { return _atpmax + _atp_mod; }
        }

        int _unitID;
        public int UnitID
        {
            get { return _unitID; }
        }

        public string NameID
        {
            get { return "[" + _unitID + "]"; }
        }
    }
}