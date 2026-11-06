using UnityEngine;
using Game.Core;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "PickableSpawnDataSo", menuName = "Scriptable Objects/PickableSpawnDataSo")]
    public class PickableSpawnDataSo : ScriptableObject
    {
        [SerializeField] private int _amount;
        public int Amount => _amount;

        [SerializeField] private PoolObjectType _poolObjectPickableType;
        public PoolObjectType PoolObjectPickableType => _poolObjectPickableType;

        [SerializeField] private int _spawnChance;
        public int SpawnChance => _spawnChance;
    }

}

