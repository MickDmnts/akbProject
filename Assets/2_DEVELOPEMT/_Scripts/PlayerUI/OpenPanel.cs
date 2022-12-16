using UnityEngine;

namespace AKB.Core.Managing.UI
{
    public class OpenPanel : MonoBehaviour
    {
        [SerializeField] GameObject persistentAdvancementsPanel;

        // Start is called before the first frame update
        void Start()
        {
            persistentAdvancementsPanel.SetActive(false);
        }

        // Update is called once per frame
        private void OnTriggerEnter(Collider other)
        {
            persistentAdvancementsPanel.SetActive(other.gameObject.CompareTag("Player"));
        }

        private void OnTriggerExit(Collider other)
        {
            persistentAdvancementsPanel.SetActive(!other.gameObject.CompareTag("Player"));
        }
    }
}