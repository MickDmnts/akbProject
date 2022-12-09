using System.Collections.Generic;
using UnityEngine;

namespace AKB.Core.Managing.UI
{
    [DefaultExecutionOrder(501)]
    public class PlayerHUDHandler : MonoBehaviour
    {
        [Header("Set in inspector")]
        [SerializeField] List<GameObject> playerHUDPanels;

        private void Start()
        {
            AddPanelReferencesToManager();
        }

        void AddPanelReferencesToManager()
        {
            ManagerHUB.GetManager.UIManager.AddExtraPanelsToManager(playerHUDPanels, true);
        }
    }
}