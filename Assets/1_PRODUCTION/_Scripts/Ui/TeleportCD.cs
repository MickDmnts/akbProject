using System.Collections;
using System.Collections.Generic;
using akb.Core.Managing;
using UnityEngine;
using UnityEngine.UI;

namespace akb.Core.Managing.UI
{
    public class TeleportCD : MonoBehaviour
    {
        [SerializeField] Image teleportCDImage;

        void Start()
        {
            ManagerHUB.GetManager.GameEventsHandler.onTeleportCooldown += UpdateTelportCDImage;
        }

        void UpdateTelportCDImage(float time)
        {
            teleportCDImage.fillAmount = time / ManagerHUB.GetManager.PlayerEntity.PlayerSpearTeleporting.CurrentTeleportationCD;

            if(time <= 0)
            {
                teleportCDImage.fillAmount = 1;
            }
        }

        void OnDestroy()
        {
            ManagerHUB.GetManager.GameEventsHandler.onTeleportCooldown -= UpdateTelportCDImage;

        }
    }
}
