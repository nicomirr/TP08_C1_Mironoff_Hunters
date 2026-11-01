using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "EnemyDamageFeedbackControllerDataSo", menuName = "Scriptable Objects/EnemyDamageFeedbackControllerDataSo")]
    public class EnemyDamageFeedbackControllerDataSo : ScriptableObject
    {
        [SerializeField] private SpriteFlickerDataSo _spriteFlickerData;
        public SpriteFlickerDataSo SpriteFlickerData => _spriteFlickerData;
    }

}

