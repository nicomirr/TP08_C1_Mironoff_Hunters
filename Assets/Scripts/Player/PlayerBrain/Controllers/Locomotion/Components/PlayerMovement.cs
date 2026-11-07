using UnityEngine;
using Game.Data;

namespace Game.Player
{
    public class PlayerMovement
    {
        private PlayerMovementDataSo _data;

        private readonly Rigidbody2D _rb;

        private float _movementSpeed;

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

        public void Move(float direction)
        {           
            _rb.linearVelocity = new Vector2(direction * _movementSpeed, _rb.linearVelocityY);
        }

        public void StopMovement()
        {
            _rb.linearVelocity = new Vector2(0, _rb.linearVelocityY);
        }       
    }
}
