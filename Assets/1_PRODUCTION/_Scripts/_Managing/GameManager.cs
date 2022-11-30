using UnityEngine;

using AKB.Core.Managing.PCG;
using AKB.Core.Managing.GameEvents;

using AKB.Entities.Player;
using AKB.Core.Managing.LevelLoading;
using AKB.Core.Managing.UI;
using AKB.Core.Managing.UpdateSystem;
using AKB.Core.Managing.InRunUpdates;

namespace AKB.Core.Managing
{
    [DefaultExecutionOrder(10)]
    public class GameManager : MonoBehaviour
    {
        static GameManager _s;
        public static GameManager S
        {
            get
            {
                if (_s is null)
                {
                    Debug.LogError("Game manager is null");
                }

                return _s;
            }
        }

        #region HANDLERS
        public GameEventsHandler GameEventsHandler { get; private set; }
        public AdvancementHandler AdvancementHandler { get; private set; }
        public SlotsHandler SlotsHandler { get; private set; }

        public LevelManager LevelManager { get; private set; }

        public UI_Manager UIManager { get; private set; }

        public RoomSelector RoomSelector { get; private set; }
        public StatusEffectManager StatusEffectManager { get; private set; }

        public SpearPool SpearPool { get; private set; }
        public ProjectilePools ProjectilePools { get; private set; }

        public PlayerEntity PlayerEntity { get; private set; }
        #endregion

        private void Awake()
        {
            _s = this;

            CacheHandlers();
        }

        #region REFERENCE_MUTATORS
        void CacheHandlers()
        {
            GameEventsHandler = new GameEventsHandler();
            AdvancementHandler = new AdvancementHandler();

            LevelManager = FindObjectOfType<LevelManager>();
            SlotsHandler = FindObjectOfType<SlotsHandler>();
        }

        public void SetUI_ManagerReference(UI_Manager reference)
        {
            UIManager = reference;
        }

        public void SetStatusEffectRef(StatusEffectManager reference)
        {
            StatusEffectManager = reference;
        }

        public void SetPlayerReference(PlayerEntity reference)
        {
            PlayerEntity = reference;
        }

        public void SetRoomSelector(RoomSelector reference)
        {
            RoomSelector = reference;
        }

        public void SetSpearPoolReference(SpearPool reference)
        {
            SpearPool = reference;
        }

        public void SetProjectilePoolsReference(ProjectilePools reference)
        {
            ProjectilePools = reference;
        }
        #endregion

        private void OnDestroy()
        {
            _s = null;
        }
    }
}
