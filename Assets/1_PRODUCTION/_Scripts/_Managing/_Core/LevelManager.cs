using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace akb.Core.Managing.LevelLoading
{
    /// <summary>
    /// All the game scenes enumerated for ease of access, the index MUST be the same as 
    /// the build index.
    /// </summary>
    public enum GameScenes
    {
        AnyScene = -1,

        GameEntryScene = 0,
        UI_Scene = 1,
        PlayerScene = 2,

        TutorialArena = 3,

        PlayerHUB = 4,

        World1Scene = 5,
        World2Scene = 6,
    }

    [DefaultExecutionOrder(-397)]
    public class LevelManager : MonoBehaviour
    {
        /// <summary>
        /// The essential game scenes. These scenes, once loaded, will neven get unloaded.
        /// </summary>
        [Header("Set the desired essential game scenes.")]
        [SerializeField, Tooltip("The essential game scenes. These scenes, once loaded, will neven get unloaded.")] List<GameScenes> essentialScenes;

        /// <summary>
        /// The level packets in order of desired loading. \n Add extra packets at the end of the list for further manual transitioning.
        /// </summary>
        [Header("Populate the list with all the level load packets of the game.")]
        [SerializeField, Tooltip("The level packets in order of desired loading. Add extra packets at the end of the list for further manual transitioning.")]
        List<LevelLoadPacket> levelsInLoadOrder;

        [SerializeField]
        LevelLoadPacket hubPacket;

        /// <summary>
        /// The currently loaded and active scenes.
        /// </summary>
        /// <typeparam name="GameScenes">Game scene enum.</typeparam>
        List<GameScenes> currentlyLoadedScenes = new List<GameScenes>();

        /// <summary>
        /// The levelsInLoadOrder trace index.
        /// <para>This index gets incremented and auto-set when a new scene loads or gets forcefully loaded.</para>
        /// </summary>
        int lastLoadedPacketIndex = 0;


        /// <summary>
        /// The currently loaded scenes in the game.
        /// </summary>
        private GameScenes[] _activeScenes;
        /// <summary>
        /// Get the currently loaded scenes in the game as an array.
        /// </summary>
        public GameScenes[] ActiveScene { get => _activeScenes; private set => _activeScenes = value; }

        /// <summary>
        /// The scene marked as focused in the game.
        /// </summary>
        private GameScenes _focusedScene;
        /// <summary>
        /// Get the scene marked as focused in the game.
        /// </summary>
        /// <value></value>
        public GameScenes FocusedScene { get => _focusedScene; private set => _focusedScene = value; }

        /// <summary>
        /// The currently active coroutine of the loader.
        /// </summary>
        IEnumerator activeCoroutine;

        /// <summary>
        /// The game transition effect.
        /// </summary>
        SceneFader fader;
        /// <summary>
        /// Sets the transition effect instance in the Level Manager.
        /// Do not nullify mid game.
        /// </summary>
        /// <param name="fader">The active transition effect.</param>
        public void SetFaderReference(SceneFader fader) => this.fader = fader;

        private void Awake()
        {
            ManagerHUB.GetManager.SetLevelManagerReference(this);
        }

        public void TransitToHub()
        {
            //force load to player hub
            ForceLoad(hubPacket.PacketIndex);
        }

        private void Start()
        {
            //This boots up all the game scene loading system.
            LoadNext(false);
        }

        #region NORMAL_PACKET_HANDLING
        /// <summary>
        /// Call to load the next scene load packet (order set from inspector).
        /// If fromForceLoad is true then a packet index must be passed.
        /// </summary>
        public void LoadNext(bool fromForceLoad, int packetIndex = 0)
        {
            activeCoroutine = InitiateUnloadLoadSequence(fromForceLoad, _LoadPassedScenes, packetIndex);
            StartCoroutine(activeCoroutine);
        }

        /// <summary>
        /// Asynchronously unload all scenes.
        /// Then call the unloadCallback and pass the fromForceLoad and packetIndex args.
        /// </summary>
        IEnumerator InitiateUnloadLoadSequence(bool fromForceLoad, Action<bool, int> unloadCallback, int packetIndex = 0)
        {
            //wait for the black to fade in
            if (fader != null) fader.FadeIn();

            foreach (GameScenes scene in levelsInLoadOrder[lastLoadedPacketIndex].ScenesToUnload)
            {
                bool isSceneLoaded = SceneManager.GetSceneByBuildIndex((int)scene).isLoaded;

                if (isSceneLoaded)
                {
                    AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync((int)scene);

                    while (!asyncLoad.isDone)
                    {
                        yield return null;
                    }
                }
                else
                {
                    continue;
                }
            }

            _ClearCurrentlyLoadedList();

            unloadCallback(fromForceLoad, packetIndex);

            yield return null;
        }

        /// <summary>
        /// Call to create a new List<GameScenes>() and pass it to currentlyLoadedScenes.
        /// </summary>
        void _ClearCurrentlyLoadedList()
        {
            currentlyLoadedScenes = new List<GameScenes>();
        }

        /// <summary>
        /// Starts _LoadPacketScenes coroutine.
        /// </summary>
        void _LoadPassedScenes(bool fromForceLoad, int packetIndex)
        {
            StopCoroutine(activeCoroutine);

            activeCoroutine = _LoadPacketScenes(fromForceLoad, OnSceneLoadingFinished, packetIndex);
            StartCoroutine(activeCoroutine);
        }

        /// <summary>
        /// Call to load every CURRENT packet scene with LoadSceneMode.Additive.
        /// Also adds every loaded scene to the currentlyLoadedScenes list.
        /// </summary>
        IEnumerator _LoadPacketScenes(bool fromForceLoad, Action<bool, int> unloadCallback, int packetIndex = 0)
        {
            foreach (GameScenes scene in levelsInLoadOrder[lastLoadedPacketIndex].ScenesToLoad)
            {
                AsyncOperation asyncOperation = SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Additive);

                while (!asyncOperation.isDone)
                {
                    yield return null;
                }

                currentlyLoadedScenes.Add(scene);
            }

            unloadCallback(fromForceLoad, packetIndex);

            yield return null;
        }

        /// <summary>
        /// The scene loading callback function.
        /// Sets the ActiveScene, FocusedScene and lastLoadedPacketIndex values.
        /// Calls the OnSceneChanged(...) event.
        /// </summary>
        void OnSceneLoadingFinished(bool fromForceLoad, int packetIndex = 0)
        {
            StopCoroutine(activeCoroutine);

            ActiveScene = currentlyLoadedScenes.ToArray();
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)ActiveScene[ActiveScene.Length - 1]));
            FocusedScene = ActiveScene[ActiveScene.Length - 1];

            //If this is a normal loading sequence...
            if (!fromForceLoad)
            {
                //... calculate the next packet index
                int tempIndex = ++lastLoadedPacketIndex;
                SetPacketIndex(tempIndex);
            }
            else
            {
                //...if this a force load
                ReadjustPacketIndexOnForceLoad(packetIndex);
            }

            ManagerHUB.GetManager.GameEventsHandler.OnSceneChanged(FocusedScene);

            if (fader != null) fader.FadeOut();
        }
        #endregion

        #region FORCE_LOAD
        /// <summary>
        /// Call to forcefully load a level, mainly used when the player dies to get back to the playerHub.
        /// </summary>
        /// <param name="levelPacketIndex">The packet index to load</param>
        public void ForceLoad(int levelPacketIndex)
        {
            StopCoroutine(activeCoroutine);

            activeCoroutine = _UnloadCurrentlyLoaded(levelPacketIndex, _ForceLoadEndCallback);
            StartCoroutine(activeCoroutine);
        }

        /// <summary>
        /// Call to unload every scene BUT the essential scenes.
        /// </summary>
        IEnumerator _UnloadCurrentlyLoaded(int levelPacketIndex, Action<bool, int> unloadEndCallback)
        {
            foreach (GameScenes scene in currentlyLoadedScenes)
            {
                if (essentialScenes.Contains(scene)) continue;

                AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync((int)scene);

                while (!asyncOperation.isDone)
                {
                    yield return null;
                }
            }

            SetPacketIndex(levelPacketIndex);

            unloadEndCallback(true, levelPacketIndex);
        }

        /// <summary>
        /// Call load the passed scene index when a force load is in action.
        /// </summary>
        void _ForceLoadEndCallback(bool fromForceLoad, int levelPacketIndex)
        {
            StopCoroutine(activeCoroutine);

            LoadNext(fromForceLoad, levelPacketIndex);
        }
        #endregion

        #region UTILITIES
        /// <summary>
        /// Call to clamp the lastLoadedPacketIndex between 0 and levelsInLoadOrder.Count.
        /// </summary>
        /// <param name="index">The incremented packet index.</param>
        void SetPacketIndex(int index)
        {
            if (index >= levelsInLoadOrder.Count)
            {
                lastLoadedPacketIndex = 0;
            }
            else
            {
                lastLoadedPacketIndex = index;
            }
        }

        /// <summary>
        /// Call to correctly position the lastLoadedPacketIndex in the levelsInLoadOrder list. 
        /// </summary>
        /// <param name="levelPacketIndex">The force load packet index in the levelsInLoadOrder list</param>
        void ReadjustPacketIndexOnForceLoad(int levelPacketIndex)
        {
            //Store the passed packet
            LevelLoadPacket packet = levelsInLoadOrder[levelPacketIndex];

            //Store its MAIN SCENE - which is always the last in the ScenesToLoad list
            GameScenes packetMainScene = packet.ScenesToLoad[packet.ScenesToLoad.Length - 1];

            //Iterate through the levelsInLoadOrder and search for a packet that its MainScene
            //is the target scene of the passed load packet.
            foreach (LevelLoadPacket levelPacket in levelsInLoadOrder)
            {
                //if found, set the FOUND level packet index to be the current list indexer.
                if (levelPacket.PacketMainScene == packetMainScene)
                {
                    SetPacketIndex(levelPacket.PacketIndex);
                    return;
                }
            }
        }
        #endregion
    }
}