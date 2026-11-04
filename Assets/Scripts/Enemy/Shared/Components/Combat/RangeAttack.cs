using UnityEngine;
using Game.Data;
using Game.ObjectPooling;

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

        public void Shoot(Vector2 targetPosition)
        {
            GameObject bullet = ObjectPool.Instance.GetPoolObject(_data.BulletType);
            
            bullet.transform.position = _shootPoint.position;
            bullet.SetActive(true);

            bullet.GetComponent<Bullet>().Move(targetPosition);
        }
    }
}
