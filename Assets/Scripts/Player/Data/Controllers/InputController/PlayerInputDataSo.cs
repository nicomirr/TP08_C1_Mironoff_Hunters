using UnityEngine;
using Game.Core;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "PlayerInputDataSo", menuName = "Scriptable Objects/PlayerInputDataSo")]
    public class PlayerInputDataSo : ScriptableObject
    {
        [SerializeField] private PlayerType _playerType;
        public PlayerType playerType => _playerType;

        [SerializeField] private string _keyboardBindingGroup;
        public string KeyboardBindingGroup => _keyboardBindingGroup;

        [SerializeField] private string _gamepadBindingGroup;
        public string GamepadBindingGroup => _gamepadBindingGroup;
    }
}

