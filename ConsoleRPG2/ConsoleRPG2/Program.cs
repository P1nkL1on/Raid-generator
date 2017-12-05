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

            int charCount = 50;
            while (charCount-- > 0)
            {
                ProfPriest nu = new ProfPriest(new Point(charCount % 20 + 4, rnd.Next(Calculator.FieldSize)));
                nu.TeamID = charCount % 2 + 1;
                bf.addUnit(nu);
            }
            LOGS.Trace();

            do
            {
                //ProfPriest serega = new ProfPriest();
                //serega.TeamID = 1;
                //serega.TraceBars();
                //for (int i = 0; i < 100; i++)
                //    serega.RecieveExp(10);
                //LOGS.Trace();

                //Calculator.Foo();
                bf.CalculateMovementForEachCharacter();
                //LOGS.Trace();

                Console.ReadKey();
                Console.Clear();
            } while (true);
        }
    }
}
