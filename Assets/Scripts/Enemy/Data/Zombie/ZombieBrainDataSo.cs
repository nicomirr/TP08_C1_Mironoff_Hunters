using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "ZombieBrainDataSo", menuName = "Scriptable Objects/ZombieBrainDataSo")]
    public class ZombieBrainDataSo : ScriptableObject
    {
        [SerializeField] private AnimationControllerDataSo _animationsData;
        public AnimationControllerDataSo AnimationsData => _animationsData;

        [SerializeField] private ZombieLocomotionControllerDataSo _locomotionData;
        public ZombieLocomotionControllerDataSo LocomotionData => _locomotionData;

        [SerializeField] private ZombieAwarenessControllerDataSo _awarenessData;
        public ZombieAwarenessControllerDataSo AwarenessData => _awarenessData;

        [SerializeField] private ZombieCombatControllerDataSo _combatData;
        public ZombieCombatControllerDataSo CombatData => _combatData;

        [SerializeField] private EnemyHealthDataSo _enemyHealthData;
        public EnemyHealthDataSo EnemyHealthData => _enemyHealthData;
    }
}
