namespace AFSInterview.Army
{
    /// <summary>
    /// Base strategy interface.
    /// </summary>
    public interface IUnitStrategy
    {
        /// <summary>
        /// Returns best unit to attack based on strategy behavior.
        /// </summary>
        /// <param name="enemyArmy"></param>
        /// <returns></returns>
        public UnitPresenter GetUnitToAttack(ArmyUnits enemyArmy);
    }
}
