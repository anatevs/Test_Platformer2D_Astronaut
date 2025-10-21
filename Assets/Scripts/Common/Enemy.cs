using UnityEngine;
using DG.Tweening;

namespace Gameplay
{
    [RequireComponent(typeof(Collider2D))]
    public class Enemy : MonoBehaviour
    {
        public bool IsDangerous => _isDangerous;

        [SerializeField]
        private float _halfPatrolLength = 2f;

        [SerializeField]
        private float _patrolDuration = 0.5f;

        [SerializeField]
        private int _damage = 2;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private Sequence _sequence;

        private bool _isDangerous = true;

        private float _centerX;
        private float[] _patrolPointsX;

        private void OnEnable()
        {
            _isDangerous = true;

            _centerX = transform.position.x;

            _patrolPointsX = new float[2]
            {
                _centerX - _halfPatrolLength,
                _centerX + _halfPatrolLength
            };

            transform
                .DOMoveX(_patrolPointsX[0], _patrolDuration / 2)
                .OnStart(() => SetLookRight(false))
                .OnComplete(() => MakePatrol());
        }

        public int PlayerCollide()
        {
            _isDangerous = false;

            return _damage;
        }

        public void PlayerExit()
        {
            _isDangerous = true;
        }

        public void Stop()
        {
            _sequence.Kill();
        }

        public void Kill()
        {
            Stop();
            transform.gameObject.SetActive(false);
        }

        private void MakePatrol()
        {
            _sequence = DOTween.Sequence().Pause();

            _sequence
                .OnStart(() => SetLookRight(true))
                .Append(transform.DOMoveX(_patrolPointsX[1], _patrolDuration))
                .AppendCallback(() => SetLookRight(false))
                .Append(transform.DOMoveX(_patrolPointsX[0], _patrolDuration))
                .AppendCallback(() => MakePatrol())
                .Play();
        }

        private void SetLookRight(bool isRight)
        {
            _spriteRenderer.flipX = isRight;
        }
    }
}