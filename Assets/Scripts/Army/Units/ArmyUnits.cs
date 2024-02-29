using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview.Army
{
    public class ArmyUnits : MonoBehaviour
    {
        [SerializeField]
        private CreatorUnits creatorUnits;

        [SerializeField]
        private ArmyPositions armyPositions;

        [SerializeField]
        private UnitsSO unitsSO;

        private List<UnitPresenter> army;
        public List<UnitPresenter> Army => army;

        public void CreateArmy(List<UnitTypeEnum> unitTypeEnum)
        {
            army = new List<UnitPresenter>();

            foreach (var unitType in unitTypeEnum)
            {
                var position = armyPositions.GetNextPosition();
                var unit = unitsSO.Units.Find(x => x.UnitType == unitType);
                if (unit == null || position == null)
                {
                    Debug.LogError($"Couldn't find empty position or unit! On {gameObject.name}");
                    break;
                }

                var inst = creatorUnits.CreateUnit(unit, position);
                army.Add(inst);
            }
        }

        public UnitPresenter GetAttackUnit()
        {
            // Find unit in army that didn't wait for attack
            return army.Find(x => x.GetUnit().CanAttack());
        }

        public void RemoveUnit(UnitPresenter unit)
        {
            army.Remove(unit);
        }

        public bool IsDefeated()
        {
            return army.Count == 0;
        }

        public void UpdateAttackIntervalInArmy()
        {
            foreach (var unit in army)
            {
                unit.GetUnit().UpdateAttackInterval();
            }
        }
    }
}
