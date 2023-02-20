using UnityEngine;
using TMPro;
using akb.Core.Managing;
using akb.Core.Managing.LevelLoading;

public class GameplayCoinUpdate : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsText;

    private void Start()
    {
        ManagerHUB.GetManager.GameEventsHandler.onCoinReceive += UpdateValue;
        ManagerHUB.GetManager.GameEventsHandler.onSceneChanged += ResetCoins;
        ManagerHUB.GetManager.GameEventsHandler.onItemBuy += UpdateValue;
    }

    void ResetCoins(GameScenes scene)
    {
        if (scene == GameScenes.PlayerHUB)
        { UpdateValue(0); }
    }

    void UpdateValue()
    {
        coinsText.SetText(ManagerHUB.GetManager.CurrencyHandler.GetHellCoins.ToString());
    }

    void UpdateValue(int value)
    {
        coinsText.SetText(ManagerHUB.GetManager.CurrencyHandler.GetHellCoins.ToString());
    }

    private void OnDestroy()
    {
        ManagerHUB.GetManager.GameEventsHandler.onCoinReceive -= UpdateValue;
        ManagerHUB.GetManager.GameEventsHandler.onItemBuy -= UpdateValue;
        ManagerHUB.GetManager.GameEventsHandler.onSceneChanged -= ResetCoins;
    }
}
