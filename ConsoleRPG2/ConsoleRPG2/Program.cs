using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibRPHG;
using MatrixFields;
using System.Drawing;

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

            LibRPHG.PlayerClasses.CharacterSelect.generateTeam(0);

            //int charCount = 50; 
            //while (charCount-- > 0)
            //{
            //    PTest nu = new PTest(new Point(charCount % 20 + 4, rnd.Next(Calculator.FieldSize)));
            //    nu.TeamNumber = charCount % 2 + 1;
            //    bf.addUnit(nu);
            //}
            //PTest lastPriest = bf.getUnits[0] as PTest;
            //while (true) if (lastPriest.Level < 5) lastPriest.RecieveExp(20); else break;

            //LOGS.Trace(); Console.WriteLine(lastPriest.TraceMoveStats());


            //Console.ReadKey();

            //do
            //{
            //    bf.CalculateMovementForEachCharacter();
            //    Console.ReadKey();
            //} while (true);
        }
    }
}
