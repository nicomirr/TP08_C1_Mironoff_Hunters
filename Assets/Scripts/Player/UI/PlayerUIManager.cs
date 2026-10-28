using UnityEngine;
using Game.Player.Brain;

namespace Game.UI
{
    [RequireComponent (typeof(UIInteractionIndicator))]
    [RequireComponent (typeof(UICoins))]
    public class PlayerUIManager : MonoBehaviour
    {
        [SerializeField] private PlayerBrain _player;

        private UIInteractionIndicator _interactionIndicator;
        private UICoins _uiCoins;

        private void Awake()
        {
            _interactionIndicator = GetComponent<UIInteractionIndicator>();
            _uiCoins = GetComponent<UICoins>();
        }

        private void OnEnable()
        {
            _player.OnCurrentDeviceChanged += _interactionIndicator.UpdateInteractionButton;
            _player.OnInteractableInRangeReceived += _interactionIndicator.DisplayInteractionButton;
            _player.OnInteractableOutOfRangeReceived += _interactionIndicator.HideInteractionButtons;
            _player.OnCoinsValueChanged += _uiCoins.UpdateCoinsText;
        }

        private void OnDisable()
        {
            _player.OnCurrentDeviceChanged -= _interactionIndicator.UpdateInteractionButton;
            _player.OnInteractableInRangeReceived -= _interactionIndicator.DisplayInteractionButton;
            _player.OnInteractableOutOfRangeReceived -= _interactionIndicator.HideInteractionButtons;
            _player.OnCoinsValueChanged -= _uiCoins.UpdateCoinsText;
        }
    }

}
