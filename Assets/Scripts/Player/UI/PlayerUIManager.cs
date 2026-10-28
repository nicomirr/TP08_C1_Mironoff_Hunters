using UnityEngine;
using Game.Player.Brain;

namespace Game.UI
{
    [RequireComponent (typeof(UIInteractionIndicator))]
    [RequireComponent (typeof(UICoins))]
    [RequireComponent (typeof(UIInventory))]

    public class PlayerUIManager : MonoBehaviour
    {
        [SerializeField] private PlayerBrain _player;

        private UIInteractionIndicator _interactionIndicator;
        private UICoins _uiCoins;
        private UIInventory _uiInventory;

        private void Awake()
        {
            _interactionIndicator = GetComponent<UIInteractionIndicator>();
            _uiCoins = GetComponent<UICoins>();
            _uiInventory = GetComponent<UIInventory>();
        }

        private void OnEnable()
        {
            _player.OnCurrentDeviceChanged += _interactionIndicator.UpdateInteractionButton;
            _player.OnInteractableInRangeReceived += _interactionIndicator.DisplayInteractionButton;
            _player.OnInteractableOutOfRangeReceived += _interactionIndicator.HideInteractionButtons;
            _player.OnCoinsValueChanged += _uiCoins.UpdateCoinsText;
            _player.OnSelectedSlotChanged += _uiInventory.ChangeCurrentInventorySprite;
        }

        private void OnDisable()
        {
            _player.OnCurrentDeviceChanged -= _interactionIndicator.UpdateInteractionButton;
            _player.OnInteractableInRangeReceived -= _interactionIndicator.DisplayInteractionButton;
            _player.OnInteractableOutOfRangeReceived -= _interactionIndicator.HideInteractionButtons;
            _player.OnCoinsValueChanged -= _uiCoins.UpdateCoinsText;
            _player.OnSelectedSlotChanged -= _uiInventory.ChangeCurrentInventorySprite;
        }
    }

}
