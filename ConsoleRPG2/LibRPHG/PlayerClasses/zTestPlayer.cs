using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRPHG
{
    public class PTest : Abstractplayer
    {
        public PTest(System.Drawing.Point startPoint)
        {
            base.SetDefaultStats(Namegen.GenerateRandomName(), startPoint,
                1, 150, 100, 2, 1, 30, 3, 5, 30);
            base.SetDefault();
        }

        public override string Prof { get { return "Testman"; } }
    }

    public class EBigDummy : Abstraceunit
    {
        public EBigDummy(System.Drawing.Point startPoint)
        {
            base.SetDefaultStats("\"DUMMY\"", startPoint,
                5, 1000000, 10, 2, 1, 80, 2, 90, -1);
        }

        public override string NameFull { get { return String.Format("{0}, the dummy", Name); } }


        //public override Abstraceunit CalculateAttack(List<Abstraceunit> whocan)
        //{
        //    return null;
        //}

        public override List<MatrixFields.Prio> CalculateSituation(Battlefield bf)
        {
            return new List<MatrixFields.Prio>();
        }
    }
}
