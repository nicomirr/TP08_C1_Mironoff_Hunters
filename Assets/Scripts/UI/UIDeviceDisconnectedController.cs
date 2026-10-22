using UnityEngine;
using Game.Core;

public class UIDeviceDisconnectedController : MonoBehaviour
{
    [SerializeField] private PlayerDeviceManager _deviceManager;

    [SerializeField] private CanvasGroup _playerOneTextCanvasGroup;
    [SerializeField] private CanvasGroup _playerTwoTextCanvasGroup;

    private int _disconnectedDevices;

    private void OnEnable()
    {
        _deviceManager.OnPlayerDeviceDisconnected += Display;
        _deviceManager.OnPlayerDeviceReconnected += Hide;
    }

    private void OnDisable()
    {
        _deviceManager.OnPlayerDeviceDisconnected -= Display;
        _deviceManager.OnPlayerDeviceReconnected -= Hide;
    }

    private void Display(PlayerType playerType)
    {
        switch(playerType)
        {
            case PlayerType.KeyboardP1:

                _playerOneTextCanvasGroup.alpha = 1;
                break;

            case PlayerType.KeyboardP2:
                _playerTwoTextCanvasGroup.alpha = 1;
                break;
        }

        _disconnectedDevices++;
    }

    private void Hide(PlayerType playerType)
    {
        switch (playerType)
        {
            case PlayerType.KeyboardP1:
                _playerOneTextCanvasGroup.alpha = 0;
                break;

            case PlayerType.KeyboardP2:
                _playerTwoTextCanvasGroup.alpha = 0;
                break;
        }

        _disconnectedDevices--;
    }
}
