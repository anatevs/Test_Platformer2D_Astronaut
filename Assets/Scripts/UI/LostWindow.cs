using UnityEngine;
using UnityEngine.UI;
using GameManagement;

namespace UI

{
	public class LostWindow: MonoBehaviour
	{
		[SerializeField]
		private Button _tryAgainButton;

		[SerializeField]
		private LevelManager _levelManager;

        public void Show()
        {
			_tryAgainButton.onClick.AddListener(TryAgain);
			gameObject.SetActive(true);
        }

        public void Hide()
        {
			_tryAgainButton.onClick.RemoveAllListeners();
			gameObject.SetActive(false);
        }

        private void TryAgain()
		{
			_levelManager.RestartCurrentLevel();
			Hide();
		}
	}
}