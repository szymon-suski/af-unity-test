using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview.Army
{
    public class ArmyUnits : MonoBehaviour
    {
        [SerializeField]
        private List<Unit> army;
        public List<Unit> Army => army;

        public void AddUnit(Unit unit)
        {
            army.Add(unit);
        }

        public void RemoveUnit(Unit unit)
        {
            army.Remove(unit);
        }

        public bool IsDefeated()
        {
            return army.Count == 0;
        }
    }
}
