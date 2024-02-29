using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview.Army
{
    [CreateAssetMenu(fileName = "UnitSO", menuName = "Army/Units")]
    public class UnitsSO : ScriptableObject
    {
        [Header("Units Settings")]
        [SerializeField]
        private List<Unit> units;
        public List<Unit> Units => units;
        
        [Header("Army Settings")]
        [SerializeField]
        private List<UnitTypeEnum> firstArmy;

        [SerializeField]
        private List<UnitTypeEnum> secondArmy;
    }
}
