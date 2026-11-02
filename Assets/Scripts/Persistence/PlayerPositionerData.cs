using Game.Core;

namespace Game.Persistence
{
    public class PlayerPositionerData
    {
        private SpawnId _spawnId;
        public SpawnId SpawnId => _spawnId;

        public PlayerPositionerData() { }

        public PlayerPositionerData(SpawnId spawnId)
        {
            _spawnId = spawnId;
        }
    }
}

