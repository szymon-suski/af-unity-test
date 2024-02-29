namespace AFSInterview.Army
{
    public interface UnitStrategy
    {
        public UnitPresenter GetUnitToAttack(ArmyUnits enemyArmy);
    }
}
