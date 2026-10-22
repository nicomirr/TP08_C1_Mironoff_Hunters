using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    public class PlayerInputs
    {
        private readonly GameControls _playerControls;

        private InputAction _moveAction;
        private InputAction _jumpAction;
        private InputAction _attackAction;
        private InputAction _interactAction;

        public float Direction => _moveAction.ReadValue<Vector2>().x;
        public bool JumpPressed => _jumpAction.WasPressedThisFrame();
        public bool JumpReleased => _jumpAction.WasReleasedThisFrame();
        public bool AttakPressed => _attackAction.WasPressedThisFrame();
        public bool InteractPressed => _interactAction.WasPressedThisFrame();

        public PlayerInputs(PlayerType playerType, int gamepadIndex)
        {
            _playerControls = new GameControls();

            Gamepad gamepad = gamepadIndex >= 0 && gamepadIndex < Gamepad.all.Count ?
                Gamepad.all[gamepadIndex] : null;

            EnablePlayerInputs(playerType, gamepad);
        }

        private void EnablePlayerInputs(PlayerType playerType, Gamepad gamepad)
        {
            _moveAction = _playerControls.Player.Movement;
            _jumpAction = _playerControls.Player.Jump;
            _attackAction = _playerControls.Player.Attack;
            _interactAction = _playerControls.Player.Interact;
                        
            string scheme = playerType.ToString();

            _playerControls.asset.devices = gamepad != null ? 
                new InputDevice[] { gamepad } : new InputDevice[] { Keyboard.current };
                

        }
    }

}
