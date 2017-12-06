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
        }
        public override string Prof { get { return "Knight"; } }
    }
    public class PBarbarian : Abstractplayer
    {
        public PBarbarian(Point startPoint)
        {
            base.SetDefaultStats(Namegen.GenerateRandomName(), startPoint,
                1, 100, 20, 5, 2, 60, 1, 35, -1);
            base.SetDefault();
        }
        public override string Prof { get { return "Barbarian"; } }
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
