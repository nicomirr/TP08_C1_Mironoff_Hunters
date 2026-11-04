using UnityEngine;
using Game.Data;
using Game.Core;
using Game.Marker;

namespace Game.Enemy
{
    public class PlantBrain : EnemyBrain
    {
        private PlantBrainDataSo _plantBrainData;

        private PlantCombatController _combatController;

        protected override void Awake()
        {
            base.Awake();

            _plantBrainData = _brainData as PlantBrainDataSo;

            _combatController = new PlantCombatController(_plantBrainData.CombatData, GetComponentInChildren<ShootPointMarker>().transform);
        }

        private void Update()
        {
            if (!_healthController.IsAlive)
            {
                _animationsController.ChangeAnimationState(AnimationStateType.Dead);
                return;
            }

            _awarenessController.Update();

            HandleAnimations();
            HandleAttack();
        }

        private void HandleAttack()
        {
            _combatController.UpdateCooldownTimer();

            if (!_combatController.CanAttack) return;

            if (_awarenessController.TargetInAttackRange)
                return;

            if (_awarenessController.TargetInRange)            
                StartCoroutine(_combatController.ShootRoutine(_awarenessController.CurrentTarget.position));                    
        }

        private void HandleAnimations()
        {

            if (_awarenessController.TargetInRange)
            {

                if (_combatController.IsShooting)                
                    _animationsController.ChangeAnimationState(AnimationStateType.Shooting);                
                else
                    _animationsController.ChangeAnimationState(AnimationStateType.Idle);

                return;
            }
        }

        //private void HandleAnimations()
        //{
        //    if (_awarenessController.TargetInAttackRange)
        //    {
        //        if (_combatController.IsAttacking)
        //            _animationsController.ChangeAnimationState(AnimationStateType.Attacking);
        //        else
        //            _animationsController.ChangeAnimationState(AnimationStateType.Idle);

        //        return;
        //    }

        //    if (_awarenessController.TargetInRange)
        //        _animationsController.ChangeAnimationState(AnimationStateType.Moving);
        //    else
        //        _animationsController.ChangeAnimationState(AnimationStateType.Idle);
        //}

        protected override void ResetEnemy(int _)
        {
            base.ResetEnemy(_);
        }
    }

}

