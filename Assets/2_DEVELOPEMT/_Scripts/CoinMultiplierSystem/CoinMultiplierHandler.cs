namespace akb.Core.Managing.Currencies
{
    [System.Serializable]
    public class CoinMultiplierHandler
    {
        public CoinMultiplierHandler(float initialMultiplier, float multiplierValue)
        {
            this.initialMultiplier = initialMultiplier;
            this.multiplierValue = multiplierValue;

            currentMultiplier = initialMultiplier;
        }

        float initialMultiplier;
        float multiplierValue;

        float currentMultiplier;

        public float GetMultiplierValue => currentMultiplier;

        void HookEvents()
        {
            ManagerHUB.GetManager.GameEventsHandler.onEnemyDeath += IncreaseMultiplier; //Multiply
            ManagerHUB.GetManager.GameEventsHandler.onPlayerHit += ResetMultiplier;
        }

        void IncreaseMultiplier()
        {
            currentMultiplier += multiplierValue;
        }

        void ResetMultiplier()
        {
            currentMultiplier = initialMultiplier;
        }

        public void UnsubEvents()
        {
            ManagerHUB.GetManager.GameEventsHandler.onEnemyDeath -= IncreaseMultiplier;
            ManagerHUB.GetManager.GameEventsHandler.onPlayerHit -= ResetMultiplier;
        }
    }
}