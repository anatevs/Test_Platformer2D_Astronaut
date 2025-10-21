using Gameplay;
using System;
using UnityEngine;

namespace GameManagement
{
	public class LevelManager : MonoBehaviour
	{
        public event Action OnNextLevelSet;

		public event Action OnGameWin;

		[SerializeField]
		private Level[] _levelPrefabs;

        [SerializeField]
        private CameraMove _cameraMover;

		[SerializeField]
		private PlayerCollisions _playerCollisions;

        [SerializeField]
        private PlayerInfoController _playerInfoController;

        private PlayerMovement _playerMovement;

		private Level _currentLevel;

		private int _currentLevelIndex = -1;

        private void Awake()
        {
            _playerMovement = _playerCollisions.GetComponent<PlayerMovement>();
        }

        private void OnEnable()
        {
			_playerCollisions.OnExitPortalCollided += SetNextLevel;

            _playerInfoController.OnHPEmpty += Restart;
        }

        private void OnDisable()
        {
            _playerCollisions.OnExitPortalCollided -= SetNextLevel;

            _playerInfoController.OnHPEmpty -= Restart;
        }

        public void StopCurrentLevel()
		{
            _currentLevel.Stop();
        }

        public void SetNextLevel()
        {
            _currentLevelIndex++;

            if (_currentLevelIndex < _levelPrefabs.Length)
            {
                SetLevel(_currentLevelIndex);
            }
            else
            {
                OnGameWin?.Invoke();
            }
        }

        public void RestartCurrentLevel()
		{
			SetLevel(_currentLevelIndex);

            _playerInfoController.ResetLevel();
		}

		public void Restart()
		{
			_currentLevelIndex = 0;

			SetLevel(_currentLevelIndex);

            _playerInfoController.ResetGame();
		}

        private void SetLevel(int index)
        {
            if (_currentLevel != null)
            {
                StopCurrentLevel();
                _currentLevel.gameObject.SetActive(false);
                GameObject.Destroy(_currentLevel.gameObject);
            }

            _currentLevel = Instantiate(_levelPrefabs[index]);

            _currentLevelIndex = index;

            _playerCollisions.SetLevelExitGO(_currentLevel.ExitPortal);

            _playerMovement.SetToStartPos();

            _cameraMover.SetToStartPos();

            OnNextLevelSet?.Invoke();
        }
    }
}