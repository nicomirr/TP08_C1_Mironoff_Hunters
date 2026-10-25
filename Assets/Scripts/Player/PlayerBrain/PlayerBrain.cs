using UnityEngine;
using Game.Data;
using Game.Marker;

namespace Game.Player.Brain
{    
    [RequireComponent (typeof(Rigidbody2D))]
    [RequireComponent (typeof(PlayerInteractionController))]
        
    public class PlayerBrain : MonoBehaviour
    {
        [SerializeField] private PlayerBrainDataSo _data;

        [SerializeField] private PlayerDeviceManager _deviceManager;

        private PlayerInputController _inputController;
        private PlayerAnimationsController _animationsController;
        private PlayerLocomotionController _locomotionController;
        private PlayerInteractionController _interactionController;
        private PlayerCombatController _combatController;
        private PlayerHealthController _healthController;
        private PlayerDamageController _damageController;

        private void Awake()
        {
            _inputController = new PlayerInputController(_data.InputData);

            _animationsController = new PlayerAnimationsController(_data.AnimationsData, GetComponentInChildren<Animator>());
            
            _locomotionController = new PlayerLocomotionController(_data.LocomotionData, GetComponent<Rigidbody2D>(), this.transform,
                GetComponentInChildren<GroundCheckMarker>().transform);

            _interactionController = GetComponent<PlayerInteractionController>();

        }

        private void OnEnable()
        {
            _deviceManager.OnPlayerDeviceReconnected += _inputController.PlayerInputs.ReconnectGamepad;

            _deviceManager.OnKeyboardKeyPressed += _inputController.PlayerInputs.IsPlayerKeyboardKey;

            _inputController.PlayerInputs.OnKeyboardValidated += _deviceManager.ConfirmKeyboardConnection;

            _locomotionController.OnJustLanded += HandleLand;
        }

        private void FixedUpdate()
        {
            if (_interactionController.IsInteracting)
                return;            

            _locomotionController.UpdateMovement(_inputController.PlayerInputs.Direction);
        }

        private void Update()
        {
            if (_interactionController.IsInteracting)
            {
                _locomotionController.StopMovement();
                _animationsController.SetMoveAnimation(false);
                _animationsController.SetJumpAnimation(false);
                return;
            }

            _animationsController.SetMoveAnimation(_inputController.PlayerInputs.Direction != 0);
            
            HandleJumpUpdate();
            HandleAttack();
            HandleInteractions();           
        }

        private void OnDisable()
        {
            _deviceManager.OnPlayerDeviceReconnected -= _inputController.PlayerInputs.ReconnectGamepad;
            _deviceManager.OnKeyboardKeyPressed -= _inputController.PlayerInputs.IsPlayerKeyboardKey;

            _inputController.PlayerInputs.OnKeyboardValidated -= _deviceManager.ConfirmKeyboardConnection;

            _locomotionController.OnJustLanded -= HandleLand;
        }

        private void HandleJumpUpdate()
        {
            _locomotionController.UpdateCoyoteTime();
            _locomotionController.UpdateGravity();
            _locomotionController.UpdateGroundState();

            if (_inputController.PlayerInputs.JumpReleased)
                _locomotionController.CutJump();

            if (_inputController.PlayerInputs.JumpPressed)
            {
                bool jumpPerformed = _locomotionController.HandleJump();
                _animationsController.SetJumpAnimation(jumpPerformed);                
            }
        }

        private void HandleLand()
        {
            _locomotionController.HandleLand();
            _animationsController.SetJumpAnimation(false);
        }

        private void HandleAttack()
        {
            if (_inputController.PlayerInputs.AttackPressed)
                _animationsController.TriggerAttack();
        }

        private void HandleInteractions()
        {           
            if (_inputController.PlayerInputs.InteractPressed)
                _interactionController.Interact(_inputController.PlayerInputs.InteractAction);
        }
    }
}

