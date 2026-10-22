using UnityEngine;
using Game.Data;

namespace Game.Player.Brain
{
    public class PlayerLocomotionController 
    {
        private PlayerMovement _movement;
        private PlayerFacing _facing;

        public PlayerLocomotionController(PlayerLocomotionDataSo data, Rigidbody2D rb, Transform ownerTransform)
        {
            _movement = new PlayerMovement(data.MovementData, rb);
            _facing = new PlayerFacing(ownerTransform);
        }

        public void Update(float direction)
        {
            _facing.Flip(direction);
            _movement.Move(direction);
        }
    }

}

