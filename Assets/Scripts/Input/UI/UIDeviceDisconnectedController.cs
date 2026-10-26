using UnityEngine;
using Game.Core;

public class UIDeviceDisconnectedController : MonoBehaviour
{
    [SerializeField] private PlayerDeviceManager _deviceManager;

    [SerializeField] private CanvasGroup _singlePlayerTextCanvasGroup;
    [SerializeField] private CanvasGroup _playerOneTextCanvasGroup;
    [SerializeField] private CanvasGroup _playerTwoTextCanvasGroup;

    private void OnEnable()
    {
        _deviceManager.OnPlayerDeviceDisconnected += Display;
        _deviceManager.OnPlayerDeviceConnected += Hide;
    }

    private void OnDisable()
    {
        _deviceManager.OnPlayerDeviceDisconnected -= Display;
        _deviceManager.OnPlayerDeviceConnected -= Hide;
    }

    private void Display(PlayerType playerType)
    {
        switch(playerType)
        {
            case PlayerType.Player1:
                _singlePlayerTextCanvasGroup.alpha = 1;
                _playerOneTextCanvasGroup.alpha = 1;
                break;

            case PlayerType.Player2:
                _playerTwoTextCanvasGroup.alpha = 1;
                break;
        }
    }

    private void Hide(PlayerType playerType)
    {
        switch (playerType)
        {           
            case PlayerType.Player1:
                _singlePlayerTextCanvasGroup.alpha = 0;
                _playerOneTextCanvasGroup.alpha = 0;
                break;

            case PlayerType.Player2:
                _playerTwoTextCanvasGroup.alpha = 0;
                break;
        }
    }
}
