using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using MatrixFields;

namespace LibRPHG.PlayerClasses
{
    public class ESkeleton : Abstraceunit
    {
        public ESkeleton(System.Drawing.Point startPoint)
        {
            base.SetDefaultStats("Skeleton", startPoint, 1, 40, 5, 4, 1, 8, 1, 5, -1);
        }

        public override string NameFull { get { return String.Format("{0}, the undead", Name); } }

        public override List<MatrixFields.Prio> CalculateSituation(Battlefield bf)
        {
            List<Abstraceunit> units = bf.getUnits;

            List<Prio> res = new List<Prio>();
            for (int i = 0; i < units.Count; i++)
                if (units[i] != this)
                {
                        if (units[i].getTeamNumber != getTeamNumber)
                        {
                            res.Add(new Prio(Calculator.GetOblast(units[i].GetPosition, _att_dist - 1, true, false), 8.0f + 1.0f * (units[i].CurrentAttackRange) + ((units[i].getCurrentHP <= units[i].getMaxHP / 2)? 8.0f : 0.0f)));
                            //if (units[i].CurrentAttackRange > 1)
                            //    res.Add(new Prio(Calculator.GetOblast(units[i].GetPosition, units[i].CurrentAttackRange, true, true), -.2f / (units[i].CurrentAttackRange * units[i].CurrentAttackRange)));
                        }
                }
            return res;
        }
    }
}
