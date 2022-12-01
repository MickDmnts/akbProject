using UnityEngine;

namespace AKB.Core.Managing.InRunUpdates
{
    /// <summary>
    /// The available player slots.
    /// Each slot type represents an array index.
    /// </summary>
    public enum SlotType
    {
        Attack = 0,
        Throw = 1,
        DodgeRoll = 2,
        Passive = 3,
        DevilRage = 4,
    }

    public class SlotsHandler : MonoBehaviour
    {
        [SerializeField] int slotSize = 5;

        IAdvanceable[] slots;

        //keep handlers here
        public AttackAdvancementHandler AttackAdvancementHandler { get; private set; }
        public SpearInRunAdvancements SpearInRunAdvancements { get; private set; }
        public DodgeInRunAdvancements DodgeInRunAdvancements { get; private set; }

        private void Awake()
        {
            slots = new IAdvanceable[slotSize];

            AttackAdvancementHandler = gameObject.AddComponent<AttackAdvancementHandler>();
            SpearInRunAdvancements = gameObject.AddComponent<SpearInRunAdvancements>();
            DodgeInRunAdvancements = gameObject.AddComponent<DodgeInRunAdvancements>();
        }

        public void SetAdvanceableAtSlot(SlotType slotType, IAdvanceable advanceable)
        {
            int slotIndex = (int)slotType;

            if (slotIndex < 0 || slotIndex >= slots.Length)
            {
                throw new System.ArgumentOutOfRangeException($"Slot size is 0-{slotSize}");
            }

            slots[slotIndex] = advanceable;
        }

        void NullifyAbilitiesOnDeath()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i] = null;
            }
        }

        void UpdateUserHUDSlots()
        {
            throw new System.NotImplementedException();
        }

        void ClearCurrentHUDSlots()
        {
            throw new System.NotImplementedException();
        }
    }
}