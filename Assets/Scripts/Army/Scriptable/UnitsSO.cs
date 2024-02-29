using UnityEngine;

namespace AFSInterview.Army
{
    [CreateAssetMenu(fileName = "UnitSO", menuName = "Army/Units")]
    public class UnitsSO : ScriptableObject
    {
        [Header("Units Settings")]
        [SerializeField]
        private LongSwordKnightUnit longSwordKnightUnit;
        public LongSwordKnightUnit LongSwordKnightUnit => longSwordKnightUnit;

        [SerializeField]
        private ArcherUnit archerUnit;
        public ArcherUnit ArcherUnit => archerUnit;

        [SerializeField]
        private CatapultUnit catapultUnit;
        public CatapultUnit CatapultUnit => catapultUnit;

        [SerializeField]
        private DruidUnit druidUnit;
        public DruidUnit DruidUnit => druidUnit;

        [SerializeField]
        private RamUnit ramUnit;
        public RamUnit RamUnit => ramUnit;
    }
}
