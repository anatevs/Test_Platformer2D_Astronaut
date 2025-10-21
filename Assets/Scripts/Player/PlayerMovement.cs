using GameManagement;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerMovement : MonoBehaviour
    {
        public bool EnemyLanding => _isLandingOnEnemy;

        [SerializeField]
        private PlayerInput _playerInput;

        [SerializeField]
        private PlayerAnimation _playerAnimation;

        [SerializeField]
        private SpriteRenderer _playerSprite;

        [SerializeField]
        private float _speed = 1f;

        [SerializeField]
        private float _jumpSpeed = 1f;

        [SerializeField]
        private float _downCheckLength = 0.05f;

        [SerializeField]
        private float _fallGravity = 2f;

        [SerializeField]
        private LayerMask _downLayerMask;

        [SerializeField]
        private int _groundLayer;

        [SerializeField]
        private int _enemyLayer;

        [SerializeField]
        private Transform _bottomBorder;

        [SerializeField]
        private GameManager _gameManager;

        private bool _isLandingOnEnemy = false;

        private bool _isGrounded = true;

        private int _maxJumps = 2;

        private int _jumpCount = 0;

        private Rigidbody2D _rb;

        private Vector2 _downCheckBox;

        private Vector3 _startPos;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            var collider = GetComponent<BoxCollider2D>();

            _downCheckBox = new Vector2(collider.bounds.size.x, _downCheckLength);

            _startPos = transform.position;
        }

        private void OnEnable()
        {
            _playerInput.OnJumped += MakeJump;
        }

        private void OnDisable()
        {
            _playerInput.OnJumped -= MakeJump;
        }

        private void FixedUpdate()
        {
            if (_gameManager.State == GameState.PLAYING)
            {
                if (transform.position.y <= _bottomBorder.position.y)
                {
                    _gameManager.SetState(GameState.LOST);
                    return;
                }

                MoveHor(_playerInput.HorMove);

                if (_rb.velocity.y < 0)
                {
                    _rb.gravityScale = _fallGravity;

                    var hit = Physics2D.BoxCast(transform.position, _downCheckBox, 0, Vector2.down, _downCheckLength, _downLayerMask);

                    if (hit)
                    {
                        _jumpCount = 0;
                        _rb.gravityScale = 1;

                        if (hit.collider.gameObject.layer == _groundLayer)
                        {
                            _isGrounded = true;
                        }
                        else if (hit.collider.gameObject.layer == _enemyLayer)
                        {
                            _isLandingOnEnemy = true;
                        }
                    }
                }
                else
                {
                    _isLandingOnEnemy = false;
                }

                if (!_isGrounded)
                {
                    _playerAnimation.SetJumping();
                }
            }

            else
            {
                MoveHor(0);
            }
        }

        public void SetToStartPos()
        {
            gameObject.SetActive(false);
            transform.position = _startPos;
            gameObject.SetActive(true);
        }

        public void MakeJump()
        {
            _isGrounded = false;

            if (_jumpCount < _maxJumps)
            {
                _jumpCount++;
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpSpeed);
            }
        }

        private void MoveHor(float horMove)
        {
            _rb.velocity = new Vector2(horMove * _speed, _rb.velocity.y);

            var isWalking = Mathf.Abs(horMove) > 0;

            _playerSprite.flipX = horMove < 0;

            if (_isGrounded)
            {
                if (isWalking)
                {
                    _playerAnimation.SetWalking();
                }
                else
                {
                    _playerAnimation.SetIdle();
                }
            }
        }
    }
}