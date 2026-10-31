using UnityEngine;
using Game.Data;

namespace Game.Enemy
{
    public class TargetDetector
    {
        private readonly TargetDetectorDataSo _data;

        private readonly Transform _ownerTransform;

        public TargetDetector(TargetDetectorDataSo data, Transform ownerTransform)
        {
            _data = data;
            _ownerTransform = ownerTransform;
        }
              
        public bool CheckIfTargetInRange()
        {
            Collider2D detected = Physics2D.OverlapCircle(_ownerTransform.position, _data.DetectionRadius,
                _data.TargetLayer);

            if (detected == null) return false;

            FaceTarget(detected.transform);
            
            return true;
        }

        private void FaceTarget(Transform target)
        {
            _ownerTransform.right = target.position.x < _ownerTransform.position.x ? Vector2.left :
                Vector2.right;
        }

        public bool TargetInAttackRange()
        {
            Collider2D hit = Physics2D.OverlapBox(_ownerTransform.position, _data.AttackDetectionAreaSize,
                0f, _data.TargetLayer);

            return hit != null;
        }

    }

}

