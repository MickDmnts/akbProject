using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDefaultOptionsButton : MonoBehaviour
{
    [SerializeField] Slider defaultSlider;

    private void OnEnable()
    {
        defaultSlider.Select();
    }
}
