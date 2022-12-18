using akb.Core.Managing.PCG;
using akb.Core.Managing.GameEvents;

using akb.Entities.Player;
using akb.Core.Managing.LevelLoading;
using akb.Core.Managing.UI;
using akb.Core.Managing.UpdateSystem;
using akb.Core.Managing.InRunUpdates;

namespace akb.Core.Managing
{
    /// <summary>
    /// This class holds all the available in-game managers the scripts will need.
    /// </summary>
    public class ManagerHUB
    {
        /// <summary>
        /// The private instnce reference.
        /// </summary>
        static ManagerHUB _s;
        /// <summary>
        /// Get the ManageHUB singleton.
        /// </summary>
        public static ManagerHUB GetManager => _s;

        /// <summary>
        /// Initializes the Manager Hub class.
        /// </summary>
        public ManagerHUB()
        {
            if (_s != null) { throw new System.Exception("Tried to create multiple manager hubs!"); }
            else
            {
                _s = this;

                GameEventsHandler = new GameEventsHandler();
                AdvancementHandler = new AdvancementHandler();
            }
        }

        #region HANDLERS
        /// <summary>
        /// Get the GameEventsHandler class.
        /// </summary>
        public GameEventsHandler GameEventsHandler { get; private set; }

        /// <summary>
        /// Get the AdvancementHandler class.
        /// </summary>
        public AdvancementHandler AdvancementHandler { get; private set; }

        /// <summary>
        /// Get the SlotsHandler class.
        /// </summary>
        public SlotsHandler SlotsHandler { get; private set; }

        /// <summary>
        /// Get the LevelManager class.
        /// </summary>
        public LevelManager LevelManager { get; private set; }

        /// <summary>
        /// Get the UI_Manager class.
        /// </summary>
        public UI_Manager UIManager { get; private set; }

        /// <summary>
        /// Get the RoomSelector class.
        /// </summary>
        public RoomSelector RoomSelector { get; private set; }

        /// <summary>
        /// Get the StatusEffectManager class.
        /// </summary>
        public StatusEffectManager StatusEffectManager { get; private set; }

        /// <summary>
        /// Get the SpearPool class.
        /// </summary>
        public SpearPool SpearPool { get; private set; }

        /// <summary>
        /// Get the ProjectilePools class.
        /// </summary>
        public ProjectilePools ProjectilePools { get; private set; }

        /// <summary>
        /// Get the PlayerEntity class.
        /// </summary>
        public PlayerEntity PlayerEntity { get; private set; }
        #endregion

        #region REFERENCE_MUTATORS
        /// <summary>
        /// Sets the LevelManager class
        /// </summary>
        public void SetLevelManagerReference(LevelManager reference)
        {
            LevelManager = reference;
        }

        /// <summary>
        /// Sets the UI_Manager class
        /// </summary>
        public void SetUI_ManagerReference(UI_Manager reference)
        {
            UIManager = reference;
        }

        /// <summary>
        /// Sets the StatusEffectManager class
        /// </summary>
        public void SetStatusEffectRef(StatusEffectManager reference)
        {
            StatusEffectManager = reference;
        }

        /// <summary>
        /// Sets the PlayerEntity class
        /// </summary>
        public void SetPlayerReference(PlayerEntity reference)
        {
            PlayerEntity = reference;
        }

        /// <summary>
        /// Sets the RoomSelector class
        /// </summary>
        public void SetRoomSelector(RoomSelector reference)
        {
            RoomSelector = reference;
        }

        /// <summary>
        /// Sets the SpearPool class
        /// </summary>
        public void SetSpearPoolReference(SpearPool reference)
        {
            SpearPool = reference;
        }

        /// <summary>
        /// Sets the ProjectilePools class
        /// </summary>
        public void SetProjectilePoolsReference(ProjectilePools reference)
        {
            ProjectilePools = reference;
        }

        /// <summary>
        /// Sets the SlotsHandler class
        /// </summary>
        public void SetSlotHandlerReference(SlotsHandler reference)
        {
            SlotsHandler = reference;
        }
        #endregion
    }
}
