using UnityEngine;

namespace akb.Core.Managing.UI
{
    public class OpenPanel : MonoBehaviour
    {
        // Update is called once per frame
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            { ManagerHUB.GetManager.UIManager.EnablePanel("AdvancementsPanel"); }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            { ManagerHUB.GetManager.UIManager.EnablePanel("GamePlayScreenPanel"); }
        }
    }
}