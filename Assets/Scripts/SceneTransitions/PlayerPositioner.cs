using UnityEngine;
using System.Collections.Generic;
using Game.Persistence;
using Game.Core;

namespace Game.SceneTransitions
{
    public class PlayerPositioner : MonoBehaviour
    {
        [SerializeField] private List<SpawnPositionId> _spawnSpots;

        [SerializeField] private Transform _playerOne;
        [SerializeField] private Transform _playerTwo;

        private SpawnId _currentSpawnId;
                       
        private void OnEnable()
        {
            SceneTransitionManager.OnSceneTransitionStarted += SaveData;
        }

        private void Start()
        {
            if (GameSessionManager.Instance.HasSavedState)
                LoadSesionData();
            else
                InitializeDefaultData();

            Transform spawnTransform = null;

            foreach(SpawnPositionId spawnId in _spawnSpots)
            {
                if(spawnId.Id == _currentSpawnId)
                {
                    spawnTransform = spawnId.transform;
                }
            }

            if (spawnTransform == null)
                return; 

            _playerOne.transform.position = spawnTransform.position;
            _playerTwo.transform.position = new Vector2(spawnTransform.position.x + 2, spawnTransform.position.y);
        }

        private void OnDisable()
        {
            SceneTransitionManager.OnSceneTransitionStarted -= SaveData;
        }

        public void SetNextSpawnId(SpawnId spawnId)
        {
            _currentSpawnId = spawnId;
        }

        private void InitializeDefaultData()
        {
            _currentSpawnId = SpawnId.Initial;
        }

        private void LoadSesionData()
        {
            _currentSpawnId = GameSessionManager.Instance.PlayerPositionerData.SpawnId;
        }

        private void SaveData()
        {
            GameSessionManager.Instance.SavePlayerPositionerData(new PlayerPositionerData(_currentSpawnId));
        }

    }
}

