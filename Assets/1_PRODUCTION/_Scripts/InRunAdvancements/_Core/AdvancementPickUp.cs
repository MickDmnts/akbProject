using akb.Entities.Player;
using UnityEngine;

namespace akb.Core.Managing.InRunUpdates
{
    public enum AdvancementSlot
    {
        None,

        Attack,
        Spear,
        Dodge,
        Passive,
        DevilRage
    }

    public class AdvancementPickUp : MonoBehaviour
    {
        /// <summary>
        /// The advancement slot THIS gameObject corresponds to.
        /// </summary>
        AdvancementSlot slot = AdvancementSlot.None;

        /// <summary>
        /// The advancement type THIS gameObject represents in the world.
        /// </summary>
        AdvancementTypes advType = AdvancementTypes.None;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerEntity>())
            {
                Debug.Log("Is player");
                DetermineSlot(slot, advType);
                Destroy(transform.root.gameObject);
            }
            else
            {
                Debug.Log("Not player");
            }
        }

        void DetermineSlot(AdvancementSlot slot, AdvancementTypes type)
        {
            switch (slot)
            {
                case AdvancementSlot.Attack:
                    {
                        ManagerHUB.GetManager.SlotsHandler.AttackInRunAdvancements.SetActiveAdvancement(type);
                    }
                    break;
                case AdvancementSlot.Spear:
                    {
                        ManagerHUB.GetManager.SlotsHandler.SpearInRunAdvancements.SetActiveAdvancement(type);
                    }
                    break;
                case AdvancementSlot.Dodge:
                    {
                        ManagerHUB.GetManager.SlotsHandler.DodgeInRunAdvancements.SetActiveAdvancement(type);
                    }
                    break;
                case AdvancementSlot.Passive:
                    {
                        ManagerHUB.GetManager.SlotsHandler.PassiveInRunAdvancements.SetActiveAdvancement(type);
                    }
                    break;
                case AdvancementSlot.DevilRage:
                    {
                        ManagerHUB.GetManager.SlotsHandler.DevilRageInRunAdvancements.SetActiveAdvancement(type);
                    }
                    break;
            }
        }

        public void SetAdvancementType(AdvancementTypes passedType)
        {
            advType = passedType;
        }

        public AdvancementTypes GetAdvancementType() => advType;

        public void SetAdvancementSlot(AdvancementSlot slot)
        {
            this.slot = slot;
        }

        public AdvancementSlot GetAdvancementSlot() => slot;
    }
}