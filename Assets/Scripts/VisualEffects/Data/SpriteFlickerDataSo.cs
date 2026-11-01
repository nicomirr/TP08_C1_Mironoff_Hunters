using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "SpriteFlickerDataSo", menuName = "Scriptable Objects/SpriteFlickerDataSo")]
    public class SpriteFlickerDataSo : ScriptableObject
    {
        [SerializeField] private float _flickerTime;
        public float FlickerTime => _flickerTime;

        [SerializeField] private int _totalBlinks;
        public int TotalBlinks => _totalBlinks;

        [SerializeField] private Color32 _flickerColor;
        public Color32 FlickerColor => _flickerColor;
    }
}

