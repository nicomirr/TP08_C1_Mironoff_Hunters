using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;
using System;
using Game.Core;

public class PlayerDeviceManager : MonoBehaviour
{
    public event Action <PlayerType> OnPlayerDeviceDisconnected;
    public event Action <PlayerType, Gamepad> OnPlayerDeviceReconnected;

    public event Action<PlayerType> OnPlayerDeviceConnected;

    public event Action<PlayerType,KeyControl> OnKeyboardKeyPressed;

    private Gamepad _player1Gamepad;
    private Gamepad _player2Gamepad;

    private bool _player1Waiting;
    private bool _player2Waiting;

    private IDisposable _keyboardSubscription;

    private void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;

        _keyboardSubscription = InputSystem.onAnyButtonPress.Call(OnButtonPressed);
    }

    private void OnButtonPressed(InputControl control)
    {
        if (control is KeyControl key)
            HandleKeyboardPressed(key);
    }

    private void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;

        _keyboardSubscription?.Dispose();
        _keyboardSubscription = null;
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
        if (device is not Gamepad gamepad)
            return;

        if(change == InputDeviceChange.Disconnected)
        {
            if (_player1Gamepad?.deviceId == gamepad.deviceId)
            {
                _player1Waiting = true;
                OnPlayerDeviceDisconnected?.Invoke(PlayerType.KeyboardP1);
            }

            else if (_player2Gamepad?.deviceId == gamepad.deviceId)
            {
                OnPlayerDeviceDisconnected?.Invoke(PlayerType.KeyboardP2);
                _player2Waiting = true;
            }
        }
        else if (change == InputDeviceChange.Reconnected)
        {
            if (_player1Waiting && _player1Gamepad?.deviceId == gamepad.deviceId)
            {
                _player1Waiting = false;
                OnPlayerDeviceReconnected?.Invoke(PlayerType.KeyboardP1, _player1Gamepad);
                OnPlayerDeviceConnected?.Invoke(PlayerType.KeyboardP1);
            }
            else if (_player2Waiting && _player2Gamepad?.deviceId == gamepad.deviceId)
            {
                _player2Waiting = false;
                OnPlayerDeviceReconnected?.Invoke(PlayerType.KeyboardP2, _player2Gamepad);
                OnPlayerDeviceConnected?.Invoke(PlayerType.KeyboardP2);
            }
        }
        else if (change == InputDeviceChange.Added)
        {
            if (_player1Waiting)
            {
                _player1Waiting = false;

                _player1Gamepad = gamepad;
                OnPlayerDeviceReconnected?.Invoke(PlayerType.KeyboardP1, _player1Gamepad);
                OnPlayerDeviceConnected?.Invoke(PlayerType.KeyboardP1);
            }
            else if (_player2Waiting)
            {
                _player2Waiting = false;

                _player2Gamepad = gamepad;
                OnPlayerDeviceReconnected?.Invoke(PlayerType.KeyboardP2, _player2Gamepad);
                OnPlayerDeviceConnected?.Invoke(PlayerType.KeyboardP2);
            }
        }
        
    }

    private void HandleKeyboardPressed(KeyControl key)
    {
        if(_player1Waiting)        
            OnKeyboardKeyPressed?.Invoke(PlayerType.KeyboardP1, key);        

        if(_player2Waiting)        
            OnKeyboardKeyPressed?.Invoke(PlayerType.KeyboardP2, key);        
    }

    public void ConfirmKeyboardConnection(PlayerType playerType)
    {
        if (playerType == PlayerType.KeyboardP1)
        {
            _player1Waiting = false;
            OnPlayerDeviceConnected?.Invoke(PlayerType.KeyboardP1);
        }

        else if (playerType == PlayerType.KeyboardP2)
        {
            _player2Waiting = false;
            OnPlayerDeviceConnected?.Invoke(PlayerType.KeyboardP1);
        }
    }
}
