using UnityEngine;

namespace Gameplay
{
    public class EnemiesManager : MonoBehaviour
    {
        private Enemy[] _levelEnemies;

        private void OnEnable()
        {
            _levelEnemies = transform.GetComponentsInChildren<Enemy>();
        }

        public void StopEnemies()
        {
            for (int i = 0; i < _levelEnemies.Length; i++)
            {
                _levelEnemies[i].Stop();
            }
        }
    }
}