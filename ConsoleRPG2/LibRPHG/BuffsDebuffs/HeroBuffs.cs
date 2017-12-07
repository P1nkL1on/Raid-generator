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
    }



    public class BarbarianRage : Abstractbuff
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
        public BarbarianRage(Abstraceunit who, Abstraceunit bywho)
        {
            _buffname = "Barbarian's rage";
            _turnsleft = 3;
            ApplyFor(who, bywho);
        }
    }

    public class KnightArmor : Abstractbuff
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
        public KnightArmor(Abstraceunit who, Abstraceunit bywho)
        {
            _buffname = "Knight's armor";
            _turnsleft = 1;
            _nothit = 0;
            _currentDefenseBuff = BuffsConsts.KnightArmor[0];
            ApplyFor(who, bywho);
            _bufftarget._def_mod += _currentDefenseBuff;
        }
        
    }
}
