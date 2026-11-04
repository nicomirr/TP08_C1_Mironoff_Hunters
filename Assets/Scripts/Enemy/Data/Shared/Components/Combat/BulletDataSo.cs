using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "BulletDataSo", menuName = "Scriptable Objects/BulletDataSo")]
    public class BulletDataSo : ScriptableObject
    {
        [SerializeField] private float _speed;
        public float Speed => _speed;

        [SerializeField] private float _lifeTime;
        public float LifeTime => _lifeTime;
    }

}

