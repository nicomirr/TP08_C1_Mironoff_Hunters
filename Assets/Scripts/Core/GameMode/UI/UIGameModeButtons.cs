using UnityEngine;
using UnityEngine.UI;
using Game.Core;
using Game.SceneTransitions;
using Game.Data;

namespace Game.UI
{
    public class UIGameModeButtons : MonoBehaviour
    {
        [SerializeField] private SceneToLoadDataSo _sceneToLoad;
        [SerializeField] private SceneTransitionManager _sceneTransitionsManager;

        [SerializeField] private Button _singlePlayerButton;
        [SerializeField] private Button _coopButton;

        private void OnEnable()
        {
            _singlePlayerButton.onClick.AddListener(SetSinglePlayerMode);
            _coopButton.onClick.AddListener(SetCoopMode);
        }

        private void OnDisable()
        {
            _singlePlayerButton.onClick.RemoveAllListeners();
            _coopButton.onClick.RemoveAllListeners();
        }

        private void SetSinglePlayerMode()
        {
            GameMode.SetPlayerCount(1);
            _sceneTransitionsManager.ChangeScene(_sceneToLoad);
        }

        private void SetCoopMode()
        {
            GameMode.SetPlayerCount(2);
            _sceneTransitionsManager.ChangeScene(_sceneToLoad);
        }
    }
}
