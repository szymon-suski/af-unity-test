using System.Linq;
using UnityEngine;

namespace AFSInterview.Army
{
    public class PrioritizeArmoredStrategy : UnitStrategy
    {
        public Unit GetUnitToAttack(ArmyUnits enemyArmy)
        {
            var enemy = enemyArmy.Army.Find(e => e.GetUnit().Attributes.Any(a => a == UnitAttributes.Armored));

            if (enemy == null)
            {
                enemy = enemyArmy.Army[Random.Range(0, enemyArmy.Army.Count)];
            }

            return enemy.GetUnit();
        }
    }
}
