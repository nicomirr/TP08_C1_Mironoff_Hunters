using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "TargetDetectorDataSo", menuName = "Scriptable Objects/TargetDetectorDataSo")]
    public class TargetDetectorDataSo : ScriptableObject
    {
        [SerializeField] private float _detectionRadius;
        public float DetectionRadius => _detectionRadius;

        [SerializeField] private Vector2 _attackDetectionAreaSize;
        public Vector2 AttackDetectionAreaSize => _attackDetectionAreaSize;

        [SerializeField] private LayerMask _targetLayer;
        public LayerMask TargetLayer => _targetLayer;
    }

}


