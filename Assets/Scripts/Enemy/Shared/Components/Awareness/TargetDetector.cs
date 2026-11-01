using UnityEngine;
using Game.Data;

namespace Game.Enemy
{
    public class TargetDetector
    {
        private readonly TargetDetectorDataSo _data;

        private readonly Transform _ownerTransform;

        private Transform _currentTarget;
        public Transform CurrentTarget => _currentTarget;

        public TargetDetector(TargetDetectorDataSo data, Transform ownerTransform)
        {
            _data = data;
            _ownerTransform = ownerTransform;
        }

        public bool CheckIfTargetInRange()
        {
            Collider2D[] detected = Physics2D.OverlapCircleAll(_ownerTransform.position, _data.DetectionRadius, 
                _data.TargetLayer);

            if (detected.Length == 0) return false;

            Transform closestTarget = null;
            float closestDistance = float.MaxValue;

            foreach (Collider2D target in detected)
            {
                float distance = (target.transform.position - _ownerTransform.position).sqrMagnitude;

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target.transform;
                }
            }

            _currentTarget = closestTarget;

            return true;
        }        

        public bool TargetInAttackRange()
        {
            Collider2D hit = Physics2D.OverlapBox(_ownerTransform.position, _data.AttackDetectionAreaSize,
                0f, _data.TargetLayer);

            return hit != null;
        }

    }

}

