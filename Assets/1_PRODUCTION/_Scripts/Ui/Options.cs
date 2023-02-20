using akb.Core.Sounds;
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

        void SoundEffects()
        {
            ManagerHUB.GetManager.SoundsHandler.ControlSFXAudioSource(soundEffectsSlider.value);
        }

        void ScreenShake()
        {
            ManagerHUB.GetManager.SoundsHandler.PlayOneShot(GameAudioClip.UIClickSound);

            ManagerHUB.GetManager.UIManager.CanScreenShake = screenShakeToggle.isOn;
        }

        void RedScreenTint()
        {
            ManagerHUB.GetManager.SoundsHandler.PlayOneShot(GameAudioClip.UIClickSound);

            ManagerHUB.GetManager.UIManager.CanShowScreenTint = redScreenTintToggle.isOn;
        }

        void HideUIELements()
        {
            ManagerHUB.GetManager.SoundsHandler.PlayOneShot(GameAudioClip.UIClickSound);

            ManagerHUB.GetManager.UIManager.CanShowUIElements = hideUIElementToggle.isOn;
        }

        void DevMode()
        {
            ManagerHUB.GetManager.SoundsHandler.PlayOneShot(GameAudioClip.UIClickSound);

            GameManager.GetManager.IsDevMode = devModeToggle.isOn;
        }

        void Back()
        {
            ManagerHUB.GetManager.SoundsHandler.PlayOneShot(GameAudioClip.OpenMenu);

            ManagerHUB.GetManager.UIManager.EnablePanel("PauseMenu_UI_Panel");
        }
    }
}