using Gameplay;
using UnityEngine;

namespace GameManagement
{
    public class Level : MonoBehaviour
    {
        public GameObject ExitPortal => _exitPortal;

        [SerializeField]
        private EnemiesManager _enemiesManager;

        [SerializeField]
        private GameObject _exitPortal;

        public void Stop()
        {
            _enemiesManager.StopEnemies();
        }
    }
}