using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview.Army
{
    public class CreatorUnits : MonoBehaviour
    {
        [SerializeField]
        private List<FactoryUnit<UnitPresenter>> factoryUnits;

        public UnitPresenter CreateUnit(Unit unit, Transform parent, bool isInArmy1)
        {
            var factory = factoryUnits.Find(x => x.UnitType == unit.UnitType);

            if (factory == null)
            {
                factory = factoryUnits[0];
                Debug.LogWarning($"Couldn't find factory for {unit.UnitType} in {gameObject.name}");
            }

            var instance = factory.CreateInstance(parent);
            instance.SetUnit(unit, isInArmy1);
            instance.SetStrategyUnit(GetStrategy(unit.UnitStrategyEnum));
            return instance;
        }

        private IUnitStrategy GetStrategy(UnitStrategyEnum strategyEnum)
        {
            switch (strategyEnum)
            {
                case UnitStrategyEnum.None:
                    return new DefaultStrategy();
                case UnitStrategyEnum.PrioritizeArmored:
                    return new PrioritizeArmoredStrategy();
                case UnitStrategyEnum.PrioritizeLight:
                    return new PrioritizeLightStrategy();
                case UnitStrategyEnum.PrioritizeMechanical:
                    return new PrioritizeMechanicalStrategy();
                default:
                    Debug.Log($"Couldn't find script for strategy {strategyEnum}! Return default");
                    return new DefaultStrategy();
            }
        }
    }
}
