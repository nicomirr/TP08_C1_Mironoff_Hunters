using UnityEngine;
using Game.Data;

namespace Game.Player.Brain
{    
    [RequireComponent (typeof(Rigidbody2D))]
        
    public class PlayerBrain : MonoBehaviour
    {
        [SerializeField] private PlayerBrainDataSo _data;

        //Este campo no puede estar serializado porque los jugadores se van a instanciar
        //Se lo debe inicializar, queda asi solo como método de prueba hasta entonces
        [SerializeField] private PlayerDeviceManager _deviceManager;

        private PlayerInputController _inputController;
        private PlayerAnimationsController _animationsController;
        private PlayerLocomotionController _locomotionController;
        private PlayerCombatController _combatController;
        private PlayerHealthController _healthController;
        private PlayerDamageController _damageController;

        private void Awake()
        {
            _inputController = new PlayerInputController(_data.InputData, true);

            _locomotionController = new PlayerLocomotionController(_data.LocomotionData, GetComponent<Rigidbody2D>(), this.transform);

            _animationsController = new PlayerAnimationsController(_data.AnimationsData, GetComponentInChildren<Animator>());

        }

        private void OnEnable()
        {
            _deviceManager.OnPlayerDeviceReconnected += _inputController.PlayerInputs.ReconnectGamepad;

            _deviceManager.OnKeyboardKeyPressed += _inputController.PlayerInputs.IsPlayerKeyboardKey;

            _inputController.PlayerInputs.OnKeyboardValidated += _deviceManager.ConfirmKeyboardConnection;
        }

        private void FixedUpdate()
        {
            _locomotionController.Update(_inputController.PlayerInputs.Direction);
        }

        private void Update()
        {
            _animationsController.SetMoveAnimation(_inputController.PlayerInputs.Direction != 0);
        }

        private void OnDisable()
        {
            _deviceManager.OnPlayerDeviceReconnected -= _inputController.PlayerInputs.ReconnectGamepad;
            _deviceManager.OnKeyboardKeyPressed -= _inputController.PlayerInputs.IsPlayerKeyboardKey;

            _inputController.PlayerInputs.OnKeyboardValidated -= _deviceManager.ConfirmKeyboardConnection;
        }
    }

}

