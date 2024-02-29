using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AFSInterview.Army
{
    public enum UnitAttributes
    { 
        Light,
        Armored,
        Mechanical
    }

    [Serializable]
    public class AttackDamageOverride
    {
        public UnitAttributes unitAttributes;
        public float overrideDamage;
    }

    [Serializable]
    public class Unit
    {
        [SerializeField]
        private UnitTypeEnum unitType;
        public UnitTypeEnum UnitType => unitType;

        [SerializeField]
        private List<UnitAttributes> attributes;
        public List<UnitAttributes> Attributes => attributes;

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

        protected int currentAttackInterval = 0;

        public float GetAttackDamage(Unit target)
        {
            currentAttackInterval = attackInterval;

            var damage = attackDamage;

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

        public virtual bool ReceiveDamage(float  damage) 
        {
            healthPoints = Mathf.Max(0, healthPoints - damage);
            return healthPoints - damage <= 0;
        }

        public virtual void UpdateAttackInterval()
        {
            if (currentAttackInterval > 0)
            {
                currentAttackInterval--;
            }
        }

        public bool CanAttack()
        {
            return currentAttackInterval == 0;
        }
    }
}
