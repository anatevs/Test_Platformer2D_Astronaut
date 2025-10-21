using UI;
using UnityEngine;

namespace GameManagement
{
    public class GameManager : MonoBehaviour
    {
        public GameState State => _state;

        [SerializeField]
        private LostWindow _lostWindow;

        [SerializeField]
        private GameObject _winWindow;

        [SerializeField]
        private LevelManager _levelManager;

        private GameState _state;

        public void OnEnable()
        {
            _levelManager.OnNextLevelSet += StartLevel;
            _levelManager.OnGameWin += WinGame;
        }

        public void OnDisable()
        {
            _levelManager.OnNextLevelSet -= StartLevel;
            _levelManager.OnGameWin -= WinGame;
        }

        private void Start()
        {
            _levelManager.SetNextLevel();
        }

        public void SetState(GameState state)
        {
            _state = state;

            if (state == GameState.LOST)
            {
                _lostWindow.Show();
            }
        }

        private void StartLevel()
        {
            _state = GameState.PLAYING;
        }

        private void WinGame()
        {
            _state = GameState.WIN;
            _winWindow.SetActive(true);
        }
    }

    public enum GameState
    {
        PLAYING,
        LOST,
        WIN
    }
}