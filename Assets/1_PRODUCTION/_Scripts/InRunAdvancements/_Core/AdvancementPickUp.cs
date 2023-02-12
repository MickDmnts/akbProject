using akb.Entities.Player;
using UnityEngine;

namespace akb.Core.Managing.InRunUpdates
{
    public class AdvancementPickUp : MonoBehaviour
    {
        enum PickType
        {
            PromptPickup,
            AutoPickup
        }

        [Header("Set in inspector")]
        [SerializeField] PickType pickType = PickType.AutoPickup;
        [SerializeField] GameObject UI_Notifier;

        /// <summary>
        /// The advancement slot THIS gameObject corresponds to.
        /// </summary>
        SlotType slot = SlotType.None;

        /// <summary>
        /// The advancement type THIS gameObject represents in the world.
        /// </summary>
        AdvancementTypes advType = AdvancementTypes.None;

        private void OnTriggerEnter(Collider other)
        {
            switch (pickType)
            {
                case PickType.PromptPickup:
                    if (other.GetComponent<PlayerEntity>())
                    {
                        UI_Notifier.SetActive(true);
                    }
                    break;

                case PickType.AutoPickup:
                    if (other.GetComponent<PlayerEntity>())
                    {
                        ManagerHUB.GetManager.SlotsHandler.SetAdvancement(slot, advType);

                        Destroy(transform.root.gameObject);
                    }
                    break;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<PlayerEntity>())
            {
                UI_Notifier.SetActive(true);
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