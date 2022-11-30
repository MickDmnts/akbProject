using UnityEngine;

using AKB.Core.Managing.PCG;
using AKB.Core.Managing.GameEvents;

using AKB.Entities.Player;
using AKB.Core.Managing.LevelLoading;
using AKB.Core.Managing.UI;
using AKB.Core.Managing.UpdateSystem;

namespace AKB.Core.Managing
{
    [DefaultExecutionOrder(10)]
    public class GameManager : MonoBehaviour
    {
        static GameManager _S;
        public static GameManager S
        {
            get
            {
                if (_S is null)
                {
                    Debug.LogError("Game manager is null");
                }

                return _S;
            }
        }

        public GameEventsHandler GameEventsHandler { get; private set; }
        public AdvancementHandler AdvancementHandler { get; private set; }

        public LevelManager LevelManager { get; private set; }

        public UI_Manager UIManager { get; private set; }

        public RoomSelector RoomSelector { get; private set; }
        public StatusEffectManager StatusEffectManager { get; private set; }

        public SpearPool SpearPool { get; private set; }
        public ProjectilePools ProjectilePools { get; private set; }

        public PlayerEntity PlayerEntity { get; private set; }

        private void Awake()
        {
            _S = this;

            GameEventsHandler = new GameEventsHandler();
            AdvancementHandler = new AdvancementHandler();

            LevelManager = FindObjectOfType<LevelManager>();
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameManager.S.AdvancementHandler.AdvanceTierOf(AdvancementType.DevilRage);
            }
        }

        private void OnDestroy()
        {
            _S = null;
        }
    }
}
