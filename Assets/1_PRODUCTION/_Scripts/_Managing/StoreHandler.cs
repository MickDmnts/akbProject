using UnityEngine;

using akb.Core.Managing.InRunUpdates;

namespace akb.Core.Managing
{
    public class StoreHandler : MonoBehaviour
    {
        [SerializeField] Transform[] itemSpawnLocations;

        int[] prices;

        private void Awake()
        {
            prices = new int[] { 100, 200, 300, 400 };
        }

        private void Start()
        {
            for (int i = 0; i < itemSpawnLocations.Length; i++)
            {
                int rng = UnityEngine.Random.Range(0, ManagerHUB.GetManager.InRunAdvancementHandler.InRunAdvancementPairsCount);

                AdvancementTypes type = (AdvancementTypes)rng;

                GameObject advancement = ManagerHUB.GetManager.InRunAdvancementHandler.GetAdvancementGameObject(type);

                advancement.GetComponent<AdvancementPickUp>().SetPickupType(AdvancementPickUp.PickType.PromptPickup, 0);

                advancement.transform.position = itemSpawnLocations[i].position;
            }
        }
    }
}