using UnityEngine;
using System.Collections.Generic;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "AnimationControllerDataSo", menuName = "Scriptable Objects/AnimationControllerDataSo")]
    public class AnimationControllerDataSo : ScriptableObject
    {
        [SerializeField] private List<AnimationDataSo> _animations;
        public List<AnimationDataSo> Animations => _animations;
    }

}

