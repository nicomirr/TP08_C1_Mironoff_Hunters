using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Game.Core;
using Game.Pause;

namespace Game.UI
{
    public class UIPauseManager : UIMenuManager
    {
        [SerializeField] private GameObject _pauseGameObject;
        [SerializeField] private Button _mainMenuButton;

        private IPauseToggle _pauseToggle;

        protected override void Awake()
        {
            base.Awake();

            _pauseToggle = _pauseGameObject.GetComponent<IPauseToggle>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            PauseManager.OnGamePaused += DisplayPausePanel;
            PauseManager.OnGameUnpaused += ResetPauseState;

            _mainMenuButton.onClick.AddListener(OnMainMenuClicked);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            PauseManager.OnGamePaused -= DisplayPausePanel;
            PauseManager.OnGameUnpaused -= ResetPauseState;

            _mainMenuButton.onClick.RemoveAllListeners();
        }

        protected override void Start()
        {
            base.Start();                
            
            _currentPanel.HidePanel();
        }

        protected override void OnPlayClicked()
        {
            _audioSource.Play();

            _pauseToggle.TogglePauseState();
            _currentPanel.HidePanel();
        }

        private void OnMainMenuClicked()
        {
            _audioSource.Play();

            _currentPanel.HidePanel();

            _transitionManager.ChangeScene(_sceneToLoad);
        }

        private void DisplayPausePanel()
        {
            EventSystem.current.SetSelectedGameObject(_currentPanel.GamepadInitialButton.gameObject);
            _currentPanel.DisplayPanel();
        }

        private void ResetPauseState()
        {
            _currentPanel.HidePanel();
            _currentPanel = _mainPanel;
            EventSystem.current.SetSelectedGameObject(_currentPanel.GamepadInitialButton.gameObject);
        }
    }

}

