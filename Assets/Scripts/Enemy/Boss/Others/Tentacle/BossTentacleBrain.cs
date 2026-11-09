using UnityEngine;
using Game.Animations;
using Game.Checkpoints;
using Game.Core;
using Game.Data;
using Game.Marker;

namespace Game.Enemy
{
    public class BossTentacleBrain : MonoBehaviour
    {
        [SerializeField] private BossTentacleDataSo _brainData;
        [SerializeField] private BurrowLimitsDataSo _burrowLimitsData;

        private AnimationsController _animationsController;

        private EnemyAwarenessController _awarenessController;
        private EnemyBurrowController _burrowController;
        private EnemyDamageFeedbackController _damageFeedbackController;

        private BossTentacleCombatController _combatController;

        private Vector2 _initialPos;

        private void Awake()
        {
            _animationsController = new AnimationsController(_brainData.AnimationsData, GetComponentInChildren<Animator>(), Core.AnimationStateType.Idle);

            _awarenessController = new EnemyAwarenessController(_brainData.AwarenessData, this.transform);
            _burrowController = new EnemyBurrowController(_brainData.BurrowData, _burrowLimitsData, GetComponent<Rigidbody2D>(), GetComponentInChildren<BodyColliderMarker>().GetComponent<Collider2D>(),
                this.transform);
            _damageFeedbackController = new EnemyDamageFeedbackController(_brainData.DamageFeedbackData, GetComponentInChildren<EnemyImageMarker>().GetComponent<SpriteRenderer>());

            _combatController = new BossTentacleCombatController(_brainData.CombatData, GetComponentInChildren<EnemyAttackAreaMarker>().GetComponent<Collider2D>());

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


            FaceTarget(_awarenessController.CurrentTarget);

            HandleAnimations();
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

        private void FaceTarget(Transform target)
        {
            if (target == null) return;

            this.transform.right = target.position.x < this.transform.position.x ? Vector2.right :
                Vector2.left;
        }

        private void HandleAttack()
        {
            _combatController.UpdateCooldownTimer();

            if (!_combatController.CanAttack)
                return;

            if (_awarenessController.TargetInAttackRange)
            {
                StartCoroutine(_combatController.AttackRoutine());
                return;
            }
        }

        private void HandleAnimations()
        {
            if (_combatController.IsAttacking)
            {
                _animationsController.ChangeAnimationState(AnimationStateType.Attacking);
                return;
            }            

            _animationsController.ChangeAnimationState(AnimationStateType.Idle);
        }

        public void HandleDamageFeedback()
        {
            if (!_damageFeedbackController.IsFlickering)
                StartCoroutine(_damageFeedbackController.FlickRoutine);
        }

        private void ResetEnemy(int checkpointNumber)
        {
            StopAllCoroutines();

            _burrowController.Reset();
            _combatController.Reset();

            this.transform.position = _initialPos;
        }
    }

}

