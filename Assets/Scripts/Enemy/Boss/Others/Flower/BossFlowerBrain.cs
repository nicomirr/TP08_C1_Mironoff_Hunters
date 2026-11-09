using UnityEngine;
using System;
using Game.Checkpoints;
using Game.Core;
using Game.Data;
using Game.Marker;

namespace Game.Enemy
{
    public class BossFlowerBrain : MonoBehaviour
    {
        [SerializeField] private BossFlowerDataSo _brainData;
        [SerializeField] private BurrowLimitsDataSo _burrowLimitsData;

        private EnemyAwarenessController _awarenessController;
        private EnemyBurrowController _burrowController;
        private EnemyDamageFeedbackController _damageFeedbackController;

        private BossCombatController _combatController;

        private Vector2 _initialPos;

        public event Action<float, DamageType> OnDamageTaken;

        private void Awake()
        {
            _awarenessController = new EnemyAwarenessController(_brainData.AwarenessData, this.transform);
            _burrowController = new EnemyBurrowController(_brainData.BurrowData, _burrowLimitsData, GetComponent<Rigidbody2D>(), GetComponentInChildren<BodyColliderMarker>().GetComponent<Collider2D>(),
                this.transform);
            _damageFeedbackController = new EnemyDamageFeedbackController(_brainData.DamageFeedbackData, GetComponentInChildren<EnemyImageMarker>().GetComponent<SpriteRenderer>());

            _combatController = new BossCombatController(_brainData.CombatData, GetComponentInChildren<ShootPointMarker>().transform);
        }

        private void OnEnable()
        {
            Checkpoint.OnCheckpointUsed += ResetEnemy;
        }
        private void Start()
        {
            _initialPos = this.transform.position;
        }

        private void OnDisable()
        {
            Checkpoint.OnCheckpointUsed -= ResetEnemy;
        }

        private void Update()
        {
            _awarenessController.Update();

            if (!_awarenessController.TargetInRange) return;

            HandleUnburrow();

            if (_burrowController.IsBurrowed || _burrowController.IsBurrowing) return;

            HandleBurrow();

            if (_burrowController.IsBurrowing) return;

            HandleAttack();
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
                
        private void HandleAttack()
        {            
            _combatController.UpdateCooldownTimer();

            if (!_combatController.CanAttack)
                return;

            if (_awarenessController.TargetInRange)
                StartCoroutine(_combatController.ShootRoutine(_awarenessController.CurrentTarget));
        }       

        private void ResetEnemy(int _, bool __, PlayerType ___)
        {
            StopAllCoroutines();

            _burrowController.Reset();
            _combatController.Reset();

            this.transform.position = _initialPos;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamageProvider damageProvider))
            {
                OnDamageTaken?.Invoke(damageProvider.Damage, damageProvider.DamageType);

                if (!_damageFeedbackController.IsFlickering)
                    StartCoroutine(_damageFeedbackController.FlickRoutine);
            }
        }
    }

}
