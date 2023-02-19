using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using akb.Core.Managing;
using akb.Core.Managing.InRunUpdates;

public class UpdateCurrency : MonoBehaviour
{
    Dictionary<AdvancementTypes, Sprite> inRunAdvancementsSpritesPairs = new Dictionary<AdvancementTypes, Sprite>();

    [SerializeField]
    TextMeshProUGUI HellsCoinsText;

    [SerializeField]
    TextMeshProUGUI SinnersSoulsText;

    [SerializeField]
    Image advacementSprite1;
    [SerializeField]
    Image advacementSprite2;
    [SerializeField]
    Image advacementSprite3;
    [SerializeField]
    Image advacementSprite4;
    [SerializeField]
    Image advacementSprite5;

    [SerializeField]
    Sprite defaultImage;

    private void OnEnable()
    {
        HellsCoinsText.SetText("Hell Coins : " + ManagerHUB.GetManager.CurrencyHandler.GetHellCoins.ToString());
        SinnersSoulsText.SetText("Sinner's Souls : " + ManagerHUB.GetManager.CurrencyHandler.GetSinnerSouls.ToString());

        SetInRunAdvacementsSprites();
    }

    void SetInRunAdvacementsSprites()
    {
        Dictionary<SlotType, AdvancementTypes> value = ManagerHUB.GetManager.SlotsHandler.GetInRunAdvancementTypes();

        if (ManagerHUB.GetManager.SlotsHandler.GetInRunAdvancementTypes() == null)
        {
            advacementSprite1.sprite = ManagerHUB.GetManager.InRunAdvancementHandler.GetAdvacementSprite(value[SlotType.Attack]);
            advacementSprite2.sprite = ManagerHUB.GetManager.InRunAdvancementHandler.GetAdvacementSprite(value[SlotType.Throw]);
            advacementSprite3.sprite = ManagerHUB.GetManager.InRunAdvancementHandler.GetAdvacementSprite(value[SlotType.DodgeRoll]);
            advacementSprite4.sprite = ManagerHUB.GetManager.InRunAdvancementHandler.GetAdvacementSprite(value[SlotType.Passive]);
            advacementSprite5.sprite = ManagerHUB.GetManager.InRunAdvancementHandler.GetAdvacementSprite(value[SlotType.DevilRage]);
        }
        else
        {
            advacementSprite1.sprite = defaultImage;
            advacementSprite2.sprite = defaultImage;
            advacementSprite3.sprite = defaultImage;
            advacementSprite4.sprite = defaultImage;
            advacementSprite5.sprite = defaultImage;
        }
    }
}
