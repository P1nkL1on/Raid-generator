using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRPHG
{
    public static class LOGS
    {
        // field is near 100 x 100



        static List<string> logs = new List<string>();
        static int nowOnLog = -1;
        public static void Add(string log)
        {
            DateTime now = DateTime.Now;
            log = String.Format("[{0}]\t{1}", now.ToLongTimeString(), log);
            logs.Add(log);
        }

        public static void Trace()
        {
            Trace("");
        }
        public static void Trace(string Filter)
        {
            while (nowOnLog < logs.Count - 1)
            {
                string what = logs[++nowOnLog];
                if (what.IndexOf(Filter) >= 0 || Filter == "")
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    if (what.IndexOf("attack") > 0 || what.IndexOf("hit") > 0)
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    if (what.ToUpper().IndexOf("HP") > 0 || what.IndexOf("die") > 0)
                        Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(what);
                }
            }
        }

        public static string TraceBar(float now, float max)
        {
            float mnozh = max / 20.0f;
            return TraceBar(now, max, mnozh);
        }

        public static string TraceBar(float now, float max, float mnozh)
        {

            int to = (int)(max / mnozh), t = (int)(now / mnozh);
            string res = "";
            for (int i = 0; i < to; i++)
                res += ((i < t) ? "■" : "-");//"█" : "░");
            return String.Format("{0}", res);
        }
    }
}