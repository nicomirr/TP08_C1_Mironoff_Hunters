using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIMainMenuManager : UIMenuManager
    {
        [SerializeField] private Button _creditsButton;

        [SerializeField] private UIPanel _creditsPanel;

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

        private void OnCreditsClicked()
        {
            _audioSource.Play();

            _previousPanel = _currentPanel;
            _currentPanel = _creditsPanel;

            ChangePanels();
        }
    }

}

