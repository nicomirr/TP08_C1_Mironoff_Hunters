using UnityEngine;
using System.Collections.Generic;
using Game.Persistence;
using Game.Core;

namespace Game.SceneTransitions
{
    public class PlayerPositioner : MonoBehaviour
    {
        [SerializeField] private List<SpawnPositionInfo> _spawnSpots;

        [SerializeField] private Transform _singlePlayerCamera;
        [SerializeField] private Transform _playerOneCamera;
        [SerializeField] private Transform _playerTwoCamera;

        [SerializeField] private Transform _playerOne;
        [SerializeField] private Transform _playerTwo;

        private SpawnId _currentSpawnId;
        private Vector2 _currentOrientation;
                       
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

            foreach(SpawnPositionInfo spawnId in _spawnSpots)
            {
                if(spawnId.Id == _currentSpawnId)
                {
                    spawnTransform = spawnId.transform;
                }
            }

            if (spawnTransform == null)
                return; 

            _singlePlayerCamera.transform.position = new Vector3(spawnTransform.position.x, spawnTransform.position.y, -10);
            _playerOneCamera.transform.position = new Vector3(spawnTransform.position.x, spawnTransform.position.y, -10);
            _playerTwoCamera.transform.position = new Vector3(spawnTransform.position.x + 2, spawnTransform.position.y, -10);

            _playerOne.transform.position = spawnTransform.position;
            _playerOne.transform.right = _currentOrientation;

            _playerTwo.transform.position = new Vector2(spawnTransform.position.x + 2, spawnTransform.position.y);
            _playerTwo.transform.right = _currentOrientation;
        }

        private void OnDisable()
        {
            SceneTransitionManager.OnSceneTransitionStarted -= SaveData;
        }

        public void SetNextSpawnInfo(SpawnId spawnId, Vector2 orientation)
        {
            _currentSpawnId = spawnId;
            _currentOrientation = orientation;
        }

        private void InitializeDefaultData()
        {
            _currentSpawnId = SpawnId.Initial;
            _currentOrientation = Vector2.right;
        }

        private void LoadSesionData()
        {
            _currentSpawnId = GameSessionManager.Instance.PlayerPositionerData.SpawnId;
            _currentOrientation = GameSessionManager.Instance.PlayerPositionerData.Orientation;
        }

        private void SaveData()
        {
            GameSessionManager.Instance.SavePlayerPositionerData(new PlayerPositionerData(_currentSpawnId, _currentOrientation));
        }

    }
}

