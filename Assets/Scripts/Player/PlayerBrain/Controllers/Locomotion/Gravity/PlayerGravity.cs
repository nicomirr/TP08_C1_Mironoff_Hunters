using UnityEngine;
using Game.Data;

namespace Game.Player
{
    public class PlayerGravity
    {
        private readonly PlayerGravityDataSo _data;

        private readonly Rigidbody2D _rb;

        public PlayerGravity(PlayerGravityDataSo data, Rigidbody2D rb)
        {
            _data = data;
            _rb = rb;            
        }

        public void UpdateGravity()
        {
            if (_rb.linearVelocityY > _data.ApexVelocityTreshold)
                _rb.gravityScale = _data.RisingGravity;

            else if (_rb.linearVelocityY >= -_data.ApexVelocityTreshold)
                _rb.gravityScale = _data.ApexGravity;

            else
                _rb.gravityScale = _data.FallingGravity;            
        }

        public void SetFallingGravity()
        {
            _rb.gravityScale = _data.FallingGravity;
        }
    }
}
