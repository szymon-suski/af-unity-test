namespace AFSInterview.Army
{
    public interface IUnitStrategy
    {
        public UnitPresenter GetUnitToAttack(ArmyUnits enemyArmy);
    }
}
