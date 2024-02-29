using UnityEngine;

namespace AFSInterview.Army
{
    public class UnitPresenter : MonoBehaviour
    {
        [SerializeField]
        private Unit unit;

        public void SetUnit(Unit unit)
        {
            this.unit = new Unit(unit.UnitType, unit.Attributes, unit.HealthPoints, unit.ArmorPoints, unit.AttackInterval, unit.AttackDamage, unit.AttackDamageOverrides);
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
