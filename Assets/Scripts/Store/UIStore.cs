using Game.Core;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UIStore : MonoBehaviour
{
    [SerializeField] private CanvasGroup _singlePlayerCanvasGroup;
    [SerializeField] private TMP_Text _singlePlayerTextPrice;
    [SerializeField] private TMP_Text _singlePlayerTextDescription;

    [SerializeField] private CanvasGroup _player1CanvasGroup;
    [SerializeField] private TMP_Text _player1TextPrice;
    [SerializeField] private TMP_Text _player1TextDescription;

    [SerializeField] private CanvasGroup _player2CanvasGroup;
    [SerializeField] private TMP_Text _player2TextPrice;
    [SerializeField] private TMP_Text _player2TextDescription;

    public void DisplayText(PlayerType playerType, int value, string description)
    {
        switch(playerType)
        {
            case PlayerType.Player1:

                if(GameMode.CurrentGameModeType == GameModeType.SinglePlayer)
                {
                    _singlePlayerCanvasGroup.alpha = 1;
                    _singlePlayerTextPrice.text = value.ToString();
                    _singlePlayerTextDescription.text = description;

                    return;
                }

                _player1CanvasGroup.alpha = 1;
                _player1TextPrice.text = value.ToString();
                _player1TextDescription.text = description;

                break;

            case PlayerType.Player2:

                _player2CanvasGroup.alpha = 1;
                _player2TextPrice.text = value.ToString();
                _player2TextDescription.text = description;

                break;
        }
    }
    
    public void HideText(PlayerType playerType)
    {
        switch (playerType)
        {
            case PlayerType.Player1:

                if (GameMode.CurrentGameModeType == GameModeType.SinglePlayer)
                {
                    _singlePlayerCanvasGroup.alpha = 0;
                    return;
                }

                _player1CanvasGroup.alpha = 0;
                break;

            case PlayerType.Player2:

                _player2CanvasGroup.alpha = 0;
                break;
        }
    }
}
