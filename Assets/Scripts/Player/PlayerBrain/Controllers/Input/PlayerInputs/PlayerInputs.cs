using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using System;
using Game.Core;

namespace Game.Player
{
    public class PlayerInputs
    {
        private readonly GameControls _playerControls;
        private readonly PlayerType _playerType;

        private InputDevice _currentDevice;
        public InputDevice CurrentDevice => _currentDevice;

        private InputAction _moveAction;
        private InputAction _jumpAction;
        private InputAction _attackAction;
        private InputAction _interactAction;
        private InputAction _nextItemAction;
        private InputAction _previousItemAction;

        private string _keyboardBindingGroup;
        private string _gamepadBindingGroup;

        public float Direction => _moveAction.ReadValue<Vector2>().x;
        public bool JumpPressed => _jumpAction.WasPressedThisFrame();
        public bool JumpReleased => _jumpAction.WasReleasedThisFrame();
        public bool AttackPressed => _attackAction.WasPressedThisFrame();
        public bool InteractPressed => _interactAction.WasPressedThisFrame();
        public bool NextItemPressed => _nextItemAction.WasPressedThisFrame();
        public bool PreviousItemPressed => _previousItemAction.WasPressedThisFrame();

        public InputAction InteractAction => _interactAction;

        public event Action<PlayerType> OnKeyboardValidated;
        public event Action<InputDevice> OnCurrentDeviceChanged;

        public PlayerInputs(PlayerType playerType, string keyboardBindingGroup, string gamepadBindingGroup)
        {
            _playerControls = new GameControls();
            _playerType = playerType;

            _keyboardBindingGroup = keyboardBindingGroup;
            _gamepadBindingGroup = gamepadBindingGroup;
            
            _playerControls.Player.Get().actionTriggered += OnActionTriggered;

            EnablePlayerInputs();

        }

        private void EnablePlayerInputs()
        {
            _moveAction = _playerControls.Player.Movement;
            _jumpAction = _playerControls.Player.Jump;
            _attackAction = _playerControls.Player.Attack;
            _interactAction = _playerControls.Player.Interact;
            _nextItemAction = _playerControls.Player.NextItem;
            _previousItemAction = _playerControls.Player.PreviousItem;

            _playerControls.asset.bindingMask =
                InputBinding.MaskByGroup($"{_keyboardBindingGroup};{_gamepadBindingGroup}");

            _playerControls.asset.devices =
                new InputDevice[] { Keyboard.current };

            _playerControls.Player.Enable();
        }

        public void ReconnectGamepad(PlayerType playerType, Gamepad gamepad)
        {
            if (_playerType != playerType || gamepad == null)
                return;

            _playerControls.Player.Disable();

            _playerControls.asset.bindingMask =
                InputBinding.MaskByGroup($"{_keyboardBindingGroup};{_gamepadBindingGroup}");

            _playerControls.asset.devices =
                new InputDevice[] { Keyboard.current, gamepad };

            _playerControls.Player.Enable();
        }

        public void IsPlayerKeyboardKey(PlayerType playerType, KeyControl key)
        {
            if (_playerType != playerType) return;

            var scheme = _playerControls.asset.FindControlScheme(_keyboardBindingGroup);

            if (!scheme.HasValue) return;

            InputBinding mask =
                InputBinding.MaskByGroup(scheme.Value.bindingGroup);

            foreach (InputBinding binding in _playerControls.Player.Get().bindings)
            {
                if (!mask.Matches(binding)) continue;
                if (string.IsNullOrEmpty(binding.effectivePath)) continue;

                if (InputControlPath.Matches(binding.effectivePath, key))
                {
                    OnKeyboardValidated?.Invoke(_playerType);
                    return;
                }
            }
        }

        private void OnActionTriggered(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            InputDevice device = context.control.device;

            if (_currentDevice == device) return;

            _currentDevice = device;
            OnCurrentDeviceChanged?.Invoke(device);
        }

        public void Deinitialize()
        {
            _playerControls.Player.Get().actionTriggered -= OnActionTriggered;

            _playerControls.Player.Disable();
            _playerControls.Dispose();
        }
    }
}