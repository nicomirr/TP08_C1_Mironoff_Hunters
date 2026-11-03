using UnityEngine;
using Game.Core;

namespace Game.SceneTransitions
{
    public class SpawnPositionInfo : MonoBehaviour
    {
        [SerializeField] private SpawnId _id;
        public SpawnId Id => _id;

        [SerializeField] private Vector2 _orientation;
        public Vector2 Orientation => _orientation;
    }
}
