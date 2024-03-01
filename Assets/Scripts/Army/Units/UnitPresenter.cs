using UnityEngine;

namespace AFSInterview.Army
{
    /// <summary>
    /// Class that holds unit and visual of this unit.
    /// </summary>
    public class UnitPresenter : MonoBehaviour
    {
        [SerializeField]
        private Unit unit;

        /// <summary>
        /// Assigning unit.
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="isInArmy1"></param>
        public void SetUnit(Unit unit, bool isInArmy1)
        {
            this.unit = new Unit(unit.UnitType, unit.Attributes, unit.HealthPoints, unit.ArmorPoints, unit.AttackInterval, unit.AttackDamage, unit.AttackDamageOverrides, unit.UnitStrategyEnum, isInArmy1);
        }

        /// <summary>
        /// Assigning strategy behavior.
        /// </summary>
        /// <param name="strategy"></param>
        public void SetStrategyUnit(IUnitStrategy strategy)
        {
            unit.SetStrategy(strategy);
        }

        /// <summary>
        /// Returns unit.
        /// </summary>
        /// <returns></returns>
        public Unit GetUnit()
        {
            return unit;
        }

        /// <summary>
        /// Destroy unit and game object.
        /// </summary>
        public void DestroyUnit()
        {
            Destroy(gameObject);
        }
    }
}
