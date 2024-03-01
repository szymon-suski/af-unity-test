using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AFSInterview.Army
{
    /// <summary>
    /// Class that holds connection between UnitAttributes and overrideDamage.
    /// </summary>
    [Serializable]
    public class AttackDamageOverride
    {
        public UnitAttributesEnum unitAttributes;
        public float overrideDamage;
    }

    /// <summary>
    /// Base unit class that specify all unit settings.
    /// </summary>
    [Serializable]
    public class Unit
    {
        [SerializeField]
        private UnitTypeEnum unitType;
        public UnitTypeEnum UnitType => unitType;

        [SerializeField]
        private List<UnitAttributesEnum> attributes;
        public List<UnitAttributesEnum> Attributes => attributes;

        [SerializeField]
        private float healthPoints;
        public float HealthPoints => healthPoints;

        [SerializeField]
        private float armorPoints;
        public float ArmorPoints => armorPoints;

        [SerializeField]
        private int attackInterval;
        public int AttackInterval => attackInterval;

        [SerializeField]
        private float attackDamage;
        public float AttackDamage => attackDamage;

        [SerializeField]
        private List<AttackDamageOverride> attackOverrides;
        public List<AttackDamageOverride> AttackDamageOverrides => attackOverrides;

        [SerializeField]
        private UnitStrategyEnum unitStrategy;
        public UnitStrategyEnum UnitStrategyEnum => unitStrategy;

        protected IUnitStrategy strategy;
        public IUnitStrategy Strategy => strategy;

        protected bool isInArmy1;
        public bool IsInArmy1 => isInArmy1;

        protected int currentAttackInterval = 0;

        public Unit(UnitTypeEnum unitType, List<UnitAttributesEnum> attributes, float healthPoints, float armorPoints, int attackInterval, float attackDamage, List<AttackDamageOverride> attackOverrides, UnitStrategyEnum unitStrategy, bool isInArmy1)
        {
            this.unitType = unitType;
            this.attributes = attributes;
            this.healthPoints = healthPoints;
            this.armorPoints = armorPoints;
            this.attackInterval = attackInterval;
            this.attackDamage = attackDamage;
            this.attackOverrides = attackOverrides;
            this.unitStrategy = unitStrategy;
            this.isInArmy1 = isInArmy1;
            this.currentAttackInterval = 0;
        }

        /// <summary>
        /// Set behavior strategy for this unit.
        /// </summary>
        /// <param name="unitStrategy"></param>
        public void SetStrategy(IUnitStrategy unitStrategy)
        {
            strategy = unitStrategy;
        }

        /// <summary>
        /// Calculate damage that will deal this unit to target unit.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public float GetAttackDamage(Unit target)
        {
            // Sets attack interval so unit can attack only on currentAttackInterval is 0.
            currentAttackInterval = attackInterval;
             
            // Base damage
            var damage = attackDamage;

            // Try to find override attack damage.
            var attackOverride = attackOverrides.Where(x => target.Attributes
                                                .Contains(x.unitAttributes))
                                                .OrderByDescending(x => x.overrideDamage)
                                                .FirstOrDefault();
            if (attackOverride != null)
            {
                damage = attackOverride.overrideDamage;
            }

            return damage;
        }

        /// <summary>
        /// Remove damage points from health points.
        /// </summary>
        /// <param name="damage"></param>
        /// <returns>True if unit has 0 health points and false if not.</returns>
        public virtual bool ReceiveDamage(float damage)
        {
            healthPoints = Mathf.Max(0, healthPoints - damage);
            return healthPoints - damage <= 0;
        }

        /// <summary>
        /// Update attack interval for next turn.
        /// </summary>
        public virtual void UpdateAttackInterval()
        {
            if (currentAttackInterval > 0)
            {
                currentAttackInterval--;
            }
        }

        /// <summary>
        /// Returns if unit can attack in this turn.
        /// </summary>
        /// <returns></returns>
        public bool CanAttack()
        {
            return currentAttackInterval == 0;
        }
    }
}
