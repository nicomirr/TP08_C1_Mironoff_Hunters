using Game.Data;

namespace Game.Player.Brain
{
    public class PlayerInputController 
    {        
        private PlayerInputs _playerInputs;
        public PlayerInputs PlayerInputs => _playerInputs;
               
        //Esto tiene que llegar desde afuera, para determinar si usa gamepad o no
        public PlayerInputController(PlayerInputDataSo data, bool useGamepad)
        {
            _playerInputs = new PlayerInputs(data.playerType);
        }
    }

}
