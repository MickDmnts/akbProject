using UnityEngine;

namespace AKB.Core.Managing.LevelLoading.Hub
{
    public class TutorialEntryHandler : MonoBehaviour
    {
        private void Start()
        {
            ManagerHUB.GetManager.PlayerEntity.PlayerMovement.TeleportEntity(transform.position);
        }

        private void Update()
        {
            //DEBUG------------------------------------------------------------
            if (Input.GetKeyDown(KeyCode.G))
            {
                ManagerHUB.GetManager.LevelManager.LoadNext(false);
            }
            //-----------------------------------------------------------------
        }
    }
}