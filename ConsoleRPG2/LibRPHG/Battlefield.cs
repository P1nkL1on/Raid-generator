﻿using System;
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
        List<IUnit> _units;

        public Battlefield()
        {
            _units = new List<IUnit>();
        }
        public List<IUnit> getUnits { get { return _units; } }

        public void addUnit(IUnit newUnit) { _units.Add(newUnit); LOGS.Add(String.Format("{0} joined the battle\t(for team {1})", newUnit.NameFull, newUnit.GetTeamNumber)); }

        public void CalculateMovementForEachCharacter()
        {
            List<Point> currentUnitPoses = new List<Point>();
            for (int i = 0; i < _units.Count; i++)
                currentUnitPoses.Add(_units[i].GetPosition);

            for (int i = 0; i < _units.Count; i++)
            {
                IUnit unit = _units[i];
                List<Point> unitCanGo = Calculator.deleteListFromList(Calculator.GetOblast(currentUnitPoses[i], unit.GetSpd, false, true), currentUnitPoses.ToArray());
                if (unitCanGo.Count == 0)
                    continue;
                List<Prio> unitThinks = unit.CalculateSituation(this);
                ///
                Point decided = Calculator.CalculateMovement(unitCanGo, unitThinks);
                
                currentUnitPoses[i] = decided;
                _units[i].MoveTo(decided);
            }
            Console.Clear();
            Calculator.TraceBattlefieldToConsole(currentUnitPoses);
        }
    }
}
