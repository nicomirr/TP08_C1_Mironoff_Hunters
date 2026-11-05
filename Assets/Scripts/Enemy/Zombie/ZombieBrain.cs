using UnityEngine;
using System;
using Game.Animations;
using Game.Data;
using Game.Core;
using Game.Marker;
using Game.Checkpoints;

namespace Game.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ZombieBrain : EnemyBrain
    {
        [SerializeField] private BurrowLimitsDataSo _limitsData;
        
        private ZombieBrainDataSo _zombieBrainData;
                
        private ZombieLocomotionController _locomotionController;
        private ZombieCombatController _combatController;
        private ZombieBurrowController _burrowController;

        protected override void Awake()
        {      
            base.Awake();

            _zombieBrainData = _brainData as ZombieBrainDataSo;

            Rigidbody2D _rb = GetComponent<Rigidbody2D>();
            
            _locomotionController = new ZombieLocomotionController(_zombieBrainData.LocomotionData, _rb, this.transform);
            _combatController = new ZombieCombatController(_zombieBrainData.CombatData, GetComponentInChildren<EnemyAttackAreaMarker>().GetComponent<Collider2D>());
            _burrowController = new ZombieBurrowController(_zombieBrainData.BurrowData, _limitsData, _rb, GetComponentInChildren<BodyColliderMarker>().GetComponent<Collider2D>(), this.transform);
        }

        private void FixedUpdate()
        {
            if (!_healthController.IsAlive) return;

            if (_burrowController.IsBurrowed || _burrowController.IsBurrowing) return;

            HandleMovement();
        }

        private void Update()
        {
            if (!_healthController.IsAlive)
            {
                _combatController.DisableAttackCollider();
                _animationsController.ChangeAnimationState(AnimationStateType.Dead);
                return;
            }

            _awarenessController.Update();

            HandleUnburrow();

            if (_burrowController.IsBurrowed || _burrowController.IsBurrowing) return;

            HandleBurrow();

            if (_burrowController.IsBurrowing) return;

            if (!_combatController.IsAttacking)
                _locomotionController.FaceTarget(_awarenessController.CurrentTarget);

            HandleAttack();

            HandleAnimations();
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

        protected override void ResetEnemy(int _)
        {
            _burrowController.Reset();

            base.ResetEnemy(_);

            _combatController.Reset();
        }
        
    }

}

