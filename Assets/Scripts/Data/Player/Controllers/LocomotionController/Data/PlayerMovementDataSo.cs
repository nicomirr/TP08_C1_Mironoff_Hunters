using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "PlayerMovementDataSo", menuName = "Scriptable Objects/PlayerMovementDataSo")]
    public class PlayerMovementDataSo : ScriptableObject
    {
        [SerializeField] private float _movementSpeed;
        public float MovementSpeed => _movementSpeed;
    }
}

