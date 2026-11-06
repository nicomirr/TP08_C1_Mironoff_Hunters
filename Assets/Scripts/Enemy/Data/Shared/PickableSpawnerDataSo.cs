using UnityEngine;
using System.Collections.Generic;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "PickableSpawnerDataSo", menuName = "Scriptable Objects/PickableSpawnerDataSo")]
    public class PickableSpawnerDataSo : ScriptableObject
    {
        [SerializeField] private List<PickableSpawnDataSo> _pickablesData;
        public List<PickableSpawnDataSo> PickablesData => _pickablesData;
    }
}
