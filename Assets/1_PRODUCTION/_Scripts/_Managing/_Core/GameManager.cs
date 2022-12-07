using UnityEngine;

namespace AKB.Core.Managing
{
    public enum GameState
    {
        ENTRY,
        RUNNING,
        PAUSED,
        EXITING,
    }

    public class GameManager : MonoBehaviour
    {
        static GameManager _s;
        public static GameManager GetManager()
        {
            return _s;
        }

        private GameState _state = GameState.ENTRY;
        public GameState GetGameState() => _state;
        public void SetGameState(GameState state) => _state = state;

        private void Awake()
        {
            if (_s = null)
            {
                _s = this;
            }
        }
    }
}