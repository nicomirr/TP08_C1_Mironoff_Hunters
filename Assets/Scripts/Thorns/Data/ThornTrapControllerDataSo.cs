using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "ThornTrapControllerDataSo", menuName = "Scriptable Objects/ThornTrapControllerDataSo")]
    public class ThornTrapControllerDataSo : ScriptableObject
    {
        [SerializeField] private float _thornSpawnTime;
        public float ThornSpawnTime => _thornSpawnTime;

        [SerializeField] private float _minInitialDelayTime;
        public float MinInitialDelayTime => _minInitialDelayTime;

        [SerializeField] private float _maxInitialDelayTime;
        public float MaxInitialDelayTime => _maxInitialDelayTime;

        [SerializeField] private float _activeTime;
        public float ActiveTime => _activeTime;

        [SerializeField] private float _inactiveTime;
        public float InactiveTime => _inactiveTime;
    }

}
