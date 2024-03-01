using System.Linq;
using UnityEngine;

namespace AFSInterview.Army
{
    /// <summary>
    /// Strategy class that returns light unit from enemy army.
    /// In case of not existing return random unit.
    /// </summary>
    public class PrioritizeLightStrategy : IUnitStrategy
    {
        public UnitPresenter GetUnitToAttack(ArmyUnits enemyArmy)
        {
            var enemy = enemyArmy.Army.Find(e => e.GetUnit().Attributes.Any(a => a == UnitAttributesEnum.Light));

            if (enemy == null)
            {
                enemy = enemyArmy.Army[Random.Range(0, enemyArmy.Army.Count)];
            }

            return enemy;
        }
    }
}
