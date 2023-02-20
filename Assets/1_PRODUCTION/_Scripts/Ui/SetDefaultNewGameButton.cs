using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDefaultNewGameButton : MonoBehaviour
{
    [SerializeField] Button defaultButton;

    private void OnEnable()
    {
        defaultButton.Select();
    }
}
