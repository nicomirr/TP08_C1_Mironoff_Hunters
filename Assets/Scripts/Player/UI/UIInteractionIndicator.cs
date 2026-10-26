using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class UIInteractionIndicator : MonoBehaviour
{
    [SerializeField] private List<CanvasGroup> _keyboardInteractionButton;
    [SerializeField] protected List<CanvasGroup> _controllerInteractionButton;

    private bool _isVisible;

    public void DisplayInteractionButton(InputDevice currentDevice)
    {
        _isVisible = true;
        UpdateInteractionButton(currentDevice);
    }

    private void LateUpdate()
    {
        foreach (CanvasGroup group in _controllerInteractionButton)
            group.transform.rotation = Quaternion.identity;

        foreach (CanvasGroup group in _keyboardInteractionButton)
            group.transform.rotation = Quaternion.identity;
    }

    public void HideInteractionButtons()
    {
        _isVisible = false;

        foreach (CanvasGroup group in _controllerInteractionButton)
            group.alpha = 0;

        foreach (CanvasGroup group in _keyboardInteractionButton)
            group.alpha = 0;
    }

    public void UpdateInteractionButton(InputDevice currentDevice)
    {
        if (!_isVisible) return;

        foreach (CanvasGroup group in _controllerInteractionButton)
            group.alpha = currentDevice is Gamepad ? 1 : 0;

        foreach (CanvasGroup group in _keyboardInteractionButton)
            group.alpha = currentDevice is Gamepad ? 0 : 1;
    }
}
