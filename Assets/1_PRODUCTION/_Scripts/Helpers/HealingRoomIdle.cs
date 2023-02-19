using akb.Core.Managing;
using akb.Core.Sounds;
using UnityEngine;

public class HealingRoomIdle : MonoBehaviour
{
    private void Awake()
    {
        ManagerHUB.GetManager.SoundsHandler.PlayOneShot(GameAudioClip.HealthFountainIDLE);
    }
}
