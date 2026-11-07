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
      
        private float _jumpForce;

        private bool _isOnMud;

        public bool AllJumpsUsed => _jumpUsed && _doubleJumpUsed;

        public bool CanJump => (_coyoteTimer > 0f && !_jumpUsed) || (_jumpUsed && !_doubleJumpUsed);

        public PlayerJump(PlayerJumpDataSo data, Rigidbody2D rb)
        {
            _data = data;
            _rb = rb;

            _jumpForce = _data.RegularJumpForce;
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
            if (_isOnMud) return;

            _coyoteTimer = 0;

            _rb.linearVelocityY = 0f;
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

            if (!_jumpUsed)
            {
                _jumpUsed = true;
                return;
            }

            _doubleJumpUsed = true;
        }

        public void ChangeJumpState(bool isOnMud)
        {
            _isOnMud = isOnMud;
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

