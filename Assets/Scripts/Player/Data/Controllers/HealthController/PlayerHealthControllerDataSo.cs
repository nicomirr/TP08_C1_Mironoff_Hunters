using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "PlayerHealthControllerDataSo", menuName = "Scriptable Objects/PlayerHealthControllerDataSo")]
    public class PlayerHealthControllerDataSo : ScriptableObject
    {
        [SerializeField] private int _maxHealth;
        public int MaxHealth => _maxHealth;
    }
}

