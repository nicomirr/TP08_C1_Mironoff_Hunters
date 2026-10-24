using UnityEngine;
using Game.Data;

namespace Game.Player
{
    public class PlayerAnimationsController
    {
        private readonly PlayerAnimationsControllerDataSo _data;

        private readonly Animator _animator;

        public PlayerAnimationsController(PlayerAnimationsControllerDataSo data, Animator animator)
        {
            _data = data;
            _animator = animator;
        }

        public void SetMoveAnimation(bool value)
        {
            _animator.SetBool(_data.IsMovingBoolHash, value);
        }

        public void SetJumpAnimation(bool value)
        {
            _animator.SetBool(_data.IsJumpingBoolHash, value);
        }        

        public void TriggerAttack()
        {
            _animator.SetTrigger(_data.AttackTriggerHash);
        }
    }
}
