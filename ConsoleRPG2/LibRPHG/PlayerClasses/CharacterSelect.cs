using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace LibRPHG.PlayerClasses
{

    public class CharacterSelect
    {
        // TANK DPS SUPPORT CONTROL
        static int[][] whowhat = new int[][] {
            new int[]{8,2,0,0},
            new int[]{5,5,0,0},
            new int[]{ 5,2,3,0},
            new int[]{ 7,2,1,0},
            new int[]{ 4,6,0,0},
            new int[]{ 0,0,0,0},
            new int[]{ 0,9,0,1},
            new int[]{ 2,8,0,0},
            new int[]{ 0,8,0,2},
            new int[]{ 1,9,0,0},
            new int[]{ 0,10,0,0},
            new int[]{ 1,7,0,2},
            new int[]{ 3,3,0,4},
            new int[]{ 1,0,8,1},
            new int[]{ 3,3,1,3},
            new int[]{ 1,4,0,5},
            new int[]{ 0,0,5,5},
            new int[]{ 0,0,0,0},
            new int[]{ 1,4,4,1},
            new int[]{ 0,7,3,0},
            new int[]{ 1,9,0,0},
            new int[]{ 0,0,8,2},
            new int[]{ 1,8,1,0},
            new int[]{ 6,3,0,1},
            new int[]{ 9,0,1,0}
        };

        public static void generateTeam(int howMuch)
        {
            Abstractplayer d;
            string tst = "/:-#";
            for (int i = 0; i < 25; i++)
            {
                d = getPlayerByIndex(i, new Point(0, 0));
                //d.DamageFor(23);
                //d.SpendManaFor(40);
                Console.WriteLine(d.TraceBars() + "\n" + d.TraceMoveStats());
                Console.WriteLine(); 
                for (int j = 0; j < 4; j++)
                    for (int k = 0; k < whowhat[i][j]; k++)
                        for (int a = 0; a < 2; a++)
                            Console.Write(tst[j]);
                Console.ReadKey();
                Console.Clear();
            }
            //return new List<IUnit>();
        }
        public static Abstractplayer getPlayerByIndex(int index, Point start)
        {
            switch (index)
            {
                case 0:
                    return new PKnight(start);
                case 1:
                    return new PBarbarian(start);
                case 2:
                    return new PPaladin(start);
                case 3:
                    return new PDruid(start);
                case 4:
                    return new PSpearman(start);
                case 5:
                    return new PWoodo(start);
                case 6:
                    return new PArcher(start);
                case 7:
                    return new PHunter(start);
                case 8:
                    return new PRogue(start);
                case 9:
                    return new PAssasin(start);
                case 10:
                    return new PNinja(start);
                case 11:
                    return new PDeadarcher(start);
                case 12:
                    return new PSatanist(start);
                case 13:
                    return new PPriest(start);
                case 14:
                    return new PNecromancer(start);
                case 15:
                    return new PTrickster(start);
                case 16:
                    return new PPixie(start);
                case 17:
                    return new PDriad(start);
                case 18:
                    return new PSorcerer(start);
                case 19:
                    return new PCultist(start);
                case 20:
                    return new PDeathKnight(start);
                case 21:
                    return new PShaman(start);
                case 22:
                    return new PGhoul(start);
                case 23:
                    return new PBrawler(start);
                case 24:
                    return new PGuard(start);
                default:
                    throw new Exception("Incorrect character select!");
            }
        }
    }
}
