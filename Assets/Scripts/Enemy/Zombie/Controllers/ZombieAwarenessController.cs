using UnityEngine;
using Game.Data;

namespace Game.Enemy
{
    public class ZombieAwarenessController
    {
        private readonly TargetDetector _targetDetector;

        private bool _targetInRange;
        public bool TargetInRange => _targetInRange;

        private bool _targetInAttackRange;
        public bool TargetInAttackRange => _targetInAttackRange;

        public Transform CurrentTarget => _targetDetector.CurrentTarget;

        public ZombieAwarenessController(ZombieAwarenessControllerDataSo data, Transform ownerTransform)
        {
            _targetDetector = new TargetDetector(data.TargetDetectorData, ownerTransform);
        }

        public void Update()
        {
            HasDetectedTarget();
            HasTargetInAttackRange();
        }

        public void HasDetectedTarget()
        {
            _targetInRange =  _targetDetector.CheckIfTargetInRange();
        }

        public void HasTargetInAttackRange()
        {
            _targetInAttackRange =  _targetDetector.TargetInAttackRange();
        }
    }
}
