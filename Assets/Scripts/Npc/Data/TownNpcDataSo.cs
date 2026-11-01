using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "TownNpcDataSo", menuName = "Scriptable Objects/TownNpcDataSo")]
    public class TownNpcDataSo : ScriptableObject
    {
        [SerializeField] private DialogueDataSo _dialogueData;
        public DialogueDataSo DialogueData => _dialogueData;
    }

}

