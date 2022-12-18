using UnityEngine;

namespace akb.Core.Managing.Currencies
{
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
        }

        //ERASE VALUES ON NEW GAME EVENT
        //LOAD VALUES ON LOAD EVENT

        #region UTILITIES
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
    }
}