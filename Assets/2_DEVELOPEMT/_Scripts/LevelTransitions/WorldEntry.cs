using UnityEngine;

using akb.Core.Managing;
using akb.Core.Managing.PCG;

namespace akb.Gameplay
{
    public class WorldEntry : MonoBehaviour
    {
        void Start()
        {
            CreateRoomAndMovePlayer();
        }

        void CreateRoomAndMovePlayer()
        {
            RoomWorld activeWorld = ManagerHUB.GetManager.RoomSelector.GetActiveWorld;
            Vector3 nextRoomEntry = ManagerHUB.GetManager.RoomSelector.PlaceNextRoom(activeWorld);

            ManagerHUB.GetManager.PlayerEntity.PlayerMovement.TeleportEntity(nextRoomEntry);
        }
    }
}