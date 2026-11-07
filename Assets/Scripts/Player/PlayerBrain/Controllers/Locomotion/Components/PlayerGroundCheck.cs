using UnityEngine;
using System;
using Game.Data;

namespace Game.Player
{
    public class PlayerGroundCheck
    {
        private readonly Transform _groundCheckTransform;

        private readonly LayerMask _groundLayer;
        private readonly LayerMask _mudLayer;

        private readonly Vector2 _groundCheckSize;
        private readonly float _groundCheckDistance;

        public event Action OnJustLanded;

        private bool _isGroundedPreviousState;
        private bool _isGrounded;

        private bool _isOnMud;
        public bool IsGrounded => _isGrounded;
        public bool IsOnMud => _isOnMud;

        public PlayerGroundCheck(PlayerGroundCheckDataSo data, Transform groundCheckTransform)
        {
            _groundCheckTransform = groundCheckTransform;

            _groundLayer = data.GroundLayer;
            _mudLayer = data.MudLayer;

            _groundCheckSize = data.GroundCheckSize;
            _groundCheckDistance = data.GroundCheckDistance;

            _isGroundedPreviousState = _isGrounded;
        }

        public void UpdateGroundedState()
        {
            RaycastHit2D hit = Physics2D.BoxCast(_groundCheckTransform.position, _groundCheckSize, 0, Vector3.down,
                _groundCheckDistance, _groundLayer | _mudLayer);

            _isGrounded = hit.collider != null && hit.normal.y > 0.1f;
            _isOnMud = _isGrounded && (_mudLayer.value & (1 << hit.collider.gameObject.layer)) != 0;

            if (_isGrounded != _isGroundedPreviousState)
            {
                if (_isGrounded)
                    OnJustLanded?.Invoke();

                _isGroundedPreviousState = _isGrounded;
            }
        }
    }

}

