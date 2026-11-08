using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "BossTentacleDataSo", menuName = "Scriptable Objects/BossTentacleDataSo")]
    public class BossTentacleDataSo : ScriptableObject
    {
        [SerializeField] private EnemyAwarenessControllerDataSo _awarenessData;
        public EnemyAwarenessControllerDataSo AwarenessData => _awarenessData;
    }
}

