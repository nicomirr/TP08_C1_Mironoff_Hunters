using UnityEngine;
using Game.Core;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "ThrowableEffectDataSo", menuName = "Scriptable Objects/ThrowableEffectDataSo")]
    public class ThrowableEffectDataSo : ScriptableObject
    {
        [SerializeField] private PoolObjectType _throwableType;
        public PoolObjectType ThrowableType => _throwableType;

        [SerializeField] private float _yOffset;
        public float YOffset => _yOffset;
    }

}

