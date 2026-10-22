using UnityEngine;
using UnityEngine.InputSystem;
using Game.Core;
using System;

public class PlayerDeviceManager : MonoBehaviour
{
    public event Action <PlayerType> OnPlayerDeviceDisconnected;
    public event Action <PlayerType> OnPlayerDeviceReconnected;

    private Gamepad _player1Gamepad;
    private Gamepad _player2Gamepad;

    private void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    public void RegisterGamepad(PlayerType playerType, int gamepadIndex)
    {
        if (gamepadIndex < 0 || gamepadIndex >= Gamepad.all.Count) return;

        Gamepad gamepad = Gamepad.all[gamepadIndex];

        if (playerType == PlayerType.KeyboardP1)
            _player1Gamepad = gamepad;
        else
            _player2Gamepad = gamepad;
    }


    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (change != InputDeviceChange.Disconnected &&
            change != InputDeviceChange.Removed)
            return;

        if (device is not Gamepad gamepad)
            return;

        if(change == InputDeviceChange.Disconnected)
        {
            if (_player1Gamepad?.deviceId == gamepad.deviceId)
                OnPlayerDeviceDisconnected?.Invoke(PlayerType.KeyboardP1);

            else if (_player2Gamepad?.deviceId == gamepad.deviceId)
                OnPlayerDeviceDisconnected?.Invoke(PlayerType.KeyboardP2);
        }
        else if (change == InputDeviceChange.Reconnected)
        {
            if (_player1Gamepad?.deviceId == gamepad.deviceId)
                OnPlayerDeviceReconnected?.Invoke(PlayerType.KeyboardP1);

            else if (_player2Gamepad?.deviceId == gamepad.deviceId)
                OnPlayerDeviceReconnected?.Invoke(PlayerType.KeyboardP2);
        }        
    }
}
