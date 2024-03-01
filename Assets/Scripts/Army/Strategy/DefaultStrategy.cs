using UnityEngine;

namespace AFSInterview.Army
{
    /// <summary>
    /// Default strategy that return random unit.
    /// </summary>
    public class DefaultStrategy : IUnitStrategy
    {
        public UnitPresenter GetUnitToAttack(ArmyUnits enemyArmy)
        {
            return enemyArmy.Army[Random.Range(0, enemyArmy.Army.Count)];
        }
    }
}
