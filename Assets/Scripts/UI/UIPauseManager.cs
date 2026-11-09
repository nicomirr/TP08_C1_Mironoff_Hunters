using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Game.Core;

namespace Game.UI
{
    public class UIPauseManager : UIMenuManager
    {
        [SerializeField] private GameObject _pauseGameObject;
        [SerializeField] private Button _mainMenuButton;

        private IPauseToggle _pauseToggle;
        private IPauseNotifier _pauseNotifier;

        protected override void Awake()
        {
            base.Awake();

            _pauseToggle = _pauseGameObject.GetComponent<IPauseToggle>();
            _pauseNotifier = _pauseGameObject.GetComponent<IPauseNotifier>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _pauseNotifier.OnGamePaused += DisplayPausePanel;
            _pauseNotifier.OnGameUnpaused += ResetPauseState;

            _mainMenuButton.onClick.AddListener(OnMainMenuClicked);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _pauseNotifier.OnGamePaused -= DisplayPausePanel;
            _pauseNotifier.OnGameUnpaused -= ResetPauseState;

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

