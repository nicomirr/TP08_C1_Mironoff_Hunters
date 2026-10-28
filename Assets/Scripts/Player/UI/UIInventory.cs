using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private List<Image> _currentInventoryImages;

    public void ChangeCurrentInventorySprite(Sprite currentInventorySprite)
    {
        foreach (Image image in _currentInventoryImages)
        {
            image.sprite = currentInventorySprite;  
        }
    }
}
