using UnityEngine;

using akb.Core.Managing;
using akb.Core.Managing.PCG;

namespace akb.Gameplay
{
    public class WorldEntry : MonoBehaviour
    {
        private void Awake()
        {
            ManagerHUB.GetManager.GameEventsHandler.onSceneLoaded += CreateRoomAndMovePlayer;
        }

        void CreateRoomAndMovePlayer()
        {
            RoomWorld activeWorld = ManagerHUB.GetManager.RoomSelector.GetActiveWorld;

            GameObject nextRoom = ManagerHUB.GetManager.RoomSelector.PlaceNextRoom(activeWorld);
            Vector3 nextRoomEntry = nextRoom.GetComponent<RoomData>().GetRoomEntryPoint().position;

            ManagerHUB.GetManager.PlayerEntity.PlayerMovement.TeleportEntity(nextRoomEntry);
        }

        private void OnDestroy()
        {
            ManagerHUB.GetManager.GameEventsHandler.onSceneLoaded -= CreateRoomAndMovePlayer;
        }
    }
}