using System.Collections;
using System.Collections.Generic;
using AKB.Core.Managing;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundEffectsSlider;

    [SerializeField] Toggle screenShakeToggle;
    [SerializeField] Toggle redScreenTintToggle;
    [SerializeField] Toggle hideUIElementToggle;
    [SerializeField] Toggle DevModeToggle;

    [SerializeField] Button backButton;

    bool sstIsActive = false;
    bool rstIsActive = false;
    bool huietIsActive = false;
    bool dmtIsActive = false;


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
        musicSlider.onValueChanged.AddListener(delegate { Music(); });
        soundEffectsSlider.onValueChanged.AddListener(delegate { SoundEffects(); });

        screenShakeToggle.onValueChanged.AddListener(delegate { ScreenShake(); });
        redScreenTintToggle.onValueChanged.AddListener(delegate { RedScreenTint(); });
        hideUIElementToggle.onValueChanged.AddListener(delegate { HideUIELements(); });
        DevModeToggle.onValueChanged.AddListener(delegate { DevMode(); });

        backButton.onClick.AddListener(Back);
    }

    //Update sliders
    void Master()
    {
        Debug.Log(masterSlider.value);
    }

    void Music()
    {
        Debug.Log(musicSlider.value);
    }

    void SoundEffects()
    {
        Debug.Log(soundEffectsSlider.value);
    }

    void ScreenShake()
    {

        if (!sstIsActive)
        {
            screenShakeToggle.isOn = true;
            sstIsActive = true;
        }
        else
        {
            screenShakeToggle.isOn = false;
            sstIsActive = false;

        }
    }

    void RedScreenTint()
    {
        if (!rstIsActive)
        {
            redScreenTintToggle.isOn = true;
            rstIsActive = true;
        }
        else
        {
            redScreenTintToggle.isOn = false;
            rstIsActive = false;

        }
    }

    void HideUIELements()
    {
        if (!huietIsActive)
        {
            hideUIElementToggle.isOn = true;
            huietIsActive = true;
        }
        else
        {
            hideUIElementToggle.isOn = false;
            huietIsActive = false;

        }
    }

    void DevMode()
    {
        if (!dmtIsActive)
        {
            DevModeToggle.isOn = true;
            dmtIsActive = true;
        }
        else
        {
            DevModeToggle.isOn = false;
            dmtIsActive = false;

        }
    }

    void Back()
    {
        GameManager.S.UIManager.EnablePanel("PauseMenu_UI_Panel");
    }
}