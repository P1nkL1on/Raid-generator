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

            int charCount = 4;
            while (charCount-- > 0)
            {
                Abstractplayer nu =
                    LibRPHG.PlayerClasses.CharacterSelect.getPlayerByIndex(rnd.Next(2),
                    new Point(charCount % 20 + 4, rnd.Next(Calculator.FieldSize)));

                nu.TeamNumber = charCount % 2 + 1;
                bf.addUnit(nu);
            }
            Abstractplayer lastPriest = bf.getUnits[0] as Abstractplayer;

            LOGS.Trace();
            Console.ReadKey();

            do
            {
                bf.CalculateMovementForEachCharacter();
                if (Console.ReadKey().KeyChar == 't')
                {
                    Console.ResetColor();
                    for (int i = 0; i < bf.getUnits.Count; i++)
                        Console.WriteLine(bf.getUnits[i].TraceMoveStats());
                    LOGS.Trace();
                    Console.ReadKey();
                }
            } while (true);
        }
    }
}
