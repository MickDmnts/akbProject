using UnityEngine;

using akb.Core.Managing;

namespace akb.Entities.AI.Implementations.Astaroth
{
    public class AstarothRockHandler : MonoBehaviour
    {
        [SerializeField] AstarothRock[] astarothRocks;

        int totalActiveRockCount = 0;

        private void Awake()
        {
            ManagerHUB.GetManager.GameEventsHandler.onRockBroken += SubtractRock;

            totalActiveRockCount = astarothRocks.Length;
        }

        void SubtractRock(int rockID)
        {
            astarothRocks[rockID].MarkBroken();

            totalActiveRockCount--;

            CheckForAllRocksBroken();
        }

        void CheckForAllRocksBroken()
        {
            if (totalActiveRockCount <= 0)
            {
                ManagerHUB.GetManager.GameEventsHandler.OnAllRocksBroken();
            }
        }

        private void OnDestroy()
        {
            ManagerHUB.GetManager.GameEventsHandler.onRockBroken -= SubtractRock;
        }
    }
}