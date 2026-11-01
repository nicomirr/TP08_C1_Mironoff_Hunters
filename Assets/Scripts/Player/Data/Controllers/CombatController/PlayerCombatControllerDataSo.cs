using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "PlayerCombatDataSo", menuName = "Scriptable Objects/PlayerCombatDataSo")]
    public class PlayerCombatControllerDataSo : ScriptableObject
    {
        [SerializeField] private float _damage;
        public float Damage => _damage;

        [SerializeField] private float _cooldownTime;
        public float CooldownTime => _cooldownTime;

        [SerializeField] private float _attackTime;
        public float AttackTime => _attackTime; 
    }

}

