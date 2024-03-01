using UnityEngine;

namespace AFSInterview.Army
{
    public class DefaultStrategy : IUnitStrategy
    {
        public UnitPresenter GetUnitToAttack(ArmyUnits enemyArmy)
        {
            return enemyArmy.Army[Random.Range(0, enemyArmy.Army.Count)];
        }
    }
}
