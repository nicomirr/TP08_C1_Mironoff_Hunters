using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "ZombieBurrowControllerDataSo", menuName = "Scriptable Objects/ZombieBurrowControllerDataSo")]
    public class ZombieBurrowControllerDataSo : ScriptableObject
    {
        [SerializeField] private float _distance;
        public float Distance => _distance;

        [SerializeField] private float _duration;
        public float Duration => _duration;

        [SerializeField] private float _minUnburrowCooldown;
        public float MinUnburrowCooldown => _minUnburrowCooldown;

        [SerializeField] private float _maxUnburrowCooldown;
        public float MaxUnburrowCooldown => _maxUnburrowCooldown;

        [SerializeField] private float _minBurrowCooldown;
        public float MinBurrowCooldown => _minBurrowCooldown;

        [SerializeField] private float _maxBurrowCooldown;
        public float MaxBurrowCooldown => _maxBurrowCooldown;

        [SerializeField] private float _minOffsetDistance;
        public float MinOffsetDistance => _minOffsetDistance;

        [SerializeField] private float _maxOffsetDistance;
        public float MaxOffsetDistance => _maxOffsetDistance;
    }

}

