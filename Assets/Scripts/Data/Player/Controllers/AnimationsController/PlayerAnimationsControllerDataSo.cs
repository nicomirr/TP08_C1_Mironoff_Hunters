using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "PlayerAnimationsControllerDataSo", menuName = "Scriptable Objects/PlayerAnimationsControllerDataSo")]
    public class PlayerAnimationsControllerDataSo : ScriptableObject
    {
        [SerializeField] private string _isMovingBool;
        public int IsMovingBoolHash => Animator.StringToHash(_isMovingBool);
    }
}

