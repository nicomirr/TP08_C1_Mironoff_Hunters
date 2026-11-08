using Game.Animations;
using Game.Checkpoints;
using Game.Core;
using Game.Data;
using Game.Marker;
using System;
using UnityEngine;

namespace Game.Enemy
{
    public class BossBrain : MonoBehaviour
    {
        [SerializeField] private BossBrainDataSo _brainData;

        private AnimationsController _animationsController;

        private EnemyAwarenessController _awarenessController;
        private EnemyHealthController _healthController;
        private EnemyDamageFeedbackController _damageFeedbackController;

        private BossCombatController _combatController;

        public event Action<float, float> OnHealthChanged;

        private void Awake()
        {
            _animationsController = new AnimationsController(_brainData.AnimationsData, GetComponentInChildren<Animator>(), AnimationStateType.Idle);

            _awarenessController = new EnemyAwarenessController(_brainData.AwarenessData, this.transform);
            _healthController = new EnemyHealthController(_brainData.EnemyHealthData);
            _damageFeedbackController = new EnemyDamageFeedbackController(_brainData.DamageFeedbackData, GetComponentInChildren<EnemyImageMarker>().GetComponent<SpriteRenderer>());

            _combatController = new BossCombatController(_brainData.CombatData, GetComponentInChildren<ShootPointMarker>().transform);
        }

        private void OnEnable()
        {
            Checkpoint.OnCheckpointUsed += ResetBoss;

            _healthController.OnHealthChanged += NotifyHealthChanged;
        }

        private void Update()
        {
            if (!_healthController.IsAlive)
            {
                _animationsController.ChangeAnimationState(AnimationStateType.Dead);
                return;
            }

            _awarenessController.Update();

            HandleAttack();
        }

        private void OnDisable()
        {
            Checkpoint.OnCheckpointUsed -= ResetBoss;

            _healthController.OnHealthChanged -= NotifyHealthChanged;
        }

        private void HandleAttack()
        {
            _combatController.UpdateCooldownTimer();

            if (!_combatController.CanAttack)
                return;

            if (_awarenessController.TargetInRange)            
                StartCoroutine(_combatController.ShootRoutine(_awarenessController.CurrentTarget));            
        }

        private void NotifyHealthChanged(float currentHealth, float maxHealth)
        {
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }

        private void ResetBoss(int _)
        {
            StopAllCoroutines();

            _combatController.Reset();
            _animationsController.Reset();
            _healthController.Reset();            
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

