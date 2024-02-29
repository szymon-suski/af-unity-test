namespace AFSInterview.Army
{
    public interface UnitStrategy
    {
        public Unit GetUnitToAttack(ArmyUnits enemyArmy);
    }
}
