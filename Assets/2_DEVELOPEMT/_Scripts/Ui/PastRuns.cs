using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace akb.Core.Managing.UI
{
    public class PastRuns : MonoBehaviour
    {
        [SerializeField] Button backButton;

        private void Start()
        {
            EntrySetup();
        }

        /// <summary>
        /// Call to set up the default script behaviour.
        /// </summary>
        void EntrySetup()
        {
            //Tutorials Buttons
            backButton.onClick.AddListener(Back);
        }


        void Back()
        {
            ManagerHUB.GetManager.UIManager.EnablePanel("PauseMenu_UI_Panel");
        }

        //[SerializeField] GameObject pastRunsPrefab;
        //[SerializeField] GameObject pastRunsContainer;

        //[SerializeField]
        //List<int> numbers = new List<int>();

        //// Start is called before the first frame update
        //void Start()
        //{
        //    foreach (int num in numbers)
        //    {
        //        GameObject pastRunsGameobject = Instantiate(pastRunsPrefab);
        //        pastRunsGameobject.transform.SetParent(pastRunsContainer.transform, false);
        //        pastRunsGameobject.transform.localScale = Vector3.one;
        //    }
        //}
    }
}