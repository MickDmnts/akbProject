using UnityEngine;

using akb.Entities.Player;

namespace akb.Core.Managing.InRunUpdates
{
    public class AdvancementPickUp : MonoBehaviour
    {
        public enum PickType
        {
            PromptPickup,
            AutoPickup
        }

        [Header("Set in inspector")]
        [SerializeField] PickType pickType = PickType.AutoPickup;
        [SerializeField] GameObject UI_Notifier;

        bool canPickup = false;

        int price = 0;

        /// <summary>
        /// The advancement slot THIS gameObject corresponds to.
        /// </summary>
        SlotType slot = SlotType.None;

        /// <summary>
        /// The advancement type THIS gameObject represents in the world.
        /// </summary>
        AdvancementTypes advType = AdvancementTypes.None;

        public bool CanPickup => canPickup;
        public int Price => price;

        public SlotType Slot => slot;
        public AdvancementTypes AdvType => advType;
        public PickType PickupType => pickType;

        private void OnTriggerEnter(Collider other)
        {
            switch (pickType)
            {
                case PickType.PromptPickup:
                    if (other.GetComponent<PlayerEntity>())
                    {
                        UI_Notifier.SetActive(true);
                        canPickup = true;
                    }
                    break;

                case PickType.AutoPickup:
                    if (other.GetComponent<PlayerEntity>())
                    {
                        ManagerHUB.GetManager.SlotsHandler.SetAdvancement(slot, advType);
                        Debug.Log($"Set {slot} to {advType}");
                        Destroy(transform.root.gameObject);
                    }
                    break;
            }
        }

        private void Update()
        {
            if (canPickup)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (ManagerHUB.GetManager.CurrencyHandler.GetHellCoins >= price)
                    {
                        ManagerHUB.GetManager.SlotsHandler.SetAdvancement(slot, advType);

                        ManagerHUB.GetManager.CurrencyHandler.DecreaseHellCoinsBy(price);

                        Destroy(transform.root.gameObject);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            switch (pickType)
            {
                case PickType.PromptPickup:
                    if (other.GetComponent<PlayerEntity>())
                    {
                        UI_Notifier.SetActive(true);
                        canPickup = false;
                    }
                    break;

                case PickType.AutoPickup:

                    break;
            }
        }

        public void SetPickupType(PickType type, int price)
        {
            pickType = type;

            if (type == PickType.PromptPickup)
            {
                this.price = price;
            }
        }

        public void SetAdvancementType(AdvancementTypes passedType)
        {
            advType = passedType;
        }

        public AdvancementTypes GetAdvancementType() => advType;

        public void SetAdvancementSlot(SlotType slot)
        {
            this.slot = slot;
        }

        public SlotType GetAdvancementSlot() => slot;
    }
}