using Game.Data;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "BossFlowerDataSo", menuName = "Scriptable Objects/BossFlowerDataSo")]
    public class BossFlowerDataSo : ScriptableObject
    {
        [SerializeField] private AnimationControllerDataSo _animationsData;
        public AnimationControllerDataSo AnimationsData => _animationsData;

        [SerializeField] private EnemyAwarenessControllerDataSo _awarenessData;
        public EnemyAwarenessControllerDataSo AwarenessData => _awarenessData;

        [SerializeField] private EnemyBurrowControllerDataSo _burrowData;
        public EnemyBurrowControllerDataSo BurrowData => _burrowData;

        [SerializeField] private BossCombatControllerDataSo _combatData;
        public BossCombatControllerDataSo CombatData => _combatData;

    }

}

