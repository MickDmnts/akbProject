using UnityEngine;

namespace akb.Core.Managing.LevelLoading.Hub
{
    public class HubEntryHandler : MonoBehaviour
    {
        [Header("Set in inspector")]
        [SerializeField] Transform entryPoint;

        private void Start()
        {
            ManagerHUB.GetManager.PlayerEntity.PlayerMovement.TeleportEntity(entryPoint.position);

            //Notify scripts of hub entry
            ManagerHUB.GetManager.GameEventsHandler.OnPlayerHubEntry();
        }
    }
}