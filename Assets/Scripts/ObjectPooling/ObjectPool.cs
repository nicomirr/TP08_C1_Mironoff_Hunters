using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Game.Core;
using Game.Data;
using Game.Checkpoints;
using Game.Enemy;

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

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            Checkpoint.OnCheckpointUsed += DisableBullets;
            BossBrain.OnBossDefeated += DisableBossBullets;
        }
        
        private void Start()
        {
            _objectsPool = _factory.GetPoolObjects();
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Checkpoint.OnCheckpointUsed -= DisableBullets;
            BossBrain.OnBossDefeated -= DisableBossBullets;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            foreach(KeyValuePair<PoolObjectType, List<GameObject>> pair in _objectsPool)
            {
                foreach(GameObject obj in pair.Value)
                    obj.SetActive(false);
            }
        }

        public GameObject GetPoolObject(PoolObjectType type)
        {
            List<GameObject> pool = _objectsPool[type];

            foreach (GameObject obj in pool)
            {
                if(!obj.activeSelf)                
                    return obj;                
            }

            Debug.LogError("Error. El pool deberia devolver siempre un objeto", this);

            return null;
        }

        public void DisableDamageEffects()
        {
            List<GameObject> fireDamageEffects = _objectsPool[PoolObjectType.FireDamageEffect];
            List<GameObject> poisonEffects = _objectsPool[PoolObjectType.PoisonDamageEffect];

            foreach(GameObject obj in fireDamageEffects)
                obj.SetActive(false);

            foreach(GameObject obj in poisonEffects)
                obj.SetActive(false);
        }

        public void DisableInvulnerabilityEffects()
        {
            List<GameObject> invulnerabilityEffects = _objectsPool[PoolObjectType.InvulnerabilityEffect];

            foreach (GameObject obj in invulnerabilityEffects)
                obj.SetActive(false);
        }

        private void DisableBullets(int _, bool __, PlayerType ___)
        {
            List<GameObject> plantFastBullets = _objectsPool[PoolObjectType.PlantBulletFast];
            List<GameObject> plantNormalBullets = _objectsPool[PoolObjectType.PlantBulletNormal];
            List<GameObject> bossBullets = _objectsPool[PoolObjectType.ForestBossBullet];

            foreach(GameObject obj in plantFastBullets)
                obj.SetActive(false);

            foreach (GameObject obj in plantNormalBullets)
                obj.SetActive(false);

            foreach(GameObject obj in  bossBullets)
                obj.SetActive(false);
        }

        private void DisableBossBullets()
        {
            List<GameObject> plantFastBullets = _objectsPool[PoolObjectType.PlantBulletFast];
            List<GameObject> plantNormalBullets = _objectsPool[PoolObjectType.PlantBulletNormal];
            List<GameObject> bossBullets = _objectsPool[PoolObjectType.ForestBossBullet];

            foreach (GameObject obj in plantFastBullets)
                obj.SetActive(false);

            foreach (GameObject obj in plantNormalBullets)
                obj.SetActive(false);

            foreach (GameObject obj in bossBullets)
                obj.SetActive(false);
        }
    }

}
