using System.Collections;
using System.Collections.Generic;
using akb.Core.Managing;
using akb.Core.Managing.PCG;
using UnityEngine;

namespace akb.Gameplay
{
    public class WorldEntry : MonoBehaviour
    {
        // Start is called before the first frame update
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