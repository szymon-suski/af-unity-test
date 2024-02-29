using UnityEngine;

namespace AFSInterview.Army
{
    public class DefaultStrategy : UnitStrategy
    {
        public Unit GetUnitToAttack(ArmyUnits enemyArmy)
        {
            return enemyArmy.Army[Random.Range(0, enemyArmy.Army.Count)];
        }
    }
}
