using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRPHG
{
    public class ProfPriest : AbstractPlayer
    {
        public ProfPriest(System.Drawing.Point startPoint)
        {
            base.SetDefault(Namegen.GenerateRandomName(), startPoint, 3, 1, 30.0f, 3, 150.0f, 80.0f);
            base.SetDefault();
        }

        public override string Prof { get { return "Priest"; } }
    }
}
