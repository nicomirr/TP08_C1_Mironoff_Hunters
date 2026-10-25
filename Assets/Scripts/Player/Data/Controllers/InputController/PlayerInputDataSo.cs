using UnityEngine;
using Game.Core;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "PlayerInputDataSo", menuName = "Scriptable Objects/PlayerInputDataSo")]
    public class PlayerInputDataSo : ScriptableObject
    {
        [SerializeField] private PlayerType _playerType;
        public PlayerType playerType => _playerType;
    }
}

