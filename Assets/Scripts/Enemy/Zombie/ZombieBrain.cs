using UnityEngine;
using Game.Animations;
using Game.Data;
using Game.Core;
using System;

namespace Game.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ZombieBrain : MonoBehaviour, IHealthChangedNotifier
    {
        [SerializeField] private ZombieBrainDataSo _data;

        private AnimationsController _animationsController;

        private ZombieLocomotionController _locomotionController;
        private ZombieAwarenessController _awarenessController;
        private ZombieCombatController _combatController;
        private EnemyHealthController _healthController;

        public event Action<float, float> OnHealthChanged;

        private void Awake()
        {
            _animationsController = new AnimationsController(_data.AnimationsData, GetComponentInChildren<Animator>());

            _locomotionController = new ZombieLocomotionController(_data.LocomotionData, GetComponent<Rigidbody2D>(), this.transform);
            _awarenessController = new ZombieAwarenessController(_data.AwarenessData, this.transform);
            _combatController = new ZombieCombatController(_data.CombatData, GetComponentInChildren<EnemyAttackAreaMarker>().GetComponent<Collider2D>());
            _healthController = new EnemyHealthController(_data.EnemyHealthData);
        }

        private void OnEnable()
        {
            _healthController.OnHealthChanged += NotifyHealthChanged;
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void Update()
        {
            _awarenessController.Update();

            HandleAttack();

            HandleAnimations();
        }

        private void OnDisable()
        {
            _healthController.OnHealthChanged -= NotifyHealthChanged;
        }

        private void HandleMovement()
        {
            if (_awarenessController.TargetInAttackRange) return;

            if (_awarenessController.TargetInRange)
                _locomotionController.UpdateMovement();
        }

        private void HandleAttack()
        {
            if (!_awarenessController.TargetInAttackRange) return;

            if (!_combatController.CanAttack) return;

            StartCoroutine(_combatController.AttackRoutine());
        }

        private void HandleAnimations()
        {
            if (_awarenessController.TargetInAttackRange) 
            {
                if (_combatController.IsAttacking)
                    _animationsController.ChangeAnimationState(AnimationStateType.Attacking);
                else
                    _animationsController.ChangeAnimationState(AnimationStateType.Idle);
                
                return;
            }

            if (_awarenessController.TargetInRange)
                _animationsController.ChangeAnimationState(AnimationStateType.Moving);
            else
                _animationsController.ChangeAnimationState(AnimationStateType.Idle);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out IDamageProvider damageProvider)) return;

            _healthController.TakeDamage(damageProvider.Damage);
        }

        private void NotifyHealthChanged(float currentHealth, float maxHealth)
        {
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }

    }

}

