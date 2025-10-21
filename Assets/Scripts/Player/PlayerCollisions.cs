using System;
using UnityEngine;

namespace Gameplay
{
    public class PlayerCollisions : MonoBehaviour
    {
        public event Action OnExitPortalCollided;

        [SerializeField]
        private PlayerInfoController _playerInfo;

        [SerializeField]
        private PlayerMovement _playerMove;

        private GameObject _levelExitPortal;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<Reward>(out var reward))
            {
                _playerInfo.AddReward(reward.Cost);

                reward.Hide();
            }

            else if (collision.gameObject.TryGetComponent<Enemy>(out var enemy))
            {
                if (enemy.IsDangerous)
                {
                    var damage = enemy.PlayerCollide();

                    if (!_playerMove.EnemyLanding)
                    {
                        _playerInfo.ChangeHP(-damage);
                    }

                    else
                    {
                        _playerInfo.ChangeHP(damage);
                        _playerMove.MakeJump();
                        enemy.Kill();
                    }
                }
            }

            else if (collision.gameObject == _levelExitPortal)
            {
                OnExitPortalCollided?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.PlayerExit();
            }
        }

        public void SetLevelExitGO(GameObject levelExitPortal)
        {
            _levelExitPortal = levelExitPortal;
        }
    }
}