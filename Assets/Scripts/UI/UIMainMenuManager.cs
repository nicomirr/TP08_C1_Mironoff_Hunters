using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIMainMenuManager : UIMenuManager
    {
        [SerializeField] private Button _creditsButton;

        [SerializeField] private UIPanel _creditsPanel;
        [SerializeField] private UIPanel _gameModeSelectionPanel;

        protected override void OnEnable()
        {
            base.OnEnable();

            _creditsButton.onClick.AddListener(OnCreditsClicked);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _creditsButton.onClick.RemoveAllListeners();
        }

        protected override void OnPlayClicked()
        {
            base.OnPlayClicked();

            _previousPanel = _currentPanel;
            _currentPanel = _gameModeSelectionPanel;

            ChangePanels();

        }

        private void OnCreditsClicked()
        {
            _audioSource.Play();

            _previousPanel = _currentPanel;
            _currentPanel = _creditsPanel;

            ChangePanels();
        }
    }

}

