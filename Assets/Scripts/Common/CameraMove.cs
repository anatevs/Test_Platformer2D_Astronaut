using UnityEngine;

namespace Gameplay
{
    public class CameraMove : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private Transform[] _levelBordersX;

        [SerializeField]
        private Transform[] _levelBordersY;

        [SerializeField]
        private Transform _target;

        private float _halfX;
        private float _halfY;

        private float _zPos;
        private float _lerpCoef = 0.5f;

        private Vector3 _startPos;

        private void Awake()
        {
            _halfX = _camera.orthographicSize * _camera.aspect;
            _halfY = _camera.orthographicSize;

            _zPos = transform.position.z;

            _startPos = transform.position;
        }

        private void FixedUpdate()
        {
            var xPos = _target.position.x;
            var yPos = _target.position.y;
            if (!IsInBordersX(xPos))
            {
                xPos = transform.position.x;
            }
            if (!IsInBordersY(yPos))
            {
                yPos = transform.position.y;
            }

            var newPos = 
                new Vector3(xPos, yPos, _zPos);

            transform.position = Vector3.Lerp(transform.position, newPos, _lerpCoef);
        }

        public void SetToStartPos()
        {
            transform.position = _startPos;
        }

        private bool IsInBordersX(float xPos)
        {
            return (xPos - _halfX >= _levelBordersX[0].position.x &&
                xPos + _halfX <= _levelBordersX[1].position.x);
        }

        private bool IsInBordersY(float yPos)
        {
            return (yPos - _halfY >= _levelBordersY[0].position.y);
        }
    }
}