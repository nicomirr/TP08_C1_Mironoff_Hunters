using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "PlayerGroundCheckDataSo", menuName = "Scriptable Objects/PlayerGroundCheckDataSo")]
    public class PlayerGroundCheckDataSo : ScriptableObject
    {
        [SerializeField] private LayerMask _groundLayer;
        public LayerMask GroundLayer => _groundLayer;

        [SerializeField] private Vector2 _groundCheckSize;
        public Vector2 GroundCheckSize => _groundCheckSize;

        [SerializeField] private float _groundCheckDistance;
        public float GroundCheckDistance => _groundCheckDistance;   
    }
}

