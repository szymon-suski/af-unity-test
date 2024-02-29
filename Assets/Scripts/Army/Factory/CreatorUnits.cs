using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview.Army
{
    public class CreatorUnits : MonoBehaviour
    {
        [SerializeField]
        private List<FactoryUnit> factoryUnits;

        public UnitPresenter CreateUnit(Unit unit, Transform parent)
        {
            var factory = factoryUnits.Find(x => x.UnitType == unit.UnitType);

            if (factory == null)
            {
                factory = factoryUnits[0];
                Debug.LogWarning($"Couldn't find factory for {unit.UnitType} in {gameObject.name}");
            }

            var instance = factory.CreateInstance(parent);
            instance.SetUnit(unit);
            return instance;
        }
    }
}
