using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "PlayerBrainDataSo", menuName = "Scriptable Objects/PlayerBrainDataSo")]
    public class PlayerBrainDataSo : ScriptableObject
    {
        [SerializeField] private PlayerInputDataSo _inputData;
        public PlayerInputDataSo InputData => _inputData;

        [SerializeField] private PlayerLocomotionDataSo _locomotionData;
        public PlayerLocomotionDataSo LocomotionData => _locomotionData;

        [SerializeField] private PlayerAnimationsControllerDataSo _animationsData;
        public PlayerAnimationsControllerDataSo AnimationsData => _animationsData;

        [SerializeField] private PlayerCurrencyControllerDataSo _currencyData;
        public PlayerCurrencyControllerDataSo CurrencyData => _currencyData;
    }
}

