using UnityEngine;
using System;
using Game.Data;
using Game.Core;

namespace Game.Player
{
    public class PlayerLocomotionController 
    {
        private readonly PlayerMovement _movement;
        private readonly PlayerJump _jump;
        private readonly PlayerFacing _facing;
        private readonly PlayerGroundCheck _groundCheck;
        private readonly PlayerGravity _gravity;
        private readonly PlayerFootsteps _footsteps;

        public IMovementProvider MovementProvider => _movement.MovementProvider;
        public bool IsGrounded => _groundCheck.IsGrounded;

        public event Action OnJustLanded
        {
            add => _groundCheck.OnJustLanded += value;
            remove => _groundCheck.OnJustLanded -= value;
        }

        public PlayerLocomotionController(PlayerLocomotionControllerDataSo data, Rigidbody2D rb, Transform ownerTransform, Transform groundCheckTransform, AudioSource footstepsAudioSource)
        {
            _movement = new PlayerMovement(data.MovementData, rb);
            _jump = new PlayerJump(data.JumpData, rb);
            _facing = new PlayerFacing(ownerTransform);
            _groundCheck = new PlayerGroundCheck(data.GroundCheckData, groundCheckTransform);
            _gravity = new PlayerGravity(data.GravityData, rb);
            _footsteps = new PlayerFootsteps(footstepsAudioSource);
        }        

        public void UpdateMovement(float direction)
        {
            _facing.Flip(direction);
            _movement.ChangeDirection(direction);
            _movement.UpdateMovement();
        }

        public void HandleFootstepsSound()
        {
            if (!_groundCheck.IsGrounded) return;

            _footsteps.PlayFootsteps();
        }

        public void HandleOutsideMovement(IMovementProvider movementProvider)
        {
            _movement.HandleOutsideMovement(movementProvider);
        }

        public void StopOutsideMovement()
        {
            _movement.StopOutsideMovement();
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

        public void UpdateLocomotionBySurfaceType()
        {
            _movement.ChangeMovementSpeedType(_groundCheck.IsOnMud);
            _jump.ChangeJumpState(_groundCheck.IsOnMud);

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

