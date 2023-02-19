using System;
using UnityEngine;
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
        public event Action onSceneLoaded;
        /// <summary>
        /// Call to notify the onSceneLoaded callbacks.
        /// </summary>
        public void OnSceneLoaded()
        {
            if (onSceneLoaded != null)
            {
                onSceneLoaded();
            }
        }

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

        public event Action onFadeOut;
        public void OnFadeOut()
        {
            if (onFadeOut != null)
            {
                onFadeOut();
            }
        }

        public event Action onFadeIn;
        public void OnFadeIn()
        {
            if (onFadeIn != null)
            {
                onFadeIn();
            }
        }

        /// <summary>
        /// Add to this event to get notified when it's time to save.
        /// </summary>
        public event Action onSaveInitialized;
        /// <summary>
        /// Call to notify the onSaveInitialized callbacks.
        /// </summary>
        public void OnSaveInitialized()
        {
            if (onSaveInitialized != null)
            {
                onSaveInitialized();
            }
        }

        /// <summary>
        /// Add to this event to get notified when the player gets in the hub.
        /// </summary>
        public event Action onPlayerHubEntry;
        /// <summary>
        /// Call to notify the onPlayerHubEntry callbacks.
        /// </summary>
        public void OnPlayerHubEntry()
        {
            if (onPlayerHubEntry != null)
            {
                onPlayerHubEntry();
            }
        }

        /// <summary>
        /// Add to this event to get notified when a new room gets placed.
        /// </summary>
        public event Action onNextRoomEntry;
        /// <summary>
        /// Call to notify the onNextRoomEntry callbacks.
        /// </summary>
        public void OnNextRoomEntry()
        {
            if (onNextRoomEntry != null)
            {
                onNextRoomEntry();
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
        /// Add to this event to get notified when the player must receive coins
        /// </summary>
        public event Action<int> onCoinReceive;
        /// <summary>
        /// Call to notify onCoinReceive callbacks.
        /// </summary>
        public void OnCoinReceive(int coinValue)
        {
            if (onCoinReceive != null)
            {
                onCoinReceive(coinValue);
            }
        }

        /// <summary>
        /// Add to this event to get notified when all the room enemies die
        /// </summary>
        public event Action onRoomClear;
        /// <summary>
        /// Call to notify onRoomClear callbacks.
        /// </summary>
        public void OnRoomClear()
        {
            if (onRoomClear != null)
            {
                onRoomClear();
            }
        }

        /// <summary>
        /// Add to this event to get notified when an enemy dies.
        /// </summary>
        public event Action<int> onEnemyEntryUpdate;
        /// <summary>
        /// Call to notify onEnemyEntryUpdate callbacks.
        /// </summary>
        public void OnEnemyEntryUpdate(int enemyID)
        {
            if (onEnemyEntryUpdate != null)
            {
                onEnemyEntryUpdate(enemyID);
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
        public event Action<float, float> onPlayerHealthChange;
        /// <summary>
        /// Call to notify onPlayerHealthChange callbacks.
        /// </summary>
        public void OnPlayerHealthChange(float value, float maxValue)
        {
            if (onPlayerHealthChange != null)
            {
                onPlayerHealthChange(value, maxValue);
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

        /// <summary>
        /// Add to this event to get notified when Astaroth enters his first phase.
        /// </summary>
        public event Action onAstarothFirstPhase;
        /// <summary>
        /// Call to notify onAstarothFirstPhase callbacks.
        /// </summary>
        public void OnAstarothFirstPhase()
        {
            if (onAstarothFirstPhase != null)
            {
                onAstarothFirstPhase();
            }
        }

        /// <summary>
        /// Add to this event to get notified when Astaroth enters his second phase.
        /// </summary>
        public event Action onAstarothSecondPhase;
        /// <summary>
        /// Call to notify onAstarothSecondPhase callbacks.
        /// </summary>
        public void OnAstarothSecondPhase()
        {
            if (onAstarothSecondPhase != null)
            {
                onAstarothSecondPhase();
            }
        }

        /// <summary>
        /// Add to this event to get notified when an astaroth rock breaks.
        /// </summary>
        public event Action<int> onRockBroken;
        /// <summary>
        /// Call to notify onRockBroken callbacks.
        /// </summary>
        public void OnRockBroken(int value)
        {
            if (onRockBroken != null)
            {
                onRockBroken(value);
            }
        }

        /// <summary>
        /// Add to this event to get notified when all astaroth guardian stones are broken.
        /// </summary>
        public event Action onAllRocksBroken;
        /// <summary>
        /// Call to notify onRockBroken callbacks.
        /// </summary>
        public void OnAllRocksBroken()
        {
            if (onAllRocksBroken != null)
            {
                onAllRocksBroken();
            }
        }

        /// <summary>
        /// Add to this event to get notified when Astaroth enters his third phase.
        /// </summary>
        public event Action onAstarothThirdPhase;
        /// <summary>
        /// Call to notify onAstarothThirdPhase callbacks.
        /// </summary>
        public void OnAstarothThirdPhase()
        {
            if (onAstarothThirdPhase != null)
            {
                onAstarothThirdPhase();
            }
        }

        /// <summary>
        /// Add to this event to get notified when Astaroth dies.
        /// </summary>
        public event Action onAstarothDeath;
        /// <summary>
        /// Call to notify onAstarothDeath callbacks.
        /// </summary>
        public void OnAstarothDeath()
        {
            if (onAstarothDeath != null)
            {
                onAstarothDeath();
            }
        }


        /// <summary>
        /// Add to this event to get notified when Hells Grimoire panel opens.
        /// </summary>
        public event Action onHellsGrimoirePanelOpen;
        /// <summary>
        /// Call to notify onHellsGrimoirePanelOpen callbacks.
        /// </summary>
        public void OnHellsGrimoirePanelOpen()
        {
            if (onHellsGrimoirePanelOpen != null)
            {
                onHellsGrimoirePanelOpen();
            }
        }

        /// <summary>
        /// Add to this event to get notified when Hells Grimoire panel opens.
        /// </summary>
        public event Action onTutorialButtonPanelOpen;
        /// <summary>
        /// Call to notify onHellsGrimoirePanelOpen callbacks.
        /// </summary>
        public void OnTutorialButtonPanelOpen()
        {
            if (onTutorialButtonPanelOpen != null)
            {
                onTutorialButtonPanelOpen();
            }
        }

        /// <summary>
        /// Add to this event to get notified when Hells Grimoire panel opens.
        /// </summary>
        public event Action onMonsterButtonPanelOpen;
        /// <summary>
        /// Call to notify onHellsGrimoirePanelOpen callbacks.
        /// </summary>
        public void OnMonsterButtonPanelOpen()
        {
            if (onMonsterButtonPanelOpen != null)
            {
                onMonsterButtonPanelOpen();
            }
        }
    }
}
