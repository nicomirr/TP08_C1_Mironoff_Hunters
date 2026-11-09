using UnityEngine;
using System.Collections.Generic;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "VolumeButtonTypeSo", menuName = "Scriptable Objects/VolumeButtonTypeSo")]
    public class VolumeButtonsTypeSo : ScriptableObject
    {
        [SerializeField] private List<string> _volumeTypes;
        public List<string> VolumeTypes => _volumeTypes;
    }

}

