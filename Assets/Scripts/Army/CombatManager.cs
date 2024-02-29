using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview.Army
{
    using AFSInterview.GameMode;

    public class CombatManager : GameMode
    {
        [SerializeField]
        private UnitsSO unitsSO;

        [SerializeField]
        private ArmyPositions army1Pos;

        [SerializeField]
        private ArmyPositions army2Pos;

        [SerializeField]
        private CreatorUnits creatorUnits;

        [SerializeField]
        private float delayBetweenAttacks = 3f;

        private bool gameStarted = false;
        private ArmyUnits army1 = new ArmyUnits();
        private ArmyUnits army2 = new ArmyUnits();

        public override void DisableMode()
        {
            
        }

        public override void EnableMode()
        {
            if (gameStarted)
            {
                // Return
                return;
            }

            PrepareBatteField();
        }

        private void PrepareBatteField()
        {
            army1 = CreateArmy(unitsSO.FirstArmy, army1Pos);
            army2 = CreateArmy(unitsSO.SecondArmy, army2Pos);
        }

        private ArmyUnits CreateArmy(List<UnitTypeEnum> unitTypeEnum, ArmyPositions armyPos)
        {
            var army = new ArmyUnits();

            foreach (var unitType in unitTypeEnum)
            {
                var position = armyPos.GetNextPosition();
                var unit = unitsSO.Units.Find(x => x.UnitType == unitType);
                if(unit == null || position == null)
                {
                    Debug.LogError($"Couldn't find empty position or unit! On {gameObject.name}");
                    break;
                }

                var inst = creatorUnits.CreateUnit(unit, position);
                army.AddUnit(inst);
            }
            return army;
        }

    }
}
