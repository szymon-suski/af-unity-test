using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview.Army
{
    /// <summary>
    /// Contains all army units and handles operation on them.
    /// </summary>
    public class ArmyUnits : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private CreatorUnits creatorUnits;

        [SerializeField]
        private ArmyPositions armyPositions;

        [Header("Scriptable")]
        [SerializeField]
        private UnitsSO unitsSO;

        private List<UnitPresenter> army;
        public List<UnitPresenter> Army => army;

        /// <summary>
        /// Create army from given list of unit types.
        /// </summary>
        /// <param name="unitTypeEnum"></param>
        /// <param name="isInArmy1"></param>
        public void CreateArmy(List<UnitTypeEnum> unitTypeEnum, bool isInArmy1)
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

                var inst = creatorUnits.CreateUnit(unit, position, isInArmy1);
                army.Add(inst);
            }
        }

        /// <summary>
        /// Remove unit from army.
        /// </summary>
        /// <param name="unit"></param>
        public void RemoveUnit(UnitPresenter unit)
        {
            army.Remove(unit);
        }

        /// <summary>
        /// Returns true if army count is 0.
        /// </summary>
        /// <returns></returns>
        public bool IsDefeated()
        {
            return army.Count == 0;
        }

        /// <summary>
        /// Update all army units attack interval.
        /// </summary>
        public void UpdateAttackIntervalInArmy()
        {
            foreach (var unit in army)
            {
                unit.GetUnit().UpdateAttackInterval();
            }
        }

        /// <summary>
        /// Destroy all army units and clear army.
        /// </summary>
        public void ClearArmy()
        {
            if (army.Count < 1) return;
            foreach (var unit in army)
            {
                unit.DestroyUnit();
            }
            army.Clear();
        }
    }
}
