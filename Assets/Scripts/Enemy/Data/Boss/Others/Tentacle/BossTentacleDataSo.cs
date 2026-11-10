using UnityEngine;
using Game.Animations;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "BossTentacleDataSo", menuName = "Scriptable Objects/BossTentacleDataSo")]
    public class BossTentacleDataSo : ScriptableObject
    {
        [SerializeField] private AnimationControllerDataSo _animationsData;
        public AnimationControllerDataSo AnimationsData => _animationsData;

        [SerializeField] private EnemyAwarenessControllerDataSo _awarenessData;
        public EnemyAwarenessControllerDataSo AwarenessData => _awarenessData;

        [SerializeField] private EnemyBurrowControllerDataSo _burrowData;
        public EnemyBurrowControllerDataSo BurrowData => _burrowData;

        [SerializeField] private EnemyDamageFeedbackControllerDataSo _damageFeedbackData;
        public EnemyDamageFeedbackControllerDataSo DamageFeedbackData => _damageFeedbackData;

        [SerializeField] private BossTentacleCombatControllerDataSo _combatData;
        public BossTentacleCombatControllerDataSo CombatData => _combatData;

        [SerializeField] private AudioConfigSo _audioConfig;
        public AudioConfigSo AudioConfig => _audioConfig;   
    }
}

