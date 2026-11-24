using UnityEngine;

namespace Game.Core
{
    public class GameModeController : MonoBehaviour
    {
        // Suggestion: flag de prueba (_testCoop) que quedó en el build. Si queda tildado en la escena fuerza el modo Coop. Sacalo o envolvelo en #if UNITY_EDITOR.
        [SerializeField] private bool _testCoop;

        [SerializeField] private GameObject _coopCameras;
        [SerializeField] private GameObject _singlePlayerCamera;

        [SerializeField] private GameObject _playerTwoRespawner;
        [SerializeField] private GameObject _playerTwoObject;

        [SerializeField] private CanvasGroup _splitScreenBar;

        private void Awake()
        {
            if(_testCoop)
                GameMode.SetGameMode(GameModeType.Coop);

            if (GameMode.CurrentGameModeType == GameModeType.SinglePlayer)
            {
                _playerTwoRespawner.SetActive(false);
                _playerTwoObject.SetActive(false);
                _coopCameras.SetActive(false);
                _singlePlayerCamera.SetActive(true);

                _splitScreenBar.alpha = 0;
            }
            else
            {
                _playerTwoRespawner.SetActive(true);
                _playerTwoObject.SetActive(true);
                _coopCameras.SetActive(true);
                _singlePlayerCamera.SetActive(false);

                _splitScreenBar.alpha = 1;
            }
        }
    }

}

