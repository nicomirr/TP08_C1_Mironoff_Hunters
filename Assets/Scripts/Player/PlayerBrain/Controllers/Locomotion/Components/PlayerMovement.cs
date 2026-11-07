using UnityEngine;
using Game.Data;
using Game.Core;

namespace Game.Player
{
    public class PlayerMovement
    {
        private readonly PlayerMovementDataSo _data;

        private readonly Rigidbody2D _rb;

        private IMovementProvider _movementProvider;

        private float _movementSpeed;

        private float _outsideMovement;
        private float _direction;
        public IMovementProvider MovementProvider => _movementProvider;

        public PlayerMovement(PlayerMovementDataSo data, Rigidbody2D rb)
        {
            _data = data;

            _rb = rb;
            _movementSpeed = data.RegularMovementSpeed;
        }

        public void ChangeMovementSpeedType(bool isOnMud)
        {
            if (isOnMud)
            {
                _movementSpeed = _data.MudMovementSpeed;
                return;
            }

            _movementSpeed = _data.RegularMovementSpeed;
        }

        public void ChangeDirection(float direction)
        {           
            _direction = direction;
        }

        public void UpdateMovement()
        {
            if (_movementProvider != null)
                _outsideMovement = _movementProvider.Speed;

            _rb.linearVelocityX = _direction * _movementSpeed + _outsideMovement;
        }

        public void HandleOutsideMovement(IMovementProvider movementProvider)
        {
            _movementProvider = movementProvider;
        }

        public void StopOutsideMovement()
        {
            _outsideMovement = 0;
        }

        public void StopMovement()
        {
            _movementProvider = null;
            _rb.linearVelocity = new Vector2(0, _rb.linearVelocityY);
        }       
    }
}
