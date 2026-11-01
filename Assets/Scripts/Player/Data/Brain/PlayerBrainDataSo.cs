using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "PlayerBrainDataSo", menuName = "Scriptable Objects/PlayerBrainDataSo")]
    public class PlayerBrainDataSo : ScriptableObject
    {
        [SerializeField] private PlayerInputDataSo _inputData;
        public PlayerInputDataSo InputData => _inputData;

        [SerializeField] private PlayerLocomotionControllerDataSo _locomotionData;
        public PlayerLocomotionControllerDataSo LocomotionData => _locomotionData;

        [SerializeField] private AnimationControllerDataSo _animationsData;
        public AnimationControllerDataSo AnimationsData => _animationsData;

        [SerializeField] private PlayerCurrencyControllerDataSo _currencyData;
        public PlayerCurrencyControllerDataSo CurrencyData => _currencyData;

        [SerializeField] private PlayerCombatControllerDataSo _combatData;
        public PlayerCombatControllerDataSo CombatData => _combatData;

        [SerializeField] private PlayerHealthControllerDataSo _healthData;
        public PlayerHealthControllerDataSo HealthData => _healthData;

        [SerializeField] private PlayerDamageFeedbackControllerDataSo _damageHandlerData;
        public PlayerDamageFeedbackControllerDataSo DamageHandlerData => _damageHandlerData;
    }
}

