using UnityEngine;

namespace AFSInterview.Army
{
    public class UnitPresenter : MonoBehaviour
    {
        [SerializeField]
        private Unit unit;

        public void SetUnit(Unit unit, bool isInArmy1)
        {
            this.unit = new Unit(unit.UnitType, unit.Attributes, unit.HealthPoints, unit.ArmorPoints, unit.AttackInterval, unit.AttackDamage, unit.AttackDamageOverrides, unit.UnitStrategyEnum, isInArmy1);
        }

        public void SetStrategyUnit(IUnitStrategy strategy)
        {
            unit.SetStrategy(strategy);
        }

        public Unit GetUnit()
        {
            return unit;
        }

        public void DestroyUnit()
        {
            Destroy(gameObject);
        }
    }
}
