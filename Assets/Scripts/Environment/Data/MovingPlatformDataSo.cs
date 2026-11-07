using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "MovingPlatformDataSo", menuName = "Scriptable Objects/MovingPlatformDataSo")]
    public class MovingPlatformDataSo : ScriptableObject
    {
        [SerializeField] private float _speed;
        public float Speed => _speed;
    }

}

