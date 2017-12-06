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
}
