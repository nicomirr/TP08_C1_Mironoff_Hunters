using UnityEngine;
using Game.Data;
using Game.ObjectPooling;
using Game.Core;

namespace Game.Enemy
{
    public class RangeAttack
    {
        private readonly RangeAttackDataSo _data;
        private readonly Transform _shootPoint;

        public RangeAttack(RangeAttackDataSo data, Transform shootPoint)
        {
            _data = data;
            _shootPoint = shootPoint;
        }

        public void Shoot(Transform target)
        {
            GameObject bulletObject = ObjectPool.Instance.GetPoolObject(_data.BulletType);
            Bullet bullet = bulletObject.GetComponent<Bullet>();

            Vector2 targetPosition = target.position;

            if (target.GetComponent<IMovementProviderHolder>().MovementProvider != null)
            {
                Debug.Log("Entra");
                targetPosition = AnticipateMovement(target, bullet.Speed);        
            }

            bulletObject.transform.position = _shootPoint.position;
            bulletObject.SetActive(true);

            bullet.Move(targetPosition);
        }

        private Vector2 AnticipateMovement(Transform target, float bulletSpeed)
        {
            Vector2 targetPosition = target.position;

            Vector2 shootPosition = _shootPoint.position;

            Rigidbody2D targetRb = target.GetComponent<Rigidbody2D>();

            float travelTime = Vector2.Distance(shootPosition, targetPosition) / bulletSpeed;

            targetPosition.x += targetRb.linearVelocityX * travelTime;

            return targetPosition;
        }
    }
}
