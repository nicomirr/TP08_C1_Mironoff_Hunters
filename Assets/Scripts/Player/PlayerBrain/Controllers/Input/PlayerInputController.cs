using Game.Data;

namespace Game.Player
{
    public class PlayerInputController 
    {        
        private PlayerInputs _playerInputs;
        public PlayerInputs PlayerInputs => _playerInputs;
               
        public PlayerInputController(PlayerInputDataSo data)
        {
            _playerInputs = new PlayerInputs(data.playerType, data.KeyboardBindingGroup, data.GamepadBindingGroup);
        }
    }
}
