using UnityEngine;
using System.Collections.Generic;
using Game.Data;
using Game.Core;

namespace Game.ObjectPooling
{
    public class Factory
    {
        private readonly List<ObjectPoolDataSo> _poolObjects = new();
        private readonly Transform _parent;

        public Factory(List<ObjectPoolDataSo> poolObjects, Transform parent)
        {
            _poolObjects = poolObjects;
        }

        public Dictionary<PoolObjectType, List<GameObject>> GetPoolObjects()
        {
            Dictionary<PoolObjectType, List<GameObject>> pool = new();

            List<GameObject> objs = new();

            foreach (ObjectPoolDataSo poolObjectData in _poolObjects)
            {
                for(int i = 0; i < poolObjectData.Amount; i++)
                {
                    GameObject prefab = GameObject.Instantiate(poolObjectData.Prefab, _parent);
                    prefab.SetActive(false);

                    objs.Add(prefab);
                }                

                pool.Add(poolObjectData.PoolObjectType, objs);                
            }

            return pool;
        }
    }
}
