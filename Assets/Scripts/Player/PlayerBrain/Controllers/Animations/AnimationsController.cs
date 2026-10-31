using UnityEngine;
using System.Collections.Generic;
using Game.Data;
using Game.Core;


namespace Game.Animations
{
    public class AnimationsController
    {
        private readonly Dictionary<AnimationStateType, int> _animations = new();

        private readonly Animator _animator;

        private AnimationStateType _currentState;


        public AnimationsController(AnimationControllerDataSo data, Animator animator)
        {            
            _animator = animator;

            foreach (AnimationDataSo animation in data.Animations)
                _animations.Add(animation.AnimationStateName, animation.AnimationNameHash);

            _currentState = AnimationStateType.Idle;
        }

        public void ChangeAnimationState(AnimationStateType animationState)
        {            
            if (animationState == _currentState) return;

            _currentState = animationState;

            _animator.Play(_animations[animationState]);
        }       
    }
}
