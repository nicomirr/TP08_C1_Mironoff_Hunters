using UnityEngine;
using Game.Core;

namespace Game.Persistence
{
    public class PlayerPositionerData
    {
        private SpawnId _spawnId;
        public SpawnId SpawnId => _spawnId;
        
        private Vector2 _orientation;
        public Vector2 Orientation => _orientation;

        public PlayerPositionerData() { }

        public PlayerPositionerData(SpawnId spawnId, Vector2 orientation)
        {
            _spawnId = spawnId;
            _orientation = orientation;
        }
    }
}

