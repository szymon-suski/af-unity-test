namespace AFSInterview.Army
{
    using AFSInterview.GameMode;
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    /// <summary>
    /// Handles all combat behavior.
    /// </summary>
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

        private List<UnitPresenter> turnOrder = new List<UnitPresenter>();

        public override void DisableMode()
        {
            battleText.text = string.Empty;
            battleText.gameObject.SetActive(false);

            army1.ClearArmy();
            army2.ClearArmy();
            army1.gameObject.SetActive(false);
            army2.gameObject.SetActive(false);
            turnOrder.Clear();
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

        /// <summary>
        /// Prepare battlefield by creating army and turn order.
        /// </summary>
        private void PrepareBatteField()
        {
            // Create both army.
            army1.CreateArmy(unitsSO.FirstArmy, true);
            army2.CreateArmy(unitsSO.SecondArmy, false);

            // Add them to turn order.
            turnOrder.AddRange(army1.Army);
            turnOrder.AddRange(army2.Army);

            // Randomize turn order positions.
            ShuffleOrder(turnOrder);

            // Start battle
            StartCoroutine(Battle());
        }

        /// <summary>
        /// Shuffle list of unit presenters.
        /// </summary>
        /// <param name="list"></param>
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

        /// <summary>
        /// Main battle method.
        /// </summary>
        /// <returns></returns>
        private IEnumerator Battle()
        {
            // Until any of army is defeated go through units in turn order.
            while(!army1.IsDefeated() && !army2.IsDefeated()) 
            {
                foreach (var unitPre in turnOrder)
                {
                    // Turn order didn't finish but army is already defeated
                    if (army1.IsDefeated() || army2.IsDefeated()) break;

                    var unit = unitPre.GetUnit();
                    if (unit.CanAttack())
                    {
                        // Get enemy army and attack unit from that army.
                        var enemyArmy = unit.IsInArmy1 ? army2 : army1;
                        Attack(unitPre, enemyArmy);

                        yield return new WaitForSeconds(delayBetweenAttacks);

                        // Turn finished update attack interval.
                        army1.UpdateAttackIntervalInArmy();
                        army2.UpdateAttackIntervalInArmy();
                    }
                }

                // Remove all dead units from turn order.
                turnOrder.RemoveAll(x => x == null);
            }

            FinishGame(army1.IsDefeated() ? army2 : army1);
        }

        /// <summary>
        /// Handles attacking target enemy units.
        /// </summary>
        /// <param name="attackUnitPresenter"></param>
        /// <param name="targetArmy"></param>
        private void Attack(UnitPresenter attackUnitPresenter, ArmyUnits targetArmy)
        {
            // Get attack unit
            var attackUnit = attackUnitPresenter.GetUnit();

            // Get target unit
            var targetUnitPresenter = attackUnit.Strategy.GetUnitToAttack(targetArmy);
            var targetUnit = targetUnitPresenter.GetUnit();

            // Get attack damage
            var attackDamage = attackUnit.GetAttackDamage(targetUnit);
            var damageDeal = Mathf.Max(1, attackDamage - targetUnitPresenter.GetUnit().ArmorPoints);

            Debug.Log($"Unit of type <b>{attackUnit.UnitType}</b> attacks unit of army {targetArmy.name} of type <b>{targetUnit.UnitType}</b> and deal <b>{damageDeal} pts</b> damage. Health left {targetUnit.HealthPoints}.");

            // Deal damage to target unit.
            if (targetUnit.ReceiveDamage(damageDeal))
            {
                // Unit is dead
                targetArmy.RemoveUnit(targetUnitPresenter);
                targetUnitPresenter.DestroyUnit();
                battleText.text = $"Unit of type <b>{attackUnit.UnitType}</b> attacks unit of army {targetArmy.name} of type <b>{targetUnit.UnitType}</b> and deal <b>{damageDeal} pts</b> damage. No health left! Unit dies!";
                Debug.Log($"Unit from {targetArmy.name} of type {targetUnit.UnitType} dies");
            }
            else
            {
                // Unit is still alive.
                battleText.text = $"Unit of type <b>{attackUnit.UnitType}</b> attacks unit of army {targetArmy.name} of type <b>{targetUnit.UnitType}</b> and deal <b>{damageDeal} pts</b> damage. Health left {targetUnit.HealthPoints}.";
            }
        }

        /// <summary>
        /// Handles finish game settings.
        /// </summary>
        /// <param name="winner"></param>
        private void FinishGame(ArmyUnits winner)
        {
            Debug.Log($"{winner.name} WON!");
            battleText.text = $"{winner.name} <b>WON</b> THE BATTLE!";
            changeGameModeButton.SetActive(true);
        }
    }
}
