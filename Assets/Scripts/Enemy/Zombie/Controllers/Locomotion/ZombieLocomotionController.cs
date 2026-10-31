using Game.Data;
using UnityEngine;

namespace Game.Enemy
{
    public class ZombieLocomotionController
    {        
        private readonly ForwardMovement _movement;
        
        public ZombieLocomotionController(ZombieLocomotionControllerDataSo data, Rigidbody2D rb, Transform ownerTransform)
        {
            _movement = new ForwardMovement(data.MovementData, rb, ownerTransform);
        }

        public void UpdateMovement()
        {
            _movement.Move();
        }
    }

}
