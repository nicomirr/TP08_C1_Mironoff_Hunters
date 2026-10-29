using UnityEngine;
using Game.Core;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "InventoryItemDataSo", menuName = "Scriptable Objects/InventoryItemDataSo")]
    public class InventoryItemDataSo : ScriptableObject
    {
        [SerializeField] private ItemType _itemType;
        public ItemType ItemType => _itemType;

        [SerializeField] private Sprite _itemImage;
        public Sprite ItemImage => _itemImage;

        [SerializeField] private int _maxQuantity;
        public int MaxQuantity => _maxQuantity;
    }
}

