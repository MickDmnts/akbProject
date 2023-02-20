using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDefaultLoadGameButton : MonoBehaviour
{
    [SerializeField] Button defaultButton;

    private void OnEnable()
    {
        defaultButton.Select();
    }
}
