using UnityEngine;
using Game.Data;

namespace Game.Player
{
    public class PlayerMovement
    {
        private readonly Rigidbody2D _rb;

        private readonly float _movementSpeed;

        public PlayerMovement(PlayerMovementDataSo data, Rigidbody2D rb)
        {
            _movementSpeed = data.MovementSpeed;
            _rb = rb;
        }

    public void Move(float direction)
        {
            _rb.linearVelocity = new Vector2(direction * _movementSpeed, _rb.linearVelocityY);
        }
    }

}
