using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview.Army
{
    /// <summary>
    /// Handles creating prefabs unit.
    /// </summary>
    public class CreatorUnits : MonoBehaviour
    {
        [SerializeField]
        private List<FactoryUnit<UnitPresenter>> factoryUnits;

        /// <summary>
        /// Creates unit prefab under given parent and set up with unit.
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="parent"></param>
        /// <param name="isInArmy1"></param>
        /// <returns></returns>
        public UnitPresenter CreateUnit(Unit unit, Transform parent, bool isInArmy1)
        {
            // Find factory based on unit type.
            var factory = factoryUnits.Find(x => x.UnitType == unit.UnitType);

            // If couldn't find default to the first factory.
            if (factory == null)
            {
                factory = factoryUnits[0];
                Debug.LogWarning($"Couldn't find factory for {unit.UnitType} in {gameObject.name}");
            }

            // Create instance and assign unit and strategy behavior.
            var instance = factory.CreateInstance(parent);
            instance.SetUnit(unit, isInArmy1);
            instance.SetStrategyUnit(GetStrategy(unit.UnitStrategyEnum));
            return instance;
        }

        /// <summary>
        /// Return strategy based of strategy type that unit has.
        /// By default return DefaultStrategy.
        /// </summary>
        /// <param name="strategyEnum"></param>
        /// <returns></returns>
        private IUnitStrategy GetStrategy(UnitStrategyEnum strategyEnum)
        {
            // Return strategy based of strategy type that unit has.
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
