using UnityEngine;
using Game.Core;
using Game.Data;

namespace Game.Throwables
{
    public class ThrowableExplosion : MonoBehaviour, IDamageProvider
    {
        [SerializeField] private ThrowableExplosionDataSo _data;

        public float Damage => _data.Damage;
        public DamageType DamageType => _data.DamageType;
    }
}

