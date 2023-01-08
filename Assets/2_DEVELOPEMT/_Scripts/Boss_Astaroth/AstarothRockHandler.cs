using UnityEngine;

using akb.Core.Managing;

namespace akb.Entities.AI.Implementations.Astaroth
{
    public class AstarothRockHandler : MonoBehaviour
    {
        [SerializeField] AstarothRock[] astarothRocks;

        private void Awake()
        {
            ManagerHUB.GetManager.GameEventsHandler.onRockBroken += SubtractRock;
        }

        void SubtractRock(int rockID)
        {
            astarothRocks[rockID].MarkBroken();
        }

        private void OnDestroy()
        {
            ManagerHUB.GetManager.GameEventsHandler.onRockBroken -= SubtractRock;
        }
    }
}