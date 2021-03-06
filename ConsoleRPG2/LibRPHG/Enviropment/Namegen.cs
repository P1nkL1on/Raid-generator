﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibRPHG
{
    public static class Namegen
    {
        static string kost = "QWERTYUIOPASDFGHJKLZXCVBNM";

        static Random rnd = new Random();
        static string glasLetters = "eyuioa";
        static string soglasLetters = "qwrtpsdfghjklzxcvbnm";
        static string[] ends = new string[]{"alias", "ondor","or", "enier", "an", "silii", "ya", "on", "arield", "ald",
                                            "om","ous","ey","od" };
        static string[] femaleEnds = new string[] { "iya","ella", "ina", "ora", "asha", "eisha", "ina", "iliya", "ena"
                                                   ,"ola","y","a"};
        static string randomGlas()
        {
            return glasLetters[rnd.Next(glasLetters.Length)] + "";
        }
        static string randomSoglas()
        {
            return soglasLetters[rnd.Next(soglasLetters.Length)] + "";
        }
        static string randomEnd(bool isFemale)
        {
            if (!isFemale)
                return ends[rnd.Next(ends.Length)];
            return femaleEnds[rnd.Next(femaleEnds.Length)];
        }

        public static string GenerateRandomName()
        {
            return GenerateRandomName(false);
        }

        public static string GenerateRandomName(bool isFemale)
        {
            //return kost[rnd.Next(kost.Length)] + "";

            string resName = "";
            int slogCountVer = rnd.Next(100), slogCount = -1;
            if (slogCountVer < 60)
                slogCount = 1;
            if (slogCountVer >= 60 && slogCountVer < 90)
                slogCount = 2;
            if (slogCountVer >= 90)
                slogCount = 3;

            for (int i = 0; i < slogCount; i++)
            {
                resName += randomGlas() + randomSoglas();
                if (i > 0 && rnd.Next(101) < 5)
                    resName += resName[resName.Length - 1];
            }
            resName += randomEnd(isFemale);
            // first later
            if (rnd.Next(101) < 60)
                resName = randomSoglas().ToUpper() + resName;
            else
                resName = (resName[0] + "").ToUpper() + resName.Substring(1);
            return resName;
        }


    }
}