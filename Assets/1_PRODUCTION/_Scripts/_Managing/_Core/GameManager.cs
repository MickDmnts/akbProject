using AKB.Core.Database;
using UnityEngine;

namespace AKB.Core.Managing
{
    /// <summary>
    /// All the states the games can be in.
    /// </summary>
    public enum GameState
    {
        ENTRY,
        RUNNING,
        PAUSED,
        EXITING,
    }

    /// <summary>
    /// The Game Manager of the game.
    /// <para>Singleton present</para>
    /// </summary>
    [DefaultExecutionOrder(-450)]
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// Gives access to the games' database methods.
        /// </summary>
        SQLiteHandler _database;
        public SQLiteHandler Database => _database;

        /// <summary>
        /// Game manager Singleton.
        /// </summary>
        static GameManager _s;
        /// <summary>
        /// Get the GameManager singleton.
        /// </summary>
        public static GameManager GetManager => _s;

        /// <summary>
        /// The Manager Hub instance.
        /// </summary>
        ManagerHUB _managerHUB;
        /// <summary>
        /// Get the manager hub instance
        /// </summary>
        public ManagerHUB ManagerHUB => _managerHUB;

        /// <summary>
        /// The current game state of the game.
        /// </summary>
        private GameState _state = GameState.ENTRY;
        /// <summary>
        /// Get the current game state of the game.
        /// </summary>
        public GameState GetGameState => _state;

        /// <summary>
        /// Set the current game state of the game.
        /// </summary>
        /// <param name="state">The current game state</param>
        public void SetGameState(GameState state) => _state = state;

        private void Awake()
        {
            if (_s == null)
            {
                _s = this;

                _database = new SQLiteHandler();
                _managerHUB = new ManagerHUB();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}