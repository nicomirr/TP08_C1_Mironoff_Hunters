using UnityEngine;
using System.Collections.Generic;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "DialogueDataSo", menuName = "Scriptable Objects/DialogueDataSo")]
    public class DialogueDataSo : ScriptableObject
    {       
        [TextArea][SerializeField] private List<string> _dialogue;
        public List<string> Dialogue => _dialogue;

        [SerializeField] private float _letterDelayTime;
        public float LetterDelayTime => _letterDelayTime;
    }
}

