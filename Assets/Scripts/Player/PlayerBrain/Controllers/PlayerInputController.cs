using UnityEngine;

namespace Game.Player.Brain
{
    public class PlayerInputController : MonoBehaviour
    {
        private PlayerInputs _playerInputs;

        private void Awake()
        {
            //_playerInputs = new PlayerInputs()
        }

        public void Initialize(PlayerType playerType, int gamepadIndex)
        {
            _playerInputs = new PlayerInputs(playerType, gamepadIndex);
        }
    }

}
