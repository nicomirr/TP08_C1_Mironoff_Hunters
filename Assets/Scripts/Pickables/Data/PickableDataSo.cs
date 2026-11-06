using UnityEngine;
using Game.Core;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "PickableDataSo", menuName = "Scriptable Objects/PickableDataSo")]
    public class PickableDataSo : ScriptableObject
    {
        [SerializeField] private PickableType _pickableType;
        public PickableType PickableType => _pickableType;

        [SerializeField] private int _amount;
        public int Amount => _amount;

        [SerializeField] private float _minAngle;
        public float MinAngle => _minAngle;

        [SerializeField] private float _maxAngle;
        public float MaxAngle => _maxAngle;

        [SerializeField] private float _minForce;
        public float MinForce => _minForce;

        [SerializeField] private float _maxForce;
        public float MaxForce => _maxForce;

        [SerializeField] private float _lifetime;
        public float Lifetime => _lifetime;
    }
}


