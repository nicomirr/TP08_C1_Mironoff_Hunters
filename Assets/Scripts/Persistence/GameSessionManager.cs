using UnityEngine;
using Game.Core;
using Game.Checkpoints;
using System.Security.Cryptography;

namespace Game.Persistence
{
    public class GameSessionManager : MonoBehaviour
    {
        private static GameSessionManager _instance;
        public static GameSessionManager Instance => _instance;

        private PlayerSessionData _player1Data = new();
        public PlayerSessionData Player1Data => _player1Data;

        private PlayerSessionData _player2Data = new();
        public PlayerSessionData Player2Data => _player2Data;

        private PlayerPositionerData _playerPositionerData = new();
        public PlayerPositionerData PlayerPositionerData => _playerPositionerData;

        private CheckpointSessionData _checkpoint1Data = new();
        public CheckpointSessionData Checkpoint1Data => _checkpoint1Data;

        private CheckpointSessionData _checkpoint2Data = new();
        public CheckpointSessionData Checkpoint2Data => _checkpoint2Data;

        private CheckpointSessionData _checkpoint3Data = new();
        public CheckpointSessionData Checkpoint3Data => _checkpoint3Data;

        private CheckpointSessionData _checkpoint4Data = new();
        public CheckpointSessionData Checkpoint4Data => _checkpoint4Data;

        private CheckpointSessionData _checkpoint5Data = new();
        public CheckpointSessionData Checkpoint5Data => _checkpoint5Data;

        private CheckpointSessionData _checkpoint6Data = new();
        public CheckpointSessionData Checkpoint6Data => _checkpoint6Data;

        private CheckpointManagerSessionData _checkpointManagerData = new();
        public CheckpointManagerSessionData CheckpointManagerData => _checkpointManagerData;

        private bool _hasSavedState;
        public bool HasSavedState => _hasSavedState;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void SavePlayerData(PlayerSessionData data, PlayerType playerType)
        {
            if(!_hasSavedState)
                _hasSavedState = true;  

            if(playerType == PlayerType.Player1)
            {
                _player1Data = data;
                return;
            }

            _player2Data = data;
        }                

        public void SavePlayerPositionerData(PlayerPositionerData data)
        {
            if(!_hasSavedState)
                _hasSavedState = true;

            _playerPositionerData = data;
        }

        public void SaveCheckpointData(CheckpointSessionData data, int checkpointNumber)
        {
            switch(checkpointNumber)
            {
                case 1:
                    _checkpoint1Data = data;
                    break;

                case 2:
                    _checkpoint2Data = data; 
                    break;

                case 3:
                    _checkpoint3Data = data;
                    break;

                case 4:
                    _checkpoint4Data = data;
                    break;

                case 5:
                    _checkpoint5Data = data;
                    break;

                case 6:
                    _checkpoint6Data = data;
                    break;
            }
        }

        public void SaveCheckpointManagerData(CheckpointManagerSessionData data)
        {
            _checkpointManagerData = data;
        }
    }
}
