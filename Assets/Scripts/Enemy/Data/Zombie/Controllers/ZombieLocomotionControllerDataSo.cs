using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "ZombieLocomotionControllerDataSo", menuName = "Scriptable Objects/ZombieLocomotionControllerDataSo")]
    public class ZombieLocomotionControllerDataSo : ScriptableObject
    {
        [SerializeField] private ForwardMovementDataSo _movementData;
        public ForwardMovementDataSo MovementData => _movementData;
    }
}

