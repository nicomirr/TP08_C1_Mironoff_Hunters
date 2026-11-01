using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "UIEnemyHealthDataSo", menuName = "Scriptable Objects/UIEnemyHealthDataSo")]
    public class UIEnemyHealthDataSo : ScriptableObject
    {
        [SerializeField] private float _displayTime;
        public float DisplayTime => _displayTime;
    }

}

