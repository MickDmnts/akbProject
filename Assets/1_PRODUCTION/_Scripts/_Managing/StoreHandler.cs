using UnityEngine;

using akb.Core.Managing.InRunUpdates;

namespace akb.Core.Managing
{
    [System.Serializable]
    public struct ItemPacket
    {
        public Transform itemSpawn;
        public int itemPrice;
    }

    public class StoreHandler : MonoBehaviour
    {
        [SerializeField] string[] itemDescriptions;
        [SerializeField] ItemPacket[] itemSpawnLocations;

        private void Start()
        {
            for (int i = 0; i < itemSpawnLocations.Length; i++)
            {
                if (itemSpawnLocations[i].itemSpawn == null) continue;

                int rng = UnityEngine.Random.Range(0, ManagerHUB.GetManager.InRunAdvancementHandler.InRunAdvancementPairsCount);

                AdvancementTypes type = (AdvancementTypes)rng;

                GameObject advancement = ManagerHUB.GetManager.InRunAdvancementHandler.GetAdvancementGameObject(type);
                advancement.transform.SetParent(ManagerHUB.GetManager.RoomSelector.CurrentRoomGO.transform);

                if (advancement.TryGetComponent<AdvancementPickUp>(out AdvancementPickUp tempAdv))
                {
                    tempAdv.SetPickupType(AdvancementPickUp.PickType.PromptPickup, itemSpawnLocations[i].itemPrice);

                    if (itemSpawnLocations[i].itemSpawn.TryGetComponent<ItemInfo>(out ItemInfo tempCanvas))
                    {
                        tempCanvas.SetDescriptionText(itemDescriptions[(int)type]);
                        tempCanvas.SetIcon(ManagerHUB.GetManager.InRunAdvancementHandler.GetAdvacementSprite(type));
                        tempCanvas.SetPriceText(itemSpawnLocations[i].itemPrice);
                    }
                }

                advancement.transform.position = itemSpawnLocations[i].itemSpawn.position;
            }
        }
    }
}