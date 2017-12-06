using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace LibRPHG.PlayerClasses
{
    public class PArcher : Abstractplayer
    {
        public PArcher(Point startPoint)
        {
            base.SetDefaultStats(Namegen.GenerateRandomName(), startPoint,
                1, 50, 35, 6, 2, 35, 7, 20, 60);
            base.SetDefault();
        }
        public override string Prof { get { return "Archer"; } }
    }
    public class PDeadarcher : Abstractplayer
    {
        public PDeadarcher(Point startPoint)
        {
            base.SetDefaultStats(Namegen.GenerateRandomName(), startPoint,
                1, 50, 120, 5, 2, 40, 6, 20, 55);
            base.SetDefault();
        }
        public override string Prof { get { return "Dead archer"; } }
    }

    public class PHunter : Abstractplayer
    {
        public PHunter(Point startPoint)
        {
            base.SetDefaultStats(Namegen.GenerateRandomName(), startPoint,
                1, 45, 30, 7, 3, 30, 5, 5, 50);
            base.SetDefault();
        }
        public override string Prof { get { return "Hunter"; } }
    }
    public class PRogue : Abstractplayer
    {
        public PRogue(Point startPoint)
        {
            base.SetDefaultStats(Namegen.GenerateRandomName(), startPoint,
                1, 60, 50, 6, 3, 35, 1, 15, -1);
            base.SetDefault();
        }
        public override string Prof { get { return "Rogue"; } }
    }
    public class PAssasin : Abstractplayer
    {
        public PAssasin(Point startPoint)
        {
            base.SetDefaultStats(Namegen.GenerateRandomName(), startPoint,
                1, 45, 60, 8, 5, 32, 1, 10, -1);
            base.SetDefault();
        }
        public override string Prof { get { return "Assasin"; } }
    }
    public class PNinja : Abstractplayer
    {
        public PNinja(Point startPoint)
        {
            base.SetDefaultStats(Namegen.GenerateRandomName(), startPoint,
                1, 55, 20, 7, 4, 30, 1, 5, -1);
            base.SetDefault();
        }
        public override string Prof { get { return "Ninja"; } }
    }
    public class PGhoul : Abstractplayer
    {
        public PGhoul(Point startPoint)
        {
            base.SetDefaultStats(Namegen.GenerateRandomName(), startPoint,
                1, 60, 15, 6, 2, 50, 1, 10, -1 );
            base.SetDefault();
        }
        public override string Prof { get { return "Ghoul"; } }
    }
    public class PCultist : Abstractplayer
    {
        public PCultist(Point startPoint)
        {
            base.SetDefaultStats(Namegen.GenerateRandomName(), startPoint,
                1, 75, 70, 4, 2, 45, 3, 15, 40);
            base.SetDefault();
        }
        public override string Prof { get { return "Cultist"; } }
    }
    public class PSatanist : Abstractplayer
    {
        public PSatanist(Point startPoint)
        {
            base.SetDefaultStats(Namegen.GenerateRandomName(), startPoint,
                1, 100, 90, 4, 2, 30, 5, 5, 20);
            base.SetDefault();
        }
        public override string Prof { get { return "Satanist"; } }
    }
}
