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
        private PlayerLocomotionController _locomotionController;
        private PlayerCombatController _combatController;
        private PlayerHealthController _healthController;
        private PlayerDamageController _damageController;

        private void Awake()
        {
            _inputController = new PlayerInputController(_data.InputData, true);
            _deviceManager.RegisterGamepad(_data.InputData.playerType, _data.InputData.GamepadIndex);

            _locomotionController = new PlayerLocomotionController(_data.LocomotionData, GetComponent<Rigidbody2D>(), this.transform);
        }

        private void FixedUpdate()
        {
            _locomotionController.Update(_inputController.PlayerInputs.Direction);
        }
    }

}

