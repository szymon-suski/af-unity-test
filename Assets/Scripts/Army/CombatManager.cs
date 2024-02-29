using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview.Army
{
    using AFSInterview.GameMode;

    public class CombatManager : GameMode
    {
        [SerializeField]
        private UnitsSO unitsSO;

        [SerializeField]
        private ArmyUnits army1;

        [SerializeField]
        private ArmyUnits army2;

        [SerializeField]
        private CreatorUnits creatorUnits;

        [SerializeField]
        private float delayBetweenAttacks = 3f;

        private bool gameStarted = false;
        private bool army1Turn;
        private Coroutine battelCoroutine;

        public override void DisableMode()
        {

        }

        public override void EnableMode()
        {
            if (gameStarted)
            {
                // Return
                return;
            }

            PrepareBatteField();
        }

        private void PrepareBatteField()
        {
            army1.CreateArmy(unitsSO.FirstArmy);
            army2.CreateArmy(unitsSO.SecondArmy);

            // Randomize who starts
            army1Turn = Random.Range(0, 100) >= 50;

            battelCoroutine = StartCoroutine(Battle());
        }

        private IEnumerator Battle()
        {
            if (army1Turn)
            {
                Attack(army1, army2);
            }
            else
            {
                Attack(army2, army1);
            }

            yield return new WaitForSeconds(delayBetweenAttacks);

            army1.UpdateAttackIntervalInArmy();
            army2.UpdateAttackIntervalInArmy();
            army1Turn = !army1Turn;

            if (army1.IsDefeated())
            {
                Debug.Log($"{army2.name} WON!");
                yield break;
            }
            else if (army2.IsDefeated())
            {
                Debug.Log($"{army1.name} WON!");
                yield break;
            }

            battelCoroutine = StartCoroutine(Battle());
        }

        private void Attack(ArmyUnits attackArmy, ArmyUnits targetArmy)
        {
            var attackUnitPresenter = attackArmy.GetAttackUnit();
            if (attackUnitPresenter == null)
            {
                Debug.Log($"Any of units aren't available in {attackArmy.name}");
                return;
            }

            var attackUnit = attackUnitPresenter.GetUnit();
            var bestOpponent = attackUnit.GetBestUnitAttributeToAttack();

            var strategyDefault = new DefaultStrategy();
            UnitPresenter targetUnit = strategyDefault.GetUnitToAttack(targetArmy);

            switch (bestOpponent)
            {
                case UnitAttributes.Light:
                    var strategyLight = new PrioritizeLightStrategy();
                    targetUnit = strategyLight.GetUnitToAttack(targetArmy);
                    break;
                case UnitAttributes.Armored:
                    var strategyArmored = new PrioritizeArmoredStrategy();
                    targetUnit = strategyArmored.GetUnitToAttack(targetArmy);
                    break;
                case UnitAttributes.Mechanical:
                    var strategyMechanical = new PrioritizeMechanicalStrategy();
                    targetUnit = strategyMechanical.GetUnitToAttack(targetArmy);
                    break;
            }

            var attackDamage = attackUnit.GetAttackDamage(targetUnit.GetUnit());
            var damageDeal = Mathf.Max(1, attackDamage - targetUnit.GetUnit().ArmorPoints);

            Debug.Log($"Unit from {attackArmy.name} of type {attackUnit.UnitType} attacks unit of army {targetArmy.name} of type {targetUnit.GetUnit().UnitType} and deal {damageDeal} pts damage");

            if (targetUnit.GetUnit().ReceiveDamage(damageDeal))
            {
                targetArmy.RemoveUnit(targetUnit);
                targetUnit.DestroyUnit();
                Debug.Log($"Unit from {targetArmy.name} of type {targetUnit.GetUnit().UnitType} dies");
            }
        }
    }
}
