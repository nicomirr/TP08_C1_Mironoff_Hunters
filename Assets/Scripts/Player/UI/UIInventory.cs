using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections.Generic;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private List<CanvasGroup> _keyboardKeys;
    [SerializeField] private List<CanvasGroup> _buttons;

    [SerializeField] private List<Image> _currentInventoryImages;
    [SerializeField] private List<TMP_Text> _quantityTextBoxes;

    private void Start()
    {        
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

    public void UpdateUI(InputDevice device)
    {
        if(device is Keyboard)
        {
            foreach (CanvasGroup group in _keyboardKeys)
                group.alpha = 1;

            foreach (CanvasGroup group in _buttons)
                group.alpha = 0;

            return;
        }

        foreach (CanvasGroup group in _keyboardKeys)
            group.alpha = 0;

        foreach (CanvasGroup group in _buttons)
            group.alpha = 1;

        return;
    }

    public void HideInventory()
    {
        foreach (Image image in _currentInventoryImages)
        {
            image.sprite = null;
            image.enabled = false;

            foreach (TMP_Text textBox in _quantityTextBoxes)
            {
                textBox.text = "";
            }
        }
    }
}
