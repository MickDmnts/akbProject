using UnityEngine;

using akb.Core.Managing;
using akb.Entities.Player.Interactions;

namespace akb.Entities.AI.Implementations.Astaroth
{
    public class AstarothPlayerConfuser : MonoBehaviour
    {
        PlayerInteractable playerInteractable;

        bool canConfuse = false;
        float confusionInterval = 3f;

        float currentInterval = 0;

        private void Start()
        {
            ManagerHUB.GetManager.GameEventsHandler.onAstarothSecondPhase += StartConfusing;
            ManagerHUB.GetManager.GameEventsHandler.onAllRocksBroken += StopConfusing;

            playerInteractable = ManagerHUB.GetManager.PlayerEntity.PlayerInteractable;
        }

        void StartConfusing()
        {
            canConfuse = true;
        }

        void StopConfusing()
        {
            canConfuse = false;
        }

        void Update()
        {
            if (!canConfuse) { return; }

            currentInterval += Time.deltaTime;
            if (currentInterval >= confusionInterval)
            {
                playerInteractable.ApplyConfusedInteraction();
                currentInterval = 0f;
            }
        }

        private void OnDestroy()
        {
            ManagerHUB.GetManager.GameEventsHandler.onAstarothSecondPhase -= StartConfusing;
            ManagerHUB.GetManager.GameEventsHandler.onAllRocksBroken -= StopConfusing;
        }
    }
}