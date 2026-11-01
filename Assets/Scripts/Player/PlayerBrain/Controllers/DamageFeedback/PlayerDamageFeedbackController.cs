using UnityEngine;
using Game.Data;
using Game.VisualEffects;
using System.Collections;

namespace Game.Player.Brain
{
    public class PlayerDamageFeedbackController 
    {
        private readonly PlayerKnockback _knockback;
        private readonly SpriteFlicker _spriteFlicker;

        private bool _isKnockedback;
        public bool IsKnockedback => _isKnockedback;

        public IEnumerator FlickRoutine => _spriteFlicker.FlickerRoutine();

        public PlayerDamageFeedbackController(PlayerDamageFeedbackControllerDataSo data, Rigidbody2D rb, SpriteRenderer spriteRenderer)
        {
            _knockback = new PlayerKnockback(data.KnockbackData, rb);
            _spriteFlicker = new SpriteFlicker(data.SpriteFlickerData, spriteRenderer);
        }

        public IEnumerator ApplyKnockbackRoutine(float direction)
        {
            _isKnockedback = true;
            _knockback.ApplyKnockback(direction);

            yield return new WaitForSeconds(_knockback.KnockbackTime);

            _isKnockedback = false;

        }
    }

}

