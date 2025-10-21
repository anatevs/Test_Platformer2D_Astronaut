using System;
using UnityEngine;

namespace Gameplay
{
    public class PlayerInput : MonoBehaviour
    {
        public event Action OnJumped;

        public float HorMove => _horMove;

        public bool IsJump => _isJump;

        private float _horMove;

        private bool _isJump;

        private void Update()
        {
            _horMove = Input.GetAxis("Horizontal");

            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnJumped?.Invoke();
            }
        }
    }
}