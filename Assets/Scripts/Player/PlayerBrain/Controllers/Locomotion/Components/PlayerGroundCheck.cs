using UnityEngine;
using System;
using Game.Data;

public class PlayerGroundCheck 
{
    private readonly Transform _groundCheckTransform;

    private readonly LayerMask _groundLayer;
    private readonly Vector2 _groundCheckSize;
    private readonly float _groundCheckDistance;

    public event Action OnJustLanded;

    private bool _isGroundedPreviousState;
    private bool _isGrounded;
    public bool IsGrounded => _isGrounded;
    
    public PlayerGroundCheck(PlayerGroundCheckDataSo data, Transform groundCheckTransform)
    {
        _groundCheckTransform = groundCheckTransform;

        _groundLayer = data.GroundLayer;
        _groundCheckSize = data.GroundCheckSize;
        _groundCheckDistance = data.GroundCheckDistance;

        _isGroundedPreviousState = _isGrounded;
    }

    public void UpdateGroundedState()
    {
        RaycastHit2D hit = Physics2D.BoxCast(_groundCheckTransform.position, _groundCheckSize, 0, Vector3.down,
            _groundCheckDistance, _groundLayer);

        _isGrounded = hit;

        if (_isGrounded != _isGroundedPreviousState)
        {
            if(_isGrounded)
                OnJustLanded?.Invoke();

            _isGroundedPreviousState = _isGrounded;
        }        
    }
}
