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

        private readonly AnimationStateType _initialState;

        private AnimationStateType _currentState;


        public AnimationsController(AnimationControllerDataSo data, Animator animator, AnimationStateType initialState)
        {            
            _animator = animator;

            foreach (AnimationDataSo animation in data.Animations)
                _animations.Add(animation.AnimationStateName, animation.AnimationNameHash);

            _initialState = initialState;

            _currentState = _initialState;
        }

        public void ChangeAnimationState(AnimationStateType animationState)
        {            
            if (animationState == _currentState) return;

            _currentState = animationState;

            _animator.Play(_animations[animationState]);
        }      
        
        public void Reset()
        {
            _currentState = _initialState;
        }
    }
}
