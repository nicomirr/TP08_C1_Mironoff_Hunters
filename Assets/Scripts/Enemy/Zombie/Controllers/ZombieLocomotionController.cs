using UnityEngine;
using Game.Data;

namespace Game.Enemy
{
    public class ZombieLocomotionController
    {        
        private readonly ForwardMovement _movement;

        private readonly Transform _ownerTransform;
        
        public ZombieLocomotionController(ZombieLocomotionControllerDataSo data, Rigidbody2D rb, Transform ownerTransform)
        {
            _ownerTransform = ownerTransform;

            _movement = new ForwardMovement(data.MovementData, rb, _ownerTransform);
        }

        public void UpdateMovement()
        {
            _movement.Move();
        }

        public void FaceTarget(Transform target)
        {
            if (target == null) return;

            _ownerTransform.right = target.position.x < _ownerTransform.position.x ? Vector2.left :
                Vector2.right;
        }                
    }

}
