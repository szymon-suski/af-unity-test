using System.Linq;
using UnityEngine;

namespace AFSInterview.Army
{
    public class PrioritizeMechanicalStrategy : IUnitStrategy
    {
        public UnitPresenter GetUnitToAttack(ArmyUnits enemyArmy)
        {
            var enemy = enemyArmy.Army.Find(e => e.GetUnit().Attributes.Any(a => a == UnitAttributesEnum.Mechanical));

            if (enemy == null)
            {
                enemy = enemyArmy.Army[Random.Range(0, enemyArmy.Army.Count)];
            }

            return enemy;
        }
    }
}
