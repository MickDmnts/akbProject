using UnityEngine;

using akb.Entities.Player;
using UnityEngine.InputSystem;

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

        bool canPickup = false;

        int price = 0;
        InputAction buyAction;

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
            buyAction = ManagerHUB.GetManager.PlayerEntity.PlayerInputs.Player.Buy;
            buyAction.Enable();
            switch (pickType)
            {
                case PickType.PromptPickup:
                    if (other.CompareTag("Player"))
                    {
                        canPickup = true;
                    }
                    break;

                case PickType.AutoPickup:
                    if (other.GetComponent<PlayerEntity>())
                    {
                        ManagerHUB.GetManager.SlotsHandler.SetAdvancement(slot, advType);
                        Destroy(gameObject);
                    }
                    break;
            }
        }

        private void Update()
        {
            if (canPickup)
            {
                if (buyAction.WasPressedThisFrame())
                {
                    if (ManagerHUB.GetManager.CurrencyHandler.GetHellCoins >= price)
                    {
                        ManagerHUB.GetManager.SlotsHandler.SetAdvancement(slot, advType);

                        ManagerHUB.GetManager.CurrencyHandler.DecreaseHellCoinsBy(price);

                        Destroy(gameObject);
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