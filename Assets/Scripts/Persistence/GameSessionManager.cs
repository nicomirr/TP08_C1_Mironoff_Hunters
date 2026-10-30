using UnityEngine;
using Game.Core;

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
    }
}
