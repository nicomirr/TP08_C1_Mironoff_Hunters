using UnityEngine;
using Game.Player.Brain;

namespace Game.UI
{
    [RequireComponent(typeof(UIInteractionIndicator))]
    public class PlayerUIManager : MonoBehaviour
    {
        [SerializeField] private PlayerBrain _player;

        private UIInteractionIndicator _interactionIndicator;

        private void Awake()
        {
            _interactionIndicator = GetComponent<UIInteractionIndicator>();
        }

        private void OnEnable()
        {
            _player.OnCurrentDeviceChanged += _interactionIndicator.UpdateInteractionButton;
            _player.OnInteractableInRangeReceived += _interactionIndicator.DisplayInteractionButton;
            _player.OnInteractableOutOfRangeReceived += _interactionIndicator.HideInteractionButtons;
        }

        private void OnDisable()
        {
            _player.OnCurrentDeviceChanged -= _interactionIndicator.UpdateInteractionButton;
            _player.OnInteractableInRangeReceived -= _interactionIndicator.DisplayInteractionButton;
            _player.OnInteractableOutOfRangeReceived -= _interactionIndicator.HideInteractionButtons;
        }
    }

}
