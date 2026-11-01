using UnityEngine;
using System.Collections;
using Game.Data;
using Game.Player;
using Game.VisualEffects;

namespace Game.Enemy
{
    public class EnemyDamageFeedbackController
    {
        private readonly SpriteFlicker _spriteFlicker;
      
        public bool IsFlickering => _spriteFlicker.IsFlickering;

        public IEnumerator FlickRoutine => _spriteFlicker.FlickerRoutine();

        public EnemyDamageFeedbackController(EnemyDamageFeedbackControllerDataSo data, SpriteRenderer spriteRenderer)
        {
            _spriteFlicker = new SpriteFlicker(data.SpriteFlickerData, spriteRenderer);
        }               
    }
}
