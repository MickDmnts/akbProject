using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using akb.Core.Managing;

public class UpdateSoulsValue : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI SinnersSoulsText;

    private void OnEnable()
    {
        SinnersSoulsText.SetText(ManagerHUB.GetManager.CurrencyHandler.GetSinnerSouls.ToString());
    }
}
