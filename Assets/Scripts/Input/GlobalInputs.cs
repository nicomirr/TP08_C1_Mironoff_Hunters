using Game.Checkpoints;
using Game.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Inputs
{
    public class GlobalInputs : MonoBehaviour, IToggleable
    {
        [SerializeField] private GameObject _pauseToggleGameObject;

        private GameControls _globalControls;
     
        private InputAction _pauseInput;

        private IPauseToggle _pauseToggle;

        private void Awake()
        {
            _globalControls = new GameControls();

            _pauseInput = _globalControls.Global.Pause;
        }

        private void OnEnable()
        {
            _pauseInput.performed += OnPausePressed;
        }

        private void Start()
        {
            _pauseToggle = _pauseToggleGameObject.GetComponent<IPauseToggle>();

            StartCoroutine(EnablePauseRoutine());
        }

        private void OnDisable()
        {
            _pauseInput.performed -= OnPausePressed;
            _globalControls.Global.Disable();
        }

        private IEnumerator EnablePauseRoutine()
        {
            yield return new WaitForSeconds(0.4f);
            _globalControls.Global.Enable();
        }

        private void OnPausePressed(InputAction.CallbackContext ctx)
        {
            if (FastTravel.FastTravelOpen) return;

            _pauseToggle.TogglePauseState();
        }

        public void Enable()
        {
            _globalControls.Global.Enable();
        }

        public void Disable()
        {
            _globalControls.Global.Disable();
        }
    }
}

