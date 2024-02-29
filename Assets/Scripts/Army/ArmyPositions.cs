using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AFSInterview.Army
{
    public class ArmyPositions : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> positions = new List<Transform>();

        public Transform GetNextPosition()
        {
            return positions.FirstOrDefault(x => x.transform.childCount == 0);
        }
    }
}
