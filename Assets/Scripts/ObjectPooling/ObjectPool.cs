using UnityEngine;
using System.Collections.Generic;
using Game.Data;
using Game.Core;

namespace Game.ObjectPooling
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private List<ObjectPoolDataSo> _poolObjects;

        private Dictionary<PoolObjectType, List<GameObject>> _objectsPool = new();

        private Factory _factory;

        private static ObjectPool _instance;
        public static ObjectPool Instance => _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(this.gameObject);

            _factory = new Factory(_poolObjects, this.transform);
        }

        private void Start()
        {
            _objectsPool = _factory.GetPoolObjects();
        }

        public GameObject GetPoolObject(PoolObjectType type)
        {
            List<GameObject> pool = _objectsPool[type];

            foreach (GameObject obj in pool)
            {
                if(!obj.activeSelf)
                {
                    obj.SetActive(false);
                    return obj;
                }
            }

            Debug.LogError("Error. El pool deberia devolver siempre un objeto", this);

            return null;
        }
    }

}
