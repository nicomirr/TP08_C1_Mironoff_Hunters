using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "PlayerLocomotionDataSo", menuName = "Scriptable Objects/PlayerLocomotionDataSo")]
    public class PlayerLocomotionDataSo : ScriptableObject
    {
        [SerializeField] private PlayerMovementDataSo _movementData;
        public PlayerMovementDataSo MovementData => _movementData;

    }
}
