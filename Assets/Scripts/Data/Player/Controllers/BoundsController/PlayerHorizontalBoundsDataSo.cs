using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "PlayerHorizontalBoundsDataSo", menuName = "Scriptable Objects/PlayerHorizontalBoundsDataSo")]
    public class PlayerHorizontalBoundsDataSo : ScriptableObject
    {
        [SerializeField] private float _minXViewportPoint;
        public float MinXViewportPoint => _minXViewportPoint;

        [SerializeField] private float _maxXViewportPoint;
        public float MaxXViewportPoint => _maxXViewportPoint;
    }

}

