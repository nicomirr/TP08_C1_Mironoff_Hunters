using UnityEngine;
using System.Collections.Generic;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "PlayerAnimationControllerDataSo", menuName = "Scriptable Objects/PlayerAnimationControllerDataSo")]
    public class PlayerAnimationControllerDataSo : ScriptableObject
    {
        [SerializeField] private List<PlayerAnimationDataSo> _animations;
        public List<PlayerAnimationDataSo> Animations => _animations;
    }

}

