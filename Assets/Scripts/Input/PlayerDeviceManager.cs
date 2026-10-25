using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;
using System;
using Game.Core;

public class PlayerDeviceManager : MonoBehaviour
{
    public event Action<PlayerType> OnPlayerDeviceDisconnected;
    public event Action<PlayerType, Gamepad> OnPlayerDeviceReconnected;
    public event Action<PlayerType> OnPlayerDeviceConnected;
    public event Action<PlayerType, KeyControl> OnKeyboardKeyPressed;

    private Gamepad _player1Gamepad;
    private Gamepad _player2Gamepad;

    private bool _player1Waiting;
    private bool _player2Waiting;

    private bool _player1Disconnected;
    private bool _player2Disconnected;

    private bool _player2Disabled;

    private IDisposable _keyboardSubscription;

    private void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;

        _keyboardSubscription = InputSystem.onAnyButtonPress.Call(OnButtonPressed);
    }

    private void Start()
    {
        AssignConnectedGamepads();
    }

    private void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;

        _keyboardSubscription?.Dispose();
        _keyboardSubscription = null;
    }

    public void AssignConnectedGamepads()
    {
        foreach (Gamepad gamepad in Gamepad.all)
        {
            HandleAvailableGamepad(gamepad);
        }
    }

    private void HandleAvailableGamepad(Gamepad gamepad)
    {
        // Primero, comprobar si es un gamepad ya registrado.

        if (_player1Gamepad == gamepad)
        {
            if (_player1Disconnected)
                RestoreGamepad(PlayerType.KeyboardP1, gamepad);

            return;
        }

        if (_player2Gamepad == gamepad)
        {            
            if (_player2Disconnected)
                RestoreGamepad(PlayerType.KeyboardP2, gamepad);

            return;
        }

        // Si es nuevo, asignarlo al primer jugador disponible.

        if (_player1Gamepad == null || _player1Disconnected)
        {
            _player1Gamepad = gamepad;
            RestoreGamepad(PlayerType.KeyboardP1, gamepad);
        }
        else if (_player2Gamepad == null || _player2Disconnected)
        {
            _player2Gamepad = gamepad;
            RestoreGamepad(PlayerType.KeyboardP2, gamepad);
        }
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (device is not Gamepad gamepad)
            return;

        switch (change)
        {
            case InputDeviceChange.Disconnected:
            case InputDeviceChange.Removed:
                HandleDisconnection(gamepad);
                break;

            case InputDeviceChange.Reconnected:
            case InputDeviceChange.Added:
                HandleAvailableGamepad(gamepad);
                break;
        }
    }   
    

    private void HandleDisconnection(Gamepad gamepad)
    {
        if (_player1Gamepad == gamepad && !_player1Disconnected)
        {
            _player1Disconnected = true;
            _player1Waiting = true;

            OnPlayerDeviceDisconnected?.Invoke(PlayerType.KeyboardP1);
        }
        else if (_player2Gamepad == gamepad && !_player2Disconnected)
        {
            _player2Disconnected = true;
            _player2Waiting = true;

            OnPlayerDeviceDisconnected?.Invoke(PlayerType.KeyboardP2);
        }
    }        

    private void RestoreGamepad(PlayerType playerType, Gamepad gamepad)
    {
        bool wasWaiting;

        if (playerType == PlayerType.KeyboardP1)
        {
            wasWaiting = _player1Waiting;

            _player1Disconnected = false;
            _player1Waiting = false;
        }
        else
        {
            wasWaiting = _player2Waiting;

            _player2Disconnected = false;
            _player2Waiting = false;
        }

        OnPlayerDeviceReconnected?.Invoke(playerType, gamepad);

        if (wasWaiting)
            OnPlayerDeviceConnected?.Invoke(playerType);
    }   

    public void ConfirmKeyboardConnection(PlayerType playerType)
    {
        if (playerType == PlayerType.KeyboardP1 && _player1Waiting)
        {
            _player1Waiting = false;
            OnPlayerDeviceConnected?.Invoke(PlayerType.KeyboardP1);
        }
        else if (playerType == PlayerType.KeyboardP2 && _player2Waiting)
        {
            _player2Waiting = false;
            OnPlayerDeviceConnected?.Invoke(PlayerType.KeyboardP2);
        }
    }

    private void OnButtonPressed(InputControl control)
    {
        if (control is KeyControl key)
            HandleKeyboardPressed(key);
    }

    private void HandleKeyboardPressed(KeyControl key)
    {
        if (_player1Waiting)
            OnKeyboardKeyPressed?.Invoke(PlayerType.KeyboardP1, key);

        if (_player2Waiting)
            OnKeyboardKeyPressed?.Invoke(PlayerType.KeyboardP2, key);
    }

}