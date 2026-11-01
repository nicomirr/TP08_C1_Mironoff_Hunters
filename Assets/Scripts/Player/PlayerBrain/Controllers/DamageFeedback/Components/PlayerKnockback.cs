using UnityEngine;
using Game.Data;

namespace Game.Player
{
    public class PlayerKnockback
    {
        private readonly PlayerKnockbackDataSo _data;
        private readonly Rigidbody2D _rb;

        private readonly float _knockbackTime;
        public float KnockbackTime => _knockbackTime;

        public PlayerKnockback(PlayerKnockbackDataSo data, Rigidbody2D rb)
        {
            _data = data;
            _rb = rb;

            _knockbackTime = data.KnockbackTime;
        }

        public void ApplyKnockback(float direction)
        {
            _rb.linearVelocity = Vector2.zero;

            Vector2 force = new Vector2(Mathf.Sign(direction) * _data.HorizontalKnockbackForce, _data.VerticalKnockbackForce);
            _rb.AddForce(force, ForceMode2D.Impulse);
        }
    }

}
