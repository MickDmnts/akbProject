using UnityEngine;

namespace akb.Core.Managing.Currencies
{
    [DefaultExecutionOrder(-393)]
    public class CurrencyHandler : MonoBehaviour
    {
        [Header("Set coin multiplier values")]
        [SerializeField] float initialMultiplier;
        [SerializeField] float multiplierValue;

        int hellCoins = -1;
        int sinnerSouls = 100;

        public int GetHellCoins => hellCoins;
        public int GetSinnerSouls => sinnerSouls;

        CoinMultiplierHandler coinMultiplier;

        private void Start()
        {
            ManagerHUB.GetManager.SetCurrencyHandlerReference(this);

            coinMultiplier = new CoinMultiplierHandler(initialMultiplier, multiplierValue);

            ManagerHUB.GetManager.GameEventsHandler.onNewGame += NewGameBehaviour;
            ManagerHUB.GetManager.GameEventsHandler.onLoadGame += LoadGameBehaviour;

            ManagerHUB.GetManager.GameEventsHandler.onCoinReceive += AddCoinsToPlayer;
        }

        void NewGameBehaviour(int saveFileID)
        {
            hellCoins = 0;
            sinnerSouls = 0;

            GameManager.GetManager.Database.UpdateSoulsValue(saveFileID, sinnerSouls);
        }

        void LoadGameBehaviour(int saveFileID)
        {
            sinnerSouls = GameManager.GetManager.Database.GetSoulsValue(saveFileID);
            hellCoins = 0;
        }

        #region UTILITIES
        void AddCoinsToPlayer(int coinValue)
        {
            coinValue = coinValue * (int)coinMultiplier.GetMultiplierValue;

            hellCoins += coinValue;
        }

        public void IncreaseHellCoinsBy(int value)
        {
            if (hellCoins == 0) value++;

            hellCoins += value;
        }

        public void DecreaseHellCoinsBy(int value) => hellCoins -= value;

        public void IncreaseSinnerSoulsBy(int value)
        {
            if (sinnerSouls == 0) value++;

            sinnerSouls += value;
        }

        public void DecreaseSinnerSoulsBy(int value) => sinnerSouls -= value;

        public void SetHellCoinsValue(int value) => hellCoins = value;

        public void SetSinnerSoulsValue(int value) => sinnerSouls = value;

        public float GetMultiplierValue() => coinMultiplier.GetMultiplierValue;
        #endregion

        private void OnDestroy()
        {
            ManagerHUB.GetManager.GameEventsHandler.onNewGame -= NewGameBehaviour;
            ManagerHUB.GetManager.GameEventsHandler.onLoadGame -= LoadGameBehaviour;

            ManagerHUB.GetManager.GameEventsHandler.onCoinReceive -= AddCoinsToPlayer;

            coinMultiplier.UnsubEvents();
        }
    }
}