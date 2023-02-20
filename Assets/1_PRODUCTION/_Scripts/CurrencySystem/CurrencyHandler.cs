using akb.Core.Managing.LevelLoading;
using UnityEngine;

namespace akb.Core.Managing.Currencies
{
    [DefaultExecutionOrder(-393)]
    public class CurrencyHandler : MonoBehaviour
    {
        [Header("Set coin multiplier values")]
        [SerializeField] float initialMultiplier;
        [SerializeField] float multiplierValue;
        [SerializeField] GameObject coinGainGFX;

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
            ManagerHUB.GetManager.GameEventsHandler.onAstarothDeath += ReceiveBossSouls;
            ManagerHUB.GetManager.GameEventsHandler.onSceneChanged += ResetCoinsOnHub;
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
        void ResetCoinsOnHub(GameScenes scene)
        {
            if (scene == GameScenes.PlayerHUB)
            {
                hellCoins = 0;
            }
        }

        void ReceiveBossSouls()
        {
            IncreaseSinnerSoulsBy(3);
        }

        void AddCoinsToPlayer(int coinValue)
        {
            coinValue = coinValue * (int)coinMultiplier.GetMultiplierValue;

            hellCoins += coinValue;

            GameObject temp = Instantiate(coinGainGFX);
            temp.transform.position = ManagerHUB.GetManager.PlayerEntity.transform.position + Vector3.up;

            if (ManagerHUB.GetManager.RoomSelector.CurrentRoomGO != null)
            { temp.transform.SetParent(ManagerHUB.GetManager.RoomSelector.CurrentRoomGO.transform); }

            //Update the database value
            GameManager.GetManager.Database.UpdateInRunCoinValue(hellCoins, GameManager.GetManager.ActiveFileID);
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

            GameManager.GetManager.Database.UpdateSoulsValue(sinnerSouls, GameManager.GetManager.ActiveFileID);
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
            ManagerHUB.GetManager.GameEventsHandler.onAstarothDeath -= ReceiveBossSouls;
            ManagerHUB.GetManager.GameEventsHandler.onSceneChanged -= ResetCoinsOnHub;

            coinMultiplier.UnsubEvents();
        }
    }
}