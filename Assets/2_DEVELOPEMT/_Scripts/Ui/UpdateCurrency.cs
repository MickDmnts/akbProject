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

        if (value[SlotType.Attack] != AdvancementTypes.None)
        { advacementSprite1.sprite = ManagerHUB.GetManager.InRunAdvancementHandler.GetAdvacementSprite(value[SlotType.Attack]); }
        else
        { advacementSprite1.sprite = defaultImage; }

        if (value[SlotType.Throw] != AdvancementTypes.None)
        { advacementSprite2.sprite = ManagerHUB.GetManager.InRunAdvancementHandler.GetAdvacementSprite(value[SlotType.Throw]); }
        else
        { advacementSprite2.sprite = defaultImage; }

        if (value[SlotType.DodgeRoll] != AdvancementTypes.None)
        { advacementSprite3.sprite = ManagerHUB.GetManager.InRunAdvancementHandler.GetAdvacementSprite(value[SlotType.DodgeRoll]); }
        else
        { advacementSprite3.sprite = defaultImage; }

        if (value[SlotType.DevilRage] != AdvancementTypes.None)
        { advacementSprite4.sprite = ManagerHUB.GetManager.InRunAdvancementHandler.GetAdvacementSprite(value[SlotType.DevilRage]); }
        else
        { advacementSprite4.sprite = defaultImage; }

        if (value[SlotType.Passive] != AdvancementTypes.None)
        { advacementSprite5.sprite = ManagerHUB.GetManager.InRunAdvancementHandler.GetAdvacementSprite(value[SlotType.Passive]); }
        else
        { advacementSprite5.sprite = defaultImage; }
    }
}
