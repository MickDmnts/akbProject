using System;

using akb.Core.Managing.LevelLoading;

namespace akb.Core.Managing.GameEvents
{
    /// <summary>
    /// A class containing all game-wide events.
    /// </summary>
    public class GameEventsHandler
    {
        /// <summary>
        /// Add to this event to get notified when a a Focused Scene gets changed.
        /// (Through the SceneLoader)
        /// </summary>
        public event Action<GameScenes> onSceneChanged;
        /// <summary>
        /// Call to notify the onSceneChanged callbacks.
        /// </summary>
        /// <param name="focusedScene">The currently focused scene.</param>
        public void OnSceneChanged(GameScenes focusedScene)
        {
            if (onSceneChanged != null)
            {
                onSceneChanged(focusedScene);
            }
        }

        /// <summary>
        /// Add to this event to get notified when a new game starts.
        /// </summary>
        public event Action<int> onNewGame;
        /// <summary>
        /// Call to notify the onNewGame callbacks.
        /// </summary>
        public void OnNewGame(int saveFileID)
        {
            if (onNewGame != null)
            {
                onNewGame(saveFileID);
            }
        }

        /// <summary>
        /// Add to this event to get notified when a load game starts.
        /// </summary>
        public event Action<int> onLoadGame;
        /// <summary>
        /// Call to notify the onLoadGame callbacks.
        /// </summary>
        public void OnLoadGame(int saveFileID)
        {
            if (onLoadGame != null)
            {
                onLoadGame(saveFileID);
            }
        }

        /// <summary>
        /// Add to this event to get notified when an enemy gets hit.
        /// </summary>
        public event Action onEnemyHit;
        /// <summary>
        /// Call to notify the onEnemyHit callbacks.
        /// </summary>
        public void OnEnemyHit()
        {
            if (onEnemyHit != null)
            {
                onEnemyHit();
            }
        }

        /// <summary>
        /// Add to this event to get notified when an enemy dies.
        /// </summary>
        public event Action onEnemyDeath;
        /// <summary>
        /// Call to notify onEnemyDeath callbacks.
        /// </summary>
        public void OnEnemyDeath()
        {
            if (onEnemyDeath != null)
            {
                onEnemyDeath();
            }
        }

        /// <summary>
        /// Add to this event to get notified when the player gets hit.
        /// </summary>
        public event Action onPlayerHit;
        /// <summary>
        /// Call to notify onPlayerHit callbacks.
        /// </summary>
        public void OnPlayerHit()
        {
            if (onPlayerHit != null)
            {
                onPlayerHit();
            }
        }

        /// <summary>
        /// Add to this event to get notified when the player health changes.
        /// </summary>
        public event Action<float> onPlayerHealthChange;
        /// <summary>
        /// Call to notify onPlayerHealthChange callbacks.
        /// </summary>
        public void OnPlayerHealthChange(float value)
        {
            if (onPlayerHealthChange != null)
            {
                onPlayerHealthChange(value);
            }
        }

        /// <summary>
        /// Add to this event to get notified when the player rage changes.
        /// </summary>
        public event Action<float> onPlayerRageChange;
        /// <summary>
        /// Call to notify onPlayerRageChange callbacks.
        /// </summary>
        public void OnPlayerRageChange(float value)
        {
            if (onPlayerRageChange != null)
            {
                onPlayerRageChange(value);
            }
        }
    }
}
