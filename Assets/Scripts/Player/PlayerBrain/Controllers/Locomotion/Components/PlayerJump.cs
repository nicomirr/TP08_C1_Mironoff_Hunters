using UnityEngine;
using Game.Data;

namespace Game.Player
{
    public class PlayerJump
    {
        private readonly PlayerJumpDataSo _data;

        private readonly Rigidbody2D _rb;

        private float _coyoteTimer;

        private bool _jumpUsed;
        private bool _doubleJumpUsed;

        public bool AllJumpsUsed => _jumpUsed && _doubleJumpUsed;

        public bool CanJump => (_coyoteTimer > 0f && !_jumpUsed) || (_jumpUsed && !_doubleJumpUsed);

        public PlayerJump(PlayerJumpDataSo data, Rigidbody2D rb)
        {
            _data = data;
            _rb = rb;
        }

        public void UpdateCoyoteTime(bool isGrounded)
        {
            if (isGrounded)
                _coyoteTimer = _data.CoyoteTime;
            else
                _coyoteTimer -= Time.deltaTime;
        }

        public void Jump()
        {            
            _coyoteTimer = 0;

            _rb.linearVelocityY = 0f;
            _rb.AddForce(Vector2.up * _data.JumpForce, ForceMode2D.Impulse);

            if (!_jumpUsed)
            {
                _jumpUsed = true;
                return;
            }

            _doubleJumpUsed = true;
        }

        public void CutJump()
        {
            if (_rb.linearVelocityY <= 0)
                return;

            _rb.linearVelocityY *= _data.JumpCutMultiplier;
        }     
        
        public void ResetJump()
        {
            _jumpUsed = false;
            _doubleJumpUsed = false;
        }
    }
}

