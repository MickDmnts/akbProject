using System;

using AKB.Core.Managing.LevelLoading;

namespace AKB.Core.Managing.GameEvents
{
    /// <summary>
    /// A class containing all game-wide events.
    /// </summary>
    [Serializable]
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
                onSceneChanged(focusedScene);
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
                onEnemyHit();
        }
    }
}
