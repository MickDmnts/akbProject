using UnityEngine;

namespace akb.Core.Managing.LevelLoading.Hub
{
    public class TutorialEntryHandler : MonoBehaviour
    {
        private void Start()
        {
            ManagerHUB.GetManager.PlayerEntity.PlayerMovement.TeleportEntity(transform.position);
            Debug.Log("Press G to teleport to Hub - DEBUG PURPOSES");
        }

        private void Update()
        {
            //DEBUG------------------------------------------------------------
            if (Input.GetKeyDown(KeyCode.G))
            {
                ManagerHUB.GetManager.LevelManager.TransitToHub();
            }
            //-----------------------------------------------------------------
        }
    }
}