using UnityEngine;

namespace Game.Core
{
    public class GameModeController : MonoBehaviour
    {
        [SerializeField] private GameObject _playerTwoObject;

        private void Awake()
        {
            if (GameMode.PlayerCount == 1)
                _playerTwoObject.SetActive(false);
        }
    }

}

