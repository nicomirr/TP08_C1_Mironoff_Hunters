using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "EnemyBrainDataSo", menuName = "Scriptable Objects/EnemyBrainDataSo")]
    public class EnemyBrainDataSo : ScriptableObject
    {
        [SerializeField] private AnimationControllerDataSo _animationsData;
        public AnimationControllerDataSo AnimationsData => _animationsData;

        [SerializeField] private EnemyAwarenessControllerDataSo _awarenessData;
        public EnemyAwarenessControllerDataSo AwarenessData => _awarenessData;

        [SerializeField] private EnemyHealthDataSo _enemyHealthData;
        public EnemyHealthDataSo EnemyHealthData => _enemyHealthData;

        [SerializeField] private EnemyDamageFeedbackControllerDataSo _damageFeedbackData;
        public EnemyDamageFeedbackControllerDataSo DamageFeedbackData => _damageFeedbackData;

        [SerializeField] private PickableSpawnerDataSo _spawnerData;
        public PickableSpawnerDataSo SpawnerData => _spawnerData;
    }

}
