using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "BurrowLimitsDataSo", menuName = "Scriptable Objects/BurrowLimitsDataSo")]
    public class BurrowLimitsDataSo : ScriptableObject
    {
        [SerializeField] private float _minX;
        public float MinX => _minX;

        [SerializeField] private float _maxX;
        public float MaxX => _maxX;
    }

}
