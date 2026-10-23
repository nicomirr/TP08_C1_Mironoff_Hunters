using Game.Core;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Game.Player
{
    public class PlayerInputs
    {
        private readonly GameControls _playerControls;

        private PlayerType _playerType;

        private InputAction _moveAction;
        private InputAction _jumpAction;
        private InputAction _attackAction;
        private InputAction _interactAction;

        public float Direction => _moveAction.ReadValue<Vector2>().x;
        public bool JumpPressed => _jumpAction.WasPressedThisFrame();
        public bool JumpReleased => _jumpAction.WasReleasedThisFrame();
        public bool AttakPressed => _attackAction.WasPressedThisFrame();
        public bool InteractPressed => _interactAction.WasPressedThisFrame();

        public event Action<PlayerType> OnKeyboardValidated;

        public PlayerInputs(PlayerType playerType, int gamepadIndex)
        {
            _playerControls = new GameControls();
            _playerType = playerType;

            Gamepad gamepad = gamepadIndex >= 0 && gamepadIndex < Gamepad.all.Count ?
                Gamepad.all[gamepadIndex] : null;

            EnablePlayerInputs(gamepad);
        }

        private void EnablePlayerInputs(Gamepad gamepad)
        {
            _playerControls.Player.Enable();

            _moveAction = _playerControls.Player.Movement;
            _jumpAction = _playerControls.Player.Jump;
            _attackAction = _playerControls.Player.Attack;
            _interactAction = _playerControls.Player.Interact;
                        
            string scheme = gamepad == null ? _playerType.ToString() : "Gamepad";

            _playerControls.asset.bindingMask = InputBinding.MaskByGroup(scheme);

            _playerControls.asset.devices = gamepad != null ? 
                new InputDevice[] { gamepad } : new InputDevice[] { Keyboard.current };               
        }

        public void ReconnectGamepad(PlayerType playerType, Gamepad gamepad)
        {
            if (_playerType != playerType) return;

            _playerControls.Player.Disable();

            _playerControls.asset.devices = new InputDevice[] { gamepad };

            _playerControls.Player.Enable();
        }

        public void IsPlayerKeyboardKey(PlayerType playerType, KeyControl key)
        {
            if (_playerType != playerType) return;

            var scheme = _playerControls.asset.FindControlScheme(playerType.ToString());

            if (!scheme.HasValue) return;

            InputBinding mask = InputBinding.MaskByGroup(scheme.Value.bindingGroup);

            foreach (InputBinding binding in _playerControls.Player.Get().bindings)
            {
                if (!mask.Matches(binding)) continue;
                if (string.IsNullOrEmpty(binding.effectivePath)) continue;

                if (InputControlPath.Matches(binding.effectivePath, key))
                {
                    SwitchToKeyboard();
                    OnKeyboardValidated?.Invoke(_playerType);
                    return;
                }
            }

        }

        private void SwitchToKeyboard()
        {
            _playerControls.Player.Disable();

            _playerControls.asset.bindingMask = InputBinding.MaskByGroup(_playerType.ToString());

            _playerControls.asset.devices = new InputDevice[] { Keyboard.current };

            _playerControls.Player.Enable();
        }

        public void Deinitialize()
        {
            _playerControls.Player.Disable();
            _playerControls.Dispose();
        }
    }
}
