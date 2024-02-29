using System.Linq;
using UnityEngine;

namespace AFSInterview.Army
{
    public class PrioritizeMechanicalStrategy : UnitStrategy
    {
        public Unit GetUnitToAttack(ArmyUnits enemyArmy)
        {
            var enemy = enemyArmy.Army.Find(e => e.Attributes.Any(a => a == UnitAttributes.Mechanical));
            
            if (enemy == null)
            {
                enemy = enemyArmy.Army[Random.Range(0, enemyArmy.Army.Count)];
            }
            
            return enemy;
        }
    }
}
