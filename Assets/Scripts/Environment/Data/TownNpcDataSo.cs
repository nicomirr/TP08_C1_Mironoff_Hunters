using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "TownNpcDataSo", menuName = "Scriptable Objects/TownNpcDataSo")]
    public class TownNpcDataSo : ScriptableObject
    {
        [SerializeField] private string _walkingAnimationBool;
        public int WalkingAnimationBoolHash => Animator.StringToHash(_walkingAnimationBool);

        [SerializeField] private float _movementSpeed;
        public float MovementSpeed => _movementSpeed;

        [SerializeField] private int _minIdleTime;
        public int MinIdleTime => _minIdleTime; 

        [SerializeField] private int _maxIdleTime;
        public int MaxIdleTime => _maxIdleTime;
    }
}

