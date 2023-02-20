using UnityEngine;

using akb.Core.Managing;
using akb.Entities.Player;

public class VoidChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerEntity>())
        {
            if (ManagerHUB.GetManager.LevelManager.FocusedScene == akb.Core.Managing.LevelLoading.GameScenes.TutorialArena)
            {
                ManagerHUB.GetManager.PlayerEntity.PlayerMovement.TeleportEntity(Vector3.up * 2);
            }
            else
            {
                ManagerHUB.GetManager.LevelManager.TransitToHub();
            }
        }
    }
}
