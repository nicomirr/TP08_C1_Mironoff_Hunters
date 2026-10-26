using UnityEngine;
using Game.Data;
using Game.Core;

namespace Game.CameraSystem
{
    public class TargetFollower : MonoBehaviour
    {
        [SerializeField] private PlayerType _playerTypeTarget;

        [SerializeField] private Camera _camera;
        
        [Header("Camera Bounds")]
        [SerializeField] private bool _useLeftLimit;
        [SerializeField] private bool _useRightLimit;
        [SerializeField] private bool _useTopLimit;
        [SerializeField] private bool _useBottomLimit;

        [SerializeField] private float _minX;
        [SerializeField] private float _maxX;
        [SerializeField] private float _minY;
        [SerializeField] private float _maxY;


        [SerializeField] private Transform _target;

        [SerializeField] private TargetFollowerDataSo _data;

        private float _smoothTime;

        private float _velocityX;
        private float _velocityY;

                
        private void Start()
        {
            _camera.transform.position = new Vector3(_target.position.x, _target.position.y,
                _camera.transform.position.z);

            _smoothTime = _data.SmoothTime;
        }

        private void LateUpdate()
        {
            float xPosition = Mathf.SmoothDamp(_camera.transform.position.x, _target.position.x, 
                ref _velocityX, _smoothTime);

            float yPosition = Mathf.SmoothDamp(_camera.transform.position.y, _target.position.y,
                ref _velocityY, _smoothTime);

            float halfHeight = _camera.orthographicSize;
            float halfWidth = halfHeight * _camera.aspect;

            if (_useLeftLimit)
                xPosition = Mathf.Max(xPosition, _minX + halfWidth);

            if (_useRightLimit)
                xPosition = Mathf.Min(xPosition, _maxX - halfWidth);

            if (_useBottomLimit)
                yPosition = Mathf.Max(yPosition, _minY + halfHeight);

            if (_useTopLimit)
                yPosition = Mathf.Min(yPosition, _maxY - halfHeight);

            _camera.transform.position = new Vector3(xPosition, yPosition, _camera.transform.position.z);
            
        }

        public void ChangeLeftLimit(float newLimit)
        {
            _minX = newLimit;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            float lineLength = 100f;

            if (_useLeftLimit)            
                Gizmos.DrawLine(new Vector3(_minX, -lineLength, 0f), new Vector3(_minX, lineLength, 0f));            

            if (_useRightLimit)            
                Gizmos.DrawLine(new Vector3(_maxX, -lineLength, 0f), new Vector3(_maxX, lineLength, 0f));
            
            if (_useBottomLimit)            
                Gizmos.DrawLine(new Vector3(-lineLength, _minY, 0f), new Vector3(lineLength, _minY, 0f));
            

            if (_useTopLimit)            
                Gizmos.DrawLine(new Vector3(-lineLength, _maxY, 0f), new Vector3(lineLength, _maxY, 0f));            
        }

    }

}

