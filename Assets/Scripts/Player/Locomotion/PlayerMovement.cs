using UnityEngine;
using Game.Data;

namespace Game.Player
{
    public class PlayerMovement
    {
        private readonly PlayerHorizontalBounds _bounds;
        
        private readonly Rigidbody2D _rb;

        private readonly float _movementSpeed;

        public PlayerMovement(PlayerMovementDataSo data, Rigidbody2D rb)
        {
            _bounds = new PlayerHorizontalBounds(data.PlayerBoundsData);

            _rb = rb;
            _movementSpeed = data.MovementSpeed;

        }

        public void Move(float direction)
        {
            Vector2 bounds = _bounds.GetHorizontalBounds();

            float minBounds = bounds.x;
            float maxBounds = bounds.y;

            float currentX = Mathf.Clamp(_rb.position.x, minBounds, maxBounds);

            if (currentX != _rb.position.x)
                _rb.position = new Vector2(currentX, _rb.position.y);

            float velocityX = direction * _movementSpeed;

            float nextX = currentX + velocityX * Time.fixedDeltaTime;
            float clampedNextX = Mathf.Clamp(nextX, minBounds, maxBounds);

            velocityX = (clampedNextX - currentX) / Time.fixedDeltaTime;

            _rb.linearVelocity = new Vector2(velocityX, _rb.linearVelocityY);
        }
    }

}
