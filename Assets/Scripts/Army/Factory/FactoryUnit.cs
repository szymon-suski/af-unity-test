using UnityEngine;

namespace AFSInterview.Army
{
    public class FactoryUnit<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField]
        protected UnitTypeEnum unitType;
        public UnitTypeEnum UnitType => unitType;

        [SerializeField]
        protected T prefab;

        public virtual T CreateInstance(Transform parent)
        {
            var inst = Instantiate(prefab, parent);
            return inst;
        }
    }
}
