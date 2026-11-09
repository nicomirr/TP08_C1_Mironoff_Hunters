using UnityEngine;
using System;
using Game.Data;
using Game.Core;
using Game.Marker;
using Game.Animations;
using Game.Checkpoints;

namespace Game.Enemy
{
    public abstract class EnemyBrain : MonoBehaviour, IHealthChangedNotifier
    {
        [SerializeField] protected EnemyBrainDataSo _brainData;

        protected AnimationsController _animationsController;

        protected EnemyAwarenessController _awarenessController;
        protected EnemyHealthController _healthController;
        protected EnemyDamageFeedbackController _damageFeedbackController;

        protected PickableSpawner _pickablesSpawner;

        public event Action<float, float> OnHealthChanged;

        protected Vector2 _initialPos;

        protected virtual void Awake()
        {
            _animationsController = new AnimationsController(_brainData.AnimationsData, GetComponentInChildren<Animator>(), AnimationStateType.Idle);

            _awarenessController = new EnemyAwarenessController(_brainData.AwarenessData, this.transform);
            _healthController = new EnemyHealthController(_brainData.EnemyHealthData);
            _damageFeedbackController = new EnemyDamageFeedbackController(_brainData.DamageFeedbackData, GetComponentInChildren<EnemyImageMarker>().GetComponent<SpriteRenderer>());

            _pickablesSpawner = new PickableSpawner(_brainData.SpawnerData, this.transform);
        }

        private void OnEnable()
        {
            Checkpoint.OnCheckpointUsed += ResetEnemy;

            _healthController.OnHealthChanged += NotifyHealthChanged;
        }

        private void Start()
        {
            _initialPos = this.transform.position;
        }

        private void OnDisable()
        {
            Checkpoint.OnCheckpointUsed -= ResetEnemy;

            _healthController.OnHealthChanged -= NotifyHealthChanged;
        }

        private void NotifyHealthChanged(float currentHealth, float maxHealth)
        {
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }

        protected virtual void ResetEnemy(int _, bool __, PlayerType ___)
        {            
            this.transform.position = _initialPos;

            _animationsController.Reset();
            _healthController.Reset();
            _pickablesSpawner.Reset();
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (!_healthController.IsAlive) return;

            if (collision.TryGetComponent(out IDamageProvider damageProvider))
            {
                _healthController.TakeDamage(damageProvider.Damage, damageProvider.DamageType);

                if (!_damageFeedbackController.IsFlickering)
                    StartCoroutine(_damageFeedbackController.FlickRoutine);
            }
            
        }
    }
}
