using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "EnemyAwarenessControllerDataSo", menuName = "Scriptable Objects/EnemyAwarenessControllerDataSo")]
    public class EnemyAwarenessControllerDataSo : ScriptableObject
    {
        [SerializeField] private TargetDetectorDataSo _targetDetectorData;
        public TargetDetectorDataSo TargetDetectorData => _targetDetectorData;
    }

}

