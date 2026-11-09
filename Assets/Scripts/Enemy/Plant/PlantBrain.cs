using UnityEngine;
using Game.Data;
using Game.Core;
using Game.Marker;

namespace Game.Enemy
{
    public class PlantBrain : EnemyBrain
    {
        private PlantBrainDataSo _plantBrainData;

        private PlantLocomotionController _locomotionController;
        private PlantCombatController _combatController;

        protected override void Awake()
        {
            base.Awake();

            _plantBrainData = _brainData as PlantBrainDataSo;

            _locomotionController = new PlantLocomotionController(this.transform);
            _combatController = new PlantCombatController(_plantBrainData.CombatData, GetComponentInChildren<ShootPointMarker>().transform,
                GetComponentInChildren<EnemyAttackAreaMarker>().GetComponent<Collider2D>());
        }

        private void Update()
        {
            if (!_healthController.IsAlive)
            {
                _combatController.DisableAttackCollider();
                _animationsController.ChangeAnimationState(AnimationStateType.Dead);
                _pickablesSpawner.SpawnPickables();

                return;
            }

            _awarenessController.Update();

            if (!_combatController.IsAttacking && !_combatController.IsShooting)
                _locomotionController.FaceTarget(_awarenessController.CurrentTarget);

            HandleAnimations();
            HandleAttack();
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

            if (_awarenessController.TargetInRange)            
                StartCoroutine(_combatController.ShootRoutine(_awarenessController.CurrentTarget));            
        }

        private void HandleAnimations()
        {
            if (_combatController.IsAttacking)
            {
                _animationsController.ChangeAnimationState(AnimationStateType.Attacking);
                return;
            }

            if (_combatController.IsShooting)
            {
                _animationsController.ChangeAnimationState(AnimationStateType.Shooting);
                return;
            }

            _animationsController.ChangeAnimationState(AnimationStateType.Idle);
        }

        protected override void ResetEnemy(int _, bool __, PlayerType ___)
        {
            StopAllCoroutines();

            base.ResetEnemy(_, __, ___);

            _combatController.Reset();
        }
    }
}
