using UnityEngine;
using System.Collections.Generic;
using Game.Data;
using Game.Core;


namespace Game.Player
{
    public class PlayerAnimationsController
    {
        private readonly Dictionary<PlayerAnimationState, int> _animations = new();

        private readonly Animator _animator;

        private PlayerAnimationState _currentState;


        public PlayerAnimationsController(PlayerAnimationControllerDataSo data, Animator animator)
        {            
            _animator = animator;

            foreach (PlayerAnimationDataSo animation in data.Animations)
                _animations.Add(animation.AnimationStateName, animation.AnimationNameHash);

            _currentState = PlayerAnimationState.Idle;
        }

        public void ChangeAnimationState(PlayerAnimationState animationState)
        {            
            if (animationState == _currentState) return;

            _currentState = animationState;

            _animator.Play(_animations[animationState]);
        }       
    }
}
