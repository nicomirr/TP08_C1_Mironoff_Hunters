namespace Game.Core
{
    public static class GameMode
    {
        private static GameModeType _gameModeType;
        public static GameModeType CurrentGameModeType => _gameModeType;

        public static void SetGameMode(GameModeType gameModeType)
        {           
            _gameModeType = gameModeType;
        }
    }
}
