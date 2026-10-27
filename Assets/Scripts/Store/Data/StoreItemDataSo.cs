using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "StoreItemDataSo", menuName = "Scriptable Objects/StoreItemDataSo")]
    public class StoreItemDataSo : ScriptableObject
    {
        [SerializeField] private int _value;
        public int Value => _value;

        [TextArea] [SerializeField] private string _description;
        public string Description => _description;
    }
}

