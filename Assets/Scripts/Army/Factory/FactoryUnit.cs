using UnityEngine;

namespace AFSInterview.Army
{
    public class FactoryUnit<T> : MonoBehaviour where T : UnitPresenter
    {
        [SerializeField]
        protected T prefab;

        public virtual T CreateInstance()
        {
            var inst = Instantiate(prefab, GetParent());
            return inst;
        }

        protected virtual Transform GetParent()
        {
            return transform;
        }
    }
}
