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

        static void Main(string[] args)
        {
            Console.SetWindowSize(220, 84);

            int charCount = 500;
            while (--charCount > 0)
            {
                ProfPriest nu = new ProfPriest(new Point(charCount % 20 + 4, charCount / 20));
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
