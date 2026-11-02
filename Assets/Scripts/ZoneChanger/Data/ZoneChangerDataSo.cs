using UnityEngine;
using Game.Core;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "ZoneChangerDataSo", menuName = "Scriptable Objects/ZoneChangerDataSo")]
    public class ZoneChangerDataSo : ScriptableObject
    {
        [SerializeField] private SpawnId _newSpawnId;
        public SpawnId NewSpawnId => _newSpawnId;

        [SerializeField] private LayerMask _playerLayer;
        public LayerMask PlayerLayer => _playerLayer;

        [SerializeField] private SceneToLoadDataSo _sceneToLoad;
        public SceneToLoadDataSo SceneToLoad => _sceneToLoad;

        [SerializeField] private string _availableTransitionText;
        public string AvailableTransitionText => _availableTransitionText;

        [SerializeField] private string _unavailableTransitionText;
        public string UnavailableTransitionText => _unavailableTransitionText;

        [SerializeField] private float _interactionRadius;
        public float InteractionRadius => _interactionRadius;

        [SerializeField] private float _closeEnoughRadius;
        public float CloseEnoughRadius => _closeEnoughRadius;

    }

}

