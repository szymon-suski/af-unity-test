using UnityEngine;

namespace AFSInterview.Army
{
    public class FactoryUnit : MonoBehaviour
    {
        [SerializeField]
        protected UnitTypeEnum unitType;
        public UnitTypeEnum UnitType => unitType;

        [SerializeField]
        protected UnitPresenter prefab;

        public virtual UnitPresenter CreateInstance(Transform parent)
        {
            var inst = Instantiate(prefab, parent);
            return inst;
        }
    }
}
