using UnityEngine;
using System.Collections;
using Game.Data;
using Game.Throwables;
using Game.ObjectPooling;

namespace Game.Player
{
    public class ThrowableEffect : ItemEffect
    {
        private readonly ThrowableEffectDataSo _data;

        private readonly Rigidbody2D _userRb;
        private readonly Transform _userTransform;

        public ThrowableEffect(ThrowableEffectDataSo data, Rigidbody2D userRb, Transform userTransform)
        {
            _data = data;

            _userRb = userRb;
            _userTransform = userTransform;
        }

        public override IEnumerator UseItemRoutine()
        {
            Throwable hollyWater = ObjectPool.Instance.GetPoolObject(_data.ThrowableType).GetComponent<Throwable>();
            hollyWater.transform.position = new Vector2(_userTransform.position.x, _userTransform.position.y + _data.YOffset);
            hollyWater.gameObject.SetActive(true);

            hollyWater.Throw(_userTransform.transform.right.x, _userRb.linearVelocity);
            yield return null;
        }
    }
}


