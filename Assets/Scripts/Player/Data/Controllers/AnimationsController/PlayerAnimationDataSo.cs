using UnityEngine;
using Game.Core;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "PlayerAnimationDataSo", menuName = "Scriptable Objects/PlayerAnimationDataSo")]
    public class PlayerAnimationDataSo : ScriptableObject
    {
        [SerializeField] private PlayerAnimationState _animationStateName;
        public PlayerAnimationState AnimationStateName => _animationStateName;

        [SerializeField] private string _animationName;
        public int AnimationNameHash => Animator.StringToHash(_animationName);                       
    }
}

