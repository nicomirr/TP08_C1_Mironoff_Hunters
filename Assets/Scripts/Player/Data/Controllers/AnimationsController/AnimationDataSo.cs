using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "AnimationDataSo", menuName = "Scriptable Objects/AnimationDataSo")]
    public class AnimationDataSo : ScriptableObject
    {
        [SerializeField] private Core.AnimationStateType _animationStateName;
        public Core.AnimationStateType AnimationStateName => _animationStateName;

        [SerializeField] private string _animationName;
        public int AnimationNameHash => Animator.StringToHash(_animationName);                       
    }
}

