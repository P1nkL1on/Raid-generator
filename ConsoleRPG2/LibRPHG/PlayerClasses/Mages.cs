using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace LibRPHG.PlayerClasses
{
    class PWoodo : Abstractplayer
    {
        public PWoodo(Point startPoint)
        {
            base.SetDefaultStats(Namegen.GenerateRandomName(), startPoint,
                1, 1,1,1,1,1,1,1,-1);
            base.SetDefault();
        }
        public override string Prof { get { return "Woodo doctor"; } }
    }
    class PPriest : Abstractplayer
    {
        public PPriest(Point startPoint)
        {
            base.SetDefaultStats(Namegen.GenerateRandomName(), startPoint,
                1, 150, 100, 2, 1, 30, 3, 5, 30);
            base.SetDefault();
        }
        public override string Prof { get { return "Priest"; } }
    }
    class PNecromancer : Abstractplayer
    {
        public PNecromancer(Point startPoint)
        {
            base.SetDefaultStats(Namegen.GenerateRandomName(), startPoint,
                1, 80, 120, 4, 2, 20, 4, 10, 35);
            base.SetDefault();
        }
        public override string Prof { get { return "Necromancer"; } }
    }
    class PTrickster : Abstractplayer
    {
        public PTrickster(Point startPoint)
        {
            base.SetDefaultStats(Namegen.GenerateRandomName(), startPoint,
                1, 80, 90, 5, 1, 40, 6, 5, 50);
            base.SetDefault();
        }
        public override string Prof { get { return "Trickster"; } }
    }
    class PPixie : Abstractplayer
    {
        public PPixie(Point startPoint)
        {
            base.SetDefaultStats(Namegen.GenerateRandomName(true), startPoint,
                1, 75, 100, 4, 1, 35, 6, 0, 60);
            base.SetDefault();
        }
        public override string Prof { get { return "Pixie"; } }
    }
    class PDriad : Abstractplayer
    {
        public PDriad(Point startPoint)
        {
            base.SetDefaultStats(Namegen.GenerateRandomName(true), startPoint,
                1, 50, 100, 3, 2, 25, 5, 5, 40);
            base.SetDefault();
        }
        public override string Prof { get { return "Driad"; } }
    }
    class PSorcerer : Abstractplayer
    {
        public PSorcerer(Point startPoint)
        {
            base.SetDefaultStats(Namegen.GenerateRandomName(), startPoint,
                1, 80, 150, 4, 2, 20, 4, 10, 35);
            base.SetDefault();
        }
        public override string Prof { get { return "Sorcerer"; } }
    }
    class PShaman : Abstractplayer
    {
        public PShaman(Point startPoint)
        {
            base.SetDefaultStats(Namegen.GenerateRandomName(), startPoint,
                1, 65, 50, 4, 1, 40, 5, 20, 30);
            base.SetDefault();
        }
        public override string Prof { get { return "Shaman"; } }
    }
}
