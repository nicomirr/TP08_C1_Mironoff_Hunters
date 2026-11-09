using UnityEngine;
using System.Collections.Generic;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "AudioGroupsSo", menuName = "Scriptable Objects/AudioGroupsSo")]
    public class AudioGroupsSo : ScriptableObject
    {
        [SerializeField] private List<string> _audioGroups;
        public List<string> AudioGroups => _audioGroups;
    }

}

