namespace Game.Core
{
    public static class GameMode
    {
        private static int _playerCount;
        public static int PlayerCount => _playerCount;

        public static void SetPlayerCount(int count)
        {
            if (count != 1 && count != 2)
                return;

            _playerCount = count;
        }
    }
}
