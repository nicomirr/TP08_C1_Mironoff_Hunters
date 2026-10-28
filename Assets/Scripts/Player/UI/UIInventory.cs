using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private List<Image> _currentInventoryImages;
    [SerializeField] private List<TMP_Text> _quantityTextBoxes;

    private void Start()
    {
        //cambiar a canvas renderer? 

        foreach(Image image  in _currentInventoryImages)
        {
            if (image.sprite == null)
                image.enabled = false;
        }
    }

    public void ChangeCurrentInventorySprite(Sprite currentInventorySprite, int quantity)
    {
        foreach (Image image in _currentInventoryImages)
        {
            image.sprite = currentInventorySprite;

            if (image.sprite != null)
                image.enabled = true;
        }

        foreach (TMP_Text textBox in _quantityTextBoxes)
        {
            textBox.text = quantity.ToString();
        }           
    }
}
