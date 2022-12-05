using UnityEngine;

namespace AKB.Core.Managing.PCG
{
    public class PCGTester : MonoBehaviour
    {
        [SerializeField] RoomWorld world;

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.T))
            {
                RoomData nextRoom = ManagerHUB.GetManager.RoomSelector.SelectNextRoom(world);
                nextRoom.GetRoomPrefab().SetActive(true);
            }
        }
    }
}