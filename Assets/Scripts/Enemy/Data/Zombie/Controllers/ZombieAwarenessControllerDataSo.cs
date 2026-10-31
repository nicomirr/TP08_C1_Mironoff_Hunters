using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "ZombieAwarenessControllerDataSo", menuName = "Scriptable Objects/ZombieAwarenessControllerDataSo")]
    public class ZombieAwarenessControllerDataSo : ScriptableObject
    {
        [SerializeField] private TargetDetectorDataSo _targetDetectorData;
        public TargetDetectorDataSo TargetDetectorData => _targetDetectorData;
    }

}

