using UnityEngine;
using Game.Core;
using Game.Data;

namespace Game.Throwables
{
    public class ThrowableExplosion : MonoBehaviour, IDamageProvider
    {
        [SerializeField] private ThrowableExplosionDataSo _data;

        private AudioSource _audioSource;
        public float Damage => _data.Damage;
        public DamageType DamageType => _data.DamageType;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _audioSource.Play();
        }

    }
}

