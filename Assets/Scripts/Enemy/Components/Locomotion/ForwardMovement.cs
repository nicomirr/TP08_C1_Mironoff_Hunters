using UnityEngine;
using Game.Data;

namespace Game.Enemy
{
    public class ForwardMovement
    {
        protected readonly float _movementSpeed;
        protected readonly Rigidbody2D _rb;
        protected readonly Transform _ownerTransform;

        public ForwardMovement(ForwardMovementDataSo data, Rigidbody2D rb, Transform enemyTransform)
        {
            _movementSpeed = data.MovementSpeed;
            _rb = rb;
            _ownerTransform = enemyTransform;
        }

        public void Move()
        {
            _rb.linearVelocity = new Vector2(_movementSpeed * _ownerTransform.right.x, _rb.linearVelocity.y);
        }
    }

}
