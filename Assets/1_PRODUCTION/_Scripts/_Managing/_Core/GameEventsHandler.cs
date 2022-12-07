using System;

using AKB.Core.Managing.LevelLoading;

namespace AKB.Core.Managing.GameEvents
{
    [Serializable]
    public class GameEventsHandler
    {
        public event Action<GameScenes> onSceneChanged;
        public void OnSceneChanged(GameScenes activeScene)
        {
            if (onSceneChanged != null)
                onSceneChanged(activeScene);
        }

        public event Action onEnemyHit;
        public void OnEnemyHit()
        {
            if (onEnemyHit != null)
                onEnemyHit();
        }
    }
}
