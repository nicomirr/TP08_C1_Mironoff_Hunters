using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "PlayerGravityDataSo", menuName = "Scriptable Objects/PlayerGravityDataSo")]
    public class PlayerGravityDataSo : ScriptableObject
    {
        [SerializeField] private float _risingGravity;
        public float RisingGravity => _risingGravity;

        [SerializeField] private float _fallingGravity;
        public float FallingGravity => _fallingGravity; 

        [SerializeField] private float _apexGravity;
        public float ApexGravity => _apexGravity;

        [SerializeField] private float _apexVelocityTreshold;
        public float ApexVelocityTreshold => _apexVelocityTreshold;
    }
}
