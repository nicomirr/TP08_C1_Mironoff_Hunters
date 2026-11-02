using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "CheckpointDataSo", menuName = "Scriptable Objects/CheckpointDataSo")]
    public class CheckpointDataSo : ScriptableObject
    {
        [SerializeField] private string _availableCheckpointText;
        public string AvailableCheckpointText => _availableCheckpointText;

        [SerializeField] private string _unavailableCheckpointText;
        public string UnavailableCheckpointText => _unavailableCheckpointText;

        [SerializeField] private float _closeEnoughRadius;
        public float CloseEnoughRadius => _closeEnoughRadius;

        [SerializeField] private float _interactionRadius;
        public float InteractionRadius => _interactionRadius;

        [SerializeField] private LayerMask _playerLayer;
        public LayerMask PlayerLayer => _playerLayer;

        [SerializeField] private AnimationControllerDataSo _animationData;
        public AnimationControllerDataSo AnimationData => _animationData;
    }
}


