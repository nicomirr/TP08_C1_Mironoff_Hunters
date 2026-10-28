using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class UICoins : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> _coinsTextBoxes;

    public void UpdateCoinsText(int value)
    {
        foreach (TMP_Text textBox in _coinsTextBoxes)
        {
            textBox.text = value.ToString();
        }
    }
}
