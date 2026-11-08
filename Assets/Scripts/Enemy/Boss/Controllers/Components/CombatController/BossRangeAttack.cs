using UnityEngine;
using Game.Data;
using Game.ObjectPooling;

namespace Game.Enemy
{
    public class BossRangeAttack
    {
        private readonly RangeAttackDataSo _data;
        private readonly Transform _shootPoint;

        public BossRangeAttack(RangeAttackDataSo data, Transform shootPoint)
        {
            _data = data;
            _shootPoint = shootPoint;
        }

        public void Shoot(Transform target)
        {
            GameObject bulletObject = ObjectPool.Instance.GetPoolObject(_data.BulletType);
            Bullet bullet = bulletObject.GetComponent<Bullet>();

            bulletObject.transform.position = _shootPoint.position;
            bulletObject.SetActive(true);

            bullet.Move(target.position);
        }        
    }

}
