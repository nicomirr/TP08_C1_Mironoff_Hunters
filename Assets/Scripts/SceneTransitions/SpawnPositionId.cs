using UnityEngine;
using Game.Core;

namespace Game.SceneTransitions
{
    public class SpawnPositionId : MonoBehaviour
    {
        [SerializeField] private SpawnId _id;
        public SpawnId Id => _id;
    }
}
