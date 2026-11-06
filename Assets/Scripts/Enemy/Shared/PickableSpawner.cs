using UnityEngine;
using System.Collections.Generic;
using Game.Data;
using Game.ObjectPooling;

namespace Game.Enemy
{
    public class PickableSpawner
    {
        private readonly List<PickableSpawnDataSo> _pickablesData = new();
        private readonly Transform _ownerTransform;

        private bool _lootSpawned;

        public PickableSpawner(PickableSpawnerDataSo data, Transform ownerTransform)
        {
            _pickablesData = data.PickablesData;
            _ownerTransform = ownerTransform;
        }

        public void SpawnPickables()
        {
            if (_lootSpawned) return;

            foreach (PickableSpawnDataSo pickableData in _pickablesData)
            {
                int RandomChance = Random.Range(1, 101);

                if (RandomChance <= pickableData.SpawnChance)
                {
                    for (int i = 0; i < pickableData.Amount; i++)
                    {
                        GameObject obj = ObjectPool.Instance.GetPoolObject(pickableData.PoolObjectPickableType);

                        obj.transform.position = _ownerTransform.position;
                        obj.SetActive(true);
                    }                    
                }
            }

            _lootSpawned = true;

            //se spawnean todos los tipos de pickables
            //si o si se spawnean los orbes de experiencia, varia de enemigo en enemigo el tipo de orbe (dan mas experiencia algunos)
            //Moneda posiblemente sea siempre 1. 
            //las monedas no se spawnean siempre ¿quizas la chance varie de enemigo en enemigo?

            //IMPORTANTE: como se prueban todos los pickables no necesitamos el diccionario. Solo una lista de pickablespawndataSo, y que se recorra toda
            //intentando spawnear cada pickable.
        }

        public void Reset()
        {
            _lootSpawned = false;
        }
    }

}

