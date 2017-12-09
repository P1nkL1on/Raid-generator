using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MatrixFields;
using System.Drawing;

namespace LibRPHG
{
    public class Battlefield
    {
        List<Abstraceunit> _units;

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

            for (int i = 0; i < _units.Count; i++)
                try
                {
                    Abstraceunit unit = _units[i];
                    List<Point> unitCanGo = Calculator.deleteListFromList(Calculator.GetOblast(currentUnitPoses[i], unit.GetSpd, false, true), currentUnitPoses.ToArray());
                    if (unitCanGo.Count == 0)
                        continue;
                    List<Prio> unitThinks = unit.CalculateSituation(this);
                    /// movement
                    Point decided = Calculator.CalculateMovement(unitCanGo, unitThinks);

                    currentUnitPoses[i] = decided;
                    _units[i].MoveTo(decided);

                    // attack
                    for (int att = 0; att < _units[i].CurrentATP; att++)
                    {
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
            Calculator.TraceBattlefieldToConsole(currentUnitPoses);
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
    }
}
