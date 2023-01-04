using System.Collections;
using akb.Core.Managing;
using akb.Core.Managing.LevelLoading;
using UnityEngine;

using akb.Core.Managing.PCG;

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

        [Header("Select transistor type")]
        [SerializeField] TransistorType transistorType;

        IEnumerator activeBehaviour;

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
                        activeBehaviour = CreateRoomAndMovePlayer();
                        StartCoroutine(activeBehaviour);
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

        IEnumerator CreateRoomAndMovePlayer()
        {
            RoomWorld activeWorld = ManagerHUB.GetManager.RoomSelector.GetActiveWorld;
            Vector3 nextRoomEntry = ManagerHUB.GetManager.RoomSelector.PlaceNextRoom(activeWorld);

            ManagerHUB.GetManager.PlayerEntity.PlayerMovement.TeleportEntity(nextRoomEntry);

            //Waits until the player is properly moved
            while (ManagerHUB.GetManager.PlayerEntity.transform.position != nextRoomEntry)
            { yield return null; }

            ManagerHUB.GetManager.GameEventsHandler.OnNextRoomEntry();
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}