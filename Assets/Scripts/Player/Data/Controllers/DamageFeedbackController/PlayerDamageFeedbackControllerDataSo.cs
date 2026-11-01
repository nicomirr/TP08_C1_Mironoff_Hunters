using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "PlayerDamageFeedbackControllerDataSo", menuName = "Scriptable Objects/PlayerFeedbackControllerDataSo")]
    public class PlayerDamageFeedbackControllerDataSo : ScriptableObject
    {
        [SerializeField] private PlayerKnockbackDataSo _knockbackData;
        public PlayerKnockbackDataSo KnockbackData => _knockbackData;

        [SerializeField] private SpriteFlickerDataSo _spriteFlickerData;
        public SpriteFlickerDataSo SpriteFlickerData => _spriteFlickerData;
    }
}

