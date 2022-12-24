using akb.Entities.Player;
using UnityEngine;

namespace akb.Core.Managing.InRunUpdates
{
    public class AdvancementPickUp : MonoBehaviour
    {
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
            if (other.GetComponent<PlayerEntity>())
            {
                Debug.Log("Is player");

                ManagerHUB.GetManager.SlotsHandler.SetAdvancement(slot, advType);

                Destroy(transform.root.gameObject);
            }
            else
            {
                Debug.Log("Not player");
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