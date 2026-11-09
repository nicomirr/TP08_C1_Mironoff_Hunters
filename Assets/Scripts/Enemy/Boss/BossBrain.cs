using UnityEngine;
using System;
using Game.Animations;
using Game.Checkpoints;
using Game.Core;
using Game.Data;
using Game.Marker;

namespace Game.Enemy
{
    public class BossBrain : MonoBehaviour, IHealthChangedNotifier
    {
        [SerializeField] private BossBrainDataSo _brainData;

        [SerializeField] private GameObject _tentacle;
        [SerializeField] private GameObject _flower;

        private BossFlowerBrain _flowerBrain;

        private AnimationsController _animationsController;

        private EnemyAwarenessController _awarenessController;
        private EnemyHealthController _healthController;
        private EnemyDamageFeedbackController _damageFeedbackController;

        private BossCombatController _combatController;

        private bool _bossDefeatedSent;

        private Collider2D _damageCollider;

        public event Action<float, float> OnHealthChanged;

        public static event Action OnBossDefeated;

        private void Awake()
        {
            _animationsController = new AnimationsController(_brainData.AnimationsData, GetComponentInChildren<Animator>(), AnimationStateType.Idle);

            _awarenessController = new EnemyAwarenessController(_brainData.AwarenessData, this.transform);
            _healthController = new EnemyHealthController(_brainData.EnemyHealthData);
            _damageFeedbackController = new EnemyDamageFeedbackController(_brainData.DamageFeedbackData, GetComponentInChildren<EnemyImageMarker>().GetComponent<SpriteRenderer>());

            _combatController = new BossCombatController(_brainData.CombatData, GetComponentInChildren<ShootPointMarker>().transform);

            _damageCollider = GetComponent<Collider2D>();

            _flowerBrain = _flower.GetComponent<BossFlowerBrain>();
        }

        private void OnEnable()
        {
            Checkpoint.OnCheckpointUsed += ResetBoss;

            _flowerBrain.OnDamageTaken += HandleDamage;

            _healthController.OnHealthChanged += NotifyHealthChanged;
        }

        private void Update()
        {
            if (!_healthController.IsAlive)
            {
                if(!_bossDefeatedSent)
                {
                    OnBossDefeated?.Invoke();
                    _bossDefeatedSent = true;
                }

                _tentacle.SetActive(false);
                _flower.SetActive(false);

                if(_damageCollider.enabled)
                    _damageCollider.enabled = false;

                _animationsController.ChangeAnimationState(AnimationStateType.Dead);
                return;
            }

            _awarenessController.Update();

            HandleAttack();
        }

        private void OnDisable()
        {
            Checkpoint.OnCheckpointUsed -= ResetBoss;

            _flowerBrain.OnDamageTaken -= HandleDamage;

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

        private void ResetBoss(int _, bool __, PlayerType ___)
        {
            StopAllCoroutines();

            _combatController.Reset();
            _animationsController.Reset();
            _healthController.Reset();            
        }

        private void HandleDamage(float damage, DamageType damageType)
        {
            if (!_healthController.IsAlive) return;

            _healthController.TakeDamage(damage, damageType);

            if (!_damageFeedbackController.IsFlickering)
                StartCoroutine(_damageFeedbackController.FlickRoutine);
        }        
    }
}

