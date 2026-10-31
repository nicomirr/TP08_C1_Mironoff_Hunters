using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "ForwardMovementDataSo", menuName = "Scriptable Objects/ForwardMovementDataSo")]
    public class ForwardMovementDataSo : ScriptableObject
    {
        [SerializeField] private float _movementSpeed;
        public float MovementSpeed => _movementSpeed;
    }
}

