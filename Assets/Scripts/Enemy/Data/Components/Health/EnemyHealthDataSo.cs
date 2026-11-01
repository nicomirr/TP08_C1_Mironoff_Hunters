using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "EnemyHealthDataSo", menuName = "Scriptable Objects/EnemyHealthDataSo")]
    public class EnemyHealthDataSo : ScriptableObject
    {
        [SerializeField] private float _maxHealth;
        public float MaxHealth => _maxHealth;
    }

}

