using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview.Army
{
    public class ArmyUnits : MonoBehaviour
    {
        [SerializeField]
        private List<UnitPresenter> army = new List<UnitPresenter>();
        public List<UnitPresenter> Army => army;

        public void AddUnit(UnitPresenter unit)
        {
            army.Add(unit);
        }

        public void RemoveUnit(UnitPresenter unit)
        {
            army.Remove(unit);
        }

        public bool IsDefeated()
        {
            return army.Count == 0;
        }
    }
}
