using UnityEngine;

namespace Game.CameraSystem
{
    public class TargetFollower : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        [SerializeField] private TargetFollowerDataSo _data;

        private float _smoothTime;

        private float _velocityX;
        private float _velocityY;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

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

            _camera.transform.position = new Vector3(xPosition, yPosition, _camera.transform.position.z);
            
        }
    }
}

