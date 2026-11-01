using UnityEngine;
using System;
using Game.Animations;
using Game.Data;
using Game.Core;
using Game.Marker;

namespace Game.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ZombieBrain : MonoBehaviour, IHealthChangedNotifier
    {
        [SerializeField] private ZombieBrainDataSo _brainData;
        [SerializeField] private BurrowLimitsDataSo _limitsData;

        private AnimationsController _animationsController;

        private ZombieLocomotionController _locomotionController;
        private ZombieAwarenessController _awarenessController;
        private ZombieCombatController _combatController;
        private EnemyHealthController _healthController;
        private EnemyDamageFeedbackController _damageFeedbackController;
        private ZombieBurrowController _burrowController;

        public event Action<float, float> OnHealthChanged;

        private void Awake()
        {
            Rigidbody2D _rb = GetComponent<Rigidbody2D>();

            _animationsController = new AnimationsController(_brainData.AnimationsData, GetComponentInChildren<Animator>());

            _locomotionController = new ZombieLocomotionController(_brainData.LocomotionData, _rb, this.transform);
            _awarenessController = new ZombieAwarenessController(_brainData.AwarenessData, this.transform);
            _combatController = new ZombieCombatController(_brainData.CombatData, GetComponentInChildren<EnemyAttackAreaMarker>().GetComponent<Collider2D>());
            _healthController = new EnemyHealthController(_brainData.EnemyHealthData);
            _damageFeedbackController = new EnemyDamageFeedbackController(_brainData.DamageFeedbackData, GetComponentInChildren<EnemyImageMarker>().GetComponent<SpriteRenderer>());
            _burrowController = new ZombieBurrowController(_brainData.BurrowData, _limitsData, _rb, GetComponentInChildren<BodyColliderMarker>().GetComponent<Collider2D>(), this.transform);
        }

        private void OnEnable()
        {
            _healthController.OnHealthChanged += NotifyHealthChanged;
        }

        private void FixedUpdate()
        {
            if (_burrowController.IsBurrowed || _burrowController.IsBurrowing) return;

            HandleMovement();
        }

        private void Update()
        {
            _awarenessController.Update();

            HandleUnburrow();

            if (_burrowController.IsBurrowed || _burrowController.IsBurrowing) return;

            HandleBurrow();

            if (_burrowController.IsBurrowing) return;

            _locomotionController.FaceTarget(_awarenessController.CurrentTarget);

            HandleAttack();

            HandleAnimations();
        }

        private void OnDisable()
        {
            _healthController.OnHealthChanged -= NotifyHealthChanged;
        }       

        private void HandleUnburrow()
        {
            _burrowController.UpdateUnburrowTimer();

            if (!_awarenessController.TargetInRange) return;

            _burrowController.FollowTarget(_awarenessController.CurrentTarget);

            if (!_burrowController.IsBurrowed || _burrowController.IsUnburrowing) return;

            if (!_burrowController.CanUnburrow) return;

            StartCoroutine(_burrowController.UnburrowRoutine());
        }

        private void HandleBurrow()
        {
            _burrowController.UpdateBurrowTimer();

            if (!_burrowController.CanBurrow) return;

            StartCoroutine(_burrowController.BurrowRoutine());
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

            if (!_damageFeedbackController.IsFlickering)
                StartCoroutine(_damageFeedbackController.FlickRoutine);
        }

        private void NotifyHealthChanged(float currentHealth, float maxHealth)
        {
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }
    }

}

