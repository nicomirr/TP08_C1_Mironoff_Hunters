using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIPanel : MonoBehaviour
    {
        [SerializeField] private Button _gamepadInitialButton;
        public Button GamepadInitialButton => _gamepadInitialButton;

        private CanvasGroup _canvasGroup;

        protected virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }       

        public virtual void DisplayPanel()
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
        }

        public virtual void HidePanel()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
        }
    }
}
