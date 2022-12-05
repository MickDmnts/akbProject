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
            Debug.Log("sstIsActive toggle is <On> on options menu screen");
        }
        else
        {
            screenShakeToggle.isOn = false;
            sstIsActive = false;
            Debug.Log("sstIsActive toggle is <Off> on options menu screen");
        }
    }

    void RedScreenTint()
    {
        if (!rstIsActive)
        {
            redScreenTintToggle.isOn = true;
            rstIsActive = true;
            Debug.Log("rstIsActive toggle is <On> on options menu screen");
        }
        else
        {
            redScreenTintToggle.isOn = false;
            rstIsActive = false;
            Debug.Log("rstIsActive toggle is <Off> on options menu screen");
        }
    }

    void HideUIELements()
    {
        if (!huietIsActive)
        {
            hideUIElementToggle.isOn = true;
            huietIsActive = true;
            Debug.Log("huietIsActive toggle is <On> on options menu screen");
        }
        else
        {
            hideUIElementToggle.isOn = false;
            huietIsActive = false;
            Debug.Log("huietIsActive toggle is <Off> on options menu screen");
        }
    }

    void DevMode()
    {
        if (!dmtIsActive)
        {
            DevModeToggle.isOn = true;
            dmtIsActive = true;
            Debug.Log("dmtIsActive toggle is <On> on options menu screen");
        }
        else
        {
            DevModeToggle.isOn = false;
            dmtIsActive = false;
            Debug.Log("dmtIsActive toggle is <Off> on options menu screen");
        }
    }

    void Back()
    {
        ManagerHUB.GetManager.UIManager.EnablePanel("PauseMenu_UI_Panel");
    }
}