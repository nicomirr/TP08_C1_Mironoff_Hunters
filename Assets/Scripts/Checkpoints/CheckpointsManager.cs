using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Data;
using Game.Persistence;
using Game.SceneTransitions;
using Game.Core;

namespace Game.Checkpoints
{
    public class CheckpointsManager : MonoBehaviour
    {
        [SerializeField] private CheckpointManagerDataSo _data;

        [SerializeField] private Transform _playerOneTransform;
        [SerializeField] private Transform _playerTwoTransform;

        [SerializeField] private FastTravel _fastTravelSingle;
        [SerializeField] private FastTravel _fastTravelPlayerOne;
        [SerializeField] private FastTravel _fastTravelPlayerTwo;

        [SerializeField] private List<Checkpoint> _checkpoints;
        public List<Checkpoint> Checkpoints => _checkpoints;

        [SerializeField] private List<GameObject> _checkpointFlashes;

        private List<int> _checkpointsReached = new();
        public List<int> CheckpointsReached => _checkpointsReached;
        
        private int _currentCheckpointNumber;
        public int CurrentCheckpointNumber => _currentCheckpointNumber;


        private void Awake()
        {
            if (GameSessionManager.Instance.HasSavedState)
                _checkpointsReached = GameSessionManager.Instance.CheckpointManagerData.CheckpointsReached;
        }

        private void OnEnable()
        {
            SceneTransitionManager.OnSceneTransitionStarted += SaveSessionData;
            Checkpoint.OnCheckpointUsed += HandleCheckpointUsed;            
        }

        private void Start()
        {
            _currentCheckpointNumber = 0;
        }

        private void OnDisable()
        {
            SceneTransitionManager.OnSceneTransitionStarted -= SaveSessionData;
            Checkpoint.OnCheckpointUsed -= HandleCheckpointUsed;
        }

        private void HandleCheckpointUsed(int checkpointNumber, bool checkpointReached, PlayerType playerType)
        {
            _currentCheckpointNumber = checkpointNumber;
            
            if(!checkpointReached)
                _checkpointsReached.Add(checkpointNumber);

            StartCoroutine(DisplayFlashRoutine());

            if(playerType == PlayerType.Player2)
            {
                _fastTravelPlayerTwo.DisplayFastTravel();
                return;
            }

            if(GameMode.CurrentGameModeType == GameModeType.Coop)
            {
                _fastTravelPlayerOne.DisplayFastTravel();
                return;
            }

            _fastTravelSingle.DisplayFastTravel();
        }

        private IEnumerator DisplayFlashRoutine()
        {
            foreach (GameObject checkPointFlash in _checkpointFlashes)
                checkPointFlash.SetActive(true);

            yield return new WaitForSeconds(_data.DisplayFlashTime);

            foreach (GameObject checkPointFlash in _checkpointFlashes)
                checkPointFlash.SetActive(false);
        }      
        
        public void FastTravel(int checkpointNumber)
        {
            StartCoroutine(DisplayFlashRoutine());

            foreach(Checkpoint checkpoint in _checkpoints)
            {
                if(checkpoint.CheckpointNumber == checkpointNumber)
                {
                    _playerOneTransform.position = checkpoint.transform.position;
                    _playerTwoTransform.position = checkpoint.transform.position;

                    break;
                }
            }

        }

        private void SaveSessionData()
        {
            CheckpointManagerSessionData data = new CheckpointManagerSessionData(_checkpointsReached);

            GameSessionManager.Instance.SaveCheckpointManagerData(data);
        }

    }

}
