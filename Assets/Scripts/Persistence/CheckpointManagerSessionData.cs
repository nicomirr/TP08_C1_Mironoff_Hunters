using System.Collections.Generic;

namespace Game.Persistence
{
    public class CheckpointManagerSessionData
    {
        private List<int> _checkpointsReached = new();
        public List<int> CheckpointsReached => _checkpointsReached;

        public CheckpointManagerSessionData() { }

        public CheckpointManagerSessionData(List<int> checkpointsReached)
        {
            _checkpointsReached = checkpointsReached;
        }
    }
}
