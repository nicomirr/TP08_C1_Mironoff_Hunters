namespace Game.Persistence
{
    public class CheckpointSessionData
    {
        private bool _checkpointReached;
        public bool CheckpointReached => _checkpointReached;

        public CheckpointSessionData() { }

        public CheckpointSessionData(bool checkpointReached)
        {
            _checkpointReached = checkpointReached;
        }
    }

}

