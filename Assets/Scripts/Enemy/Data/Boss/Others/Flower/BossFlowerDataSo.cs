using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "BossFlowerDataSo", menuName = "Scriptable Objects/BossFlowerDataSo")]
    public class BossFlowerDataSo : ScriptableObject
    {        
        [SerializeField] private EnemyAwarenessControllerDataSo _awarenessData;
        public EnemyAwarenessControllerDataSo AwarenessData => _awarenessData;

        [SerializeField] private EnemyBurrowControllerDataSo _burrowData;
        public EnemyBurrowControllerDataSo BurrowData => _burrowData;

        [SerializeField] private EnemyDamageFeedbackControllerDataSo _damageFeedbackData;
        public EnemyDamageFeedbackControllerDataSo DamageFeedbackData => _damageFeedbackData;

        [SerializeField] private BossCombatControllerDataSo _combatData;
        public BossCombatControllerDataSo CombatData => _combatData;

        [SerializeField] private AudioConfigSo _audioConfig;
        public AudioConfigSo AudioConfig => _audioConfig;

    }

}

