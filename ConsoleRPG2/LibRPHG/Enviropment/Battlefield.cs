using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MatrixFields;
using System.Drawing;

namespace LibRPHG
{
    public static class BFConst
    {
        public static int currentUnitID = 0;
    }

    public class Battlefield
    {
        List<Abstraceunit> _units;
        

        public void SORTUNITS( bool FromMinToMax )
        {
            List<Abstraceunit> want = _units;
            for (int i = 0; i  < want.Count; i++)
            {
                int needInd = i, lowestMVP = want[i].CurrentMVP;
                Abstraceunit chosen = want[i];

                for (int j = i + 1; j < want.Count; j++)
                    if ((lowestMVP > want[j].CurrentMVP && FromMinToMax)
                        || (lowestMVP < want[j].CurrentMVP && !FromMinToMax))
                    { needInd = j; chosen = want[j]; lowestMVP = want[j].CurrentMVP; }

                Abstraceunit temp = want[i];
                want[i] = want[needInd];
                want[needInd] = temp;
            }
            _units = want;
        }



        public Battlefield()
        {
            _units = new List<Abstraceunit>();
        }
        public List<Abstraceunit> getUnits { get { return _units; } }

        public void addUnit(Abstraceunit newUnit) { _units.Add(newUnit); LOGS.Add(String.Format("{0} joined the battle\t(for team {1})", newUnit.NameFull, newUnit.getTeamNumber)); }

        public void CalculateMovementForEachCharacter()
        {
            List<Point> currentUnitPoses = new List<Point>();
            for (int i = 0; i < _units.Count; i++)
                currentUnitPoses.Add(_units[i].GetPosition);


            SORTUNITS(true);
            for (int i = 0; i < _units.Count; i++)
            {
                Abstraceunit unit = _units[i];
                List<Point> unitCanGo = Calculator.deleteListFromList(Calculator.GetOblast(currentUnitPoses[i], unit.GetSpd, false, true), currentUnitPoses.ToArray());
                unitCanGo.Add(_units[i].GetPosition);
                if (unitCanGo.Count == 0)
                    continue;
                List<Prio> unitThinks = unit.CalculateSituation(this);
                Point decided = Calculator.CalculateMovement(unitCanGo, unitThinks);
                currentUnitPoses[i] = decided;
                _units[i].MoveTo(decided);
            }


            for (int i = 0; i < _units.Count; i++)
                try
                {
                    // attack
                    for (int att = 0; att < _units[i].CurrentATP; att++)
                    {
                        if (i == _units.Count - 1)
                            Console.WriteLine(_units[i].NameFull);
                        Abstraceunit decideAttack = _units[i].CalculateAttack(getEnemyesInObl(_units[i].GetPosition, _units[i].CurrentAttackRange, true, _units[i].getTeamNumber));
                        if (decideAttack != null)
                        {
                            _units[i].Attack(decideAttack);
                            if (decideAttack.isDead)
                                _units.Remove(decideAttack);
                        }
                    }
                }
                catch (Exception e)
                {
                    LOGS.Add("Unit can't do anything - he is dead. (" + e.Message + ")");
                }
            // end turn
            for (int i = 0; i < _units.Count; i++)
                _units[i].OnTurnEnd();

            Console.Clear();
            List<MapPicture> mp = new List<MapPicture>();
            for (int i = 0; i < _units.Count; i++)
                mp.Add(new MapPicture(_units[i].GetType().Name.ToString()[1], (_units[i].getTeamNumber == 0) ? ConsoleColor.Red : ConsoleColor.Green, _units[i].GetPosition));

            Calculator.TraceBattlefieldToConsole(mp);
        }

        public List<Abstraceunit> getUnitsInObl(Point center, int rad, bool isAttack, int teamNumber)
        {
            List<Abstraceunit> res = new List<Abstraceunit>();
            for (int i = 0; i < _units.Count; i++)
                if ((_units[i]).DistanceTo(center, !isAttack) <= rad && _units[i].getTeamNumber == teamNumber)
                    res.Add(_units[i]);
            return res;
        }
        public List<Abstraceunit> getEnemyesInObl(Point center, int rad, bool isAttack, int friendTeamNumber)
        {
            List<Abstraceunit> res = new List<Abstraceunit>();
            for (int i = 0; i < _units.Count; i++)
                if ((_units[i]).DistanceTo(center, !isAttack) <= rad && _units[i].getTeamNumber != friendTeamNumber)
                    res.Add(_units[i]);
            return res;
        }

        public int SummHPteam(int team)
        {
            int res = 0;
            for (int i = 0; i < _units.Count; i++)
                if (_units[i].getTeamNumber == team)
                    res += _units[i].getCurrentHP;
            return res;
        }

        public void TraceHealth()
        {
            int Pad = 30,
                maxWidCount = (Console.WindowWidth - 20) / Pad;

            for (int T = 1; T > 0; T--)
            {
                Console.WriteLine(); //if (T == 1) Console.ForegroundColor = ConsoleColor.DarkGreen; else Console.ForegroundColor = ConsoleColor.DarkRed;
                int curUnit = 0;
                List<Abstraceunit> team = new List<Abstraceunit>();
                for (int j = 0; j < _units.Count; j++)
                    if (_units[j].getTeamNumber == T)
                        team.Add(_units[j]);


                for (int j = 0; j <= team.Count / maxWidCount; j++)
                {
                    int need = Math.Min(team.Count - curUnit, maxWidCount);
                    //Console.WriteLine(need);
                    string resLine = "";
                    for (int i = 0; i < need; i++)
                        resLine += team[curUnit + i].NameFull.PadRight(Pad);
                    Console.WriteLine(resLine);
                    resLine = "";
                    for (int i = 0; i < need; i++)
                        resLine += team[curUnit + i].TraceBar(true).PadRight(Pad);
                    Console.WriteLine(resLine);
                    resLine = "";
                    for (int i = 0; i < need; i++)
                        resLine += team[curUnit + i].TraceBar(false).PadRight(Pad);
                    Console.WriteLine(resLine);

                    curUnit += maxWidCount;
                }
            }
            Console.WriteLine();
        }
    }
}
