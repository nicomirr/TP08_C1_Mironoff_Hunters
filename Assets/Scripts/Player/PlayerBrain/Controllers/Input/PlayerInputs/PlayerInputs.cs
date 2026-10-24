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

        private InputAction _moveAction;
        private InputAction _jumpAction;
        private InputAction _attackAction;
        private InputAction _interactAction;

        public float Direction => _moveAction.ReadValue<Vector2>().x;
        public bool JumpPressed => _jumpAction.WasPressedThisFrame();
        public bool JumpReleased => _jumpAction.WasReleasedThisFrame();
        public bool AttackPressed => _attackAction.WasPressedThisFrame();
        public bool InteractPressed => _interactAction.WasPressedThisFrame();

        public event Action<PlayerType> OnKeyboardValidated;

        public PlayerInputs(PlayerType playerType)
        {
            _playerControls = new GameControls();
            _playerType = playerType;

            EnablePlayerInputs();
        }

        private void EnablePlayerInputs()
        {
            _moveAction = _playerControls.Player.Movement;
            _jumpAction = _playerControls.Player.Jump;
            _attackAction = _playerControls.Player.Attack;
            _interactAction = _playerControls.Player.Interact;

            _playerControls.asset.bindingMask =
                InputBinding.MaskByGroup($"{_playerType};Gamepad");

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
                InputBinding.MaskByGroup($"{_playerType};Gamepad");

            _playerControls.asset.devices =
                new InputDevice[] { Keyboard.current, gamepad };

            _playerControls.Player.Enable();
        }

        public void IsPlayerKeyboardKey(PlayerType playerType, KeyControl key)
        {
            if (_playerType != playerType) return;

            var scheme = _playerControls.asset.FindControlScheme(
                playerType.ToString()
            );

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

        public void Deinitialize()
        {
            _playerControls.Player.Disable();
            _playerControls.Dispose();
        }
    }
}