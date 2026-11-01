using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "PlayerKnockbackDataSo", menuName = "Scriptable Objects/PlayerKnockbackDataSo")]
    public class PlayerKnockbackDataSo : ScriptableObject
    {
        [SerializeField] private float _horizontalKnockbackForce;
        public float HorizontalKnockbackForce => _horizontalKnockbackForce;

        [SerializeField] private float _verticalKnockbackForce;
        public float VerticalKnockbackForce => _verticalKnockbackForce;

        [SerializeField] private float _knockbackTime;
        public float KnockbackTime => _knockbackTime;
    }

}

