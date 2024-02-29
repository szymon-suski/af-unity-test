using System;
using System.Collections.Generic;
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
        private List<UnitAttributes> attributes;

        [SerializeField]
        private float healthPoints;

        [SerializeField] 
        private float armorPoints;

        [SerializeField]
        private int attackInterval;

        [SerializeField]
        private float attackDamage;

        [SerializeField]
        private List<AttackDamageOverride> attackOverrides;

        protected int currentAttackInterval = 0;

        public virtual void Attack(Unit target)
        {
        }

        public virtual bool ReceiveDamage(float  damage) 
        {
            return healthPoints - damage <= 0;
        }

        public virtual void UpdateAttackInterval()
        {
            if (currentAttackInterval > 0)
            {
                currentAttackInterval--;
            }
        }
    }
}
