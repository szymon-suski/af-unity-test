namespace AFSInterview.Army
{
    using AFSInterview.GameMode;
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    public class CombatManager : GameMode
    {
        [Header("Settings")]
        [SerializeField]
        private float delayBetweenAttacks = 3f;

        [Header("References")]
        [SerializeField]
        private ArmyUnits army1;

        [SerializeField]
        private ArmyUnits army2;

        [SerializeField]
        private CreatorUnits creatorUnits;

        [SerializeField]
        private TextMeshProUGUI battleText;

        [SerializeField]
        private GameObject changeGameModeButton;

        [Header("Scriptable")]
        [SerializeField]
        private UnitsSO unitsSO;

        private bool army1Turn;
        private List<UnitPresenter> turnOrder = new List<UnitPresenter>();

        public override void DisableMode()
        {
            battleText.text = string.Empty;
            battleText.gameObject.SetActive(false);

            army1.ClearArmy();
            army2.ClearArmy();
            army1.gameObject.SetActive(false);
            army2.gameObject.SetActive(false);
        }

        public override void EnableMode()
        {
            changeGameModeButton.SetActive(false);
            battleText.text = string.Empty;
            battleText.gameObject.SetActive(true);
            army1.gameObject.SetActive(true);
            army2.gameObject.SetActive(true);

            PrepareBatteField();
        }

        private void PrepareBatteField()
        {
            army1.CreateArmy(unitsSO.FirstArmy, true);
            army2.CreateArmy(unitsSO.SecondArmy, false);

            turnOrder.AddRange(army1.Army);
            turnOrder.AddRange(army2.Army);

            ShuffleOrder(turnOrder);

            StartCoroutine(Battle());
        }

        private void ShuffleOrder(List<UnitPresenter> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);

                // Swap elements
                var temp = list[i];
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }

        private IEnumerator Battle()
        {
            while(!army1.IsDefeated() && !army2.IsDefeated()) 
            {
                foreach (var unitPre in turnOrder)
                {
                    if (army1.IsDefeated() || army2.IsDefeated()) break;
                    var unit = unitPre.GetUnit();
                    if (unit.CanAttack())
                    {
                        var enemyArmy = unit.IsInArmy1 ? army2 : army1;
                        Attack(unitPre, enemyArmy);

                        yield return new WaitForSeconds(delayBetweenAttacks);

                        army1.UpdateAttackIntervalInArmy();
                        army2.UpdateAttackIntervalInArmy();
                    }
                }

                turnOrder.RemoveAll(x => x == null);
            }
            FinishGame(army1.IsDefeated() ? army2 : army1);
        }

        private void Attack(UnitPresenter attackUnitPresenter, ArmyUnits targetArmy)
        {
            var attackUnit = attackUnitPresenter.GetUnit();

            var targetUnitPresenter = attackUnit.Strategy.GetUnitToAttack(targetArmy);
            var targetUnit = targetUnitPresenter.GetUnit();

            var attackDamage = attackUnit.GetAttackDamage(targetUnit);
            var damageDeal = Mathf.Max(1, attackDamage - targetUnitPresenter.GetUnit().ArmorPoints);

            Debug.Log($"Unit of type <b>{attackUnit.UnitType}</b> attacks unit of army {targetArmy.name} of type <b>{targetUnit.UnitType}</b> and deal <b>{damageDeal} pts</b> damage. Health left {targetUnit.HealthPoints}.");

            if (targetUnit.ReceiveDamage(damageDeal))
            {
                targetArmy.RemoveUnit(targetUnitPresenter);
                targetUnitPresenter.DestroyUnit();
                battleText.text = $"Unit of type <b>{attackUnit.UnitType}</b> attacks unit of army {targetArmy.name} of type <b>{targetUnit.UnitType}</b> and deal <b>{damageDeal} pts</b> damage. No health left! Unit dies!";
                Debug.Log($"Unit from {targetArmy.name} of type {targetUnit.UnitType} dies");
            }
            else
            {
                battleText.text = $"Unit of type <b>{attackUnit.UnitType}</b> attacks unit of army {targetArmy.name} of type <b>{targetUnit.UnitType}</b> and deal <b>{damageDeal} pts</b> damage. Health left {targetUnit.HealthPoints}.";
            }
        }

        private void FinishGame(ArmyUnits winner)
        {
            Debug.Log($"{winner.name} WON!");
            battleText.text = $"{winner.name} <b>WON</b> THE BATTLE!";
            changeGameModeButton.SetActive(true);
        }
    }
}
