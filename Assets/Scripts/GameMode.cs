using System;
using UnityEngine;

namespace AFSInterview.GameMode
{
    /// <summary>
    /// Game mode abstract class.
    /// </summary>
    public abstract class GameMode : MonoBehaviour
    {
        /// <summary>
        /// Game mode type.
        /// </summary>
        public GameModeType GameModeType;

        /// <summary>
        /// Call to enable game mode behavior.
        /// </summary>
        public abstract void EnableMode();

        /// <summary>
        /// Call to disable game mode behavior.
        /// </summary>
        public abstract void DisableMode();
    }
}
