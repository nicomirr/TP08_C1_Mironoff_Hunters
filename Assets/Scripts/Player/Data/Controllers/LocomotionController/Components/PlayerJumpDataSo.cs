using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "PlayerJumpDataSo", menuName = "Scriptable Objects/PlayerJumpDataSo")]
    public class PlayerJumpDataSo : ScriptableObject
    {
        [SerializeField] private float _coyoteTime;
        public float CoyoteTime => _coyoteTime;

        [SerializeField] private float _regularJumpForce;
        public float RegularJumpForce => _regularJumpForce;

        [SerializeField] private float _jumpCutMultiplier;
        public float JumpCutMultiplier => _jumpCutMultiplier;
    }
}

