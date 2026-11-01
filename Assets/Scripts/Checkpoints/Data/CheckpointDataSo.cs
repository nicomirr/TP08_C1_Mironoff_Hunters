using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "CheckpointDataSo", menuName = "Scriptable Objects/CheckpointDataSo")]
    public class CheckpointDataSo : ScriptableObject
    {
        [SerializeField] private float _checkpointUsedDelayTime;
        public float CheckpointUsedDelayTime => _checkpointUsedDelayTime;

        [SerializeField] private AnimationControllerDataSo _animationData;
        public AnimationControllerDataSo AnimationData => _animationData;
    }
}


