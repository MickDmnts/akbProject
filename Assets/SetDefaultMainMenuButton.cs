using System.Collections;
using System.Collections.Generic;
using akb.Core.Managing;
using UnityEngine;
using UnityEngine.UI;

public class SetDefaultMainMenuButton : MonoBehaviour
{
    [SerializeField] Button defaultButton;

    private void OnEnable()
    {
        defaultButton.Select();
    }
}
