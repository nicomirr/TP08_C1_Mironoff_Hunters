using UnityEngine;
using Game.Data;

namespace Game.Enemy
{
    public class BossTentacle : MonoBehaviour
    {
        [SerializeField] private BossTentacleDataSo _data;

        private EnemyAwarenessController _awarenessController;

        private void Awake()
        {
            _awarenessController = new EnemyAwarenessController(_data.AwarenessData, this.transform);
        }

        private void Update()
        {
            _awarenessController.Update();

            if (_awarenessController.TargetInRange)
                FaceTarget(_awarenessController.CurrentTarget);
        }

        private void FaceTarget(Transform target)
        {
            if (target == null) return;

            this.transform.right = target.position.x < this.transform.position.x ? Vector2.right :
                Vector2.left;
        }
    }

}

