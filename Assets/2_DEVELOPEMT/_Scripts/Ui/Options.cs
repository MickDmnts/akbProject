using UnityEngine;
using UnityEngine.UI;

namespace akb.Core.Managing.UI
{
    public class Options : MonoBehaviour
    {
        [SerializeField] Slider masterSlider;
        //[SerializeField] Slider musicSlider;
        [SerializeField] Slider soundEffectsSlider;

        [SerializeField] Toggle screenShakeToggle;
        [SerializeField] Toggle redScreenTintToggle;
        [SerializeField] Toggle hideUIElementToggle;
        [SerializeField] Toggle devModeToggle;

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
            masterSlider.onValueChanged.AddListener(delegate { Master(); });
            //musicSlider.onValueChanged.AddListener(delegate { Music(); });
            soundEffectsSlider.onValueChanged.AddListener(delegate { SoundEffects(); });

            screenShakeToggle.onValueChanged.AddListener(delegate { ScreenShake(); });
            redScreenTintToggle.onValueChanged.AddListener(delegate { RedScreenTint(); });
            hideUIElementToggle.onValueChanged.AddListener(delegate { HideUIELements(); });
            devModeToggle.onValueChanged.AddListener(delegate { DevMode(); });

            backButton.onClick.AddListener(Back);
        }

        //Update sliders
        void Master()
        {
            ManagerHUB.GetManager.SoundsHandler.SetMasterVolume(masterSlider.value);
        }

        //void Music()
        //{
        //    ManagerHUB.GetManager.SoundsHandler.ControlMainAudioSource(musicSlider.value);
        //}

        void SoundEffects()
        {
            ManagerHUB.GetManager.SoundsHandler.ControlSFXAudioSource(soundEffectsSlider.value);
        }

        void ScreenShake()
        {
            screenShakeToggle.isOn = !screenShakeToggle.isOn;
            ManagerHUB.GetManager.UIManager.CanScreenShake = screenShakeToggle.isOn;
        }

        void RedScreenTint()
        {
            redScreenTintToggle.isOn = !redScreenTintToggle.isOn;
            ManagerHUB.GetManager.UIManager.CanShowScreenTint = redScreenTintToggle.isOn;
        }

        void HideUIELements()
        {
            hideUIElementToggle.isOn = !hideUIElementToggle.isOn;
            ManagerHUB.GetManager.UIManager.CanShowUIElements = hideUIElementToggle.isOn;
        }

        void DevMode()
        {
            devModeToggle.isOn = !devModeToggle.isOn;
            GameManager.GetManager.IsDevMode = devModeToggle.isOn;
        }

        void Back()
        {
            ManagerHUB.GetManager.UIManager.EnablePanel("PauseMenu_UI_Panel");
        }
    }
}