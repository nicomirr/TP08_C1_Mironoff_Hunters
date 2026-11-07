using UnityEngine;
using Game.Checkpoints;
using Game.Data;
using Game.Marker;
using Game.Core;

namespace Game.Environment
{
    public class MovingPlatform : MonoBehaviour, IMovementProvider
    {
        [SerializeField] private MovingPlatformDataSo _data;
        [SerializeField][Range(-1, 1)] private int _initialDirection; 

        private Rigidbody2D _rb;

        private Vector2 _initialPos;

        private int _currentDirection;  

        private bool _isActive;

        public float Speed => _data.Speed * _currentDirection;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            Checkpoint.OnCheckpointUsed += ResetPlatform;
        }

        private void Start()
        {
            _initialPos = this.transform.position;
            _currentDirection = _initialDirection;
        }

        private void FixedUpdate()
        {
            if (!_isActive) return;

            _rb.linearVelocityX = _data.Speed * _currentDirection;
        }

        private void OnDisable()
        {
            Checkpoint.OnCheckpointUsed -= ResetPlatform;
        }

        private void ToggleDirection()
        {
            _currentDirection *= -1;
        }

        private void ResetPlatform(int _)
        {
            _isActive = false;
            _currentDirection = _initialDirection;

            this.transform.position = _initialPos;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {           
            if(collision.gameObject.TryGetComponent(out PlayerMarker _))
                _isActive = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out DirectionChangerMarker _))
                ToggleDirection();
        }
    }
}

