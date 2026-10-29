using UnityEngine;
using Game.Core;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "ObjectPoolDataSo", menuName = "Scriptable Objects/ObjectPoolDataSo")]
    public class ObjectPoolDataSo : ScriptableObject
    {
        [SerializeField] private int _amount;
        public int Amount => _amount;

        [SerializeField] private PoolObjectType _poolObjectType;
        public PoolObjectType PoolObjectType => _poolObjectType;         

        [SerializeField] private GameObject _prefab;
        public GameObject Prefab => _prefab;

    }

}

