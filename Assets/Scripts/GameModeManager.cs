using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview.GameMode
{
    /// <summary>
    /// List of available game modes.
    /// </summary>
    public enum GameModeType
    {
        Items,
        Army
    }

    /// <summary>
    /// Handles enabling/disabling game modes.
    /// </summary>
    public class GameModeManager : MonoBehaviour
    {
        [SerializeField]
        private List<GameMode> gameModeList;

        [SerializeField]
        private GameMode defaultGameMode;

        private GameModeType gameMode = GameModeType.Items;
        private GameMode activeGameMode;

        private void Start()
        {
            // Disable all game modes.
            gameModeList.ForEach(gameMode => { gameMode.DisableMode(); });

            if (defaultGameMode == null)
            {
                Debug.LogError($"Default game mode must be set on {gameObject.name}!");
                return;
            }

            gameMode = defaultGameMode.GameModeType;
            activeGameMode = defaultGameMode;
            activeGameMode.EnableMode();

            Debug.Log($"Enable game mode {activeGameMode.GameModeType}");
        }

        /// <summary>
        /// Called by button to change game mode.
        /// </summary>
        public void OnChangeGameMode()
        {
            // Do nothing if list is empty
            if (gameModeList.Count < 1 || defaultGameMode == null) return;

            // Change game mode.
            gameMode = gameMode == GameModeType.Items ? GameModeType.Army : GameModeType.Items;

            // Find new game mode manager.
            var newGameMode = GetGameMode(gameMode);

            if (newGameMode == null)
            {
                // Can't find new game mode go back to previous one.
                gameMode = activeGameMode.GameModeType;
                Debug.Log($"Couldn't find other game mode. Active one is {activeGameMode.GameModeType}");
                return;
            }

            // Disable previous mode
            activeGameMode.DisableMode();

            // Assign new game mode.
            activeGameMode = newGameMode;

            // Enable new game mode.
            activeGameMode.EnableMode();

            Debug.Log($"Enable game mode {activeGameMode.GameModeType}");
        }

        private GameMode GetGameMode(GameModeType gameModeType)
        {
            return gameModeList.Find(x => x.GameModeType == gameModeType);
        }
    }
}
