using UnityEngine;

namespace AFSInterview.Army
{
    /// <summary>
    /// Base factory class for units.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FactoryUnit<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField]
        protected UnitTypeEnum unitType;
        public UnitTypeEnum UnitType => unitType;

        [SerializeField]
        protected T prefab;

        /// <summary>
        /// Returns create instance that is spawned under parent.
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public virtual T CreateInstance(Transform parent)
        {
            var inst = Instantiate(prefab, parent);
            return inst;
        }
    }
}
