using UnityEngine;

using akb.Core.Managing;
using akb.Core.Managing.PCG;
using akb.Core.Managing.LevelLoading;

namespace akb.Gameplay
{
    public class WorldEntry : MonoBehaviour
    {
        void Start()
        {
            ManagerHUB.GetManager.GameEventsHandler.onSceneChanged += WorldEntryBehaviour;
        }

        void WorldEntryBehaviour(GameScenes currentScene)
        {
            if (currentScene == GameScenes.World1Scene
                || currentScene == GameScenes.World2Scene)
            {
                CreateRoomAndMovePlayer();
            }
        }

        void CreateRoomAndMovePlayer()
        {
            RoomWorld activeWorld = ManagerHUB.GetManager.RoomSelector.GetActiveWorld;
            Vector3 nextRoomEntry = ManagerHUB.GetManager.RoomSelector.PlaceNextRoom(activeWorld);

            ManagerHUB.GetManager.PlayerEntity.PlayerMovement.TeleportEntity(nextRoomEntry);
        }
    }
}