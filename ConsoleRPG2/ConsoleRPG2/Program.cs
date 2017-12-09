using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibRPHG;
using MatrixFields;
using System.Drawing;
using LibRPHG.PlayerClasses;

namespace ConsoleRPG2
{
    class Program
    {
        static Battlefield bf = new Battlefield();

        static Random rnd = new Random();

        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetWindowPosition(0, 0);

            //LibRPHG.PlayerClasses.CharacterSelect.generateTeam(0);

            //int charCount = 40;
            //while (charCount-- > 0)
            //{
            //    Abstractplayer nu =
            //        LibRPHG.PlayerClasses.CharacterSelect.getPlayerByIndex(rnd.Next(25),
            //        new Point(rnd.Next(Calculator.FieldSize), rnd.Next(Calculator.FieldSize)));
            //    nu.TeamNumber = 1;
            //    bf.addUnit(nu);
            //}
            int raidCount = 20;
            while (true)
            {
                for (int i = 0; i < raidCount; i++)
                {
                    Abstraceunit dm = CharacterSelect.getPlayerByIndex(rnd.Next(25), new Point(Calculator.FieldSize / 2, Calculator.FieldSize / 2));
                    dm.TeamNumber = 1;
                    bf.addUnit(dm);
                }
                LOGS.Trace();
                Console.ReadLine();

                do
                {
                    while (bf.getUnits.Count < raidCount * 3 / 2)
                        bf.addUnit(new ESkeleton(
                                    (rnd.Next(2) == 0) ?
                                    new Point(rnd.Next(Calculator.FieldSize), rnd.Next(2) * (Calculator.FieldSize - 2) + 1)
                                    : new Point(rnd.Next(2) * (Calculator.FieldSize - 2) + 1, rnd.Next(Calculator.FieldSize))));

                    bf.CalculateMovementForEachCharacter();
                    bf.TraceHealth();
                    //for (int i = 0; i < bf.getUnits.Count; i++, Console.WriteLine(bf.getUnits[i-1].TraceBars())) ;
                    LOGS.Trace("died|joined");
                    string S = Console.ReadLine();
                    
                } while (bf.SummHPteam(1) > 0);
                LOGS.Trace();
                Console.ReadLine();
            }
        }
    }
}
