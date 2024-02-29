using UnityEngine;

namespace AFSInterview.Army
{
    public class UnitPresenter : MonoBehaviour
    {
        [SerializeField]
        private Unit unit;

        public void SetUnit(Unit unit)
        {
            this.unit = unit;
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
