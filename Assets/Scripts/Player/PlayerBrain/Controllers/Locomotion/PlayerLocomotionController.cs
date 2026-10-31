using UnityEngine;
using System;
using Game.Data;

namespace Game.Player.Brain
{
    public class PlayerLocomotionController 
    {
        private readonly PlayerMovement _movement;
        private readonly PlayerJump _jump;
        private readonly PlayerFacing _facing;
        private readonly PlayerGroundCheck _groundCheck;
        private readonly PlayerGravity _gravity;
        public bool IsGrounded => _groundCheck.IsGrounded;

        public event Action OnJustLanded
        {
            add => _groundCheck.OnJustLanded += value;
            remove => _groundCheck.OnJustLanded -= value;
        }

        public PlayerLocomotionController(PlayerLocomotionDataSo data, Rigidbody2D rb, Transform ownerTransform, Transform groundCheckTransform)
        {
            _movement = new PlayerMovement(data.MovementData, rb);
            _jump = new PlayerJump(data.JumpData, rb);
            _facing = new PlayerFacing(ownerTransform);
            _groundCheck = new PlayerGroundCheck(data.GroundCheckData, groundCheckTransform);
            _gravity = new PlayerGravity(data.GravityData, rb);
        }        

        public void UpdateMovement(float direction)
        {
            _facing.Flip(direction);
            _movement.Move(direction);
        }

        public void UpdateCoyoteTime()
        {    
            _jump.UpdateCoyoteTime(_groundCheck.IsGrounded);            
        }

        public void UpdateGravity()
        {
            _gravity.UpdateGravity();
        }

        public void UpdateGroundState()
        {
            _groundCheck.UpdateGroundedState();
        }

        public bool HandleJump()
        {
            if (!_jump.CanJump) return false;

            _jump.Jump();

            return true;
        }

        public void CutJump()
        {
            if (_groundCheck.IsGrounded) return;
            _jump.CutJump();
        }

        public void HandleLand()
        {
            _jump.ResetJump();
        }       

        public void StopMovement()
        {
            _gravity.SetFallingGravity();
            _jump.CutJump();
            _jump.ResetJump();
            _movement.StopMovement();
        }
    }
}

