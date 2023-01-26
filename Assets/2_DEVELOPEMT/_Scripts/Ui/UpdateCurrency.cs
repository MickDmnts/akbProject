using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using akb.Core.Managing;

public class UpdateCurrency : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI HellsCoinsText;

    [SerializeField]
    TextMeshProUGUI SinnersSoulsText;

    private void OnEnable()
    {
        HellsCoinsText.SetText("Hell Coins : " + ManagerHUB.GetManager.CurrencyHandler.GetHellCoins.ToString());
        SinnersSoulsText.SetText("Sinner's Souls : " + ManagerHUB.GetManager.CurrencyHandler.GetSinnerSouls.ToString());
    }
}
