using akb.Core.Managing;
using akb.Core.Managing.LevelLoading;
using akb.Core.Managing.PCG;
using UnityEngine;

namespace akb.Gameplay
{
    public class RoomTransitHandler : MonoBehaviour
    {
        enum TransistorType
        {
            HubToLevels,
            LevelToLevel,
            LevelsToHub
        }

        [SerializeField] TransistorType transistorType;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                TransitBehaviour();
            }
        }

        void TransitBehaviour()
        {
            switch (transistorType)
            {
                case TransistorType.HubToLevels:
                    {
                        ManagerHUB.GetManager.LevelManager.LoadNext(false);
                        ManagerHUB.GetManager.UIManager.EnablePanel("GamePlayScreenPanel");
                    }
                    break;
                case TransistorType.LevelToLevel:
                    {
                        CreateRoomAndMovePlayer();
                    }
                    break;
                case TransistorType.LevelsToHub:
                    {
                        ManagerHUB.GetManager.LevelManager.TransitToHub();
                        ManagerHUB.GetManager.UIManager.EnablePanel("GamePlayScreenPanel");
                    }
                    break;
                default:
                    {
                        Debug.LogWarning("Invalid type");
                    }
                    break;
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