using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AKB.Core.Managing.LevelLoading
{
    /* [CLASS DOCUMENTATION]
     * 
     * All the script variables are public and must be set from the inspector.
     * 
     * [Must Know]
     * 1. The purpose of this script is to pass the UI panels from the UI_Render scene to the GameEntry scene 
     * to update the UIManager with the new panel references present in the UI_Render scene.
     * 2. The LevelManager Fader script reference is also set from this script.
     */
    [DefaultExecutionOrder(100)]
    public class PanelReferences : MonoBehaviour
    {
        [Header("Set in inspector")]
        [SerializeField] List<GameObject> uiPanels;
        [SerializeField] SceneFader fader;

        private void Awake()
        {
            //Called in Awake!
            PassPanelReferences();
        }

        /// <summary>
        /// Call to pass the assigned panel references, the Continue and Quit buttons to the UIManager
        /// and the Fader script reference to the LevelManager.
        /// </summary>
        void PassPanelReferences()
        {
            GameManager.S.UIManager.SetUIPanels(uiPanels);
        }

        private void Start()
        {
            GameManager.S.UIManager.EnablePanel("MainMenuScreen_UI_Panel");
        }
    }
}