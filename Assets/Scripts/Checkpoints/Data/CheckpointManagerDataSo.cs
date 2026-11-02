using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "CheckpointManagerDataSo", menuName = "Scriptable Objects/CheckpointManagerDataSo")]
    public class CheckpointManagerDataSo : ScriptableObject
    {
        [SerializeField] private float _displayFlashTime;
        public float DisplayFlashTime => _displayFlashTime;
    }

}
