using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "RainControllerDataSo", menuName = "Scriptable Objects/RainControllerDataSo")]
    public class RainControllerDataSo : ScriptableObject
    {
        [SerializeField] private string _thunderAnimationTrigger;
        public int ThunderAnimationTriggerHash => Animator.StringToHash(_thunderAnimationTrigger);

        [SerializeField] private int _minThunderTime;
        public int MinThunderTime => _minThunderTime;

        [SerializeField] private int _maxThunderTime;
        public int MaxThunderTime => _maxThunderTime;
    }
}

