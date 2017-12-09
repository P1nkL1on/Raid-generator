using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace MatrixFields
{
    public static class Calculator
    {

        public static int FieldSize = 40;
        static string heroesNames = "QWERTYUIOPASDFGHJKLZXCVBNM";
        public static void TraceMatrixToConsole(float[,] mat, List<Point> prioretyze)
        {
            //DarkMagenta;
            for (int i = 0; i < mat.GetLength(0); i++, Console.WriteLine())
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    float sw = mat[i, j];
                    string S = (Math.Abs(Math.Round(sw * 10f) / 10.0f) + "");


                    Console.ForegroundColor = ConsoleColor.Black;
                    //if (false)
                    for (int k = 0; k < prioretyze.Count; k++)
                        if (prioretyze[k].X == i && prioretyze[k].Y == j)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            //S = heroesNames[k % (heroesNames.Length)] + "";
                        }


                    Console.BackgroundColor = ConsoleColor.Black;
                    if (sw <= -.05f)
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                    if (sw <= -1.1f)
                        Console.BackgroundColor = ConsoleColor.Red;
                    if (sw >= .05f)
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                    if (sw >= 1.1f)
                        Console.BackgroundColor = ConsoleColor.Green;

                    Console.Write(S.PadLeft(4));
                }
            Console.ResetColor();
        }

        public static void TraceBattlefieldToConsole(List<Point> who)
        {
            //DarkMagenta;
            for (int i = 0; i < FieldSize; i++, Console.WriteLine())
                for (int j = 0; j < FieldSize; j++)
                {
                    string S = " ";
                    if (i == 0 || j == 0 || i == FieldSize - 1 || j == FieldSize - 1) { S = "."; Console.ForegroundColor = ConsoleColor.Gray; }
                    for (int k = 0; k < who.Count; k++)
                        if (who[k].X == i && who[k].Y == j)
                        {
                            S = heroesNames[k % (heroesNames.Length)] + "";
                            Console.ForegroundColor = (k < who.Count - 1) ? ConsoleColor.Green : ConsoleColor.Red;
                        }
                    Console.Write(S.PadLeft(2));
                }
            Console.ResetColor();
        }

        static float[,] GenerateZeroMatrix(int size)
        {
            float[,] res = new float[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    res[i, j] = 0.0f;
            return res;
        }

        static int chsize(int X)
        {
            return Math.Max(0, Math.Min(FieldSize - 1, X));
        }

        static Point chpoint(Point P)
        {
            return new Point(chsize(P.X), chsize(P.Y));
        }

        static bool PointOnField(Point P)
        {
            return (P.X >= 0 && P.X < FieldSize && P.Y >= 0 && P.Y < FieldSize);
        }

        static int PointsDist(Point p1, Point p2, bool isAttack)
        {
            if (isAttack)
                return Math.Max(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
            return Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);
        }

        public static List<Point> GetOblast(Point center, int rad, bool isAttack, bool isFilled)
        {
            List<Point> res = new List<Point>();
            for (int i = center.X - rad; i <= center.X + rad; i++)
                for (int j = center.Y - rad; j <= center.Y + rad; j++)
                {
                    Point cP = new Point(i, j);
                    if (!PointOnField(cP))
                        continue;
                    int dist = PointsDist(center, cP, isAttack);
                    if ((isFilled && dist <= rad) || (!isFilled && dist == rad))
                        res.Add(cP);
                }
            return res;
        }

        static void changePoints(ref float[,] matrix, List<Point> indexes, float incrs)
        {
            for (int i = 0; i < indexes.Count; i++)
                matrix[indexes[i].X, indexes[i].Y] += incrs;
        }

        //static void changePriorityDependOnDistance(ref float[,] mat, Point center, bool isAttack)
        //{
        //    for (int i = 0; i < mat.GetLength(0); i++)
        //        for (int j = 0; j < mat.GetLength(1); j++)
        //        {
        //            int distance = PointsDist(center, new Point(i, j), isAttack);
        //            mat[i, j] /= (distance == 0.0f)? 1.0f : (float)distance;
        //        }
        //}

        public static List<Point> deleteListFromList(List<Point> from, params Point[] what)
        {
            for (int i = 0; i < from.Count; i++)
                for (int j = 0; j < what.Length; j++)
                    if (from[i].X == what[j].X && from[i].Y == what[j].Y)
                    {
                        from.RemoveAt(i); i--; break;
                    }
            return from;
        }

        static Point WhereToGo(float[,] mat, List<Point> canGo)
        {
            List<float> prioritet = new List<float>();
            bool isAttack = true;
            for (int c = 0; c < canGo.Count; c++)
            {
                float newP = 0.0f;
                for (int i = 0; i < mat.GetLength(0); i++)
                    for (int j = 0; j < mat.GetLength(1); j++)
                        if (!(canGo[c].X == i && canGo[c].Y == j))
                            newP += (mat[i, j] >= 0.0f) ?
                                        mat[i, j] / (float)(PointsDist(new Point(i, j), canGo[c], isAttack))
                                        : ((PointsDist(new Point(i, j), canGo[c], isAttack) <= 1) ? mat[i, j] : 0.0f);
                prioritet.Add(newP + ((mat[canGo[c].X, canGo[c].Y] < 0) ? -5.0f : 0f));
            }

            // choose a vaeiant
            float maxP = float.MinValue;
            int maxInd = -1;
            for (int i = 0; i < prioritet.Count; i++)
                if (prioritet[i] > maxP)
                { maxP = prioritet[i]; maxInd = i; }

            float[,] mat2 = GenerateZeroMatrix(FieldSize);
            for (int i = 0; i < canGo.Count; i++)
                mat2[canGo[i].X, canGo[i].Y] = prioritet[i];
            //TraceMatrixToConsole(mat2, canGo);

            return canGo[maxInd];
        }

        public static void Foo()
        {
            Random rnd = new Random();
            List<Point> path = new List<Point>();
            Point curPos = new Point(rnd.Next(FieldSize), rnd.Next(FieldSize));
            path.Add(curPos);


            float[,] mat = GenerateZeroMatrix(FieldSize);
            int rad1 = rnd.Next(10) + 3, rad2 = rnd.Next(30) + 2, spd = rnd.Next(5) + 2;

            for (int i = 0; i < 3; i++)
                changePoints(ref mat, GetOblast(new Point(rnd.Next(FieldSize), rnd.Next(FieldSize)), rnd.Next(5) + 2, false, true), (i == 0) ? 5.0f : 1.0f);

            for (int i = 0; i <= rad1; i += 2)
                changePoints(ref mat, GetOblast(new Point(rnd.Next(FieldSize), rnd.Next(FieldSize)), i, true, (rnd.Next(101) < 30)), -1f);
            for (int i = 0; i <= rad2; i += 2)
                changePoints(ref mat, GetOblast(new Point(rnd.Next(FieldSize), rnd.Next(FieldSize)), i, false, (rnd.Next(101) < 15)), -1f);

            for (int p = 0; p < 50; p++)
            {
                List<Point> moveObl = deleteListFromList(GetOblast(curPos, spd, false, true), curPos);
                curPos = WhereToGo(mat, moveObl);
                path.Add(curPos);
            }
            TraceMatrixToConsole(mat, path);
        }

        public static Point CalculateMovement(List<Point> canGoTo, List<Prio> prios)
        {
            float[,] mat = GenerateZeroMatrix(FieldSize);
            for (int i = 0; i < prios.Count; i++)
                changePoints(ref mat, prios[i].oblast, prios[i].offset);
            //TraceMatrixToConsole(mat, canGoTo);
            return WhereToGo(mat, canGoTo);
        }
    }

    public struct Prio
    {
        public List<Point> oblast;
        public float offset;

        public Prio(List<Point> oblast, float offset)
        {
            this.oblast = oblast; this.offset = offset;
        }
    }
}
