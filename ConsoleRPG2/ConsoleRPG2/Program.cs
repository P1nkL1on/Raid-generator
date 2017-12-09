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

            int charCount = 40;
            while (charCount-- > 0)
            {
                Abstractplayer nu =
                    LibRPHG.PlayerClasses.CharacterSelect.getPlayerByIndex(rnd.Next(25),
                    new Point(rnd.Next(Calculator.FieldSize), rnd.Next(Calculator.FieldSize)));
                nu.TeamNumber = 1;
                bf.addUnit(nu);
            }
            Abstraceunit dm = new EBigDummy(new Point(Calculator.FieldSize / 2, Calculator.FieldSize / 2));
            dm.TeamNumber = 0;
            bf.addUnit(dm);

            LOGS.Trace();
            Console.ReadKey();
            do
            {
                bf.CalculateMovementForEachCharacter();
                string S = Console.ReadLine();
                if (S.Length > 0)
                {
                    LOGS.Trace(S);
                    Console.ReadKey();
                }
            } while (true);
        }
    }
}
