using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "PlayerCombatDataSo", menuName = "Scriptable Objects/PlayerCombatDataSo")]
    public class PlayerCombatControllerDataSo : ScriptableObject
    {
        [SerializeField] private int _damage;
        public int Damage => _damage;

        [SerializeField] private float _cooldownTime;
        public float CooldownTime => _cooldownTime;
    }

}

