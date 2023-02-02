using UnityEngine;

using akb.Core.Database;

namespace akb.Core.Managing
{
    /// <summary>
    /// The Game Manager of the game.
    /// <para>Singleton present</para>
    /// </summary>
    [DefaultExecutionOrder(-450)]
    public class GameManager : MonoBehaviour
    {
        private int _activeFileID = -1;
        public int ActiveFileID => _activeFileID;

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

        public void SetActiveFileID(int id) => _activeFileID = id;

        private void Awake()
        {
            if (_s == null)
            {
                _s = this;

                _database = new SQLiteHandler();
                _managerHUB = new ManagerHUB(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}