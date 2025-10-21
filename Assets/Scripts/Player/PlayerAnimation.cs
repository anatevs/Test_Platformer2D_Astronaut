using UnityEngine;

namespace Gameplay
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        private const string IDLE = "IsIdle";
        private const string WALK = "IsWalking";
        private const string JUMP = "IsJumping";

        public void SetIdle()
        {
            if (!_animator.GetBool(IDLE))
            {
                _animator.SetBool(IDLE, true);
                _animator.SetBool(WALK, false);
                _animator.SetBool(JUMP, false);
            }
        }

        public void SetWalking()
        {
            if (!_animator.GetBool(WALK))
            {
                _animator.SetBool(WALK, true);
                _animator.SetBool(IDLE, false);
                _animator.SetBool(JUMP, false);
            }
        }

        public void SetJumping()
        {
            if (!_animator.GetBool(JUMP))
            {
                _animator.SetBool(JUMP, true);
                _animator.SetBool(IDLE, false);
                _animator.SetBool(WALK, false);
            }
        }
    }
}