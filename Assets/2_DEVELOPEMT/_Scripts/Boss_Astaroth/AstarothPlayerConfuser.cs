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

            playerInteractable = ManagerHUB.GetManager.PlayerEntity.PlayerInteractable;
        }

        void StartConfusing()
        {
            canConfuse = true;
        }

        void Update()
        {
            if (!canConfuse) { return; }
            currentInterval += Time.deltaTime;

            if (confusionInterval % currentInterval <= 0.1f)
            {
                playerInteractable.ApplyConfusedInteraction();
                currentInterval = 0f;
            }
        }

        private void OnDestroy()
        {
            ManagerHUB.GetManager.GameEventsHandler.onAstarothSecondPhase -= StartConfusing;
        }
    }
}