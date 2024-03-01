using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AFSInterview.Army
{
    /// <summary>
    /// Contains list of possible position for army.
    /// </summary>
    public class ArmyPositions : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> positions = new List<Transform>();

        /// <summary>
        /// Returns next free position. If there isn't any return null.
        /// </summary>
        /// <returns></returns>
        public Transform GetNextPosition()
        {
            return positions.FirstOrDefault(x => x.transform.childCount == 0);
        }
    }
}
