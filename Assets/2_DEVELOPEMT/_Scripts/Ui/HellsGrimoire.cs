using UnityEngine;
using UnityEngine.UI;

namespace akb.Core.Managing.UI
{
    public class HellsGrimoire : MonoBehaviour
    {
        [SerializeField] Button tutorialsButton;
        [SerializeField] Button monstersButton;
        [SerializeField] Button pastRunsButton;

        [SerializeField] GameObject Tutorials_UI_Panel;
        [SerializeField] GameObject Monsters_UI_Panel;
        [SerializeField] GameObject PastRuns_UI_Panel;

        private void Start()
        {
            EntrySetup();
        }

        //we need a back button to return to pause menu screen

        /// <summary>
        /// Call to set up the default script behaviour.
        /// </summary>
        void EntrySetup()
        {
            tutorialsButton.onClick.AddListener(Tutorials);
            monstersButton.onClick.AddListener(Monsters);
            pastRunsButton.onClick.AddListener(PastRuns);
        }

        void Tutorials()
        {
            ActivatePanel("Tutorials_UI_Panel");
        }

        void Monsters()
        {
            ActivatePanel("Monsters_UI_Panel");
        }

        void PastRuns()
        {
            ActivatePanel("PastRuns_UI_Panel");
        }

        void ActivatePanel(string panelToBeActivated)
        {
            Tutorials_UI_Panel.SetActive(panelToBeActivated.Equals(Tutorials_UI_Panel.name));
            Monsters_UI_Panel.SetActive(panelToBeActivated.Equals(Monsters_UI_Panel.name));
            PastRuns_UI_Panel.SetActive(panelToBeActivated.Equals(PastRuns_UI_Panel.name));
        }
    }
}