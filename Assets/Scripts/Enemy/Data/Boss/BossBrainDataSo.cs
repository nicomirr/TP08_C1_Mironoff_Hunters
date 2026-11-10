using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "BossBrainDataSo", menuName = "Scriptable Objects/BossBrainDataSo")]
    public class BossBrainDataSo : ScriptableObject
    {
        [SerializeField] private AnimationControllerDataSo _animationsData;
        public AnimationControllerDataSo AnimationsData => _animationsData;

        [SerializeField] private EnemyAwarenessControllerDataSo _awarenessData;
        public EnemyAwarenessControllerDataSo AwarenessData => _awarenessData;

        [SerializeField] private EnemyHealthDataSo _enemyHealthData;
        public EnemyHealthDataSo EnemyHealthData => _enemyHealthData;

        [SerializeField] private EnemyDamageFeedbackControllerDataSo _damageFeedbackData;
        public EnemyDamageFeedbackControllerDataSo DamageFeedbackData => _damageFeedbackData;

        [SerializeField] private BossCombatControllerDataSo _combatData;
        public BossCombatControllerDataSo CombatData => _combatData;

        [SerializeField] private AudioConfigSo _audioConfig;
        public AudioConfigSo AudioConfig => _audioConfig;
    }

}

