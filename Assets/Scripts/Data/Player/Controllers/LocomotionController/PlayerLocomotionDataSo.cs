using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "PlayerLocomotionDataSo", menuName = "Scriptable Objects/PlayerLocomotionDataSo")]
    public class PlayerLocomotionDataSo : ScriptableObject
    {
        [SerializeField] private PlayerMovementDataSo _movementData;
        public PlayerMovementDataSo MovementData => _movementData;

        [SerializeField] private PlayerJumpDataSo _jumpData;
        public PlayerJumpDataSo JumpData => _jumpData;

        [SerializeField] private PlayerGroundCheckDataSo _groundCheckData;
        public PlayerGroundCheckDataSo GroundCheckData => _groundCheckData;

        [SerializeField] private PlayerGravityDataSo _gravityData;
        public PlayerGravityDataSo GravityData => _gravityData;
    }
}
