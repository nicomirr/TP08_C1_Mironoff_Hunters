using UnityEngine;

namespace Game.Player.Brain
{
    [RequireComponent (typeof(PlayerInputController))]
    [RequireComponent (typeof(PlayerLocomotionController))]
    [RequireComponent (typeof(PlayerCombatController))]
    [RequireComponent (typeof(PlayerHealthController))]
    [RequireComponent (typeof(PlayerDamageController))]

    [RequireComponent (typeof(Rigidbody2D))]
        
    public class PlayerBrain : MonoBehaviour
    {
        private PlayerInputController _inputController;
        private PlayerLocomotionController _locomotionController;
        private PlayerCombatController _combatController;
        private PlayerHealthController _healthController;
        private PlayerDamageController _damageController;

        private void Awake()
        {
            _inputController = GetComponent<PlayerInputController>();
            _locomotionController = GetComponent<PlayerLocomotionController>();
            _combatController = GetComponent<PlayerCombatController>();
            _healthController = GetComponent<PlayerHealthController>();
            _damageController = GetComponent<PlayerDamageController>();
        }
    }

}

