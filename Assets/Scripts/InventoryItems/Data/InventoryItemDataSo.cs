using Game.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "InventoryItemDataSo", menuName = "Scriptable Objects/InventoryItemDataSo")]
    public class InventoryItemDataSo : ScriptableObject
    {
        [SerializeField] private ItemType _itemType;
        public ItemType ItemType => _itemType;

        [SerializeField] private Sprite _itemImage;
        public Sprite ItemImage => _itemImage;
    }
}

